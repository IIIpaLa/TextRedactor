namespace TextEditor
{
    public partial class Form1 : Form
    {
        Editor defaultWindow;        

        public Form1()
        {
            InitializeComponent();
            defaultWindow = new Editor();
            defaultWindow.Parent = panel1;//прив€зка к элементу формы
            defaultWindow.Dock = DockStyle.Fill;//раст€гиевает под размеры
            defaultWindow.Recent2.Change += ChangeInRDL;
            defaultWindow.Recent2.LoadData("test.txt");       
        }       

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultWindow.New();            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultWindow.Open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultWindow.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            defaultWindow.SaveAs();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultWindow.CloseDocument();  
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            while (defaultWindow.TabCount != 0)
            {
                defaultWindow.SelectedTab = defaultWindow.TabPages[0];
                defaultWindow.CloseDocument();
            }
            defaultWindow.Recent2.SaveData("test.txt");
            this.Close();     
        }              

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
                       
        }

        private void ChangeInRDL()
        {
            recent.DropDownItems.Clear();           
            for (int i = 0; i < defaultWindow.Recent2.Count; i++)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = defaultWindow.Recent2[i];
                recent.DropDownItems.Add(menu);
                menu.Click += Menu_Click;
            }

        }

        private void Menu_Click(object? sender, EventArgs e)
        {
            defaultWindow.OpenAs(((ToolStripMenuItem)sender).Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (defaultWindow.TabCount != 0)
            {
                defaultWindow.SelectedTab = defaultWindow.TabPages[0];
                defaultWindow.CloseDocument();
            }
            defaultWindow.Recent2.SaveData("test.txt");
        }
    }
}