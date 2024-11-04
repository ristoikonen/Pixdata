using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    public class USACIIMapper
    { 
        //const string all_chars = @" !\"#$%&'()*+,-./\r\n0123456789:;<=>?\r\n@ABCDEFGHIJKLMNO\r\nPQRSTUVWXYZ[\\]^_\r\n`abcdefghijklmno\r\npqrstuvwxyz{|}~\r\n";
        const string CharCol1 = @" !""""#$%&'()*+,-./";

        /*
 * b4	b3	b2	b1	col1	col2	col3
    1	0	0	0	1	0	0
    1	0	0	1	1	1	0
    0	0	1	0	0	1	0

    Pixel1	Pixel2	Pixel3	Pixel4	Pixel5	Pixel6	Pixel7

*/
        private PixelBlock[] pixelBlockWithCharMap =
        [
            //new PixelBlockWithChar { Character= '0' , Row=0, Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) }
            //new PixelBlockWithChar { Row=1, Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) },
            //new PixelBlockWithChar { Row=2,  Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 0 }) },
            //new PixelBlockWithChar { Row=3,  Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 1 }) },
            //new PixelBlockWithChar { Row=4,  Pix1 = new BuGeRed(new byte[] { 0, 1, 0, 0 }) },
            //new PixelBlockWithChar { Row=5,  Pix1 = new BuGeRed(new byte[] { 0, 1, 0, 1 }) },
            //new PixelBlockWithChar { Row=6,  Pix1 = new BuGeRed(new byte[] { 0, 1, 1, 0 }) },
            //new PixelBlockWithChar { Row=7,  Pix1 = new BuGeRed(new byte[] { 0, 1, 1, 1 }) },
            //new PixelBlockWithChar { Row=8,  Pix1 = new BuGeRed(new byte[] { 1, 0, 0, 0 }) },
            //new PixelBlockWithChar { Row=9,  Pix1 = new BuGeRed(new byte[] { 1, 0, 0, 1 }) },
            //new PixelBlockWithChar { Row=10,  Pix1 = new BuGeRed(new byte[] { 1, 0, 1, 0 }) },
            //new PixelBlockWithChar { Row=11,  Pix1 = new BuGeRed(new byte[] { 1, 0, 1, 1 }) },
            //new PixelBlockWithChar { Row=12,  Pix1 = new BuGeRed(new byte[] { 1, 1, 0, 0 }) },
            //new PixelBlockWithChar { Row=13,  Pix1 = new BuGeRed(new byte[] { 1, 1, 0, 1 }) },
            //new PixelBlockWithChar { Row=14,  Pix1 = new BuGeRed(new byte[] { 1, 1, 1, 0 }) },
            //new PixelBlockWithChar { Row=15,  Pix1 = new BuGeRed(new byte[] { 1, 1, 1, 1 }) }

        ];


        private PixelBlock[] pixelBlockRowMap =
        [
            new PixelBlock { Row=0, Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Row=1, Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) },
            new PixelBlock { Row=2,  Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 0 }) },
            new PixelBlock { Row=3,  Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 1 }) },
            new PixelBlock { Row=4,  Pix1 = new BuGeRed(new byte[] { 0, 1, 0, 0 }) },
            new PixelBlock { Row=5,  Pix1 = new BuGeRed(new byte[] { 0, 1, 0, 1 }) },
            new PixelBlock { Row=6,  Pix1 = new BuGeRed(new byte[] { 0, 1, 1, 0 }) },
            new PixelBlock { Row=7,  Pix1 = new BuGeRed(new byte[] { 0, 1, 1, 1 }) },
            new PixelBlock { Row=8,  Pix1 = new BuGeRed(new byte[] { 1, 0, 0, 0 }) },
            new PixelBlock { Row=9,  Pix1 = new BuGeRed(new byte[] { 1, 0, 0, 1 }) },
            new PixelBlock { Row=10,  Pix1 = new BuGeRed(new byte[] { 1, 0, 1, 0 }) },
            new PixelBlock { Row=11,  Pix1 = new BuGeRed(new byte[] { 1, 0, 1, 1 }) },
            new PixelBlock { Row=12,  Pix1 = new BuGeRed(new byte[] { 1, 1, 0, 0 }) },
            new PixelBlock { Row=13,  Pix1 = new BuGeRed(new byte[] { 1, 1, 0, 1 }) },
            new PixelBlock { Row=14,  Pix1 = new BuGeRed(new byte[] { 1, 1, 1, 0 }) },
            new PixelBlock { Row=15,  Pix1 = new BuGeRed(new byte[] { 1, 1, 1, 1 }) }

        ];


        // Alpha does not map so it's always zero
        private PixelBlock[] pixelBlockColumnMap =
        [
            new PixelBlock { Column=0,  Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 0 }),  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=1,  Pix1 = new BuGeRed(new byte[] { 0, 0, 1 , 0}),  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=2,  Pix1 = new BuGeRed(new byte[] {  0, 1, 0, 0 }) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=4,  Pix1 = new BuGeRed(new byte[] {  1, 0, 0, 0 }) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=5,  Pix1 = new BuGeRed(new byte[] { 1, 0, 1 , 0}) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=6,  Pix1 = new BuGeRed(new byte[] {  1, 1, 0, 0 }) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
            new PixelBlock { Column=7,  Pix1 = new BuGeRed(new byte[] {  1, 1, 1, 0 }) ,  Row=0, Pix2 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) },
        ];


        private PixelBlock[] pixelBlockMap =
        [
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
                new PixelBlock { Column=3,  Pix1 = new BuGeRed(new byte[] {  0, 1, 1, 0 }) },
        ];


        public char? GetChar_Col1(int columnIndex, int charIndex) 
        {
            switch (columnIndex)
            {
                case 0:
                    return CharCol1[columnIndex];
                case 1:
                    break;
                default:
                    break;
            }
            return null;
        }

        void GetString(string input, Action<string> setOutput)
        {
            if (!string.IsNullOrEmpty(input))
            {
                setOutput(input);
            }
        }


        public char GetUSACSII_Character(PixelBlock rowBlock, PixelBlock colBlock) 
        {
            var r1 = pixelBlockRowMap.Where(p => p.Equals(rowBlock));
            var c1 = pixelBlockColumnMap.Where(p => p.Equals(colBlock) );

            var r2 = pixelBlockRowMap.Where(p => Int32.IsPositive(p.Row));
            var c2 = pixelBlockColumnMap.Where(p => p.Column > -1);

            var r3 = pixelBlockRowMap.Any(p => p.Pix1 is not null && p.Pix1.Equals(rowBlock.Pix1));
            var c3 = pixelBlockColumnMap.Any(p => p.Pix1 is not null && p.Pix1.Equals(colBlock.Pix1));


            var rq3 = pixelBlockRowMap.FirstOrDefault(p => p.Pix1 is not null && p.Pix1 == rowBlock.Pix1);
            var cq3 = pixelBlockColumnMap.FirstOrDefault(p => p.Pix1 is not null && p.Pix1 == colBlock.Pix1);

            //PixelBlock[] pixelBlock =
            //[
            //    new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 0 }) }
            //    ,new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) }
            //];

            var r = pixelBlockRowMap.Where(p => p.Equals(rowBlock) && Int32.IsPositive(p.Row));
            var c = pixelBlockColumnMap.Where(p => p.Equals(colBlock) && p.Column > -1 );

            // pixelBlock[index];
            return new char();
        }

    }

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
        public static BuGeRed operator +(BuGeRed b1, BuGeRed x)
        {
            return new BuGeRed(new byte[] { (byte)((byte)b1.Blue + x.Blue), (byte)((byte)b1.Green + x.Green), (byte)((byte)b1.Red + x.Red), (byte)((byte)b1.Alpha - x.Alpha) });
        }



    }

}
