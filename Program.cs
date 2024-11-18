// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;



UsAsciiIMap map = new UsAsciiIMap();


//TODO: Refactor class!
BuGeRedCreator cr = new BuGeRedCreator("ABC", Color.Beige);
var msgList = cr.CreateMessage();
var bitmap = cr.CreateBitmap(msgList, 10, 50);

//TODO: replace bitmap!
bitmap.Save(@"c:\temp\bmpABCa.bmp");


//TODO: streamline BuGeRedCollection, note that BuGeRedCollection's pixelList is read in GetBuGeRedListFromBitmap

BuGeRedCollection BGRColl = new BuGeRedCollection();

var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\bmpABCa.bmp"));
Color basecol = bgrList[0].ToColor();
int endmark = bgrList.FindIndex(bgr => bgr.Red == (basecol.R-2));
var msglist = bgrList.GetRange(1, endmark-1);
string bitstring = msglist?[0].ToString() + msglist?[1].ToString();
//var chh = map.ConvertToChar(bitstring);


//continue
BuGeRed endof = new BuGeRed(Color.FromArgb(basecol.A - 2, basecol.R, basecol.G, basecol.B));
