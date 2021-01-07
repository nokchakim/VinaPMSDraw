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
    public partial class DigitalImageConfig : Form
    {
        DigiImageData digiimagedata = new DigiImageData();

        public DigitalImageConfig()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        public string ReturnImagePath()
        {
            string filepath = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();

            string localpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            localpath = string.Format(localpath + ("\\"));
            localpath = string.Format(localpath + "VINA PMS DRAW");
            localpath = string.Format(localpath + ("\\IMAGE\\BG IMAGE\\symbols\\"));

            dialog.InitialDirectory = localpath;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.FileName;
                return filepath;
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return "";
            }

            return "";
        }

        #region 더블클릭으로 Textbox에 path 입력하기
        public void inputPath(object sender, EventArgs e)
        {
            TextBox pathtext = (TextBox)sender;
            //여기서 앞에 경로 잘라내는 작업 해줘야합니다. 
            pathtext.AppendText(ReturnImagePath());
        }
        #endregion

        public DigiImageData ReturnValue(Control ctrl)
        {
            DigiImageData temp = new DigiImageData();

            if (text_Value1.Text != "")     temp.value[0] = Convert.ToInt16(text_Value1.Text);
            if (text_Value2.Text != "")     temp.value[1] = Convert.ToInt16(text_Value2.Text);
            if (text_Value3.Text != "")     temp.value[2] = Convert.ToInt16(text_Value3.Text);
            if (text_Value4.Text != "")     temp.value[3] = Convert.ToInt16(text_Value4.Text);
            if (text_Value5.Text != "")     temp.value[4] = Convert.ToInt16(text_Value5.Text);
            if (text_Value6.Text != "")     temp.value[5] = Convert.ToInt16(text_Value6.Text);
            if (text_Value7.Text != "")     temp.value[6] = Convert.ToInt16(text_Value7.Text);
            if (text_Value8.Text != "")     temp.value[7] = Convert.ToInt16(text_Value8.Text);


            if (text_BaseImage_Path.Text != "")     temp.basepath = text_BaseImage_Path.Text;

            if (text_Value1_Path.Text != "")    temp.path[0] = text_Value1_Path.Text;
            if (text_Value2_Path.Text != "")    temp.path[1] = text_Value2_Path.Text;
            if (text_Value3_Path.Text != "")    temp.path[2] = text_Value3_Path.Text;            
            if (text_Value4_Path.Text != "")    temp.path[3] = text_Value4_Path.Text;
            if (text_Value5_Path.Text != "")    temp.path[4] = text_Value5_Path.Text;
            if (text_Value6_Path.Text != "")    temp.path[5] = text_Value6_Path.Text;
            if (text_Value7_Path.Text != "")    temp.path[6] = text_Value7_Path.Text;
            if (text_Value8_Path.Text != "")    temp.path[7] = text_Value8_Path.Text;
                                                
            //tag데이터가 아니라 구분자를 tag라고 하드라 VS에서
            temp.UniqueTag = ctrl.Tag.ToString();
            temp.Postion = ctrl.Location;
            temp.height = ctrl.Size.Height;
            temp.weight = ctrl.Size.Width;

            if (text_TAG_Dig.Text != "") temp.tag = text_TAG_Dig.Text;            

            return temp;
        }


        public void SetDialog(DigiImageData data)
        {
            if (data.value[0] > 0) text_Value1.Text = data.value[0].ToString();
            if (data.value[1] > 0) text_Value2.Text = data.value[1].ToString();
            if (data.value[2] > 0) text_Value3.Text = data.value[2].ToString();
            if (data.value[3] > 0) text_Value4.Text = data.value[3].ToString();
            if (data.value[4] > 0) text_Value4.Text = data.value[4].ToString();
            if (data.value[5] > 0) text_Value4.Text = data.value[5].ToString();
            if (data.value[6] > 0) text_Value4.Text = data.value[6].ToString();
            if (data.value[7] > 0) text_Value4.Text = data.value[7].ToString();
            


            if (data.basepath != "") text_BaseImage_Path.Text = data.basepath;

            if (data.path[0] != "") text_Value1_Path.Text = data.path[0];
            if (data.path[1] != "") text_Value2_Path.Text = data.path[1];
            if (data.path[2] != "") text_Value3_Path.Text = data.path[2];
            if (data.path[3] != "") text_Value4_Path.Text = data.path[3];
            if (data.path[4] != "") text_Value2_Path.Text = data.path[4];
            if (data.path[5] != "") text_Value3_Path.Text = data.path[5];
            if (data.path[6] != "") text_Value4_Path.Text = data.path[6];
            if (data.path[7] != "") text_Value4_Path.Text = data.path[7];


            if (data.tag != "") text_TAG_Dig.Text = data.tag;
        }
    }
}
