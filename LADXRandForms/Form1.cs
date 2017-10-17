using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LADXRandomizer
{
    public partial class Form1 : Form
    {
        public static OpenFileDialog ofd;
        public Form1()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName));
                bw.BaseStream.Position = 0x5081D;
                bw.Write(0x07);
                bw.Close();
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            ofd.Filter = "GBC File(*.gbc)|*.gbc"; //Makes a filter for the open file dialog. You don't want the user to open .NDS files, aren't ya?
            ofd.ShowDialog();//Shows the open file dialog
            string gameCodeTest = string.Empty;//Makes a new string that will be used to the gamecode.
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                using (BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName))) //Sets a new integer to the BinaryReader
                {

                    br.BaseStream.Seek(0x50550, SeekOrigin.Begin); //The seek is starting from 0xAC
                    gameCodeTest = BitConverter.ToString(br.ReadBytes(10)); //Reads 10 bytes from 0x50550 and converts it to a string of hex values
                    br.Close(); //Closes the BinaryReader. Without it, opening the file with any other command will result the error "This file is being used by another process".
                }
                if (gameCodeTest == "09-0A-0B-0C-25-2D-2E-28-0D-0E") // Checks if the game code matches this hex string.
                {
                    labelLoadedROM.Text = "Loaded Game:" + ofd.FileName; //If it matches the usual data of the ROM, it changes the label's text.
                    btnWriteTest.Enabled = true; //Enables btnWriteTest, if the gamecode indicates the correct ROM is loaded.
                }
                else //If the gamecode doesn't match that of a normal LADX rom.
                {
                    labelLoadedROM.Text = "Loaded Game: ???"; //Changes label1's text.
                    btnWriteTest.Enabled = false; //Disables btnWriteTest, if the gamecode isn't valid.
                    MessageBox.Show("The loaded game isn't supported\nIf you believe you are using a valid rom, contact me for help.\n(ADD CONTACT INFO)"); //Creates a message box with the text "The loaded game isn't supported", since this program only supports FireRed (BPRE).
                }
            }
        }
    }

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
            linkInventory = new List<Item> { new Item(ItemConstants.SWORD), new Item(ItemConstants.SHIELD), new Item(ItemConstants.MAGIC_POWDER), new Item(ItemConstants.BOMB), new Item(ItemConstants.BOW), new Item(ItemConstants.SHOVEL)};

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
                            return Comparer.contains(i, ItemConstants.SWORD) || Comparer.contains(i, ItemConstants.POWER_BRACELET);
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
                    delegate(List<Item> i)
                    {
                        return Comparer.has(i, ItemConstants.POWER_BRACELET);
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
                    delegate(List<Item> i)
                    {
                        return Comparer.has(i, ItemConstants.SWORD);
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
                        return Comparer.contains(i, ItemConstants.POWER_BRACELET);
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
                        return Comparer.contains(i, ItemConstants.ROCS_FEATHER) && (Comparer.contains(i, ItemConstants.SWORD) || Comparer.contains(i, ItemConstants.POWER_BRACELET));
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

            locs.Add
            (
                new Location
                (

                )
            );

            /*
            r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(ItemConstants.SWORD, "AND", ItemConstants.POWER_BRACELET), "AND", rc.NewReq(ItemConstants.PEGASUS_BOOTS, "AND", ItemConstants.FLIPPERS)), "AND", ItemConstants.HOOKSHOT));
            chests.Add(new Chest(0x50564, r));
            //2
            r = new RequirementV1(rc.NewReq(ItemConstants.SWORD, "AND", ItemConstants.POWER_BRACELET));
            chests.Add(new Chest(0x50578, r));
            //3
            r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(ItemConstants.SWORD, "AND", ItemConstants.POWER_BRACELET), "AND", rc.NewReq(ItemConstants.PEGASUS_BOOTS, "AND", ItemConstants.FLIPPERS)), "AND", ItemConstants.BOMB));
            chests.Add(new Chest(0x5057D, r));
            //4
            r = new RequirementV1(rc.NewReq(rc.NewReq(rc.NewReq(ItemConstants.SWORD, "AND", rc.NewReq(ItemConstants.ROCS_FEATHER, "OR", ItemConstants.MAGIC_POWDER)), "OR", ItemConstants.POWER_BRACELET), "AND", rc.NewReq(ItemConstants.HOOKSHOT, "OR", ItemConstants.MAGIC_ROD)));
            chests.Add(new Chest(0x50594, r));
            //5
            r = new RequirementV1(rc.NewReq(rc.NewReq(ItemConstants.SWORD, "AND", rc.NewReq(ItemConstants.ROCS_FEATHER, "OR", ItemConstants.MAGIC_POWDER)), "OR", ItemConstants.POWER_BRACELET));
            chests.Add(new Chest(0x505A1, r));
            //6
            r = new RequirementV1(rc.NewReq(ItemConstants.POWER_BRACELET, "AND", rc.NewReq(ItemConstants.HOOKSHOT, "OR", ItemConstants.FLIPPERS)));
            chests.Add(new Chest(0x505BC, r));
            //7
            chests.Add(new Chest(0x505BD, r));
            //8
            r = new RequirementV1(rc.NewReq(ItemConstants.POWER_BRACELET, "AND", ItemConstants.FLIPPERS));
            chests.Add(new Chest(0x505CC, r));
            //9
            r = new RequirementV1(rc.NewReq(ItemConstants.POWER_BRACELET));
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
                            return Comparer.has(i, ItemConstants.SWORD);
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
                            return Comparer.has(i, ItemConstants.SWORD);
                        }
                    ),
                    new Location
                    (
                        0x010C,
                        0,
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, ItemConstants.BOMB);
                        }
                    ),
                    new Location
                    (
                        0x010D,
                        0,
                        delegate (List<Item> i)
                        {
                            return Comparer.has(i, ItemConstants.SWORD);
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
                            return Comparer.hasX(i, ItemConstants.SMALL_KEY, 3) && Comparer.has(i, ItemConstants.SHIELD) && Comparer.has(i, ItemConstants.SWORD);
                        },
                        new List<Item> { new Item(ItemConstants.SMALL_KEY) }
                    ),
                    new Location 
                    (
                        0x0108,
                        0,
                        delegate(List<Item> i)
                        {
                            return Comparer.hasX(i, ItemConstants.SMALL_KEY, 3) && Comparer.has(i, ItemConstants.ROCS_FEATHER);
                        },
                        new List<Item> { new Item(ItemConstants.SMALL_KEY) }
                    ),
                    new Location
                    (
                        0x010A,
                        0,
                        delegate(List<Item> i)
                        {
                            return Comparer.hasX(i, ItemConstants.SMALL_KEY, 3) && Comparer.has(i, ItemConstants.SWORD);
                        },
                        new List<Item> { new Item(ItemConstants.SMALL_KEY) }
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
                            return Comparer.has(i, ItemConstants.SWORD) || Comparer.has(i, ItemConstants.SHIELD);
                        },
                        new Item(ItemConstants.SMALL_KEY)
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
    }
}
