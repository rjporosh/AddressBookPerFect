﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public static Timer t;

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            t = new Timer();
            t.Interval = 2000;
            t.Start();
            t.Tick += T_Tick;           
        }
        public void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            Form1 f= new Form1();
            f.Show();
            this.Hide();
        }
    }
}
