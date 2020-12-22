using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnaPMSDraw;

namespace VnaPMSDraw
{
    class Makehtml
    {
        //아날로그 텍스트 넣기
        public string EditAnalogueTxt(AnaTextData anaData)
        {
            string temp = "";
            int divid = 192121212;
            string strClass = string.Format("<div class=\"PMS_Image)Item ui-draggable ui-draggable-handle ui-resizable\"");
            string strlind = string.Format("\"\" style=\"position:absolute; top: {0}px; left: {1}px; z-index:{2};\">");
            string strimagepath = string.Format("<img src=\"{0}\"");
            return temp;
        }

        public string EditDigitalImage(DigiImageData diData)
        {
            string temp = "";

            return temp;
        }

        public string EditStaticImage(StaticImageData stData)
        {
            string temp = "";

            return temp;
        }

        
        //디지털 텍스트 넣기
        public string EditDigitalTxt()
        {
            int divid = 192121212;
            string AnalogueTxt = string.Format("<div id = \"{0}\"", divid);
            //
            string strClass = string.Format("PMS_Tag_Item ui-Draggable ui-draggable-handle Text_{0} Text_{1}",0,1);

            string opctag = string.Format("opc_tag_txt=\"{'{0}', 'config':{'formats':{'bad_q':???','Float':'{1}'}}}");

            string strcolor = string.Format("d=\"#ffffff\" hh=\"#ff0000\" h=\"#e36c09\" l=\"#95b3d7\" ll=\"#0070c0\"");

            string strstyle = string.Format("\"position:absolute: top:{0}px; left:{1}px; color:rgb({2},{3},{4}); z-index:{5};>\"");

            string write;

            string edittext = AnalogueTxt + strClass + opctag + strcolor + strstyle;

            return edittext;
        }
        
    }


}
