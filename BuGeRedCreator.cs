using Pixdata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    //TODO move inside BuGeRedCollection?

    internal class BuGeRedReader 
    {

        public BuGeRedReader()
        {                
        }

        public BuGeRedReader(Bitmap bmp) {
            BuGeRedCollection BGRColl = new BuGeRedCollection();
            var bgrList = BGRColl.GetBuGeRedListFromBitmap(bmp); // new Bitmap(@"c:\temp\ColorDiffs.bmp"));
            Console.WriteLine(bgrList[0].Red);
            Console.WriteLine(bgrList[1].Red);
            Console.WriteLine(bgrList[2].Red);
            //bgrList[0].Red = (byte)(bgrList[0].Red-1);
            //Console.WriteLine(bgrList[0].Red);

            int i = 256;
            byte[] buffer = BitConverter.GetBytes(i);
            byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i));

            USACIIMapper mapper = new USACIIMapper();
            PixelBlock pixRow = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) };
            PixelBlock pixCol = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 0 }) };
            mapper.GetUSACSII_Character(pixRow, pixCol);
        }

    }

    internal class BuGeRedCreator
    {

        private string embedMessage;
        private Color color;
        BuGeRed bgrcolor;

        public BuGeRedCreator(string embedMessage, Color color) 
        { 
            this.embedMessage = embedMessage;
            this.color = color;
            this.bgrcolor = new BuGeRed(color);
        }
        /*
         * 
        public List<BuGeRed> CreateMessage()//string embed, Color color)
        {
            UsAsciiIMap map = new UsAsciiIMap();
            BuGeRedCollection bgrcoll = new BuGeRedCollection();

            List<string> binaries = new List<string>();
            List<BuGeRed> BuGeRedMessage = new List<BuGeRed>();
            List<BuGeRed> BuGeRedBase = new List<BuGeRed>();
            
            int modi = 0; bool isfour = true;


            // Create colors for message
            foreach (char ch in embedMessage.ToCharArray())
            {
                string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
                for (int ix = 0; ix < 2; ix++)
                {
                    isfour = (modi++ % 2) == 0;
                    BuGeRedMessage.Add(new BuGeRed(zeroes_ones));
                }
            }
            return BuGeRedMessage;



        }
        
        */

        // Create final Bitmap where; first BuGeRed is of base color, followed by message colors, rest is base color
        public Bitmap? CreateBitmap(List<BuGeRed> messageColors, int height, int width)
        {

            if (height <= 0 || width <= 0)
                return null;

            int totalpixelcount = height * width;

            BuGeRedCollection bgrcoll = new BuGeRedCollection();
            
            // First pixel is of base color
            bgrcoll.SetFirst( new BuGeRed(color));
            // Followed by messages colors
            bgrcoll.Add(messageColors);
            
            // Checks
            if (totalpixelcount < bgrcoll.Count)
                return null;

            
            if (totalpixelcount > bgrcoll.Count)
            {
                // we need filling with base color
                int fillcount = totalpixelcount - bgrcoll.Count;
                List<BuGeRed> filling = new List<BuGeRed>(fillcount);
                //for (int i = 0; i < fillcount; i++)
                //{
                    filling.AddRange(Enumerable.Repeat(bgrcolor, fillcount));
                //}
                // Fill rest with base color
                bgrcoll.Add(filling);
            }
            

            Bitmap bmp = bgrcoll.GetBitmapFromBuGeRedList(width, height);
            return bmp;
        }
        
        
        // Create Bitmap from message string and base color
        public Bitmap Create(string embed, Color color)
        {
            UsAsciiIMap map = new UsAsciiIMap();
            BuGeRedCollection bgrcoll = new BuGeRedCollection();

            List<string> binaries = new List<string>();
            List<BuGeRed> BuGeReds = new List<BuGeRed>();
            List<BuGeRed> BuGeRedBrowns = new List<BuGeRed>();
            int modi = 0; bool isfour = true;

            fore ach (char ch in embed.ToCharArray())
            {
                string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
                for (int ix = 0; ix< 2; ix++)
                {
                    isfour = (modi++ % 2) == 0;
                    BuGeReds.Add(new BuGeRed(zeroes_ones));
                } 
            }

            BuGeRed bgrColor = new BuGeRed(color);
            for (int ixx = 0; ixx < BuGeReds.Count; ixx++)
            {
                BuGeRedBrowns.Add(bgrColor);
                BuGeRedBrowns[ixx] += BuGeReds[ixx];
            }

            int he = BuGeReds.Count / 2;
            int we = BuGeReds.Count / he;

            bgrcoll.pixelList = BuGeRedBrowns;

            Bitmap bmpNEW = bgrcoll.GetBitmapFromBuGeRedList( we, he);
            return bmpNEW;

        }
    }
}
