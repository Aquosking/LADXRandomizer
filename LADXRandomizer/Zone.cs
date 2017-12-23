using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    /*  Groups of locations. Zones in "next" require a key to reach from this zone. 
        
    */
    class Zone
    {
        private int zoneID;
        private int staticKeys;
        private List<Zone> next;
        private List<Location> containedLocations;

        public Zone(int id_, int staticKeys_, List<Zone> next_, List<Location> locs_)
        {
            zoneID = id_;
            staticKeys = staticKeys_;
            next = next_;
            containedLocations = locs_;
        }
        public Zone(int id_, List<Zone> next_, List<Location> locs_)
            : this(id_, 0, next_, locs_)
        {

        }
        public Zone(int id_, List<Location> locs_)
            : this(id_, 0, new List<Zone> { }, locs_)
        {

        }

        public List<Zone> getNextZones()
        {
            return next;
        }

        public Zone getZone(int zoneID_)
        {
            if (zoneID_ == zoneID)
            {
                return this;
            }
            else if (next == null || next.Count <= 0)
            {
                return null;
            }
            else
            {
                Zone found = null;
                foreach(Zone z in next)
                {
                    found = z.getZone(zoneID_);
                    if (found != null) break;
                }
                return found;
            }
        }

        public List<Location> getLocations()
        {
            return containedLocations;
        }

        public List<Location> getAllLocations()
        {
            List<Location> temp = new List<Location>();
            foreach (Location l in containedLocations)
            {
                temp.Add(l);
            }
            if (next == null || next.Count <= 0)
            {
                foreach(Zone z in next)
                {
                    temp.AddRange(z.getAllLocations());
                }
            }
            return temp;
        }
    }
}
