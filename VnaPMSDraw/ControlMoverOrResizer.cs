using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VnaPMSDraw;

namespace ControlManager
{
    internal class ControlMoverOrResizer
    {
        private static bool _moving;
        private static Point _cursorStartPoint;
        private static bool _moveIsInterNal;
        private static bool _resizing;
        private static Size _currentControlStartSize;
        internal static bool MouseIsInLeftEdge { get; set; }
        internal static bool MouseIsInRightEdge { get; set; }
        internal static bool MouseIsInTopEdge { get; set; }
        internal static bool MouseIsInBottomEdge { get; set; }

        static List<AnaTextData> altextData = new List<AnaTextData>();
        static List<DigiImageData> digiImageData = new List<DigiImageData>();
        static List<StaticImageData> staImageData = new List<StaticImageData>();

        internal enum MoveOrResize
        {
            Move,
            Resize,
            MoveAndResize
        }

        internal static MoveOrResize WorkType { get; set; }

        internal static void Init(Control control)
        {
            Init(control, control);
        }

        internal static void Init(Control control, Control container)
        {
            _moving = false;
            _resizing = false;
            _moveIsInterNal = false;
            _cursorStartPoint = Point.Empty;
            MouseIsInLeftEdge = false;
            MouseIsInLeftEdge = false;
            MouseIsInRightEdge = false;
            MouseIsInTopEdge = false;
            MouseIsInBottomEdge = false;
            WorkType = MoveOrResize.MoveAndResize;
            control.MouseDown += (sender, e) => StartMovingOrResizing(control, e);
            control.MouseUp += (sender, e) => StopDragOrResizing(control);
            control.MouseMove += (sender, e) => MoveControl(container, e);
            control.MouseDoubleClick += (sender, e) => ViewConfig(control, e);
            control.KeyDown += (sender, e) => DeleteControl(control, e);

        }

        private static void UpdateMouseEdgeProperties(Control control, Point mouseLocationInControl)
        {
            if (WorkType == MoveOrResize.Move)
            {
                return;
            }
            MouseIsInLeftEdge = Math.Abs(mouseLocationInControl.X) <= 2;
            MouseIsInRightEdge = Math.Abs(mouseLocationInControl.X - control.Width) <= 2;
            MouseIsInTopEdge = Math.Abs(mouseLocationInControl.Y ) <= 2;
            MouseIsInBottomEdge = Math.Abs(mouseLocationInControl.Y - control.Height) <= 2;
        }

        private static void UpdateMouseCursor(Control control)
        {
            if (WorkType == MoveOrResize.Move)
            {
                return;
            }
            if (MouseIsInLeftEdge )
            {
                if (MouseIsInTopEdge)
                {
                    control.Cursor = Cursors.SizeNWSE;
                }
                else if (MouseIsInBottomEdge)
                {
                    control.Cursor = Cursors.SizeNESW;
                }
                else
                {
                    control.Cursor = Cursors.SizeWE;
                }
            }
            else if (MouseIsInRightEdge)
            {
                if (MouseIsInTopEdge)
                {
                    control.Cursor = Cursors.SizeNESW;
                }
                else if (MouseIsInBottomEdge)
                {
                    control.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    control.Cursor = Cursors.SizeWE;
                }
            }
            else if (MouseIsInTopEdge || MouseIsInBottomEdge)
            {
                control.Cursor = Cursors.SizeNS;
            }
            else
            {
                control.Cursor = Cursors.Default;
            }
        }

        private static void StartMovingOrResizing(Control control, MouseEventArgs e)
        {
            control.Focus();
            if (_moving || _resizing)
            {
                return;
            }
            if (WorkType!=MoveOrResize.Move &&
                (MouseIsInRightEdge || MouseIsInLeftEdge || MouseIsInTopEdge || MouseIsInBottomEdge))
            {
                _resizing = true;
                _currentControlStartSize = control.Size;
            }
            else if (WorkType!=MoveOrResize.Resize)
            {
                _moving = true;
                control.Cursor = Cursors.Hand;
            }
            _cursorStartPoint = new Point(e.X, e.Y);
            control.Capture = true;
        }

