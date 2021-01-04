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
        public string _id;
        public string FontSize;
        public string FontWeight;
        public string Tag;
        public string Float;
        public int Zindex;
        public string FontColor;

        public string HColor;
        public string HHColor;
        public string LColor;
        public string LLColor;

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
        public short[] value = new short[8];
        public string[] path = new string[8];
        public string UniqueTag;
        public Point Postion;
        public int height;
        public int weight;
        public int sizeRatio;
        public string tag;
        

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
        public int zindex;
        public int height;
        public int weight;
        public string tag;
        public string filename;
        public int sizeRatio;

        public string GetData()
        {
            string temp = "";
            return temp;
        }
        public void SetData()
        {

        }
    }

    public sealed class DataStructALL
    {
        private static DataStructALL singleDataStructAll = null;

        static List<AnaTextData> altextData = new List<AnaTextData>();
        static List<DigiImageData> digiImageData = new List<DigiImageData>();
        static List<StaticImageData> staImageData = new List<StaticImageData>();

        public static DataStructALL Instance()
        {
            if (singleDataStructAll == null)
            {
                singleDataStructAll = new DataStructALL();
            }
            return singleDataStructAll;
        }

        public void Add_AnaTextData(AnaTextData data)
        {
            altextData.Add(data);
        }

        public void Add_DigiImageData(DigiImageData data)
        {
            digiImageData.Add(data);
        }

        public void Add_StaticImageData(StaticImageData data)
        {
            staImageData.Add(data);
        }


        public void Modi_AnaTextData(AnaTextData data)
        {
            string temp = data.UniqueTag;
            int index = 0;
            foreach (AnaTextData textdata in altextData)
            {
                if (textdata.UniqueTag == temp)
                {
                    index = altextData.IndexOf(textdata);
                }
            }
            altextData[index] = data;
        }

        public void Modi_DigiImageData(DigiImageData data)
        {
            string temp = data.tag;
            int index = 0;
            foreach (DigiImageData imagedata in digiImageData)
            {
                if (imagedata.tag == temp)
                {
                    index = digiImageData.IndexOf(imagedata);
                }
            }
            digiImageData[index] = data;
        }

        public void Modi_StaticImageData(StaticImageData data)
        {
            string temp = data.tag;            
            int index = 0;
            foreach (StaticImageData imagedata in staImageData)
            {
                if (imagedata.tag == temp)
                {
                    index = staImageData.IndexOf(imagedata);                   
                }
            }
            staImageData[index] = data;
        }


        public void Delete_AnaTextData(AnaTextData data)
        {

        }

        public List<DigiImageData> Info_DigiImageData()
        {
            return digiImageData;
        }

        public List<StaticImageData> Info_StaticImageData()
        {
            return staImageData;
        }

        public List<AnaTextData> Info_AnaTextData()
        {
            return altextData;
        }

        public void Apply_DigiImageData(List<DigiImageData> data)
        {
            digiImageData.Clear();
            digiImageData = data;
        }
        public void Apply_StaticImageData(List<StaticImageData> data)
        {
            staImageData.Clear();
            staImageData = data;
        }

        public void Apply_AnaTextData(List<AnaTextData> data)
        {
            altextData.Clear();
            altextData = data;
        }


        public void Delete_DigiImageData(DigiImageData data)
        {
            string temp = data.tag;

            foreach (DigiImageData imagedata in digiImageData)
            {
                if (imagedata.tag == temp)
                {
                    int index = digiImageData.IndexOf(imagedata);
                    digiImageData.RemoveAt(index);
                }
            }
        }

        public void Delete_StaticImageData(StaticImageData data)
        {
            //tag를 분석해서 삭제하기
            string temp = data.tag;

            foreach (StaticImageData imagedata in staImageData)
            {
                if (imagedata.tag == temp)
                {
                    int index = staImageData.IndexOf(imagedata);
                    staImageData.RemoveAt(index);
                 }
            }

        }

        public void DeleteAllData()
        {
            staImageData.Clear();
            digiImageData.Clear();
            altextData.Clear();
        }
    }
}
