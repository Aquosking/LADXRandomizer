using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    delegate void KeyDistribution();

    class Dungeon
    {
        private Requirement entryReq;
        private Zone zoneTree;
        private KeyDistribution keyInstructions;
        private List <Location> locations;

        // REMOVE THESE TWO PROPERTIES
        private int keyCount;
        private List<Location> staticItems;

        public Dungeon(Requirement er_, Zone zoneTree_, KeyDistribution keyInstructions_)
        {
            entryReq = er_;
            zoneTree = zoneTree_;
            keyInstructions = keyInstructions_;
            locations = zoneTree.getAllLocations();
            foreach (Location l in locations)
            {
                l.addItemException(new Item(ItemConstants.TAIL_KEY));
            }
        }
        
        // REMOVE THIS CONSTRUCTOR
        public Dungeon(Requirement er, List <Location> locs, List <Location> si, int k)
        {
            entryReq = er;
            locations = locs;
            keyCount = k;
            staticItems = si;
            foreach(Location l in locs)
            {
                l.addItemException(new Item(ItemConstants.TAIL_KEY));
                l.setRequirement(delegate (List<Item> i) { return l.getRequirement()(i) && entryReq(i); });
            }
        }


        public Requirement getEntryReq()
        {
            return entryReq;
        }

        public void appendEntryRequirements()
        {
            foreach(Location l in locations)
            {
                l.setRequirement(delegate (List<Item> i) { return l.getRequirement()(i) && entryReq(i); });
            }
        }

        public bool canEnter(List<Item> inventory)
        {
            return entryReq(inventory);
        }

        public List<Location> getLocations()
        {
            return locations;
        }

        // REMOVE THIS FUNCTION
        public int getKeyCount()
        {
            return keyCount;
        }

        // REVIEW THIS FUNCTION
        //Sets the contents of the location at index 'index' to 'item', removes the location from locTmp, and then returns the location.
        public Location setLocationContents(List <Location> locTmp, int index, byte item)
        {
            locTmp[index].setContents(new Item(item));
            Location tmp = locTmp[index];
            locTmp.RemoveAt(index);
            return tmp;
        }

        // FIX THIS FUNCTION TO WORK FOR THE ZONE STUFF
        //Randomly places the dungeon items (in valid locations) and returns all unfilled chests
        //IMPORTANT!!! FIND OUT IF C# PASSES BY REFERENCE
        public List <Location> randomize()
        {
            keyInstructions();


            List <Location> locTemp = new List <Location> { };
            foreach(Location l in locations)
            {
                locTemp.Add(l);
            }
            List <Location> locFinished = new List <Location> { };
            Random rnd = new Random();
            int r = rnd.Next(locTemp.Count());
            for (int i = 0; i < keyCount; i++)
            {
                locFinished.Add(setLocationContents(locTemp, r, 0x1A));
            }
            r = rnd.Next(locTemp.Count());
            locFinished.Add(setLocationContents(locTemp, r, 0x16));
            r = rnd.Next(locTemp.Count());
            locFinished.Add(setLocationContents(locTemp, r, 0x17));
            r = rnd.Next(locTemp.Count());
            locFinished.Add(setLocationContents(locTemp, r, 0x18));
            r = rnd.Next(locTemp.Count());
            locFinished.Add(setLocationContents(locTemp, r, 0x19));
            
            return locTemp;
        }
    }
}
