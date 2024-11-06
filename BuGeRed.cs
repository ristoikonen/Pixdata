using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{


    /// <summary>
    /// USACII data sink made of BuGeRed's with char
    /// </summary>
    public struct PixelBlockWithChar
    {
        // 1=b4	2=b3,3=b2, 4=b1
        public BuGeRed? Pix1 { get; set; }
        public int Row { get; set; }
        public Char Character { get; set; }
    }


    /// <summary>
    /// USACII data sink made of BuGeRed's
    /// </summary>
    public struct PixelBlock
    {
        // 1=b4	2=b3,3=b2, 4=b1,5=col1,6=col2,71=col3
        public BuGeRed? Pix1 { get; set; }
        public BuGeRed? Pix2 { get; set; }
        public BuGeRed? Pix3 { get; set; }
        public BuGeRed? Pix4 { get; set; }
        public BuGeRed? Pix5 { get; set; }
        public BuGeRed? Pix6 { get; set; }
        public BuGeRed? Pix7 { get; set; }
        //int rowno;
        //int colnono;
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public record BuGeRed
    {

        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
        public BGRDiff BGRDiff;


        public BuGeRed(int v, byte[] colorsWithAlpha)
        {
            Blue = colorsWithAlpha[0];
            Green = colorsWithAlpha[1];
            Red = colorsWithAlpha[2];
            Alpha = colorsWithAlpha[3];
        }


        public BuGeRed(Color c)
        {
            Blue = c.B;
            Green = c.G;
            Red = c.R;
            Alpha = c.A;
        }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="copyMe"></param>
        public BuGeRed(BuGeRed copyMe)
        {

            this.Blue = copyMe.Blue; this.Green = copyMe.Green;
            this.Red = copyMe.Red; this.Alpha = copyMe.Alpha;
        }
        public byte[] ToBytes()
        {
            return new byte[] { this.Blue, this.Green, this.Red, this.Alpha };
        }


        /// <summary>
        /// Init from string
        /// </summary>
        /// <param name="bitstring">Should be four or 8 chars</param>
        /// <param name="firstFour">Is bitstring four or 8 chars</param>
        public BuGeRed(string bitstring)
        {
            if (bitstring.Length == 4 || bitstring.Length == 8)
            {
                this.Blue = (byte)int.Parse(bitstring.Substring(0, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(1, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(2, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(3, 1));
            }
            if (bitstring.Length == 8)
            {
                this.Blue = (byte)int.Parse(bitstring.Substring(4, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(5, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(6, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(7, 1));
            }
        }


        /// <summary>
        /// Init from string
        /// </summary>
        /// <param name="bitstring">Should be four or 8 chars</param>
        /// <param name="firstFour">Is bitstring four or 8 chars</param>
        public BuGeRed(string bitstring, bool firstFour)
        {
            if (firstFour)
            {
                this.Blue = (byte)int.Parse(bitstring.Substring(0, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(1, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(2, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(3, 1));
            }
            else
            {
                this.Blue = (byte)int.Parse(bitstring.Substring(4, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(5, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(6, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(7, 1));
            }
        }



        public override int GetHashCode()
        {
            //TODO: Change to b+g+r+a  - what happens if uint is outside int - may happen and as this is a hash:
            // Might have odd behaviour as identical things are not regognised as such.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(this.ToBytes());

            return BitConverter.ToInt32(this.ToBytes(), 0);

            //return (int)this.Blue;
        }

        /// <summary>
        /// Init from byte array
        /// </summary>
        /// <param name="barr"></param>
        public BuGeRed(byte[] barr)
        {
            this.Blue = barr[0];
            this.Green = barr[1];
            this.Red = barr[2];
            this.Alpha = barr[3];
        }

        public Color ToColor()
        {
            int alpha = Convert.ToInt32(this.Alpha);
            int red = Convert.ToInt32(this.Blue);
            int green = Convert.ToInt32(this.Green);
            int blue = Convert.ToInt32(this.Blue);
            return Color.FromArgb(alpha,red, green, blue);
        }

        public override string ToString()
        {
            string s1 = "";
            int alpha = Convert.ToInt32(this.Alpha);
            int red = Convert.ToInt32(this.Blue);
            int green = Convert.ToInt32(this.Green);
            int blue = Convert.ToInt32(this.Blue);
            return string.Format($"R:{red} G:{green} B:{blue} A:{alpha}");
        }
        /*
        public static BuGeRed operator + (BuGeRed b1, BuGeRed x)
        {
            return new BuGeRed(new byte[] { (byte)((byte)b1.Blue + x.Blue), (byte)((byte)b1.Green + x.Green), (byte)((byte)b1.Red + x.Red), (byte)((byte)b1.Alpha - x.Alpha) });
        }
        */
        // Note Alpha is deducted
        // TODO: what if rgb is 255
        public static BuGeRed operator + (BuGeRed bgr, BuGeRed addme)
        {

            return new BuGeRed(new byte[] { (byte)((byte)bgr.Blue + addme.Blue), (byte)((byte)bgr.Green + addme.Green), 
                (byte)((byte)bgr.Red + addme.Red), (byte)((byte)bgr.Alpha - addme.Alpha) });
        }

    }

}
