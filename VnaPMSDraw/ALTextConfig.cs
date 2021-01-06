using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


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
            combo_FontSIze.Items.Add("Midium");
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
                        
            combo_float.Items.Add("0");
            combo_float.Items.Add("0.0");
            combo_float.Items.Add("0.00");
            combo_float.Items.Add("0.000");
            combo_float.Items.Add("0.0000");
            combo_float.SelectedIndex = 0;

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
        }
        
        public Color HexToColor(string code)
        {
            return  Color.FromArgb(Convert.ToInt32(code, 16));
        }

        public Color ReturnColor()
        {
            return lab_Color.BackColor;
        }

        public AnaTextData ReturnValue(Control ctrl)
        {
            AnaTextData temp = new AnaTextData();
            temp.Tag = string.Format("{0}", text_TAG.Text);
            temp.FontColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_Color.BackColor.R, lab_Color.BackColor.G, lab_Color.BackColor.B);

            temp.HHColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_HH.BackColor.R, lab_HH.BackColor.G, lab_HH.BackColor.B);
            temp.HColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_H.BackColor.R, lab_H.BackColor.G, lab_H.BackColor.B);
            temp.LLColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_LL.BackColor.R, lab_LL.BackColor.G, lab_LL.BackColor.B);
            temp.LColor = string.Format("{0:X2}{1:X2}{2:X2}", lab_L.BackColor.R, lab_L.BackColor.G, lab_L.BackColor.B);

            temp.FontSize = string.Format(combo_FontSIze.SelectedItem.ToString());
            temp.FontWeight = string.Format(combo_FontBold.SelectedItem.ToString());
            temp.Zindex = combo_Zindex.SelectedIndex;
            temp.UniqueTag = ctrl.Tag.ToString();
            temp.Postion = ctrl.Location;
            temp.height = ctrl.Size.Height;
            temp.weight = ctrl.Size.Width;
            temp.Float = (string)combo_float.SelectedItem;
    
            return temp;
        }

        public void SetDialog(TextBox text, bool resetflag = false)
        {
            text_TAG.Text = text.Text;
            lab_Color.BackColor = text.ForeColor;     
        }

        public void SetDialog(AnaTextData data, bool resetflag = false)
        {
            text_TAG.Text = data.Tag;
            lab_Color.BackColor = GetColorFromHex(data.FontColor);
            lab_H.BackColor = GetColorFromHex(data.HColor);
            lab_HH.BackColor = GetColorFromHex(data.HHColor);
            lab_L.BackColor = GetColorFromHex(data.LColor);
            lab_LL.BackColor = GetColorFromHex(data.LLColor);

            if (0 > data.Float.Length - 2) combo_float.SelectedIndex = 0;
            else combo_float.SelectedIndex = data.Float.Length - 2;
            
        }

        private void lab_HH_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lab_HH.BackColor = cd.Color;
                //
            }
        }

        private void lab_LL_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lab_LL.BackColor = cd.Color;
                //
            }
        }

        private void lab_H_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lab_H.BackColor = cd.Color;
                //
            }
        }

        private void lab_L_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lab_L.BackColor = cd.Color;
                //
            }
        }

        private Color GetColorFromHex(string hexString)
        {
            hexString = hexString.Replace("#", string.Empty);
            byte r = byte.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber);

            return Color.FromArgb(byte.Parse("255"), r, g, b);
        }
    }
}
