﻿using System;
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
        bool number = false;
        bool uppercase = false;

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
            radioButton.Location = new System.Drawing.Point(19, (passwords * 20));
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
            panel1.Controls.Add(radioButton);

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
            radioButton.Location = new System.Drawing.Point(17, (passwords * 20));
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
            panel1.Controls.Add(radioButton);
        }

        private void button5_Click(object sender, EventArgs e) //creates new password buttons
        {
            create_new_password();
        }

        private void button7_Click(object sender, EventArgs e)//save passwords to the file
        {
            save();
        }

        private void save()
        {
            string filename = "passwords.txt";
            StreamWriter outputfile = new StreamWriter(filename);
            for (int i = 0; i < radioButtons.Count; i++)
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

                    }
                }

                inputFile.Close();//close file
            }
        }

        private void button2_Click(object sender, EventArgs e)//password generator
        {
            Random rnd = new Random();
            textBox3.Text = "";
            string lowerletters = "abcdefghijklmnopqrstuvwxyz";
            string upperletters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (textBox4.Text=="")
            {
                return;
            }
            for (int i=0; i<Int32.Parse(textBox4.Text);i++)
            {
                int roll = rnd.Next(0, 2);
                int upper = rnd.Next(0, 2);
                if (number==true && roll!=1)//add a number
                {
                    textBox3.Text = textBox3.Text + rnd.Next(0, 10);
                }
                else if (uppercase == true && upper!=1)//add a number
                {
                    textBox3.Text = textBox3.Text + upperletters[rnd.Next(0, 26)];
                }
                else//add a letter
                {
                    textBox3.Text = textBox3.Text + lowerletters[rnd.Next(0, 26)];
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)//adds a new catagory to the combo boxes
        {
            comboBox1.Items.AddRange(new object[] { textBox5.Text} );
            comboBox2.Items.AddRange(new object[] { textBox5.Text} );
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //pick search catagory
        {

        }

        private void button3_Click(object sender, EventArgs e)//IF UPPERCASE
        {
            uppercase = true;
        }

        private void button4_Click(object sender, EventArgs e)//if number
        {
            number = true;
        }

        private void button9_Click(object sender, EventArgs e)//message box if you click exit
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save before leaving?","Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                save();
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
