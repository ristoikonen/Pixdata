using Pixdata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    //TODO move inside BuGeRedCollection?
    internal class BuGeRedCreator
    {

        public BuGeRedCreator() { }

        public Bitmap Create(string embed, Color color)
        {

            UsAsciiIMap map = new UsAsciiIMap();
            BuGeRedCollection bgrcoll = new BuGeRedCollection();

            List<string> binaries = new List<string>();
            List<BuGeRed> BuGeReds = new List<BuGeRed>();
            List<BuGeRed> BuGeRedBrowns = new List<BuGeRed>();
            int modi = 0; bool isfour = true;

            foreach (char ch in embed.ToCharArray())
            {
                string? zeroes_ones = map.GetBinary(ch) ?? throw new InvalidOperationException("Cant convert char to 01 -string");
                for (int ix = 0; ix< 2; ix++)
                {
                    isfour = (modi++ % 2) == 0;
                    BuGeReds.Add(new BuGeRed(zeroes_ones));
                }
            }

            BuGeRed bgrColor = new BuGeRed(color);
            for (int ixx = 0; ixx < BuGeReds.Count; ixx++)
            {
                BuGeRedBrowns.Add(bgrColor);
                BuGeRedBrowns[ixx] += BuGeReds[ixx];
            }

            int he = BuGeReds.Count / 2;
            int we = BuGeReds.Count / he;

            Bitmap bmpNEW = bgrcoll.GetBitmapFromBuGeRedList(BuGeRedBrowns, we, he);
            return bmpNEW;

        }
    }
}
