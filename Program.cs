// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;
using System.Text;


Color testcolor = Color.Brown;
Color xcol = Color.FromName("Brown");
// If Color.FromName cannot find a match, it returns new Color(0,0,0);


StringBuilder builder = new();
builder.AppendLine("The following arguments are passed:");

// TODO: Add and validate arguments: R, G, B, A plus bitmaps width and height

// Display the command line arguments using the args variable.
foreach (var arg in args)
{
    builder.AppendLine($"Argument={arg}");
    string num = args[0];
    builder.AppendLine($"Argument zero={num}");
}



Console.WriteLine(builder.ToString());

//TODO: streamline BuGeRedCollection, note that BuGeRedCollection's pixelList is read in GetBuGeRedListFromBitmap
//TODO: Refactor class!

UsAsciiIMap map = new UsAsciiIMap();
string input_string = "Hello Christine!";
string output_string = "";
string bmpFileName = @"c:\temp\bmp_test.bmp";

//var fullPath = Path.GetFullPath(bmpFileName);


//BuGeRedCreator cr = new BuGeRedCreator(input_string, Color.Beige);
BuGeRedCreator cr = new BuGeRedCreator(input_string, 100, 200, 120, 255, 40, 200);
var msgList = cr.CreateMessage();
var bitmap = cr.CreateBitmap(msgList); //, 10, 50);
bitmap.Save(bmpFileName);


BuGeRedCollection BGRColl = new BuGeRedCollection();

var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(@"c:\temp\bmp_4_CD.bmp"));
BuGeRed basepix = bgrList[0];
Color basecol = bgrList[0].ToColor();
BuGeRed basepix2 = new BuGeRed(Color.FromArgb(basepix.Alpha, basepix.Red, basepix.Green, basepix.Blue));

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
    output_string += c;
}


Console.WriteLine($"{output_string}");

//string bitstring = msglist?[0].ToString() + msglist?[1].ToString();
//var chh = map.ConvertToChar(bitstring);
//BuGeRed endof = new BuGeRed(Color.FromArgb(basecol.A - 2, basecol.R, basecol.G, basecol.B));


// Return a success code.
return 0;