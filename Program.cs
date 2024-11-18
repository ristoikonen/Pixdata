// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;


//TODO: streamline BuGeRedCollection, note that BuGeRedCollection's pixelList is read in GetBuGeRedListFromBitmap
//TODO: Refactor class!

UsAsciiIMap map = new UsAsciiIMap();

BuGeRedCreator cr = new BuGeRedCreator("Ok, here's the message 007!", Color.Beige);
var msgList = cr.CreateMessage();
var bitmap = cr.CreateBitmap(msgList, 10, 50);
bitmap.Save(@"c:\temp\bmpABC1.bmp");


BuGeRedCollection BGRColl = new BuGeRedCollection();

var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\bmpABC1.bmp"));
BuGeRed basepix = bgrList[0];
Color basecol = bgrList[0].ToColor();
BuGeRed basepix2 = new BuGeRed(Color.FromArgb(basepix.Alpha, basepix.Red, basepix.Green, basepix.Blue));
Color basecol2 = basepix2.ToColor();
int endmark = bgrList.FindIndex(bgr => bgr.Red == (basecol.R-2));
var msglist = bgrList.GetRange(1, endmark-1);


BuGeRedCollection readcol = new BuGeRedCollection();
readcol.pixelList = msglist;

var pairs = Enumerable.Range(0, msglist.Count / 2)
                          .Select(i => Tuple.Create(msglist[i * 2], msglist[i * 2 + 1]));
foreach (var p in pairs)
{
    var bitsstring = p.Item1.Difference(basepix) + p.Item2.Difference(basepix);
    char c = map.ConvertToChar(bitsstring);
    Console.WriteLine($"{bitsstring} => {c}");
}

//string bitstring = msglist?[0].ToString() + msglist?[1].ToString();
//var chh = map.ConvertToChar(bitstring);
//BuGeRed endof = new BuGeRed(Color.FromArgb(basecol.A - 2, basecol.R, basecol.G, basecol.B));
