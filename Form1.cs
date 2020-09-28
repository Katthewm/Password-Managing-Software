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

namespace Password_Management_Software
{
    public partial class Form1 : Form
    {
        int passwords = -1;
        List<RadioButton> radioButtons = new List<RadioButton>();
        int clickedbutton = 0;

        public Form1()
        {
            InitializeComponent();
            load_file();
        }
        
        private void button1_Click(object sender, EventArgs e)//sets the selected buttons password to be what is in textbox 2
        {
            if(clickedbutton<0)
            {
                return;
            }
            else
            {
                radioButtons[clickedbutton].Text = textBox2.Text;
            }
        }
        private void create_new_password()//creates new password buttons on the screen
        {
            RadioButton radioButton = new RadioButton();
            radioButton.AutoSize = true;
            radioButton.Location = new System.Drawing.Point(800, (passwords*20));
            radioButton.Name = ""+passwords;
            radioButton.Size = new System.Drawing.Size(94, 19);
            radioButton.TabIndex = 1;
            radioButton.TabStop = true;
            radioButton.Text = "New Password"+passwords;
            radioButton.UseVisualStyleBackColor = true;
            radioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            passwords = passwords + 1;
            Controls.Add(radioButton);
            radioButtons.Add(radioButton);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)//puts the passwords text into the textbox
        {
            RadioButton radioButton = (RadioButton)sender;
            textBox1.Text = radioButton.Text;
            clickedbutton = Int32.Parse(radioButton.Name);
        }
        private void create_new_password_from_file(string text)//creates new password buttons on the screen
        {
            if(passwords<0)
            {
                passwords = 0;
            }
            RadioButton radioButton = new RadioButton();
            radioButton.AutoSize = true;
            radioButton.Location = new System.Drawing.Point(800, (passwords * 20));
            radioButton.Name = "" + passwords;
            radioButton.Size = new System.Drawing.Size(94, 19);
            radioButton.TabIndex = 1;
            radioButton.TabStop = true;
            radioButton.Text = text;
            radioButton.UseVisualStyleBackColor = true;
            radioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            passwords = passwords + 1;
            Controls.Add(radioButton);
            radioButtons.Add(radioButton);
        }
        private void button5_Click(object sender, EventArgs e) //creates new password buttons
        {
            create_new_password();
        }

        private void button7_Click(object sender, EventArgs e)//save passwords to the file
        {
            string filename = "passwords.txt";
            StreamWriter outputfile = new StreamWriter(filename);
            for(int i=0; i< radioButtons.Count; i++)
            {
                outputfile.WriteLine(radioButtons[i].Text);
            }
            outputfile.Close();
        }
        private void load_file()
        {
            string filename = "passwords.txt";
            if (!File.Exists(filename))
            {
                MessageBox.Show("File does not exist");
                Console.WriteLine("File does not exist or empty");
                return;
            }
            else
            {
                StreamReader inputFile = new StreamReader(filename);
                
                if (inputFile != null)
                {
                    String line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        //string[] values = line.Split(',');//splits the data into its seperate values  
                        create_new_password_from_file(line);
                        //Stock stock = new Stock(values[0], double.Parse(values[1]), double.Parse(values[2]), double.Parse(values[3]), double.Parse(values[4]), double.Parse(values[5]), Int32.Parse(values[6]));//creates a new stock object with the data from the line
                        //stocklist.Add(stock);//adds new stock object to arraylist
                    }
                }

                inputFile.Close();//close file
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
