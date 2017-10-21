using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    using static ItemConstants;

    public class Randomizer
    {
        public static List<Item> linkInventory;

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

        public Randomizer()
        {
            initLocations();
            linkInventory = new List<Item> { new Item(SWORD), new Item(SHIELD), new Item(MAGIC_POWDER), new Item(BOMB), new Item(BOW), new Item(SHOVEL) };

        }
        private void initLocations()
        {
            Location test = new Location(0x0000, 0, null, delegate () { return; }, delegate () { return; });
            locs = new List<Location> { };
            ReqCreator rc = new ReqCreator();
            RequirementV1 r = new RequirementV1(null);
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
                            return Comparer.contains(i, SWORD) || Comparer.contains(i, POWER_BRACELET);
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
                        return Comparer.has(i, POWER_BRACELET);
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
                        return Comparer.has(i, SWORD);
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
                        return Comparer.contains(i, POWER_BRACELET);
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
                        return Comparer.contains(i, ROCS_FEATHER) && (Comparer.contains(i, SWORD) || Comparer.contains(i, POWER_BRACELET));
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
                    ReqInstances.CanPassOrAvoidRaccoon
                )
            );
            /*
            locs.Add
            (
                new Location
                (

                )
            );
            */
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
        private void initDungeons()
        {
            /* FIRST DUNGEON */
            D1 = new Dungeon
            (
                ReqInstances.CanEnterD1,
                /* Locations of randomizable chests */
                new List<Location>
                {
                    new Location
                    (
                        0x0115,
                        0,
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, SWORD);
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
                            return Comparer.has(i, SWORD);
                        }
                    ),
                    new Location
                    (
                        0x010C,
                        0,
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, BOMB);
                        }
                    ),
                    new Location
                    (
                        0x010D,
                        0,
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, SWORD);
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
                            return Comparer.hasX(i, SMALL_KEY, 3) && Comparer.has(i, SHIELD) && Comparer.has(i, SWORD);
                        },
                        new List<Item> { new Item(SMALL_KEY) }
                    ),
                    new Location
                    (
                        0x0108,
                        0,
                        delegate(List<Item> i)
                        {
                            return Comparer.hasX(i, SMALL_KEY, 3) && Comparer.has(i, ROCS_FEATHER);
                        },
                        new List<Item> { new Item(SMALL_KEY) }
                    ),
                    new Location
                    (
                        0x010A,
                        0,
                        delegate(List<Item> i)
                        {
                            return Comparer.hasX(i, SMALL_KEY, 3) && Comparer.has(i, SWORD);
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
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, SWORD) || Comparer.has(i, SHIELD);
                        },
                        new Item(SMALL_KEY)
                    )
                },
                2 // Randomizable keys
            );

            /* SECOND DUNGEON */
            D2 = new Dungeon
            (
                ReqInstances.CanEnterD2,
                /* Locations of randomizable chests */
                new List<Location>
                {
                    // 9 chests
                },
                /* Items whose location can't be randomized, but getting them is important for requirements */
                new List<Location>
                {
                    // 2 keys
                },
                3 // Randomizable keys
            );
        }
        private void initChestsUnderworld2()
        {

        }
        private void randomize()
        {
            List<Location> D1Remain = D1.randomize();
            List<Location> D2Remain = D2.randomize();
            List<Location> D3Remain = D3.randomize();
            List<Location> D4Remain = D4.randomize();

        }
    }
}