        private static void MoveControl(Control control, MouseEventArgs e)
        {
            if (!_resizing && ! _moving)
            {
                UpdateMouseEdgeProperties(control, new Point(e.X, e.Y));
                UpdateMouseCursor(control);
            }
            if (_resizing)
            {
                if (MouseIsInLeftEdge)
                {
                    if (MouseIsInTopEdge)
                    {
                        control.Width -= (e.X - _cursorStartPoint.X);
                        control.Left += (e.X - _cursorStartPoint.X); 
                        control.Height -= (e.Y - _cursorStartPoint.Y);
                        control.Top += (e.Y - _cursorStartPoint.Y);
                    }
                    else if (MouseIsInBottomEdge)
                    {
                        control.Width -= (e.X - _cursorStartPoint.X);
                        control.Left += (e.X - _cursorStartPoint.X);
                        control.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;                    
                    }
                    else
                    {
                        control.Width -= (e.X - _cursorStartPoint.X);
                        control.Left += (e.X - _cursorStartPoint.X) ;
                    }
                }
                else if (MouseIsInRightEdge)
                {
                    if (MouseIsInTopEdge)
                    {
                        control.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.Width;
                        control.Height -= (e.Y - _cursorStartPoint.Y);
                        control.Top += (e.Y - _cursorStartPoint.Y);

                    }
                    else if (MouseIsInBottomEdge)
                    {
                        control.Width = (e.X - _cursorStartPoint.X) + _currentControlStartSize.Width;
                        control.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;                    
                    }
                    else
                    {
                        control.Width = (e.X - _cursorStartPoint.X)+_currentControlStartSize.Width;
                    }
                }
                else if (MouseIsInTopEdge)
                {
                    control.Height -= (e.Y - _cursorStartPoint.Y);
                    control.Top += (e.Y - _cursorStartPoint.Y);
                }
                else if (MouseIsInBottomEdge)
                {
                    control.Height = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;

                }
                else
                {
                     StopDragOrResizing(control);   
                }
         
            }
            else if (_moving)
            {
                _moveIsInterNal = !_moveIsInterNal;
                if (!_moveIsInterNal)
                {
                    int x = (e.X - _cursorStartPoint.X) + control.Left;
                    int y = (e.Y - _cursorStartPoint.Y) + control.Top;
                    control.Location = new Point(x, y);
                }
            }
        }

        #region 더블클릭으로 viewConfig 진행 
        private static void ViewConfig(Control control, MouseEventArgs e)
        {                        
            string typeName = control.GetType().Name;       
         
           
            if (typeName == "PictureBox")
            {
                if("Digital" == control.Name)
                {
                    DigitalImageConfig diform = new DigitalImageConfig();
                    PictureBox pb = (PictureBox)control;

                    //data 리스트에 구별자를 넣고  tag가 있다
                    //컨트롤 구별자랑 foreach로 다 찾아가면서 
                    //foreach(DigiImageData data in digiImageData)
                    //{
                    //    if(control.Tag.ToString() == data.UniqueTag)
                    //    {

                    //    }
                    //}
                    ////일치하는 class를 넣어준다
                    ////
                    //int datacount = 0;
                    //diform.SetDialog(digiImageData[datacount]);

                    if (diform.ShowDialog() == DialogResult.OK)
                    {
                        DigiImageData setimage = diform.ReturnValue(control);
                      //  digiImageData.Add(setimage);
                    }
                }    
                else if ("static" == control.Name)
                {
                    //컨트롤을 받아서 구조체로 넣는 함수 필요함 
                    StaticImageData setimage = new StaticImageData();
                    PictureBox pb = (PictureBox)control;

                    if(pb.Tag.ToString() == "")
                    {
                        setimage.filename = pb.ImageLocation;
                        setimage.Postion.X = pb.Left;
                        setimage.Postion.Y = pb.Top;
                        pb.BackColor = Color.Transparent;                        

                        //기존 control 에 tag로 data 작성을 표기 함
                        //데이터 구조체 tag에도 작성을 표기 함
                        //TAG 데이터가 있다 -> 그럼 둘이 비교해서 같은 경우 list에서 수정되는것
                        //TAG 데이터가 있다 -> 둘이 다르면 그냥 스킾임
                        control.Tag = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        setimage.tag = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        
                        setimage.filename = System.IO.Path.GetFileName(pb.ImageLocation);

                        DataStructALL DataContainer = DataStructALL.Instance();
                        DataContainer.Add_StaticImageData(setimage);

                        MessageBox.Show("set Data Comp");
                    }
                    else
                    {
                        MessageBox.Show("set Data");
                    }

                }
            }
            else if ( typeName =="TextBox")
            {
                ALTextConfig alform = new ALTextConfig();
                //받아와서 Set하기
                alform.SetDialog((TextBox)control);

                if (alform.ShowDialog() == DialogResult.OK)
                {
                    AnaTextData settext = alform.ReturnValue(control);
                    control.ForeColor = alform.ReturnColor();
                    control.Text = settext.Tag;

                    int size = 12;
                    //small 8 medium 12 largr 24
                    if ("Small" == settext.FontSize) size = 12;
                    else if ("Medium" == settext.FontSize) size = 12;
                    else if ("Large" == settext.FontSize) size = 24;

                    FontStyle fstyle = FontStyle.Regular;
                    if ("Bold" == settext.FontWeight) fstyle = FontStyle.Bold;
                    control.Font = new Font(control.Font.FontFamily, size, fstyle);
                    control.Refresh();


                    DataStructALL DataContainer = DataStructALL.Instance();
                    DataContainer.Add_AnaTextData(settext);


                }
            }
    
        }
        #endregion

