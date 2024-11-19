using Pixdata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    internal class BuGeRedCreator
    {

        private string embedMessage;
        //private Color color;
        private BuGeRed BaseColor;
        private int h;
        private int w;

        public BuGeRedCreator(string embedMessage, Color color, int h, int w) 
        { 
            this.embedMessage = embedMessage;
            //this.color = color;
            this.BaseColor = new BuGeRed(color);
            this.h = h;
            this.w = w;
        }

        public BuGeRedCreator(string embedMessage, int R, int G, int B, int A , int h, int w)
        {
            this.embedMessage = embedMessage;
            //this.color = Color.FromArgb(A, R, G, B);
            this.BaseColor = new BuGeRed(Color.FromArgb(A, R, G, B));
            this.h = h;
            this.w = w;
        }


        public List<BuGeRed> CreateMessage()
        {
            const int bits_per_pixel = 4;
            UsAsciiIMap map = new UsAsciiIMap();
            BuGeRedCollection bgrcoll = new BuGeRedCollection();

            List<string> binaries = new List<string>();
            List<BuGeRed> BuGeRedMessage = new List<BuGeRed>();
            List<BuGeRed> BuGeRedBase = new List<BuGeRed>();

            int modi = 0; bool isfour = true;

            //byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i))

            // Create colors for message
            foreach (char ch in embedMessage.ToCharArray())
            {
                string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
                Console.WriteLine($"{ch} => {zeroes_ones}");
                // we have 8 as "11110000" so we need to create 2 BuGeReds
                for (int ix = 0; ix < 2; ix++)
                {
                    //int readix = isfour ? 0 : bits_per_pixel;
                    //var part_of_bitstring = zeroes_ones.Substring(readix, bits_per_pixel);

                    // is it start or end part
                    isfour = (modi++ % 2) == 0;
                    //add message to base color
                    BuGeRed based_on_base = BaseColor;
                    //TODO: fix operators based_on_base
                    var newcol = new BuGeRed(zeroes_ones, isfour);
                    based_on_base += newcol;
                    based_on_base.IsFirstFour = isfour;
                    BuGeRedMessage.Add(based_on_base);
                }
            }

            return BuGeRedMessage;
        }


        public List<BuGeRed> CreateMessageOld()//string embed, Color color)
        {
            const int bits_per_pixel = 4;
            UsAsciiIMap map = new UsAsciiIMap();
            BuGeRedCollection bgrcoll = new BuGeRedCollection();

            List<string> binaries = new List<string>();
            List<BuGeRed> BuGeRedMessage = new List<BuGeRed>();
            List<BuGeRed> BuGeRedBase = new List<BuGeRed>();

            int modi = 0; bool isfour = true;

            //byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i))

            // Create colors for message
            foreach (char ch in embedMessage.ToCharArray())
            {
                string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
                Console.WriteLine($"{ch} => {zeroes_ones}");
                // we have 8 as "11110000" so we need to create 2 BuGeReds
                for (int ix = 0; ix < 2; ix++)
                {
                    //int readix = isfour ? 0 : bits_per_pixel;
                    //var part_of_bitstring = zeroes_ones.Substring(readix, bits_per_pixel);

                    // is it start or end part
                    isfour = (modi++ % 2) == 0;
                    //add message to base color
                    BuGeRed based_on_base = BaseColor;
                    //TODO: fix operators based_on_base
                    based_on_base += new BuGeRed(zeroes_ones, isfour);
                    based_on_base.IsFirstFour = isfour;
                    BuGeRedMessage.Add(based_on_base);
                }
            }

            return BuGeRedMessage;
        }

        // Create final Bitmap where; first BuGeRed is of base color, followed by message colors, rest is base color
        public Bitmap? CreateBitmap(List<BuGeRed> messageColors)//, int height, int width)
        {

            if (h <= 0 || w <= 0)
                return null;

            int totalpixelcount = h * w;

            BuGeRedCollection bgrcoll = new BuGeRedCollection();
                        
            Color basecol = BaseColor.ToColor();
            byte newred = (byte)(basecol.R - 2);
            byte[] endcolor = new byte[4] { (byte)(basecol.B), (byte)(basecol.G), (byte)(basecol.R - 2), (byte)(basecol.A) };
            BuGeRed endofmsg = new BuGeRed(endcolor);

            messageColors.Insert(0, new BuGeRed(this.BaseColor)); // color));
            //TODO fix double lists
            // End of msg is base color with Alpha minus 2
            messageColors.Add(endofmsg);

            // First pixel is of base color
            //bgrcoll.SetFirst( new BuGeRed(color));

            // second pixel has number of chars, not BuGeReds
            //bgrcoll.SetSecond(new BuGeRed(color));
            // The binary word 1 1 1 1 1 1 1 1 is equivalent to 1×128 + 1×64 + 1×32 + 1×16 + 1×8 + 1×4 + 1×2 + 1×1 = 255

            // Followed by messages colors
            //bgrcoll.Add(messageColors);
            
            //Add last color with alpha minus 2 to denote end of message

            // Checks
            if (totalpixelcount < bgrcoll.Count)
                return null;

            
            if (totalpixelcount > bgrcoll.Count)
            {
                // we need filling with base color
                int fillcount = totalpixelcount - messageColors.Count;
                List<BuGeRed> filling = new List<BuGeRed>(fillcount);
                filling.AddRange(Enumerable.Repeat(BaseColor, fillcount));
                // Fill rest with base color
                messageColors.AddRange(filling);
            }
            return bgrcoll.CreateBitmapFromBuGeRedList(messageColors, w, h);
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

            foreach (char ch in embed.ToCharArray())
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

/*
 * 
 *    
    internal class BuGeRedReaders 
    {

        public BuGeRedReaders()
        {                
        }

        public BuGeRedReaders(Bitmap bmp) {
            BuGeRedCollection BGRColl = new BuGeRedCollection();
            var bgrList = BGRColl.GetBuGeRedListFromBitmap(bmp); // new Bitmap(@"c:\temp\ColorDiffs.bmp"));

            int i = 256;
            byte[] buffer = BitConverter.GetBytes(i);
            byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i));
        }
    }
 *             /*
    foreach (char ch in embedMessage.ToCharArray())
    {
        string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException($"Cant convert char {ch} to a string of 0's and 1's");
        for (int ix = 0; ix < 2; ix++)
        {
            isfour = (modi++ % 2) == 0;
            BuGeRedMessage.Add(new BuGeRed(zeroes_ones));
        }
    }
    */
/*
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

