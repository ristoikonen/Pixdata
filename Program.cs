// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

BuGeRedCollection BGRColl = new BuGeRedCollection();

//Bitmap bmp = new Bitmap(@"c:\temp\ColorDiffsSmall.bmp");
Bitmap bmp = new Bitmap(@"c:\temp\ColorDiffsEdited.bmp");


var bgrList = BGRColl.GetBuGeRedListFromBitmap(bmp); // new Bitmap(@"c:\temp\ColorDiffs.bmp"));
Console.WriteLine(bgrList[0].Red);
Console.WriteLine(bgrList[1].Red);
Console.WriteLine(bgrList[2].Red);
//bgrList[0].Red = (byte)(bgrList[0].Red-1);
//Console.WriteLine(bgrList[0].Red);

int i = 256;
byte[] buffer = BitConverter.GetBytes(i);
byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i));



//int number = System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i);
//byte[] buffer3 = BitConverter.GetBytes(System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(i));

// row 1
BuGeRed r1 = new BuGeRed(new byte[] { 0, 0, 0, 1 });
// col 3
BuGeRed c3 = new BuGeRed(new byte[] { 0, 1, 1, 0 });

// change sec and trird pixel with above values



BuGeRed b1 = new BuGeRed(new byte[] { 1, 0, 0, 1 });
BuGeRed b2 = new BuGeRed(new byte[] { 1, 0, 0, 1 });
Console.WriteLine(((bool)(b1 == b2)).ToString());

List<BuGeRed> editme = new List<BuGeRed>(bgrList);

Console.WriteLine(editme[1].Blue);
Console.WriteLine(editme[1].Green);
Console.WriteLine(editme[1].Red);
Console.WriteLine(editme[1].Alpha);


editme[1] += r1;
editme[2] += c3;


Console.WriteLine(editme[1].Blue);
Console.WriteLine(editme[1].Green);
Console.WriteLine(editme[1].Red);
Console.WriteLine(editme[1].Alpha);

Console.WriteLine(editme[2].Blue);
Console.WriteLine(editme[2].Green);
Console.WriteLine(editme[2].Red);
Console.WriteLine(editme[2].Alpha);

Bitmap targetImage = BGRColl.GetBitmapFromBuGeRedList(editme, bmp.Width, bmp.Height);
targetImage.Save(@"c:\temp\ColorDiffsEdited.bmp");


//BuGeRed baa2 = bgrList[0];

//List<BuGeRed> editme = new List<BuGeRed>(bgrList);
//byte val = (byte)(bgrList[0].Red - 3);
//editme[0].Red = (byte)(bgrList[0].Red - 3);


//BuGeRed ba1 = editme[0];
//BuGeRed ba2 = bgrList[0];

//Array.Copy(bgrList, firstThreeAsList, 0, 3);

//List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)


Console.WriteLine("Hello, World!");


USACIIMapper mapper = new USACIIMapper();
PixelBlock pixRow = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) };
PixelBlock pixCol = new PixelBlock { Pix1 = new BuGeRed(new byte[] {  0, 0, 1,0 }) };

mapper.GetUSACSII_Character(pixRow, pixCol);

