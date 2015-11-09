using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    public class MyProperties
    {
        private string path = "";


        public void SetPath(string path)
        {

            this.path = path;


        }


        public void WriteProperties(string data)
        {

            FileStream fs = new FileStream(@path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(data);
            sw.Close();
            fs.Close();
        }

        public string ReadProperties(string key)
        {
            string value = "";
            FileStream fs = new FileStream(@path, FileMode.Open);
            StreamReader m_streamReader = new StreamReader(fs);
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            string strLine = m_streamReader.ReadLine();
            while (strLine != null && strLine != "")
            {


                string[] split = strLine.Split('=');
                string a = split[0];
                if (a.ToLower().Equals(key))
                {
                    value = split[1];
                }


                strLine = m_streamReader.ReadLine();
            }
            m_streamReader.Close();
            m_streamReader.Dispose();
            fs.Close(); fs.Dispose();



            return value;
        }


        public void clear()
        {
            if (File.Exists(@path))
            {
                //如果存在则删除
                File.Delete(@path);


            }
        }


    }
}
