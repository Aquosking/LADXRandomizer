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
}
