using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DKim2
{
    public partial class LogoWindow : Form
    {
        public LogoWindow()
        {
            InitializeComponent();

            Assembly assemObj = Assembly.GetExecutingAssembly();
            Version v = assemObj.GetName().Version;

            pictureBox1.Size = new Size(this.Width, this.Height);
            Bitmap logo = new Bitmap(global::DKim2.Properties.Resources.DKIM2logo);
            logo.MakeTransparent();
            pictureBox1.Image = logo;

            textBox1.Text = "v" + v.Major + "." + v.Minor + "   ";
        }
    }
}
