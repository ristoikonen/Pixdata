// See https://aka.ms/new-console-template for more information
using Pixdata;
using System.Drawing;
using System.Text;
using System.CommandLine;
class Program
{
    static async Task<int> Main(string[] args)
    {

        int ix = 0;
        StringBuilder builder = new();


        var fileOption = new Option<string?>(
            name: "-d",
            description: "De");

        var fileOption2 = new Option<string?>(
            name: "-e",
            description: "Emb");

        var fileOption3 = new Option<string?>(
            name: "-c", 
            description: "Col");

        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        //var sub1Command = new Command("-e", "First-level subcommand");
        //rootCommand.Add(sub1Command);
        //var sub1aCommand = new Command("-c", "First level subcommand");
        //rootCommand.Add(sub1aCommand);

        rootCommand.AddOption(fileOption);
        rootCommand.AddOption(fileOption2);
        rootCommand.AddOption(fileOption3);

        rootCommand.SetHandler((file) =>
        {
            //ReadFile(file!);
            Console.WriteLine($"de hasdler {file}");
        },
            fileOption);

        await rootCommand.InvokeAsync(args);
        /*
        pixdata userdata.bmp -c Brown -e userdetails.txt

        Argument=userdata.bmp index 0
        Argument=-c index 1
        Argument=Brown index 2
        Argument=-e index 3
        Argument=userdetails.txt index 4

         */

/*
        builder.AppendLine(
            "PIXDATA" + Environment.NewLine +
            "Description:" + Environment.NewLine + "\t" + "Embed and decode ASCII encoded string into a single color Bitmap" + Environment.NewLine +
            "Usage:" + Environment.NewLine + "\t" + "pixdata image.bmp [options]" + Environment.NewLine +
            "Options:" + Environment.NewLine + "\t" + "--c color <B, G, R, A> <Brown>" + Environment.NewLine +
            "\t" + "--e embed <inmessage.txt>" + Environment.NewLine +
            "\t" + "--d decode <outmessage.txt>" + Environment.NewLine +
            Environment.NewLine +
            "Usage samples:" + Environment.NewLine + "\t" + "pixdata userdata.bmp -c Brown -e userdetails.txt" + Environment.NewLine +
            "\t" + "Embed text in userdetails.txt into brown colored userdata.bmp bitmap" + Environment.NewLine +
            "Usage samples:" + Environment.NewLine + "\t" + "pixdata userdata.bmp -d out.txt" + Environment.NewLine +
            "\t" + "Read text embedded in userdata.bmp into out.txt" + Environment.NewLine +
            "");

        builder.AppendLine("The following arguments are passed:");

        Color testcolor = Color.Brown;
        Color xcol = Color.FromName("Brown");
        // If Color.FromName cannot find a match, it returns new Color(0,0,0);


        // Color c;
        // Setup ARGB COLOR 80, 20, 86, 20
        Color c = Color.FromArgb(80, 20, 86, 20);

        int r, g, b, a;

        r = c.R;
        g = c.G;
        b = c.B;
        a = c.A;
        String rgba = String.Format("rgba({0},{1},{2},{3})", c.R, c.G, c.B, c.A);


        //Color c = ColorTranslator.FromHtml(hexcolor);

        // TODO: Add and validate arguments: R, G, B, A plus bitmaps width and height

        // Display the command line arguments using the args variable.
        foreach (var arg in args)
        {
            builder.AppendLine($"Argument={arg} index {ix++}");
            //string num = args[0];
            //builder.AppendLine($"Argument zero={num}");
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

        int endmark = bgrList.FindIndex(bgr => bgr.Red == (basecol.R - 2));
        var msglist = bgrList.GetRange(1, endmark - 1);

        BuGeRedCollection readcol = new BuGeRedCollection();
        readcol.pixelList = msglist;

        var pairs = Enumerable.Range(0, msglist.Count / 2)
                                  .Select(i => Tuple.Create(msglist[i * 2], msglist[i * 2 + 1]));

        foreach (var p in pairs)
        {
            var bitsstring = p.Item1.Difference(basepix) + p.Item2.Difference(basepix);
            char ch = map.ConvertToChar(bitsstring);
            // Console.WriteLine($"{bitsstring} => {ch}");
            output_string += ch;
        }

        Console.WriteLine($"{output_string}");

        //string bitstring = msglist?[0].ToString() + msglist?[1].ToString();
        //var chh = map.ConvertToChar(bitstring);
        //BuGeRed endof = new BuGeRed(Color.FromArgb(basecol.A - 2, basecol.R, basecol.G, basecol.B));

        */

        // Return a success code.
        return 0;
    }
}