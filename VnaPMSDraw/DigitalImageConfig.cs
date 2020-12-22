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
            dialog.InitialDirectory = @"D:\";

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

        public DigiImageData ReturnValue()
        {
            DigiImageData temp = new DigiImageData();
           

            return temp;
        }
    }
}
