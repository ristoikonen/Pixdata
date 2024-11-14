// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Text;


//TODO: unite maps
UsAsciiIMap map = new UsAsciiIMap();
USACIIMapper asciimapper = new USACIIMapper();
/*
BuGeRedCollection bc = new BuGeRedCollection();
var lista = bc.GetBuGeRedListFromBitmap((Bitmap)Bitmap.FromFile("c:\\temp\\bmpABCa.bmp"));
*/
//TODO: fix this , refactor class!


BuGeRedCreator cr = new BuGeRedCreator("ABC", Color.Beige);
var msgList = cr.CreateMessage();
var bitmap = cr.CreateBitmap(msgList, 10, 50);
//CreateBitmapFromBuGeRedList
//TODO: replace bitmap!
bitmap.Save(@"c:\temp\bmpABCa.bmp");


//TODO: streamline BuGeRedCollection, note that BuGeRedCollection's pixelList is read in GetBuGeRedListFromBitmap

BuGeRedCollection BGRColl = new BuGeRedCollection();

var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\bmpABCa.bmp"));


//var chrr = asciimapper.GetUSACSII_Character(BGRColl);


//bgrList[0].Red = (byte)(bgrList[0].Red-1);

BuGeRedCreator cr2 = new BuGeRedCreator("ABC", Color.Beige);
var msgList2 = cr2.CreateMessage();
var bitmap2 = cr2.CreateBitmap(msgList2, 10, 50);

//TODO: replace bitmap!
bitmap2.Save(@"c:\temp\bmpABCa.bmp");


Bitmap newBmp = cr.Create("ABC", Color.Beige);

BuGeRedCollection bcol = new BuGeRedCollection();

BuGeRed bgrBrowne = new BuGeRed(Color.Brown);
BuGeRed bgrBrowneEdited = new BuGeRed(Color.Brown);
BuGeRed bgrBrowneEditedSec = new BuGeRed(Color.Brown);
string? zeros_ones = map.GetBinary('A');
BuGeRed bgrA = new BuGeRed(zeros_ones, true);
BuGeRed bgrAsec = new BuGeRed(zeros_ones, false);

// we can add one to another, except 255 valyues!  Alpha is minused.
bgrBrowneEdited += bgrA;
bgrBrowneEditedSec += bgrAsec;


List<BuGeRed> newpiclist2 = new List<BuGeRed>();
newpiclist2.Add(bgrBrowne);
newpiclist2.Add(bgrBrowneEdited); newpiclist2.Add(bgrBrowneEditedSec); 
//newpiclist2.Add(bgrBrowne); newpiclist2.Add(bgrBrowne); newpiclist2.Add(bgrBrowne); 
//newpiclist2.Add(bgrBrowne); newpiclist2.Add(bgrBrowne); newpiclist2.Add(bgrBrowne);

//int h = newpiclist2.Count / 2;
//int w = newpiclist2.Count / h;

bcol.pixelList = newpiclist2;

foreach (BuGeRed buu in bcol.Sequence(1,2))
{
    Console.WriteLine(buu.ToString());

}

foreach (BuGeRed buu in bcol)
{
    Console.WriteLine(  buu.ToString());

}


Bitmap bmpSmalle = bcol.GetBitmapFromBuGeRedList(newpiclist2.Count / (newpiclist2.Count / 2), newpiclist2.Count / 2);
bmpSmalle.Save(@"c:\temp\bmpSmall.bmp");

var list = bcol.GetBuGeRedListFromBitmap(bmpSmalle);
//TODO: fix this , refactor class!
bcol.pixelList = list;


Console.WriteLine("Hello, World!");

USACIIMapper mapper = new USACIIMapper();
PixelBlock pixRow = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 0, 1 }) };
PixelBlock pixCol = new PixelBlock { Pix1 = new BuGeRed(new byte[] { 0, 0, 1, 0 }) };
mapper.GetUSACSII_Character(pixRow, pixCol);



