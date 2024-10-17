// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;

BuGeRedCollection BGRColl = new BuGeRedCollection();
var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\ColorDiffs.bmp"));

//List<BuGeRed> GetBuGeRedListFromBitmap(Bitmap sourceImage)


Console.WriteLine("Hello, World!");