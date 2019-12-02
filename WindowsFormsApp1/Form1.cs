using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void insturctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select All");
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;//instantiating null value for stra
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//creating a instance of a dialog box
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; //creting text form
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)//if choose data is fine then enter todialog box
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null) //if the choosen file is not empty then the file is read
                    {
                        using (myStream) //dispose is done automatically
                        {
                            MultipleLines.Text = File.ReadAllText(openFileDialog1.FileName);//loading text file in multiple Line
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private async void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();//creating a instance of a dialog box
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; //creting text form
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)//if choose data is fine then enter todialog box
            {
                try
                {

                    using (StreamWriter write = new StreamWriter(sfd.FileName)) //dispose is done automatically
                    {
                        await write.WriteLineAsync(MultipleLines.Text);//loading text file in multipleLine
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
