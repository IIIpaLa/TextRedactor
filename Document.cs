using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextEditor
{
    internal class Document: TabPage
    {
        public static string globalpathToTestTXT = "D:\\УЧЁБА\\Технологии программирования\\5 семестр\\TextEditor" + "\\";

        private TextBox tb = new TextBox();
        public bool Modified;
        public string path;

        private void OnModify(object sender, EventArgs e)
       {
            Modified = true;
            if (!this.Text.Contains("(*)"))
                this.Text = this.Text + "(*)";
        }

        public Document(string pageName)
        {                      
            tb.Multiline = true;
            tb.Parent = this;//к текущей вкладке добавляется TextBox      
            tb.Dock = DockStyle.Fill;
            tb.TextChanged += OnModify;

            this.Text = pageName;            
        }        

        public void Open(string fileName)
        {
            this.path = fileName;
            string text = File.ReadAllText(fileName);                                                                
            this.tb.Text = text;
            if ( fileName.Contains("\\") )
                this.Text = trim(fileName);
            else
                this.Text = fileName;

            Modified = false;
        }
        public void Save()
        {
            try
            {
                System.IO.File.WriteAllText(path, this.tb.Text); 
                Modified = false;
                if (this.Text.Contains("(*)"))
                    this.Text = this.Text.Remove(this.Text.Length - 3);//убрали (*)
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось сохранить файл!");
            }

        }
        public void SaveAs(string fileName) 
        {            
            try
            {
                System.IO.File.WriteAllText(fileName, this.tb.Text);
                this.Text = trim(fileName);
                this.path = fileName;
                Modified = false;
                if (this.Text.Contains("(*)"))
                    this.Text = this.Text.Remove(this.Text.Length - 3);//убрали (*)
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось сохранить файл!");
            }

        }
        
        public string trim(string fileName)
        {
            int pos = fileName.LastIndexOf("\\");             
            return (fileName.Substring(pos)).Trim(new char[] { '\\' });
        }


    }
}
