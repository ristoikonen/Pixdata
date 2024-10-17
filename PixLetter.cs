using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    public class Pix4Letter
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
        public float FloatValue { get; set; }

        public BuGeRed BasePixel { get; set; }

        public Pix4Letter(BuGeRed basePixel, IEnumerable<BuGeRed> allpixels)
        {
            BasePixel = basePixel;
            int pageNumber = 1;
            int pageSize = 4;
            int count = allpixels.Count();

            if (count >= 4)
            {
                while (pageNumber* pageSize< count)
                { 
                    var pixels = allpixels.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize);
                    //TODO CHECK IF ARRAY OF SIZE!
                    //Read4Pixels(basePixel, new BuGeRedLetter(pixels.ToArray()), 1);
                }
            }

            //
        }


        private void Read4Pixels(BuGeRed basePixel, BuGeRedLetter Pixels, int index)
        {
            //BuGeRed basePixel;
            switch (index)
            {
                case 1:
                    BGRDiff d1 = new BGRDiff(Pixels.Pixel1, basePixel);
                    break;
                case 2:
                    BGRDiff d2 = new BGRDiff(Pixels.Pixel2, basePixel);
                    break;
                case 3:
                    BGRDiff d3 = new BGRDiff(Pixels.Pixel3, basePixel);
                    break;
                case 4:
                    BGRDiff d4 = new BGRDiff(Pixels.Pixel4, basePixel);
                    break;
                default:
                    // Default stuff
                    break;
            }
        }


        private void ReadItem(BuGeRed basePixel, BuGeRed Pixel, int index)
        {
            //BuGeRed basePixel;
            switch (index)
            {
                case 1:
                    BGRDiff d1 = new BGRDiff(Pixel, basePixel);
                    break;
                case 2:
                    BGRDiff d2 = new BGRDiff(Pixel, basePixel);
                    break;
                case 3:
                    BGRDiff d3 = new BGRDiff(Pixel, basePixel);
                    break;
                case 4:
                    BGRDiff d4 = new BGRDiff(Pixel, basePixel);
                    break;
                default:
                    // Default stuff
                    break;
            }
        }



    }
}
