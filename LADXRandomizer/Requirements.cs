using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    delegate bool Requirement(List <Item> items);

    class Comparer
    {
        public static bool compare(Item item, byte i)
        {
            return item.getValue() == i;
        }
        public static bool has(List <Item> items, byte i)
        {
            foreach(Item item in items)
            {
                if (compare(item, i))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool has2(List<Item> items, byte i)
        {
            int count = 0;
            foreach(Item item in items)
            {
                if (compare(item, i))
                {
                    count++;
                }
            }
            if (count == 2) return true;
            else return false;
        }
        public static bool hasX(List<Item> items, byte i, int x)
        {
            int count = 0;
            foreach(Item item in items)
            {
                if (compare(item, i))
                {
                    count++;
                }
            }
            if (count >= x) return true;
            else return false;
        }
    }

    public interface ReqOperator
    {
        bool evaluate(byte[] items);
    }

    public class Req : ReqOperator
    {
        private byte item;

        public Req(byte i)
        {
            item = i;
        }

        public bool evaluate(byte[] items)
        {
            /* TO DO: Implement the simple search for item in items */
            return false;
        }
    }

    public class ReqOR : ReqOperator
    {
        private ReqOperator item1;
        private ReqOperator item2;

        public ReqOR(ReqOperator i, ReqOperator j)
        {
            item1 = i;
            item2 = j;
        }

        public bool evaluate(byte[] items)
        {
            return item1.evaluate(items) || item2.evaluate(items);
        }
    }

    public class ReqAND : ReqOperator
    {
        private ReqOperator item1;
        private ReqOperator item2;

        public ReqAND(ReqOperator i, ReqOperator j)
        {
            item1 = i;
            item2 = j;
        }

        public bool evaluate(byte[] items)
        {
            return item1.evaluate(items) && item2.evaluate(items);
        }
    }

    // This class is responsible for creating all 
    public class ReqCreator
    {
        // Using a string, this method makes and/or requirements much more intuitively
        public ReqOperator NewReq(ReqOperator i, String o, ReqOperator j)
        {
            if (o == "AND") return new ReqAND(i, j);
            else if (o == "OR") return new ReqOR(i, j);
            else return null;
        }
        // These functions allow for different combinations of inputs
        public ReqOperator NewReq(byte i, String o, byte j)
        {
            return NewReq(new Req(i), o, new Req(j));
        }
        public ReqOperator NewReq(ReqOperator i, String o, byte j)
        {
            return NewReq(i, o, new Req(j));
        }
        public ReqOperator NewReq(byte i, String o, ReqOperator j)
        {
            return NewReq(new Req(i), o, j);
        }
        // This method allows the same function to create the Req objects, which contain the items.
        public ReqOperator NewReq(byte i)
        {
            return new Req(i);
        }
    }

    public class RequirementV1
    {
        private ReqOperator items;

        public RequirementV1(ReqOperator i)
        {
            items = i;
        }
    }
}
