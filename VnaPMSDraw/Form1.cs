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

        //contextmenu 호출 오른쪽 마우스 이벤트에 걸어놓음
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

        #region 필드에 올릴 객체들을 생성 합니다. 
        //스태틱 이미지
        public void StaticImage_Add(object sender, EventArgs e)
        {
            string strtemp = FilePath();
            PictureBox pb = new PictureBox();
            pb.Name = "static";
            pb.Image = new Bitmap(strtemp);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = ClickMousePoint.X;
            pb.Top = ClickMousePoint.Y;
            pb.BackColor = Color.Transparent;
            pb.Tag = string.Format("");
            pb.ImageLocation = strtemp;

            //key down event             
            ControlMoverOrResizer.Init(pb);
            this.Controls.Add(pb);
            this.Refresh();
        }

        //아날로그 텍스트
        private void AnalogueText_Add(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();           
            tb.Name = "analogue";
            tb.Text = "Tag Text";                  
            tb.Left = ClickMousePoint.X;
            tb.Top = ClickMousePoint.Y;
            tb.ForeColor = Color.White;
            tb.BackColor = Color.Black;
//tb.BackColor = Color.FromArgb(0, 0, 0, 0); 텍스트박스는 투명해지지 않는다!
            tb.Tag = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            ControlMoverOrResizer.Init(tb);
            this.Controls.Add(tb);
            this.Refresh();
        }

        //디지탈 이미지
        private void DigitalImage_Add(object sender, EventArgs e)
        {
            string strtemp = FilePath();
            PictureBox pb = new PictureBox();
            pb.Name = "Digital";
            pb.Image = new Bitmap(strtemp);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Left = ClickMousePoint.X;
            pb.Top = ClickMousePoint.Y;
            pb.Tag = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            pb.BackColor = Color.Transparent;

            ControlMoverOrResizer.Init(pb);
            this.Controls.Add(pb);
            this.Refresh();
        }

        #endregion

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

            DataStructALL DataContainer = DataStructALL.Instance();
            DataContainer.DeleteAllData();
        }

        private void eXPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //출력 파일 이름은 main background name 으로

              string strSavename = this.Text;
              string path = MakeHtmlTextFile(strSavename);

            //  controlsInfoStr = ControlMoverOrResizer.GetControlFormat(this);

            //foreach (AnaTextData textdata in altextData)
            //{
            //    textdata.GetData();
            //    string jsonstring = JsonConvert.SerializeObject(textdata, Formatting.None);
            //    MakeAnalogueTextHtmlCode(textdata, path);
            //}

            //foreach (DigiImageData imagedata in digiImageData)
            //{
            //    imagedata.GetData();
            //    string jsonstring = JsonConvert.SerializeObject(imagedata, Formatting.None);
            //    MakeDigitalImageHtmlCode(imagedata, path);
            //}
            DataStructALL DataContainer = DataStructALL.Instance();
            List<StaticImageData> staImageData = DataContainer.Info_StaticImageData();

            foreach (StaticImageData imagedata in staImageData)
                {
                    imagedata.GetData();
                    string jsonstring = JsonConvert.SerializeObject(imagedata, Formatting.None);

                    MakeStaticImageHtmlCode(imagedata, path);
                }
        }


        private void iMPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(controlsInfoStr))
            //{
            //    ControlMoverOrResizer.SaveControlFormat(this, controlsInfoStr);
            //}

            //json에서 구분별로 data를 가져와서 넣어줌
            //1. analogue text json불러와서 -> textbox 생성 and statictextdata에 add
            //1. static image json불러와서 -> picture 생성 and statictextdata에 add
            //1. digital image json불러와서 -> picture 생성 and statictextdata에 add

        }

        private void PictureBox_MouseOver(object sender, MouseEventArgs e)
        {            
            this.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Menu))
            {
                if (!this.menuStrip1.Visible)
                {
                    this.menuStrip1.Visible = true;
                    var OnMenuKey = menuStrip1.GetType().GetMethod("OnMenuKey",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);
                    OnMenuKey.Invoke(this.menuStrip1, null);
                }
                else
                {
                    this.menuStrip1.Visible = false;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void menuStrip1_MenuDeactivate(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { this.menuStrip1.Visible = false; }));
        }

        #region HTML TAG 만드는 구간
        private void MakeStaticImageHtmlCode(StaticImageData data, string path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            System.IO.StreamWriter writer = File.Exists(path) ? info.AppendText() : info.CreateText();

            StringBuilder code = new StringBuilder();

            string div = string.Format("</div>");
            code.Append("<div class=\"PMS_Image_Item ui-draggable ui-draggable-handle ui-resizable\" link=\"\"");
            code.Append(" style=\"position: absolute; ");

            string position = string.Format("top: {0}px; left: {1}px; z-index: {2}; \">", data.Postion.X, data.Postion.Y, data.zindex);
            code.Append(position);

            string imagepath = string.Format("<img src=\"/symbols/{0}\" style=\"width:100%;height;100%\">", data.filename);
            code.Append(imagepath);

            string temp = string.Format("<div class=\"ui-resizable-handle ui-resizable-e\" style=\"z-index: 90;\">");
            code.Append(temp);
            code.Append(div);
            code.Append(temp);
            code.Append(div);

            temp = string.Format("<div class=\"ui-resizable-handle ui-resizable-se ui-icon ui-icon-gripsmall-diagonal-se\" style=\"z-index: 90;\">");

            code.Append(temp);
            code.Append(div);
            code.Append(div);

            writer.WriteLine(code.ToString());
            writer.Flush();
            writer.Close();
        }


        private void MakeDigitalImageHtmlCode(DigiImageData data, string path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            System.IO.StreamWriter writer = File.Exists(path) ? info.AppendText() : info.CreateText();

            StringBuilder code = new StringBuilder();

            string div = string.Format("</div>");            

            writer.WriteLine(code.ToString());
            writer.Flush();
            writer.Close();
        }

        private void MakeAnalogueTextHtmlCode(AnaTextData data, string path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            System.IO.StreamWriter writer = File.Exists(path) ? info.AppendText() : info.CreateText();

            StringBuilder code = new StringBuilder();

            string div = string.Format("</div>");          
            //태그 고유아이디랑 텍스트 사이즈
            string temp = string.Format("<div id=\"{0}\" class=\"PMS_Tag_Item ui-draggable ui-draggable-handle Text_Small {1}\"", data._id, data.FontSize);
            code.Append(temp);

            //태그 주소값이랑 소수점 자리수 
            temp = string.Format(" opc-tag-txt=\"{{'tag':'{0}.Value','config': {{'formats': {{'bad_q':'???','float':'{1}'}}}}}}\"", data.Tag, data.Float);
            code.Append(temp);

            //색상 
            temp = string.Format(" d=\"#{0}\" hh=\"#{1}\" h=\"#{2}\" l=\"#{3}\" ll=\"#{4}\" style=\"position: absolute; top: {5}px; left: {6}px; color: rgb(255, 255, 255);\"",
                data.FontColor, data.HHColor, data.HColor, data.LColor, data.LLColor, data.Postion.X, data.Postion.Y);
            code.Append(temp);

            //인덱스랑 view에 쓸 내용
            temp = string.Format(" z-index: 5;\" > {0} </div>", data.Tag);
            code.Append(temp);


            writer.WriteLine(code.ToString());
            writer.Flush();
            writer.Close();
        }

        #endregion


        #region HTML 파일로 빼는 구간
        public string MakeHtmlTextFile(string PageName)
        {  
            string localpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            localpath = string.Format(localpath + ("\\"));
            localpath = string.Format(localpath + "VINA PMS DRAW"); 
            localpath = string.Format(localpath + ("\\"));
        
            StringBuilder path1 = new StringBuilder();
            path1.Append(localpath);
            if (!Directory.Exists(path1.ToString()))
            {
                try
                {
                    Directory.CreateDirectory(path1.ToString());
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return "";
                }
            }
            string srcFile = string.Format("{0}{1}.txt", localpath, PageName);
            System.IO.FileInfo info = new System.IO.FileInfo(srcFile);
            System.IO.StreamWriter writer = File.Exists(srcFile) ? info.AppendText() : info.CreateText();      
            
            writer.Flush();
            writer.Close();

            return srcFile;
        }

        #endregion
    }
}

