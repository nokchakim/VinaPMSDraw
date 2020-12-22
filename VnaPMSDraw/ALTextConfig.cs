using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VnaPMSDraw
{
    public partial class ALTextConfig : Form
    {

        AnaTextData AnaTextData = new AnaTextData();

        public ALTextConfig()
        {
            InitializeComponent();

            //컨트롤 이니셜라이즈
            combo_FontSIze.Items.Add("Large");
            combo_FontSIze.Items.Add("Midiume");
            combo_FontSIze.Items.Add("Small");
            combo_FontSIze.SelectedIndex = 1;            

            combo_FontBold.Items.Add("Normal");
            combo_FontBold.Items.Add("Bold");
            combo_FontBold.SelectedIndex = 0;

            combo_Zindex.Items.Add("5");
            combo_Zindex.Items.Add("4");
            combo_Zindex.Items.Add("3");
            combo_Zindex.Items.Add("2");
            combo_Zindex.Items.Add("1");
            combo_Zindex.SelectedIndex = 0;

            text_TAG.Text = "";
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = @"D:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.FileName;
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lab_Color_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lab_Color.BackColor = cd.Color;
                //
            }
            else {  };
        }
        
        public Color HexToColor(string code)
        {
            return  Color.FromArgb(Convert.ToInt32(code, 16));
        }

        public AnaTextData ReturnValue()
        {
            AnaTextData temp = new AnaTextData();           

            temp.FontColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_Color.BackColor.R, lab_Color.BackColor.G, lab_Color.BackColor.B);

            return temp;
        }
    }
}
