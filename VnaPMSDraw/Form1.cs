using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ControlManager;


namespace VnaPMSDraw
{
    public partial class BackForm : Form
    {
        protected bool validData;
        string path;
        protected Image image;
        protected Thread getImageThread;

        //우클릭 메뉴 
        ContextMenuStrip ctms = new ContextMenuStrip();
        
        Point ClickMousePoint;
        private string controlsInfoStr;


        List<AnaTextData> altextData = null;
        List<DigiImageData> digiImageData = null;
        List<StaticImageData> staImageData = null;


        public BackForm()
        {
            InitializeComponent();
            //우클릭 메뉴 항목 추가                              
            ctms.Items.Add("Analogue Text",null, AnalogueText_Add);
            ctms.Items.Add("Digital Image", null, DigitalImage_Add);
            ctms.Items.Add("Static Image", null, StaticImage_Add);         
 
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                path = filename;
                getImageThread = new Thread(new ThreadStart(LoadImage));
                getImageThread.Start();
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp"))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }
        protected void LoadImage()
        {
            image = new Bitmap(path);
        }

        private void btn_AnalogueText_Click(object sender, EventArgs e)
        {
            ALTextConfig alform = new ALTextConfig();
            if(alform.ShowDialog() == DialogResult.OK)
            {               
                AnaTextData settext = alform.ReturnValue();
            }

            //AlTextData 의 구조체로 반환하면 list에 넣어주기
            

        }
        
        public string FilePath()
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
        private void btn_LoadBgImg_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = @"D:\";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.FileName;
            }
            else if( dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }  
        }

        public void LoadHtmldata()
        {
            //html save 데이터 가져 온 다음에
        }

        public void ApplyLoadHtmlData()
        {
            //여기서 적용해서 picture box 에 그림 그리고
        }

        private void MouseRigltClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                Point moustpoint = new Point(e.X, e.Y);                     
                ctms.Show(this, moustpoint);
                ClickMousePoint.X = e.X;
                ClickMousePoint.Y = e.Y;                
            }
        }

        public void StaticImage_Add(object sender, EventArgs e)
        {
            string strtemp = FilePath();
            PictureBox pb = new PictureBox();
            pb.Name = "static";
            pb.Image = new Bitmap(strtemp);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = ClickMousePoint.X;
            pb.Top = ClickMousePoint.Y;

            //key down event             
            ControlMoverOrResizer.Init(pb);
            this.Controls.Add(pb);
            this.Refresh();
        }

        private void AnalogueText_Add(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();           
            tb.Name = "analogue";
            tb.Text = "Tag Text";                  
            tb.Left = ClickMousePoint.X;
            tb.Top = ClickMousePoint.Y;
            tb.ForeColor = Color.White;
            tb.BackColor = Color.Black;

            ControlMoverOrResizer.Init(tb);
            this.Controls.Add(tb);
            this.Refresh();
        }

        private void DigitalImage_Add(object sender, EventArgs e)
        {
            string strtemp = FilePath();
            PictureBox pb = new PictureBox();
            pb.Name = "Digital";
            pb.Image = new Bitmap(strtemp);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = ClickMousePoint.X;
            pb.Top = ClickMousePoint.Y;            

            ControlMoverOrResizer.Init(pb);
            this.Controls.Add(pb);
            this.Refresh();
        }

        private void ControlDelete(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DigitalImageConfig(object sender, EventArgs e)
        {
            DigitalImageConfig digitaldialog = new DigitalImageConfig();

            digitaldialog.ShowDialog();
            
        }


        //작업 이미지 불러오기 
        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = @"D:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filepath = dialog.FileName;                
                this.Text = Path.GetFileNameWithoutExtension(dialog.FileName); 
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Image bgImage = Image.FromFile(filepath);

            this.BackgroundImage = bgImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Refresh();

        }

        private void eXPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //출력 파일 이름은 main background name 으로

              string strSavename = this.Text;

            controlsInfoStr = ControlMoverOrResizer.GetSizeAndPositionOfControlsToString(this);

            foreach(AnaTextData textdata in altextData)
            {
                textdata.GetData();
                string jsonstring = JsonConvert.SerializeObject(textdata, Formatting.None);
            }

            foreach (DigiImageData imagedata in digiImageData)
            {
                imagedata.GetData();
                string jsonstring = JsonConvert.SerializeObject(imagedata, Formatting.None);
            }

            foreach (StaticImageData imagedata in staImageData)
            {
                imagedata.GetData();
                string jsonstring = JsonConvert.SerializeObject(imagedata, Formatting.None);
            }
        }


        private void iMPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(controlsInfoStr))
            {
                ControlMoverOrResizer.SetSizeAndPositionOfControlsFromString(this, controlsInfoStr);
            }

            AnaTextData anadata = new AnaTextData();
            anadata.SetData();
            altextData.Add(anadata);

            DigiImageData digidata = new DigiImageData();
            digidata.SetData();
            digiImageData.Add(digidata);

            StaticImageData sticdata = new StaticImageData();
            sticdata.SetData();
            staImageData.Add(sticdata);

        }

        private void PictureBox_MouseOver(object sender, MouseEventArgs e)
        {            
            this.Focus();
        }


    }
}

