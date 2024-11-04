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
        Dictionary<byte, char> map = new Dictionary<byte, char>();
        private string ToHex(int val)
        {
            return "0x" + val.ToString("X");
        }

        private string ToBinary(int val)
        {
            return Convert.ToString(val, 2);
        }



        public UsAsciiIMap()
        {
            byte myByte = 7;
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
            //  0110111
            byte[] bytes = new byte[] { 7 };
            char cb1a = System.Text.Encoding.ASCII.GetChars(bytes)[0];
            Console.WriteLine($"char {cb1a.ToString()} is bytes {bytes.ToString()}");
            Console.WriteLine("    Raw: " + Convert.ToChar(7) + " -- " + System.Text.Encoding.ASCII.GetBytes("7"));

            // 0011 0111

            byte[] abytes = System.Text.Encoding.ASCII.GetBytes("7");
            string abytess  = System.Text.Encoding.ASCII.GetString(abytes);
            int ctby = System.Text.Encoding.ASCII.GetByteCount(System.Text.Encoding.ASCII.GetString(abytes));

            char c1a = System.Text.Encoding.ASCII.GetChars(new byte[] { 7 })[0];


            //Will be "1100100"
            var bitstringi = Convert.ToString(7, 2);
            Console.WriteLine($"char {bitstringi} is char {c1a}");
            PrintBits(System.Text.Encoding.ASCII.GetBytes("7"));


            string hexi = BitConverter.ToString(abytes);  //  new byte[] { Convert.ToByte('<') });

            Console.WriteLine($"hexi {hexi} is byte {abytes} or {Convert.ToByte('7').ToString("x2")} ");

            var bitstring = Convert.ToString(Convert.ToByte('7'), 2);

            Console.WriteLine($"PrintBits {System.Text.Encoding.ASCII.GetBytes("7")}");
            PrintBits(abytes[0]);
            // THIS iS GOOD NOW~!
            var byss= GetBits(System.Text.Encoding.ASCII.GetBytes("7"));
            Console.WriteLine($"sev {System.Text.Encoding.ASCII.GetBytes("7")} or {byss}");
            byte bit7 = 7;

            
            Console.WriteLine($"bitstring {bitstring} is bit7 {bit7} or {Convert.ToByte('7').ToString("x2")} ");

            BitArray bits = new BitArray(bit7);

            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                if ((counter + 1) % 8 == 0)
                    Console.WriteLine();
            }


            //'\u0110111';
            char c1b = (char)7;
            Console.WriteLine($"char {c1a} is byte {c1a}");
            // (byte)
            //string CRC_raw = "0100001";
            // Console.WriteLine("    Raw: " + ToHex(CRC_raw) + " -- " + ToBinary(CRC_raw));

            //printf("dec: %u\n", x);    // prints 512
            //printf("hex: %x\n", x);    // prints 200

            map = new Dictionary<byte, char>()
            {
                {   Convert.ToByte(17),'!'  }
                //, {   Convert.ToChar("""),(byte) 0100010  }
            };
            // "0100001"
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

        void PrintBits(byte[] b)
        {
            BitArray bits = new BitArray(b);

            for (int counter = 0; counter < bits.Length; counter++)
            {
                Console.Write(bits[counter] ? "1" : "0");
                if ((counter + 1) % 8 == 0)
                    Console.WriteLine();
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            Console.WriteLine(bytes[0]); 
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
            Console.WriteLine($"{bytes[0]}  comp: {bytes == barr}");
            return barr;
        }


        char? GetBinary(byte abit)
        {
            byte a = 0xFE;
            byte b = 0xAA;
            char charactor;
            if (map.TryGetValue(abit, out charactor))
            {
                return charactor;
            }
            
            return map.FirstOrDefault(d => d.Key == abit).Value;
        
        }


        //GetUSACSII_Character


    }
}
