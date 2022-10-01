using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    internal class Editor: TabControl
    {
        private RecentDocList recent2 = new RecentDocList();
        public RecentDocList Recent2
        {
            get { return recent2; }           
        }

        public void New()
        {
            int n = this.TabCount + 1;
            string s = "Page " +n;

            Document page = new Document(s);//новая вкладка
            this.TabPages.Add(page); //вызывающему объекту добавляем страничку   
        }
        public void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "text files(*.txt)|*.txt|all files(*.*)|*.*";
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filename = ofd.FileName;

                for (int i = 0; i < this.TabCount; i++)
                {
                    Document page = (Document)this.TabPages[i];
                    if (filename == page.path & page.Modified == true)
                    {
                        string warn = "Файл уже открыт. Сохранить изменения в:'" + page.Text + "' ?";
                        DialogResult dialogResult = MessageBox.Show(warn, "Предупреждение", MessageBoxButtons.YesNoCancel);
                        if (dialogResult == DialogResult.Cancel)
                            return;

                        if (dialogResult == DialogResult.Yes)
                            Save();
                        this.TabPages.Remove(TabPages[i]);
                        break;
                    }
                    else
                    {
                        if (filename == page.path & page.Modified == false)
                        {
                            this.SelectedTab = this.TabPages[i];
                            return;   
                        }
                    }


                }

                this.New(); 
                this.SelectedTab = this.TabPages[this.TabCount - 1];//вкладка становится активной
                Document tmp = (Document)this.SelectedTab;
                tmp.Open(filename);
                Recent2.Add(filename);
            }
        }
        public void Save()
        {
            if (this.TabPages.Count != 0)
            {
                Document tmp = (Document)this.SelectedTab;
                if (tmp.path == null & tmp.Modified)
                {
                    this.SaveAs();
                }
                else
                {
                    if (tmp.Modified)
                        tmp.Save();
                }
            }    
        }
        public void SaveAs()
        {
            Document tmp2 = (Document)this.SelectedTab;
            if (this.TabPages.Count != 0 & tmp2.Modified)
            { 
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                DialogResult result = saveFileDialog1.ShowDialog();
           
                if (result == DialogResult.OK)
                {      
                    string pathName = saveFileDialog1.FileName;               
                    Document tmp = (Document)this.SelectedTab;
                    tmp.SaveAs(pathName);
                    Recent2.Add(pathName);                   
                }
            }
        }
        public void CloseDocument()
        {
            if (this.TabPages.Count != 0)
            {
                Document tmp = (Document)this.SelectedTab;
                if (tmp.Modified)
                {
                    string warn = "Сохранить файл :'" + tmp.Text + "' ?";
                    
                    if (MessageBox.Show(warn, "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.Yes)                    
                        Save();                    
                }
                this.TabPages.Remove(this.SelectedTab);
            }
        }
       

        public void OpenAs(string fileName)
        {                       
            try
            {
                for (int i = 0; i < this.TabCount; i++)
                {
                    Document page = (Document)this.TabPages[i];
                    if (fileName == page.path & page.Modified == true)
                    {
                        string warn = "Файл уже открыт. Сохранить изменения в:'" + page.Text + "' ?";
                        DialogResult dialogResult = MessageBox.Show( warn, "Предупреждение", MessageBoxButtons.YesNoCancel);
                        if (dialogResult == DialogResult.Cancel)
                            return;

                        if (dialogResult == DialogResult.Yes)
                            Save();
                        this.TabPages.Remove(TabPages[i]);
                        break;
                    }
                    else
                    {
                        if (fileName == page.path & page.Modified == false)
                        {
                            Recent2.Remove(fileName);
                            Recent2.Add(fileName);

                            this.SelectedTab = this.TabPages[i];
                            return;
                        }
                    }

                }

                this.New();
                Document tmp = (Document)this.TabPages[this.TabCount - 1];

                if (System.IO.File.Exists(fileName))
                {
                    tmp.Open(fileName);
                    Recent2.Remove(fileName);
                    Recent2.Add(fileName);
                }
                else
                    MessageBox.Show("Указанный файл не существует!");
            }
            catch (Exception ex) { MessageBox.Show("По указанному пути не удалось открыть файл!"); }
        }           
    }
}
