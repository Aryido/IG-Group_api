using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_api
{
    class FileProcessing
    {
        public static Dictionary<string, string> epicAndNodeNameDic = new Dictionary<string, string>();
        public static Dictionary<string, string> epicAndInstructmentDic = new Dictionary<string, string>();
        public static List<string> errorList = new List<string>();
        private const string FILE_NAME = "epicAndNodeName.txt";
        private const string FILE_NAME2 = "epicAndInstrumentName.txt";
        private const string FILE_NAME3 = "errorEpicsLog.txt";

        public bool existFunction()
        {
            bool result = false;

            if (File.Exists(FILE_NAME)&& File.Exists(FILE_NAME2))
            {
                result = true;
            }

            return result;
        }

        public void createFile(Dictionary<string, string> dic)
        {           
            FileStream fs = new FileStream(FILE_NAME, FileMode.Create);
            using (StreamWriter bw = new StreamWriter(fs))
            {
                foreach (KeyValuePair<string, string> kvp in dic)
                {
                    bw.Write(kvp.Key+":"+kvp.Value+"\n");
                }
                
            }            
        }

        public Dictionary<string, string> readEpicAndNodeNameFileTo()
        {
            string filePath = System.Environment.CurrentDirectory+ @"\epicAndNodeName.txt";
            FileStream fs = new FileStream(filePath, FileMode.Open);
            if(File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.EndOfStream != true)
                    {
                        string str = sr.ReadLine();
                        string[] epicAndNodeName=str.Split(':');
                        epicAndNodeNameDic.Add(epicAndNodeName[0], epicAndNodeName[1]);
                    }                
                }
            }
            return epicAndNodeNameDic;
        }

        public void createFile2(Dictionary<string, string> dic)
        {
            FileStream fs = new FileStream(FILE_NAME2, FileMode.Create);
            using (StreamWriter bw = new StreamWriter(fs))
            {
                foreach (KeyValuePair<string, string> kvp in dic)
                {
                    bw.Write(kvp.Key + ":" + kvp.Value + "\n");
                }
            }
        }

        public Dictionary<string, string> readEpicAndInstrumentNameFileTo()
        {
            string filePath = System.Environment.CurrentDirectory + @"\epicAndInstrumentName.txt";
            FileStream fs = new FileStream(filePath, FileMode.Open);
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.EndOfStream != true)
                    {
                        string str = sr.ReadLine();
                        string[] epicAndInstrumentName = str.Split(':');
                        epicAndInstructmentDic.Add(epicAndInstrumentName[0], epicAndInstrumentName[1]);
                    }
                }

            }
            return epicAndInstructmentDic;
        }

        public void createErrorFile(List<string> list)
        {
            FileStream fs = new FileStream(FILE_NAME3, FileMode.Create);
            using (StreamWriter bw = new StreamWriter(fs))
            {
                foreach (var e in list)
                {
                    bw.Write(e + "\n");
                }
            }
        }

        public List<string> readErrorFileTo()
        {
            string filePath = System.Environment.CurrentDirectory + @"\errorEpicsLog.txt";
            FileStream fs = new FileStream(filePath, FileMode.Open);
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.EndOfStream != true)
                    {
                        string str = sr.ReadLine();
                        errorList.Add(str);
                    }
                }

            }
            return errorList;
        }

    }
}
