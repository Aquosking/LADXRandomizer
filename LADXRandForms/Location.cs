using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LADXRandomizer
{
    public delegate void Write();
    class Location
    {
        // 0x50560 is the offset for treasure
        private long map; 
            // First two digits:  0x00__ = OVERWORLD, 0x01__ = UW 1, 0x02__ = UW 2, 0x03__ = COLOR DUNGEON. Color Dungeon will probably go unchanged but eh what the heck.
            // Second two digits: 0x__00 for upper left, 0x__FF for lower right.
            // All maps are named according to LALE and Artemis251's maps.
        private int id;
            // Will be used to indicate which item it is on a single screen - for screens with multiple things. 
            // Most will be 0, however on occasion there may be a heart piece, secret seashell, and/or chest sharing a screen, 
            // Which will require different behavior, as you can’t have two chests with different contents on a screen.
        private Item contents;
            //The item in this spot.
        private Requirement requirements;
            // The requirements that must be fulfilled to access the item in this location
        private Write writeHeartPiece;
        private Write writeChest;
        private List<Item> neverContain;

        /* CONSTRUCTORS */
        public Location(long theMap, int theLocationID, Requirement theRequirement) 
            : this(theMap, theLocationID, theRequirement, delegate () { return; }, null )
        {

        }
        public Location(long theMap, int theLocationID, Requirement theRequirement, List<Item> theNeverContain) 
            : this(theMap, theLocationID, theRequirement, delegate () { return; }, null, theNeverContain)
        {

        }
        public Location(long theMap, int theLocationID, Requirement theRequirement, Write theHeartPieceWriteFunc)
            : this(theMap, theLocationID, theRequirement, delegate () { return; }, theHeartPieceWriteFunc)
        {

        }
        public Location(long theMap, int theLocationID, Requirement theRequirement, Write theChestWriteFunc, Write theHeartPieceWriteFunc) 
            : this(theMap, theLocationID, theRequirement, theChestWriteFunc, theHeartPieceWriteFunc, new List<Item> {})
        {
            
        }
        public Location(long theMap, int theLocationID, Requirement theRequirement, Write theChestWriteFunc, Write theHeartPieceWriteFunc, List<Item> theNeverContain)
        {
            map = theMap;
            id = theLocationID;
            contents = null;
            requirements = theRequirement;
            writeChest = theChestWriteFunc;
            writeHeartPiece = theHeartPieceWriteFunc;
            neverContain = theNeverContain;
        }
        public Location(long theMap, int theLocationID, Requirement theRequirement, Item c)
            : this(theMap, theLocationID, theRequirement, delegate () { return; }, null)
        {
            contents = c;
        }

        /* METHODS */
        public void setContents(Item theItem)
        {
            contents = theItem;
        }
        public Item getContents()
        {
            return contents;
        }

        public long getChestAddress()
        {
            return 0x50560 + map;
        }

        public Requirement getRequirement()
        {
            return requirements;
        }

        public void setRequirement(Requirement theRequirement)
        {
            requirements = theRequirement;
        }

        public void addItemException(Item theItem)
        {
            neverContain.Add(theItem);
        }

        public void write()
        {
            if (contents.getType() == 0)
            {
                // 
                writeChest?.Invoke();
                // Writes the chest data
                long address = getChestAddress();
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(Form1.ofd.FileName));
                bw.BaseStream.Position = address;
                bw.Write(contents.getValue());
                bw.Close();
            }
            else if (contents.getType() == 1)
            {
                writeHeartPiece?.Invoke();
            }
        }
    }
}
