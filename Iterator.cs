using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixdata
{
    public abstract class Iterator
    {
        public abstract BuGeRed First();
        public abstract BuGeRed Next();
        public abstract bool IsDone();
        public abstract BuGeRed CurrentItem();
    }



    // Aggregate
    public interface IBuGeRedCollection
    {
        Iterator CreateIterator();
    }

    // ConcreteIterator
    public class BGRIterator : Iterator
    {
        private BuGeRedCollection _bgrs;
        private int _current = 0;
        public BGRIterator(BuGeRedCollection bgrs)
        {
            _bgrs = bgrs;
        }
        public override BuGeRed First()
        {
            return _bgrs[0];
        }
        public override BuGeRed Next()
        {
            _current++;
            if (IsDone())
                return null;
            else
                return _bgrs[_current];
        }
        public override bool IsDone()
        {
            return _current >= _bgrs.Count;
        }
        public override BuGeRed CurrentItem()
        {
            return _bgrs[_current];
        }
    }

}
