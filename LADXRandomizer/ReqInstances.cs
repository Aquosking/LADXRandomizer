using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    using static ItemConstants;

    class ReqInstances
    {
        public static Requirement CanLeaveWestKoholint;
        public static Requirement L2_POWER_BRACELET;
        public static Requirement L2_SHIELD;
        public static Requirement CanEnterD1;
        public static Requirement CanGetD1BossKey;
        public static Requirement CanBeatD1;
        public static Requirement CanPassOrAvoidRaccoon;
        public static Requirement CanGetBowWow;
        public static Requirement CanEnterD2;
        public static Requirement CanGetSlimeKey;
        public static Requirement CanEnterD3;
        public static Requirement CanBeatD3;
        public static Requirement CanGetAnglerKey;
        public static Requirement CanEnterD4;
        public static Requirement CanEnterD5;
        // Try to jump the gap to D6 using PEGASUS BOOTS and ROCS_FEATHER 
        public static Requirement CanEnterD6;
        public static Requirement CanEnterD7;
        public static Requirement CanEnterD8;

        //private static ReqCreator rc = new ReqCreator();
        //public static ReqOperator CanLeaveWestKoholint = rc.NewReq(ItemConstants.POWER_BRACELET);
        //public static ReqOperator L2_POWER_BRACELET = rc.NewReq(ItemConstants.POWER_BRACELET, "AND", ItemConstants.POWER_BRACELET);
        //public static ReqOperator CanEnterD1 = rc.NewReq(ItemConstants.TAIL_KEY);
        //public static ReqOperator CanGetD1BossKey = null;
        //public static ReqOperator CanBeatD1 = rc.NewReq(CanEnterD1, "AND", rc.NewReq(CanGetD1BossKey, "AND", ItemConstants.ROCS_FEATHER));
        //public static ReqOperator CanPassOrAvoidRaccoon = rc.NewReq(ItemConstants.MAGIC_POWDER, "OR", rc.NewReq(ItemConstants.ROCS_FEATHER, "OR", ItemConstants.POWER_BRACELET));
        //public static ReqOperator CanGetBowWow = CanBeatD1;
        //public static ReqOperator CanEnterD2 = rc.NewReq(CanGetBowWow, "OR", rc.NewReq(ItemConstants.MAGIC_ROD, "OR", ItemConstants.HOOKSHOT));
        //public static ReqOperator CanGetSlimeKey = null;
        //public static ReqOperator CanEnterD3 = rc.NewReq(rc.NewReq(CanLeaveWestKoholint, "AND", CanGetSlimeKey), "AND", rc.NewReq(ItemConstants.FLIPPERS, "OR", ItemConstants.ROCS_FEATHER));
        //public static ReqOperator CanBeatD3 = rc.NewReq(CanEnterD3, "AND", ItemConstants.PEGASUS_BOOTS);
        //public static ReqOperator CanGetAnglerKey = CanBeatD3;
        //public static ReqOperator CanEnterD4 = CanGetAnglerKey;
        //public static ReqOperator CanEnterD5 = rc.NewReq(CanLeaveWestKoholint, "AND", ItemConstants.FLIPPERS);

        //public static ReqOperator CanEnterD6 = rc.NewReq(CanLeaveWestKoholint, "AND", rc.NewReq(ItemConstants.HOOKSHOT, "AND", ItemConstants.FLIPPERS));

        public ReqInstances(String difficulty)
        {
            if (difficulty.Equals("CASUAL"))
            {
                CanLeaveWestKoholint = delegate (List<Item> i) { return Comparer.has(i, POWER_BRACELET); };

                L2_POWER_BRACELET = delegate (List<Item> i) { return Comparer.has2(i, POWER_BRACELET); };

                L2_SHIELD = delegate (List<Item> i) { return Comparer.has2(i, POWER_BRACELET); };

                CanEnterD1 = delegate (List<Item> i) { return Comparer.has(i, TAIL_KEY); };

                CanBeatD1 = delegate (List<Item> i) 
                {
                    /* Pass Dungeon 1's item locations or a list of every item location */
                    Location[] D1Locations = null;
                    Location D1BombRoom = getLocation(D1Locations, 0x5066C);
                    if (Comparer.compare(D1BombRoom.getContents(), NIGHTMARE_KEY))
                    {
                        return CanEnterD1(i) && Comparer.has(i, BOMB) && Comparer.has(i, ROCS_FEATHER);
                    }
                    else return CanEnterD1(i) && Comparer.has(i, ROCS_FEATHER);
                };

                CanPassOrAvoidRaccoon = delegate (List<Item> i) { return (Comparer.has(i, SWORD) && (Comparer.has(i, MAGIC_POWDER) || Comparer.has(i, ROCS_FEATHER))) || Comparer.has(i, POWER_BRACELET); };

                CanGetBowWow = delegate (List<Item> i) { return CanBeatD1(i); };

                CanEnterD2 = delegate (List<Item> i) { return CanGetBowWow(i) || Comparer.has(i, MAGIC_ROD) || Comparer.has(i, HOOKSHOT); };

                CanGetSlimeKey = delegate (List<Item> i) { return false; };

                CanEnterD3 = delegate (List<Item> i) { return CanLeaveWestKoholint(i) && CanGetSlimeKey(i) && (Comparer.has(i, FLIPPERS) || Comparer.has(i, ROCS_FEATHER)); };

                CanBeatD3 = delegate (List<Item> i) { return CanEnterD3(i) && Comparer.has(i, PEGASUS_BOOTS); };

                CanGetAnglerKey = delegate (List<Item> i) { return CanBeatD3(i); };

                CanEnterD4 = delegate (List<Item> i) { return CanGetAnglerKey(i); };

                CanEnterD5 = delegate (List<Item> i) { return CanLeaveWestKoholint(i) && Comparer.has(i, FLIPPERS); };
                
                // Try to jump the gap to D6 using PEGASUS BOOTS and ROCS_FEATHER 
                CanEnterD6 = delegate (List<Item> i) { return CanLeaveWestKoholint(i) && Comparer.has(i, FLIPPERS) && Comparer.has(i, HOOKSHOT); };

                CanEnterD7 = delegate (List<Item> i)
                {
                    return
                        L2_POWER_BRACELET(i) &&
                        Comparer.has(i, PEGASUS_BOOTS) &&
                        Comparer.has(i, ROCS_FEATHER) &&
                        Comparer.has(i, HOOKSHOT) &&
                        Comparer.has(i, OCARINA);
                };

                CanEnterD8 = delegate (List<Item> i)
                {
                    return
                        L2_SHIELD(i) &&
                        Comparer.has(i, OCARINA) &&
                        Comparer.has(i, POWER_BRACELET);
                };
            }
        }
        
        public Location getLocation(Location[] locs, long mem)
        {
            for(int i = 0; i < locs.Length; i++)
            {
                if (locs[i].getChestAddress() == mem) return locs[i];
            }
            return null;
        }
        /*
        public void CheckD1BossKey(Chest[] D1Chests) // Only call after distributing dungeon items for dungeon 1
        {
            Chest bombChest = getChest(D1Chests, 0x5066C);
            Chest featherChest = getChest(D1Chests, 0x50668);
            if (featherChest.getContents() == ItemConstants.NIGHTMARE_KEY) CanGetD1BossKey = rc.NewReq(CanEnterD1, "AND", ItemConstants.ROCS_FEATHER);
            else if (bombChest.getContents() == ItemConstants.NIGHTMARE_KEY) CanGetD1BossKey = rc.NewReq(CanEnterD1, "AND", ItemConstants.BOMB);
            return;
        }
        */
        public void CheckD3BossKey(Chest[] D3Chests)
        {

        }
    }
}
