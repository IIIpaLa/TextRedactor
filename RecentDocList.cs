using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    internal class RecentDocList
    {
        List<string> recentFiles = new List<string>();

        public delegate void ChangeHandler();
        public event ChangeHandler Change;

        public void Remove(string s)
        {
            recentFiles.Remove(s);
        }

        public void SaveData(string fileName)
        {
            string path = Document.globalpathToTestTXT + fileName;
            StreamWriter sw = new StreamWriter(path, false);
            foreach (string file in recentFiles)
                sw.WriteLine(file);
            sw.Close();
        }

        public void LoadData(string fileName)
        {
            //string path = Assembly.GetExecutingAssembly().Location+"\\" + fileName;//текущее расположение            
            string path = Document.globalpathToTestTXT + fileName;
            string line = "";
            StreamReader sr = new StreamReader(path);
            while (line != null)
            {
                line = sr.ReadLine();
                if (line != null)                
                    recentFiles.Add(line);                
            }
            sr.Close();
            Change?.Invoke();
        }

        public void Add(string filePath)
        {
            List<string> tmp = new List<string>();
            if (recentFiles.Count == 8)
            {
                for (int i = 1; i < recentFiles.Count; i++)
                    tmp.Add(recentFiles[i]);
                recentFiles.Clear();
                recentFiles.AddRange(tmp);  
            }            

            recentFiles.Add(filePath);
            Change?.Invoke();
        }

        public string this[int i] 
        {
            get
            {
                if (i < Count && i >= 0)
                    return recentFiles[i];
                
                Console.WriteLine("Некорректный индекес, при получении значения элемента");
                return "";
            }
        }
                
        public int Count { get { return recentFiles.Count; } }

        string trim(string fileName)
       {
            int pos = fileName.LastIndexOf("\\");             
            return (fileName.Substring(pos)).Trim(new char[] { '\\' });
       } 


    }
}
