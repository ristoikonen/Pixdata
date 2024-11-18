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









    }
}
