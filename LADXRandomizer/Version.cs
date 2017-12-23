using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    using static ItemConstants;
    using static Comparer;

    class Version
    {
        private String difficulty;
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
        private List<Item> allOtherItems;

        public Requirement CanLeaveWestKoholint;
        public Requirement L2_POWER_BRACELET;
        public Requirement L2_SHIELD;
        public Requirement CanEnterD1;
        public Requirement CanGetD1BossKey;
        public Requirement CanBeatD1;
        public Requirement CanPassOrAvoidRaccoon;
        public Requirement CanGetBowWow;
        public Requirement CanDestroySwampFlower;
        public Requirement CanEnterD2;
        public Requirement CanGetSlimeKey;
        public Requirement CanEnterD3;
        public Requirement CanBeatD3;
        public Requirement CanGetAnglerKey;
        public Requirement CanEnterD4;
        public Requirement CanEnterD5;
        // Try to jump the gap to D6 using PEGASUS BOOTS and ROCS_FEATHER 
        public Requirement CanEnterD6;
        public Requirement CanEnterD7;
        public Requirement CanEnterD8;

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

        public Version(String difficulty)
        {
            switch (difficulty)
            {
                case "CASUAL":
                default:
                    progressionItems = new List<Item> 
                    {
                        new Item(POWER_BRACELET),
                        new Item(TAIL_KEY),
                        new Item(SHIELD),
                        new Item(HOOKSHOT),
                        new Item(MAGIC_ROD),
                        new Item(PEGASUS_BOOTS),
                        new Item(OCARINA),
                        new Item(ROCS_FEATHER),
                        new Item(FLIPPERS),
                        new Item(POWER_BRACELET)
                    };

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

                    CanDestroySwampFlower = delegate (List<Item> i) { return CanGetBowWow(i) || Comparer.has(i, MAGIC_ROD) || Comparer.has(i, HOOKSHOT); };

                    CanEnterD2 = delegate (List<Item> i) { return CanPassOrAvoidRaccoon(i) && CanDestroySwampFlower(i); };

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
                    break;
            }
        }

        private void initLocations()
        {
            switch (difficulty)
            {
                case "CASUAL":
                    //Overworld Chests
                    //1

                    /* Heart Piece in Well */
                    /*
                    locs.Add
                    (
                            new Location
                            (
                                0x02A4,
                                0,
                                delegate(List<Item> i)
                                {
                                    return contains(i, SWORD) || Comparer.contains(i, POWER_BRACELET);
                                }
                            )
                    );
                    */

                    /* Mysterious Woods Blocked by Rock Chest */
                    locs.Add
                    (
                        new Location
                        (
                            0x0071,
                            0,
                            delegate (List<Item> i)
                            {
                                return has(i, POWER_BRACELET);
                            }
                        )
                    );

                    /* Mysterious Woods Cave 02BD */
                    locs.Add
                    (
                        new Location
                        (
                            0x02BD,
                            0,
                            delegate (List<Item> i)
                            {
                                return has(i, SWORD);
                            }
                        )
                    );

                    /* Mysterious Woods Heart Piece Cave 02AB */
                    /*
                    locs.Add
                    (
                        new Location
                        (
                            0x02AB,
                            0,
                            delegate (List<Item> i)
                            {
                                return contains(i, POWER_BRACELET);
                            }
                        )
                    );
                    */

                    /* Overworld Heart Piece Surrounded by Holes */
                    /*
                    locs.Add
                    (
                        new Location
                        (
                            0x0044,
                            0,
                            delegate (List<Item> i)
                            {
                                return contains(i, ROCS_FEATHER) && (contains(i, SWORD) || contains(i, POWER_BRACELET));
                            }
                        )
                    );
                    */

                    /* Tail Key Chest */
                    locs.Add
                    (
                        new Location
                        (
                            0x0041,
                            0,
                            CanPassOrAvoidRaccoon
                        )
                    );
                    
                    /* Chest in Goponga Swamp */
                    locs.Add
                    (
                        new Location
                        (
                            0x0034,
                            0,                          
                            delegate(List<Item> i)
                            {
                                return CanPassOrAvoidRaccoon(i) && CanDestroySwampFlower(i);
                            }
                        )
                    );

                    break;
            }
                    /*
                    r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(SWORD, "AND", POWER_BRACELET), "AND", rc.NewReq(PEGASUS_BOOTS, "AND", FLIPPERS)), "AND", HOOKSHOT));
                    chests.Add(new Chest(0x50564, r));
                    //2
                    r = new RequirementV1(rc.NewReq(SWORD, "AND", POWER_BRACELET));
                    chests.Add(new Chest(0x50578, r));
                    //3
                    r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(SWORD, "AND", POWER_BRACELET), "AND", rc.NewReq(PEGASUS_BOOTS, "AND", FLIPPERS)), "AND", BOMB));
                    chests.Add(new Chest(0x5057D, r));
                    //4
                    r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(SWORD, "AND", rc.NewReq(ROCS_FEATHER, "OR", MAGIC_POWDER)), "OR", POWER_BRACELET), "AND", rc.NewReq(HOOKSHOT, "OR", MAGIC_ROD)));
                    chests.Add(new Chest(0x50594, r));
                    //5
                    r = new RequirementV1(rc.NewReq(rc.NewReq(SWORD, "AND", rc.NewReq(ROCS_FEATHER, "OR", MAGIC_POWDER)), "OR", POWER_BRACELET));
                    chests.Add(new Chest(0x505A1, r));
                    //6
                    r = new RequirementV1(rc.NewReq(POWER_BRACELET, "AND", rc.NewReq(HOOKSHOT, "OR", FLIPPERS)));
                    chests.Add(new Chest(0x505BC, r));
                    //7
                    chests.Add(new Chest(0x505BD, r));
                    //8
                    r = new RequirementV1(rc.NewReq(POWER_BRACELET, "AND", FLIPPERS));
                    chests.Add(new Chest(0x505CC, r));
                    //9
                    r = new RequirementV1(rc.NewReq(POWER_BRACELET));
                    chests.Add(new Chest(0x505D1, r));
                    //10
                    chests.Add(new Chest(0x50645, r));
                    */
            return;
        }

        public void initDungeons()
        {
            switch (difficulty)
            {
                /** Casual Difficulty - The default difficulty **/
                case "CASUAL":
                    D1 = new Dungeon
                        (
                            CanEnterD1,
                            /* Locations of randomizable chests */
                            new List<Location>
                            {
                                new Location
                                (
                                    0x0115,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return has(i, SWORD);
                                    }
                                ),
                                new Location
                                (
                                    0x0113,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return true;
                                    }
                                ),
                                new Location
                                (
                                    0x0114,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return has(i, SWORD);
                                    }
                                ),
                                new Location
                                (
                                    0x010C,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return has(i, BOMB);
                                    }
                                ),
                                new Location
                                (
                                    0x010D,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return has(i, SWORD);
                                    }
                                ),
                                new Location
                                (
                                    0x010E,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return true;
                                    }
                                ),
                                new Location
                                (
                                    0x011D,
                                    0,
                                    delegate (List<Item> i)
                                    {
                                        return hasX(i, SMALL_KEY, 3) && has(i, SHIELD) && has(i, SWORD);
                                    },
                                    new List<Item> { new Item(SMALL_KEY) }
                                ),
                                new Location
                                (
                                    0x0108,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return hasX(i, SMALL_KEY, 3) && has(i, ROCS_FEATHER);
                                    },
                                    new List<Item> { new Item(SMALL_KEY) }
                                ),
                                new Location
                                (
                                    0x010A,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return hasX(i, SMALL_KEY, 3) && has(i, SWORD);
                                    },
                                    new List<Item> { new Item(SMALL_KEY) }
                                )
                            },

                            /* Items whose location can't be randomized, but getting them is important for requirements */
                            new List<Location>
                            {
                                new Location
                                (
                                    0x0116,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return has(i, SWORD) || has(i, SHIELD);
                                    },
                                    new Item(SMALL_KEY)
                                )
                            },
                            2 // Randomizable keys
                        );
                    /* SECOND DUNGEON */
                    D2 = new Dungeon
                    (
                        CanEnterD2,
                        /* Locations of randomizable chests */
                        new List<Location>
                        {
                                // 9 chests
                                new Location
                                (
                                    0x0136,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return has(i, POWER_BRACELET);
                                    }
                                ),
                                new Location
                                (
                                    0x012E,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return hasX(i, SMALL_KEY, 4) && has(i, SWORD) && has(i, ROCS_FEATHER);
                                    }
                                )
                        },
                        /* Items whose location can't be randomized, but getting them is important for requirements */
                        new List<Location>
                        {
                                // 2 keys
                                new Location
                                (
                                    0x0132,
                                    0,
                                    delegate(List<Item> i)
                                    {
                                        return has(i, SWORD) && has(i, MAGIC_POWDER);
                                    },
                                    new Item(SMALL_KEY)
                                )

                            },
                            3 // Randomizable keys
                        );
                    break;
            }
        }

        public List<Location> getLocations() {
            return locs;
        }

        public List<Item> getProgressionItems() 
        {
            return progressionItems;
        }

        public Location getLocation(Location[] locs, long mem)
        {
            for(int i = 0; i < locs.Length; i++)
            {
                if (locs[i].getChestAddress() == mem) return locs[i];
            }
            return null;
        }
        
        public Dungeon getDungeon(int dNumber)
        {
            switch (dNumber)
            {
                case 1:
                    return D1;
                case 2:
                    return D2;
                case 3:
                    return D3;
                case 4:
                    return D4;
                case 5:
                    return D5;
                case 6:
                    return D6;
                case 7:
                    return D7;
                case 8:
                    return D8;
                case 9:
                    return DC;
                default:
                    return null;
            }
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
