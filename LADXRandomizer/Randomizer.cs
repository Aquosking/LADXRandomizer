using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    using static ItemConstants;

    class Randomizer
    {
        public List<Item> linkInventory;

        public Version ver;

        private int seed;
        private Random rand;
        private List<Location> locs;
        private Dungeon D1;
        private Dungeon D2;
        private Dungeon D3;
        private Dungeon D4;
        private Dungeon D5;
        private Dungeon D6;
        private Dungeon D7;
        private Dungeon D8;
        private Dungeon DC;

        private readonly Item[] startInventory = new Item[] { new Item(SWORD), new Item(SHIELD), new Item(MAGIC_POWDER), new Item(BOMB), new Item(BOW), new Item(SHOVEL) };

        private List<Location> finishedLocs;

        public Randomizer(Version v)
        {
            ver = v;
            linkInventory = new List<Item> (startInventory);
            finishedLocs = new List<Location> { };
            Random rnd = new Random();
            seed = rnd.Next();
            rand = new Random(seed);
        }
        public Randomizer(Version v, int seed_)
        {
            ver = v;
            linkInventory = new List<Item> (startInventory);
            finishedLocs = new List<Location> { };
            seed = seed_;
            rand = new Random(seed);
        }

        /* Rework this function so that the index is generated inside instead of passed as argument */
        public Location distributeItem(List<Location> locs, byte item_)
        {
            bool locIsValid = false;
            int index = rand.Next(locs.Count());
            Item item = new Item(item_);
            List<Location> locTemp = new List<Location>(locs);
            while (!locIsValid && locTemp.Count() > 0)
            { 
                if (locTemp[index].getExceptions().Contains(item))
                {
                    locTemp.RemoveAt(index);
                    index = rand.Next(locTemp.Count());
                }
                else
                {
                    locIsValid = true;
                }
            }
            if (locIsValid)
            {
                locTemp[index].setContents(item);
                locs.Remove(locTemp[index]);
                return locTemp[index];
            }
            else
            {
                Console.Write("COULD NOT FIND VALID LOCATION FOR ITEM " + item_ + " IN FOLLOWING LIST OF LOCATIONS: " +  "\n" + locListToString(locs));
                return null;
            }
        }

        public String locListToString(List<Location> locs)
        {
            String tmp = "";
            foreach (Location loc in locs)
            {
                tmp += loc.getMap() + "\n";
            }
            return tmp;
        }

        private List<Location> randomizeDungeon(Dungeon d)
        {
            List<Location> locTemp = new List<Location> (d.getLocations());
            for (int i = 0; i < d.getKeyCount(); i++)
            {
                finishedLocs.Add(distributeItem(locTemp, SMALL_KEY));
            }
            finishedLocs.Add(distributeItem(locTemp, MAP));
            finishedLocs.Add(distributeItem(locTemp, STONE_BEAK));
            finishedLocs.Add(distributeItem(locTemp, COMPASS));
            finishedLocs.Add(distributeItem(locTemp, NIGHTMARE_KEY));

            return locTemp;
        }

        public void randomize()
        {
            for (int i = 1; i <= 9; i++)
            {
                locs.AddRange(randomizeDungeon(ver.getDungeon(i)));
            }

        }

        
        private void initChestsUnderworld2()
        {

        }
    }
}
