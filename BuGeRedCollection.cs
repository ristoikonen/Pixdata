using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Xml.Linq;

namespace Pixdata
{
    public class BuGeRedCollection : IEnumerable
    {
        public List<BuGeRed> pixelList { get; set; }
        private int top = 0;

        public BuGeRedCollection()
        {
            pixelList = new List<BuGeRed>();
        }

        public List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)
        {
            const int bits_per_pixel = 4;
            bool isfirstpixel = true;
            BuGeRed? firstpixel = new BuGeRed(Color.Black);
            int modi = 0; bool isfour = true;

            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            //TODO: what to do with cloud and Linux..
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            pixelList = new List<BuGeRed>(sourceBuffer.Length / bits_per_pixel);

            using (MemoryStream memoryStream = new MemoryStream(sourceBuffer))
            {
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                while (memoryStream.Position + bits_per_pixel <= memoryStream.Length)
                {
                    if (isfirstpixel && memoryStream.Position == 0)
                    {
                        firstpixel = new BuGeRed(binaryReader.ReadBytes(4));
                        //firstpixel.IsFirstFour = true;
                        pixelList.Add(firstpixel);
                        isfirstpixel = false;
                    }
                    else 
                    {
                        //string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
                        // we have 8 as "11110000" so we need to create 2 BuGeReds
                        for (int ix = 0; ix < 2; ix++)
                        {

                            if (memoryStream.Position >= memoryStream.Length)
                            {
                                Console.WriteLine(memoryStream.Position);
                                break;

                            }
                            //int readix = isfour ? 0 : bits_per_pixel;

                            // is it start or end part
                            isfour = (modi++ % 2) == 0;
                            BuGeRed pixel = new BuGeRed(binaryReader.ReadBytes(bits_per_pixel), isfour);
                            pixelList.Add(pixel);
                        }


                    }

                }
                binaryReader.Close();
            }
            return pixelList;
        }


        public byte[] GetBytesFromBitmap(Bitmap sourceImage)
        {

            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            //TODO: what to do with cloud and Linux..
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            return sourceBuffer;

        }

        //List<BuGeRed> pixelList
        public byte[] GetBytesFromBuGeRedList(int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];


            Marshal.Copy(resultData.Scan0, resultBuffer, 0, resultBuffer.Length);

            //Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            //resultBitmap.UnlockBits(resultData);

            return resultBuffer;
        }


        public Bitmap GetBitmapFromBuGeRedList( int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];

            using (MemoryStream memoryStream = new MemoryStream(resultBuffer))
            {
                memoryStream.Position = 0;
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

                foreach (BuGeRed pixel in pixelList)
                {
                    binaryWriter.Write(pixel.ToBytes());
                }

                binaryWriter.Close();
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public Bitmap CreateBitmapFromBuGeRedList(List<BuGeRed> bgrlist , int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];

            using (MemoryStream memoryStream = new MemoryStream(resultBuffer))
            {
                memoryStream.Position = 0;
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

                foreach (BuGeRed pixel in bgrlist)
                {
                    var px = pixel.ToBytes();
                    binaryWriter.Write(px);
                }

                binaryWriter.Close();
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }


        public int Count => pixelList.Count;

        public BuGeRed this[int index]
        {
            get { return pixelList[index]; }
        }

        public void SetFirst(BuGeRed bgr)
        {
            pixelList.Insert(0, bgr);
        }

        public void SetSecond(BuGeRed bgr)
        {
            pixelList.Insert(1, bgr);
        }

        public void Add(List<BuGeRed> list)
        {
            pixelList.AddRange(list);
        }


        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)pixelList).GetEnumerator();
        }

        public IEnumerable<BuGeRed> Word()
        {

            List<BuGeRed> l = new List<BuGeRed>(2);

            var curr = pixelList?.GetEnumerator().Current;

            if (pixelList?.GetEnumerator().Current ==null || pixelList?.GetEnumerator().Current.IsFirstFour == null)
            {
                //break;
            }
            if (pixelList?.GetEnumerator().Current?.IsFirstFour.Value != true)
            {
                int? nxt = pixelList?.IndexOf(pixelList?.GetEnumerator().Current);
                if (nxt is not null && nxt != -1)
                {
                    var nextBGRA = pixelList[nxt.Value];
                    yield return pixelList[nxt.Value];
                }
            }
            if (pixelList?.GetEnumerator().Current.IsFirstFour.Value != false)
            {
                //yield break;
            }
            /*
            yield return {
                pixelList.GetEnumerator().Current, 
                        pixelList.GetEnumerator().MoveNext()};
            */
            //var end = start + count;
            //for (var value = start; value < end; value++)
            //    yield return value;
        }



        public  IEnumerable<BuGeRed> Sequence(int firstNumber, int lastNumber)
        {
            //Range< BuGeRed >
            foreach (BuGeRed bgr in pixelList?[firstNumber..lastNumber])
            {
                if (pixelList?[firstNumber].IsFirstFour.Value == null)
                {
                    yield break;
                }
                if (pixelList?[firstNumber].IsFirstFour.Value != true)
                {
                    yield break;
                }
                yield return bgr;
            }
        }

        public IEnumerable<BuGeRed> TopN(int itemsFromTop)
        {
            // Return less than itemsFromTop if necessary.
            int startIndex = itemsFromTop >= top ? 0 : top - itemsFromTop;

            for (int index = top - 1; index >= startIndex; index--)
            {
                yield return pixelList[index];
            }
        }

        public IEnumerable<BuGeRed> StartN(int startIndex, int itemsFromStart)
        {
            //int startIndex = 0;// itemsFromTop >= top ? 0 : top - itemsFromTop;

            for (int index = startIndex; index >= itemsFromStart; index++)
            {
                yield return pixelList[index];
            }
        }

        public BuGeRed First()
        {
            return pixelList[0];
        }


        public IEnumerator<BuGeRed> Edited()
        {
            foreach (BuGeRed bgr in pixelList)
            {
                if (bgr.IsFirstFour is not null)
                    yield return bgr;
                //Derived d = b as Derived;
            }
        }

        public IEnumerable<BuGeRed> Marked()
        {
            //Range< BuGeRed >
            foreach (BuGeRed bgr in pixelList)
            {
                if (bgr != null && bgr.IsFirstFour != null)
                {

                    if ( bgr.IsFirstFour.Value)
                    {
                        yield return bgr;
                    }
                }
                yield break;
            }
        }
    }
}


