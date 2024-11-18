using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pixdata
{
    internal  class UsAsciiIMap
    {
        // var sevenItems = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };0x00 0xA1,
        // const string all_chars = @" !\"#$%&'()*+,-./\r\n0123456789:;<=>?\r\n@ABCDEFGHIJKLMNO\r\nPQRSTUVWXYZ[\\]^_\r\n`abcdefghijklmno\r\npqrstuvwxyz{|}~\r\n";

        Dictionary<char, string> map = new Dictionary<char, string>();
        private string ToHex(int val)
        {
            return "0x" + val.ToString("X");
        }

        private string ToBinary(int val)
        {
            return Convert.ToString(val, 2);
        }

        public byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public string ConvertToBitString(char[] c)
        {
            string bitstring = "";
            string bitstring_reversed = "";

            BitArray bitarr = new BitArray(System.Text.Encoding.ASCII.GetBytes(c));

            for (int counter = 0; counter < bitarr.Length; counter++)
            {
                bitstring += bitarr[counter] ? "1" : "0";
                //if ((counter + 1) % 8 == 0)
                //    Console.WriteLine();
            }

            // Convert to bool array (easier than below char array) for reverse to little indian!
            bool[] flags = bitstring.Select(c => c == '1').ToArray();
            Array.Reverse(flags);

            //char[] charArray = bitstring.ToCharArray();
            //Array.Reverse(charArray);
            //bitstring_reversed = new string(charArray);

            BitArray bitarr_back = new BitArray(flags);

            for (int counter = 0; counter < bitarr_back.Length; counter++)
            {
                bitstring_reversed += bitarr_back[counter] ? "1" : "0";
            }

            return bitstring_reversed;
        }

        public string Test(string data)
        {
            string s1 = "";

            for (int i = 0; i < data.Length; i++)
            {

                char c = data[i];
                byte b = (byte)c;

                if (b == 0)
                {
                    Console.WriteLine($"Char {c} Byte {b}");
                }
                else s1 += c;
            }
            return s1;
        }

        public char ConvertToChar(string? bitstring)
        {
            byte[] bytes = new byte[1];

            bool[] flags = bitstring?.Select(c => c == '1').ToArray();

            // from little indian!
            Array.Reverse(flags);

            new BitArray(flags).CopyTo(bytes, 0);
            var charstring = System.Text.Encoding.ASCII.GetString((byte[])bytes);

            return charstring.ToCharArray()[0];
        }

        public UsAsciiIMap()
        {
            string bitstr = ConvertToBitString(new char[] { 'K' });
            Console.WriteLine($"Onko 01001011: {bitstr}");
            char kerttulainen = ConvertToChar(bitstr);
            Console.WriteLine($"Onko K: {kerttulainen}");


            var allAscii = Enumerable.Range('\x1', 127).ToArray();

            //var map = new Dictionary<char, string>();

            foreach (int c in allAscii)
            {
                string bitstring = ConvertToBitString(new char[] { (char)c });
                map.Add((char)c, bitstring);
            }

            foreach (KeyValuePair<char, string> kvp in map)
            {
                Console.WriteLine($"{kvp.Key} =  {kvp.Value}");
            }

            decimal value = 7.3m;
            Console.WriteLine($"Math.Ceiling: {Math.Ceiling(value)}");
            // Math.Ceiling: 8
            decimal value2 = 7.3m;
            Console.WriteLine($"Math.Round: {Math.Round(value2 * 2, MidpointRounding.AwayFromZero) / 2}");
            // Math.Round: 7.5
        }

        void PrintBits(byte b)
        {
            BitArray bits = new BitArray(b);

            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                if ((counter + 1) % 8 == 0)
                    Console.WriteLine();
            }
        }

        void PrintBits(BitArray bits)
        {
            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                if ((counter + 1) % 8 == 0)
                    Console.WriteLine();
            }
        }

        void PrintBits(BitArray bits, bool islittleIndian)
        {
            if (islittleIndian)
            {
                for (int counter = bits.Length-1; counter >= 0; counter--)
                {
                    Console.Write(bits[counter] ? "1" : "0");
                    //if ((counter + 1) % 8 == 0)
                    //    Console.WriteLine();
                }
            }
            else
            { 
                for (int counter = 0; counter < bits.Length; counter++)
                {
                    Console.Write(bits[counter] ? "1" : "0");
                    if ((counter + 1) % 8 == 0)
                        Console.WriteLine();
                }
            }
        }

        void PrintBits(byte[] b)
        {
            BitArray bits = new BitArray(b);

            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                if ((counter + 1) % 8 == 0)
                    Console.WriteLine();
            }
            //byte[] bytes = new byte[1];
            //probelo
            //bits.CopyTo(bytes, 0);
            //Console.WriteLine(bytes[0]); 
        }


        public byte[] GetBits(byte[] b)
        {
            BitArray bits = new BitArray(b);
            byte[] barr = new byte[bits.Length];

            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                barr[bits.Length - 1- counter] = bits[counter] ? (byte)1 : (byte)0;

                //if ((counter + 1) % 8 == 0)  Console.WriteLine();
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            Console.WriteLine($"bytes first bit: {bytes[0]}  compare: {bytes == barr}");
            return barr;
        }


        public string? GetBinary(char c)
        {
            byte a = 0xFE;
            byte b = 0xAA;
            string? bitstr;
            if (map.TryGetValue(c, out bitstr))
            {
                return bitstr;
            }
            return null;
            //return map.FirstOrDefault(d => d.Key == abit).Value;
        
        }


        //GetUSACSII_Character

        /*
         * byte myByte = 7;
            // hexadecimal representation
            string hex = myByte.ToString("X2");
            // binary representation
            string bin = Convert.ToString(myByte, 2);
            Console.WriteLine("Hexadecimal: " + hex);
            Console.WriteLine("Binary: " + bin);

            PrintBits(myByte);

            int x = 7;

            Console.WriteLine($"Hexadecimal value of {x} is {x:X} or {x:X4}");
            string hexString = "00110111";
            int num = Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine(num);

            // THIS iS GOOD NOW~!
            string bitsy = ConvertToBitString("D".ToCharArray());
            //BitArray bitsa = new BitArray();

                    //{
            //    {   Convert.ToByte(17),'!'  }
            //    //, {   Convert.ToChar("""),(byte) 0100010  }
            //};

            string bitstring = Convert.ToString(Convert.ToByte('D'), 2);

            var byss= GetBits(System.Text.Encoding.ASCII.GetBytes("D"));
            var sevbytes = System.Text.Encoding.ASCII.GetBytes("D");
            BitArray sevbytesss = new BitArray(sevbytes);

            byte[] bytses = new byte[1];
            sevbytesss.CopyTo(bytses, 0);
            var isthis_seven = System.Text.Encoding.ASCII.GetString((byte[])bytses);

            BitArray sevbytesssr = sevbytesss.RightShift(1);
            //BitArray sevbytesssxr = BitArray.Xor(sevbytesss);
            byte[] ba = new byte[sevbytesss.Length];
            sevbytesss.CopyTo(ba,0);
            var charss = System.Text.Encoding.ASCII.GetChars(ba);
            //char[] result = Encoding.ASCII.GetString(ba).ToCharArray();

            BitArray bitss = new BitArray(byss);
            Console.WriteLine($"sev {System.Text.Encoding.ASCII.GetBytes("7")} or {byss}");
            byte bit7 = 7;

                //    byte[] bytses = new byte[1];
                //sevbytesss.CopyTo(bytses, 0);
                //    var isthis_seven = System.Text.Encoding.ASCII.GetString((byte[])bytses);
        */

    }
}
