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
        private List<Location> locPool;
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

        private List<Item> progressionItems;

        private readonly Item[] startInventory = new Item[] { new Item(SWORD), new Item(SHIELD), new Item(MAGIC_POWDER), new Item(BOMB), new Item(BOW), new Item(SHOVEL) };

        private List<Location> finishedLocs;

        public Randomizer(Version v)
        {
            ver = v;
            linkInventory = new List<Item> (startInventory);
            finishedLocs = new List<Location> ();
            Random rnd = new Random(); 
            seed = rnd.Next();
            rand = new Random(seed);
            locPool = ver.getLocations();
            progressionItems = ver.getProgressionItems;
        }
        public Randomizer(Version v, int seed_)
        {
            ver = v;
            linkInventory = new List<Item> (startInventory);
            finishedLocs = new List<Location> { };
            seed = seed_;
            rand = new Random(seed);
            locPool = ver.getLocations();
            progressionItems = ver.getProgressionItems;
        }

        // Distributes an item to a location in locs, removes the location from locPool, adds the item to linkInventory, and then refreshes locs
        public void distributeItem(Item item)
        {
            bool locIsValid = false;
            int index = rand.Next(locs.Count());
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
                // set the location's contents
                locTemp[index].setContents(item);
                // remove the location from locPool
                locPool.Remove(locTemp[index]);
                // adds the item to linkInventory
                linkInventory.Add(item);
                // refreshes locs
                    /* Add some function that refreshes updates locs with possible locations in locPool using linkInventory */
                // adds the location to the finished locations list
                finishedLocs.Add(locTemp[index]);
            }
            else
            {
                Console.Write("COULD NOT FIND VALID LOCATION FOR ITEM " + item_ + " IN FOLLOWING LIST OF LOCATIONS: " +  "\n" + createLocListDump(locs));
                return;
            }
        }

        public void distributeItem(byte item_) 
        {
            distributeItem(new Item(item_));
        }
        
        // Distribute items into a particular list of locations
        public void distributeItem(List<Location> locList, byte item_)
        {
            bool locIsValid = false;
            int index = rand.Next(locList.Count());
            Item item = new Item(item_);
            List<Location> locTemp = new List<Location>(locList);
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
                // set the location's contents
                locTemp[index].setContents(item);
                // remove the location from locList
                locList.Remove(locTemp[index]);
                // adds the location to the finished locations list
                finishedLocs.Add(locTemp[index]);
            }
            else
            {
                Console.Write("COULD NOT FIND VALID LOCATION FOR ITEM " + item_ + " IN FOLLOWING LIST OF LOCATIONS: " +  "\n" + createLocListDump(locList));
                return null;
            }
        }

        public String createLocListDump(List<Location> locList)
        {
            String tmp = "";
            foreach (Location loc in locList)
            {
                tmp += loc.getMap() + "\n";
            }
            return tmp;
        }

        /* Distributes the dungeon items into locations from dungeon d.
         * Adds the locations where items were placed to finishedLocs.
         * 
         * @returns - a list of locations in the dungeon that remain to be filled.
         */
        private void randomizeDungeon(Dungeon d)
        {
            List<Location> locTemp = new List<Location> (d.getLocations());
            for (int i = 0; i < d.getKeyCount(); i++)
            {
                finishedLocs.Add(distributeItem(locTemp, SMALL_KEY));
            }
            finishedLocs.Add(distributeItem(locTemp, NIGHTMARE_KEY));
            finishedLocs.Add(distributeItem(locTemp, MAP));
            finishedLocs.Add(distributeItem(locTemp, STONE_BEAK));
            finishedLocs.Add(distributeItem(locTemp, COMPASS));

            locPool.AddRange(locTemp);
        }

        private void refreshLocations() 
        {
            List<Location> toRemove = new List<Location>();
            foreach (Location loc in locPool) 
            {
                if(loc.getRequirement()(linkInventory)) 
                {
                    locs.Add(loc);
                    toRemove.Add(loc);
                }
            }
            foreach (Location loc in toRemove) 
            {
                locPool.Remove(loc);
            }
            
        }

        public void randomize()
        {
            for (int i = 1; i <= 9; i++)
            {
                randomizeDungeon(ver.getDungeon(i));
            }
            int itemIndex = 0;
            while(progressionItems.Count > 0) 
            {
                refreshLocations();
                distributeItem(progressionItems[itemIndex]);
            }

        }

        
        private void initChestsUnderworld2()
        {

        }
    }
}
