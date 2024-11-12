using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    public class USACIIMapper
    {
        //const string all_chars = @" !\"#$%&'()*+,-./\r\n0123456789:;<=>?\r\n@ABCDEFGHIJKLMNO\r\nPQRSTUVWXYZ[\\]^_\r\n`abcdefghijklmno\r\npqrstuvwxyz{|}~\r\n";
        const string CharCol1 = @" !""""#$%&'()*+,-./";


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
            var c1 = pixelBlockColumnMap.Where(p => p.Equals(colBlock));

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
            var c = pixelBlockColumnMap.Where(p => p.Equals(colBlock) && p.Column > -1);

            // pixelBlock[index];
            return new char();
        }


        public char GetUSACSII_Character(BuGeRedCollection BuGeReds)
        {
            var firstpix = BuGeReds.First;


            foreach (BuGeRed buu in BuGeReds.Word())
            {
                Console.WriteLine(buu.ToString());

            }


            

            foreach (BuGeRed buu in BuGeReds.Sequence(1, 2))
            {
                Console.WriteLine(buu.ToString());

            }

            

            return new char();
        }


    }
}
