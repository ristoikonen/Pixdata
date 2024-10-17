using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{

    public record BuGeRedLetter
    {

        public BuGeRed Pixel1;
        public BuGeRed Pixel2;
        public BuGeRed Pixel3;
        public BuGeRed Pixel4;

        public BuGeRedLetter(BuGeRed[] fourPixels, BuGeRed basePixel)
        {
            //fourPixels.ToArray();
            Pixel1 = fourPixels[0];
            Pixel2 = fourPixels[1];
            Pixel3 = fourPixels[2];
            Pixel4 = fourPixels[3];

        }

    }
    
    public record BGRDiff
    {

        public byte BlueDiff;
        public byte GreenDiff;
        public byte RedDiff;
        public byte AlphaDiff;
        public BuGeRed? BasePixel;

        public BGRDiff(BuGeRed Pixel, BuGeRed BasePixel)
        {
            this.BasePixel = BasePixel;
            this.RedDiff = (byte)(BasePixel.Red - Pixel.Red);
        }


        public BGRDiff(BGRDiff copyMe)
        {

            this.RedDiff = copyMe.RedDiff;
        }


        public byte[] GetBytes()
        {
            return new byte[] { this.RedDiff };
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


        public BGRDiff(byte[] barr)
        {


            //this.Blue = barr[0];
            //Green = barr[1];
            this.RedDiff = barr[2];
            //Alpha = barr[3];

        }

    }
}
