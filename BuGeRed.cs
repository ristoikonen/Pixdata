using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    public class USACIIMapper
    { 
        //const string all_chars = @" !\"#$%&'()*+,-./\r\n0123456789:;<=>?\r\n@ABCDEFGHIJKLMNO\r\nPQRSTUVWXYZ[\\]^_\r\n`abcdefghijklmno\r\npqrstuvwxyz{|}~\r\n";
        const string CharCol1 = @" !""""#$%&'()*+,-./";

        public char GetChar_Col1(int columnIndex, int charIndex) 
        { 
            return CharCol1[columnIndex];
        }

        void GetString(string input, Action<string> setOutput)
        {
            if (!string.IsNullOrEmpty(input))
            {
                setOutput(input);
            }
        }

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


        public BuGeRed(BuGeRed copyMe)
        {

            this.Blue = copyMe.Blue; this.Green = copyMe.Green;
            this.Red = copyMe.Red; this.Alpha = copyMe.Alpha;
        }


        public byte[] GetBytes()
        {
            return new byte[] { this.Blue, this.Green, this.Red, this.Alpha };
        }

        public override int GetHashCode()
        {
            //TODO: Change to b+g+r+a  - what happens if uint is outside int - may happen and as this is a hash:
            // Might have odd behaviour as identical things are not regognised as such.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(this.GetBytes());

            return BitConverter.ToInt32(this.GetBytes(), 0);

            //return (int)this.Blue;
        }


        public BuGeRed(byte[] barr)
        {


            this.Blue = barr[0];
            Green = barr[1];
            Red = barr[2];
            Alpha = barr[3];

        }




    }

}
