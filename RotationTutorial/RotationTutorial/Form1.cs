using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotationTutorial
{
    public partial class Form1 : Form
    {
        public Form1(string Text)
        {
            InitializeComponent();
            label1.Text = Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