        private static void DeleteControl(Control control, KeyEventArgs e)
        {
          if(e.KeyCode == Keys.Delete)
          {
               control.Dispose();
          }
          
        }

        private static void DeleteImage(object sender, EventArgs e)
        {
    
        }


        private static void StopDragOrResizing(Control control)
        {
            _resizing = false;
            _moving = false;
            control.Capture = false;
            UpdateMouseCursor(control);
        }

        #region Save And Load

        private static List<Control> GetAllChildControls(Control control, List<Control> list)
        {
            List<Control> controls = control.Controls.Cast<Control>().ToList();
            list.AddRange(controls);
            return controls.SelectMany(ctrl => GetAllChildControls(ctrl, list)).ToList();
        }

        internal static string GetControlFormat(Control container)
        {
            List<Control> controls = new List<Control>();
            GetAllChildControls(container, controls);

            //컨트롤을 다 받아와서 

            //여기서 컨트롤이랑 listClass 랑 매칭해서 json으로 출력해줘야함

            CultureInfo cultureInfo = new CultureInfo("en");
            string info = string.Empty;
            foreach (Control control in controls)
            {
                info += control.Name + ":" + control.Left.ToString(cultureInfo) + "," + control.Top.ToString(cultureInfo) + "," +
                        control.Width.ToString(cultureInfo) + "," + control.Height.ToString(cultureInfo) + "*";
            }
            return info;
        }
        internal static void SaveControlFormat(Control container, string controlsInfoStr)
        {
            List<Control> controls = new List<Control>();
            GetAllChildControls(container, controls);
            string[] controlsInfo = controlsInfoStr.Split(new []{"*"},StringSplitOptions.RemoveEmptyEntries );
            Dictionary<string, string> controlsInfoDictionary = new Dictionary<string, string>();
            foreach (string controlInfo in controlsInfo)
            {
                string[] info = controlInfo.Split(new [] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                controlsInfoDictionary.Add(info[0], info[1]);
            }
            foreach (Control control in controls)
            {
                string propertiesStr;
                controlsInfoDictionary.TryGetValue(control.Name, out propertiesStr);
                string[] properties = propertiesStr.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (properties.Length == 4)
                {
                    control.Left = int.Parse(properties[0]);
                    control.Top = int.Parse(properties[1]);
                    control.Width = int.Parse(properties[2]);
                    control.Height = int.Parse(properties[3]);
                }
            }
        }
        #endregion
    }
}