/*


string teststring = "Hello!";
List<string> binaries = new List<string>(teststring.Length);
List<BuGeRed> BuGeReds = new List<BuGeRed>();
List<BuGeRed> BuGeRedBrowns = new List<BuGeRed>();

int modi = 0;
bool isfour = true;

var xxc = teststring.ToCharArray();

foreach (char ch in teststring.ToCharArray())
{
    string? zeroes_ones = map.GetBinary(ch);
    binaries.Add(zeroes_ones);
    for (int ix = 0; ix < 2; ix++)
    {
        isfour = (modi++ % 2) == 0;
        BuGeReds.Add(new BuGeRed(zeroes_ones));
    }
}




BuGeRedCollection bcc = new BuGeRedCollection();


Color brown = Color.Brown;
BuGeRed bgrBrownColor = new BuGeRed(brown);
for(int ixx = 0; ixx< BuGeReds.Count; ixx++)
{
    BuGeRedBrowns.Add(bgrBrownColor);

    BuGeRedBrowns[ixx] += BuGeReds[ixx] ;
}



//int listctt = BuGeReds.Count/2;
int he = BuGeReds.Count / 2;
int we = BuGeReds.Count / he;
bcc.pixelList = BuGeRedBrowns;
Bitmap bmpNEW = bcc.GetBitmapFromBuGeRedList( we, he);
bmpNEW.Save(@"c:\temp\bmpNEW.bmp");


//g => 01100111
string? zerosones = map.GetBinary('g');
var tests= map.Test(zerosones);

//01100111 => g
var bits = map.ConvertToChar(zerosones);

int nums = int.Parse(zerosones.Substring(0,1));
var by1 = (byte)nums;

//var by1= (byte)Convert.ToInt32(gbin?[0]);
var bty1b = zerosones?[0];

//bxh += (byte?)bxb;
byte[] test = map.GetBytes(zerosones);

Color c = Color.Brown;
BuGeRed bgrBrown = new BuGeRed(c);
BuGeRed bgrBrownEdited = new BuGeRed(c);
BuGeRed bgr1 = new BuGeRed(zerosones, true);
//bgr1.Blue += by1; //(byte)Encoding.ASCII.GetBytes(gbin)[0]; ;

// we can add one to another, except 255 valyues!  Alpha is minused.
bgrBrownEdited += bgr1;

List<BuGeRed> newpiclist = new List<BuGeRed>();
newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited); newpiclist.Add(bgrBrownEdited);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);
newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown); newpiclist.Add(bgrBrown);

int listct = newpiclist.Count;
int h = listct / 2;
int w = listct / h;

//TODO: add logic when list and h*w do NOT match!
//if (listct > (h * w))
//{
//
//}


BuGeRedCollection bc = new BuGeRedCollection();
bc.pixelList = newpiclist;
Bitmap bmpSmall = bc.GetBitmapFromBuGeRedList( w, h);
bmpSmall.Save(@"c:\temp\bmpSmall.bmp");

// ---------------------------

BuGeRed g1 = new BuGeRed(new byte[] { (byte)Convert.ToInt32(zerosones?[0]), 
                (byte)Convert.ToInt32(zerosones?[1]), 
                (byte)Convert.ToInt32(zerosones?[2]), 
                (byte)Convert.ToInt32(zerosones?[3]) });


BuGeRedCollection BGRColl = new BuGeRedCollection();

//Bitmap bmp = new Bitmap(@"c:\temp\ColorDiffsSmall.bmp");
Bitmap bmp = new Bitmap(@"c:\temp\ColorDiffsEdited.bmp");

var x = (byte)Convert.ToInt32(zerosones?[0]);

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

//Bitmap targetImage = BGRColl.GetBitmapFromBuGeRedList(editme, bmp.Width, bmp.Height);
//targetImage.Save(@"c:\temp\ColorDiffsEdited.bmp");

var bugrrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\ColorDiffsEdited.bmp"));

foreach (var bugrr in bugrrList)
{
    Console.WriteLine(bugrr.ToString());
}

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

*/