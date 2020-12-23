using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VnaPMSDraw
{
    public class AnaTextData
    {
        public string FontSize;
        public string FontWeight;
        public string Tag;
        public string Float;
        public int Zindex;
        public string FontColor;
        public string UniqueTag;
        public Point Postion;
        public int height;
        public int weight;

        public string GetData()
        {
            string temp = "";
            return temp;
        }

        public void SetData()
        {

        }
    }

    public class DigiImageData
    {

        public string Tag;
        public string basepath;
        public short[] value = new short[4];
        public string[] path = new string[4];
        public string UniqueTag;
        public Point Postion;
        public int height;
        public int weight;
        

        public string GetData()
        {
            string temp = "";
            return temp;
        }
        public void SetData()
        {

        }
    }

    public class StaticImageData
    {
        public Point Postion;
        public int height;
        public int weight;

        public string GetData()
        {
            string temp = "";
            return temp;
        }
        public void SetData()
        {

        }
    }
}
