using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public class Item
    {
        private int type; // 0 = Treasure Chest, 1 = Heart Container. This is an int in case I decide to add more types later.
        private byte value; // The contents of a treasure chest - Not used for Heart Container

        public Item(String t)
        {
            if (t == "TREASURE")
            {
                type = 0;
                value = 0xEF;
            }
            else if (t == "HEART_CONTAINER")
            {
                type = 1;
                value = 0xEA;
            }
        }

        public Item(byte v)
        {
            type = 0;
            value = v;
        }

        public byte getValue()
        {
            return value;
        }

        public int getType()
        {
            return type;
        }
    }

    public class Chest
    {
        private byte contents;
        private long memAddress;
        private RequirementV1 requirements;
        private byte[] neverContain;

        public Chest(long mem)
        {
            contents = 0xFF;
            memAddress = mem;
        }
        public Chest(long mem, RequirementV1 r) : this(mem)
        {
            requirements = r;
        }
        public Chest(long mem, RequirementV1 r, byte[] never) : this(mem, r)
        {
            neverContain = never;
        }
        public Chest(long mem, byte[] never) : this(mem)
        {
            neverContain = never;
        }

        public byte getContents()
        {
            return contents;
        }
        public void setContents(byte c)
        {
            contents = c;
            return;
        }
        public long getAddress()
        {
            return memAddress;
        }
        public void setAddress(long mem)
        {
            memAddress = mem;
            return;
        }
        public RequirementV1 getRequirements()
        {
            return requirements;
        }
        public void setRequirements(RequirementV1 r)
        {
            requirements = r;
            return;
        }
    }
}
