using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{

    public record BuGeRed
    {

        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
        public bool? IsFirstFour = null;

        public BuGeRed(Color c)
        {
            this.Blue = c.B;
            this.Green = c.G;
            this.Red = c.R;
            this.Alpha = c.A;
        }

        /// <summary>
        /// Copy ctor
        /// </summary>
        /// <param name="copyMe"></param>
        public BuGeRed(BuGeRed copyMe)
        {
            this.Blue = copyMe.Blue; this.Green = copyMe.Green;
            this.Red = copyMe.Red; this.Alpha = copyMe.Alpha; this.IsFirstFour = copyMe.IsFirstFour;
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
                IsFirstFour = false;
            }
        }


        /// <summary>
        /// Init from string
        /// </summary>
        /// <param name="bitstring">Should be four or 8 chars</param>
        /// <param name="firstFour">Is bitstring four or 8 chars</param>
        public BuGeRed(string bitstring, bool? firstFour)
        {
            if (firstFour.HasValue && firstFour.Value)
            {
                this.Blue = (byte)int.Parse(bitstring.Substring(0, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(1, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(2, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(3, 1));
                IsFirstFour = firstFour;
            }
            else
            {
                /*
                this.Red = (byte)int.Parse(bitstring.Substring(4, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(5, 1));
                this.Blue = (byte)int.Parse(bitstring.Substring(6, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(7, 1));
                */
                this.Blue = (byte)int.Parse(bitstring.Substring(4, 1));
                this.Green = (byte)int.Parse(bitstring.Substring(5, 1));
                this.Red = (byte)int.Parse(bitstring.Substring(6, 1));
                this.Alpha = (byte)int.Parse(bitstring.Substring(7, 1));
                
                IsFirstFour = firstFour;
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
        public BuGeRed(byte[] barr, bool? firstFour)
        {
            this.Blue = barr[0];
            this.Green = barr[1];
            this.Red = barr[2];
            this.Alpha = barr[3];
            IsFirstFour = firstFour;
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
            //HACK, problem!!
            //this.IsFirstFour = Convert.ToBoolean(barr[4]);
        }


        public Color ToColor()
        {
            int alpha = Convert.ToInt32(this.Alpha);
            int red = Convert.ToInt32(this.Blue);
            int green = Convert.ToInt32(this.Green);
            int blue = Convert.ToInt32(this.Blue);
            return Color.FromArgb(alpha,red, green, blue);
        }


        public String Difference(BuGeRed basebgr)
        {
            int alpha = Convert.ToInt32(basebgr.Alpha - this.Alpha);
            int red = Convert.ToInt32(basebgr.Red - this.Red);
            int green = Convert.ToInt32(basebgr.Green - this.Green);
            int blue = Convert.ToInt32(basebgr.Blue - this.Blue);
            string test = $"{Math.Abs(green)}{Math.Abs(blue)}{Math.Abs(red)}{Math.Abs(alpha)}";
            return string.Format($"{Math.Abs(blue)}{Math.Abs(green)}{Math.Abs(red)}{Math.Abs(alpha)}");
        }

        public override string ToString()
        {
            int alpha = Convert.ToInt32(this.Alpha);
            int red = Convert.ToInt32(this.Red);
            int green = Convert.ToInt32(this.Green);
            int blue = Convert.ToInt32(this.Blue);
            //  IsFirstFour = {IsFirstFour}
            //TODO test this!
            return string.Format($"{Math.Abs(green)}{Math.Abs(blue)}{Math.Abs(red)}{Math.Abs(alpha)}");
            //return string.Format($"{Math.Abs(red)},{Math.Abs(green)},{Math.Abs(blue)},{Math.Abs(alpha)}");
        }

        // Note Alpha is deducted and IsFirstFour get its value from addme ; added BuGeRed record
        // TODO: what if rgb is 255 - add rule (max 253) to start checks of original Bitmap validation!
        public static BuGeRed operator + (BuGeRed bgr, BuGeRed addme)
        {

            return new BuGeRed(new byte[] { (byte)((byte)bgr.Blue + addme.Blue), (byte)((byte)bgr.Green + addme.Green), 
                (byte)((byte)bgr.Red + addme.Red), (byte)((byte)bgr.Alpha - addme.Alpha), Convert.ToByte(addme.IsFirstFour) });
        }

    }

}
