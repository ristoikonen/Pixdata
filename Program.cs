// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;

BuGeRedCollection BGRColl = new BuGeRedCollection();
var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\ColorDiffs.bmp"));

//List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)


Console.WriteLine("Hello, World!");


USACIIMapper mapper = new USACIIMapper();
PixelBlock pixRow = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) };
PixelBlock pixCol = new PixelBlock { Pix1 = new BuGeRed(new byte[] {  0, 0, 1,0 }) };

mapper.GetUSACSII_Character(pixRow, pixCol);