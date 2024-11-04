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
using System.Runtime.InteropServices;

namespace Pixdata
{
    internal class BuGeRedCollection
    {
        public List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)
        {
            bool isfirstpixel = true;
            BuGeRed? firstpixel = new BuGeRed(Color.Black);
            

            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            //TODO: what to do with cloud and Linux..
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            List<BuGeRed> pixelList = new List<BuGeRed>(sourceBuffer.Length / 4);

            using (MemoryStream memoryStream = new MemoryStream(sourceBuffer))
            {
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                while (memoryStream.Position + 4 <= memoryStream.Length)
                {
                    if (isfirstpixel && memoryStream.Position == 0)
                    {
                        firstpixel = new BuGeRed(binaryReader.ReadBytes(4));
                        BGRDiff bgrdiff = new BGRDiff(firstpixel, firstpixel);
                        firstpixel.BGRDiff = bgrdiff;
                        pixelList.Add(firstpixel);
                        isfirstpixel = false;
                    }
                    else 
                    {
                        BuGeRed pixel = new BuGeRed(binaryReader.ReadBytes(4));
                        BGRDiff bgrdiff = new BGRDiff(pixel, firstpixel);
                        pixel.BGRDiff = bgrdiff;
                        //pixel.BGRDiff.BasePixel = firstpixel ?? new BuGeRed(Color.Black);
                        pixelList.Add(pixel);
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


        public byte[] GetBytesFromBuGeRedList(List<BuGeRed> pixelList, int width, int height)
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
        /*
        public Bitmap GetBitmapFromBuGeRedList(List<BuGeRed> pixelList, int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];


            Marshal.Copy(resultData.Scan0, resultBuffer, 0, resultBuffer.Length);

            //Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            //resultBitmap.UnlockBits(resultData);

            using (var ms = new MemoryStream(resultBuffer))
            {
                resultBitmap = new Bitmap(ms);
            }

            return resultBitmap;
        }
        */


    public Bitmap GetBitmapFromBuGeRedList(List<BuGeRed> pixelList, int width, int height)
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
                    binaryWriter.Write(pixel.GetBytes());
                }

                binaryWriter.Close();
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

    }
}


/*
 
 public static byte[] Add(this byte[] A, byte[] B)
{
    List<byte> array = new List<byte>(A);
    for (int i = 0; i < B.Length; i++)
        array = _add_(array, B[i], i);

    return array.ToArray();
}
private static List<byte> _add_(List<byte> A, byte b, int idx = 0, byte rem = 0)
{
    short sample = 0;
    if (idx < A.Count)
    {
        sample = (short)((short)A[idx] + (short)b);
        A[idx] = (byte)(sample % 256);
        rem = (byte)((sample - A[idx]) % 255);
        if (rem > 0)
            return _add_(A, (byte)rem, idx + 1);
    }
    else A.Add(b);

    return A;
}

Subtraction
public static byte[] Subtract(this byte[] A, byte[] B)
{
    // find which array has a greater value for accurate
    // operation if one knows a better way to find which 
    // array is greater in value, do let me know. 
    // (MyArray.Length is not a good option here because
    // an array {255} - {100 000 000} will not yield a
    // correct answer.)
    int x = A.Length-1, y = B.Length-1;
    while (A[x] == 0 && x > -1) { x--; }
    while (B[y] == 0 && y > -1) { y--; }
    bool flag;
    if (x == y) flag = (A[x] > B[y]);
    else flag = (x > y);

    // using this flag, we can determine order of operations
    // (this flag can also be used to return whether
    // the array is negative)
    List<byte> array = new List<byte>(flag?A:B);
    int len = flag ? B.Length : A.Length;
    for (int i = 0; i < len; i++)
        array = _sub_(array, flag ? B[i] : A[i], i);

    return array.ToArray();
}
private static List<byte> _sub_(List<byte> A, byte b, int idx, byte rem = 0)
{
    short sample = 0;
    if (idx < A.Count)
    {
        sample = (short)((short)A[idx] - (short)b);
        A[idx] = (byte)(sample % 256);
        rem = (byte)(Math.Abs((sample - A[idx])) % 255);
        if (rem > 0)
            return _sub_(A, (byte)rem, idx + 1);
    }
    else A.Add(b);

    return A;
}


Multiplication
public static byte[] Multiply(this byte[] A, byte[] B)
{
    List<byte> ans = new List<byte>();

    byte ov, res;
    int idx = 0;
    for (int i = 0; i < A.Length; i++)
    {
        ov = 0;
        for (int j = 0; j < B.Length; j++)
        {
            short result = (short)(A[i] * B[j] + ov);

            // get overflow (high order byte)
            ov = (byte)(result >> 8);
            res = (byte)result;
            idx = i + j;

            // apply result to answer array
            if (idx < (ans.Count))
                ans = _add_(ans, res, idx); else ans.Add(res);
        }
        // apply remainder, if any
        if(ov > 0) 
            if (idx+1 < (ans.Count)) 
                ans = _add_(ans, ov, idx+1); 
            else ans.Add(ov);
    }

    return ans.ToArray();
}

Division
- on the way -


Modulus
n = n - ( mod * ⌊ n / mod ⌋ )
 */