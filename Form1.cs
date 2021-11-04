using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notebooks
{
    public partial class mainForm : Form
    {
        public string filename;
        public bool FileChanged;

        public mainForm()
        {
            InitializeComponent();
            init();
            StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#0F3460");
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt";
        }

        public void init()
        {
            filename = "";
            FileChanged = false;
            UpdateText();
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!FileChanged)
            {
                this.Text = this.Text.Replace('*', ' ');
                FileChanged = true;
                this.Text = "*" + this.Text;
            }
        }

        public void UpdateText()
        {
            if (filename != "")
                this.Text = filename + " - Notebook";
            else
                this.Text = "Noname - Notebook";
        }

        private void AboutProg(object sender, EventArgs e)
        {
            aboutProg ab = new aboutProg();
            ab.Show();
        }

        private void CreateNewDoc(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            filename = "";
            textBox1.Text = "";
            FileChanged = false;
            
        }

        private void SaveFile(string _filename)
        {
            if(_filename == "")
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                writer(_filename);
            }
            catch
            {
                MessageBox.Show("Невохможно сохранить файл!");
            }
            UpdateText();
        }

        private void writer(string _filename)
        {
            StreamWriter sw = new StreamWriter(_filename);
            sw.Write(textBox1.Text);
            sw.Close();
            filename = _filename;
            FileChanged = false;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    FileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
            UpdateText();
        }
        public void Save(object sender, EventArgs e)
        {
            SaveFile(filename);
        }

        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }

        public void SaveUnsavedFile()
        {
            if (FileChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Сохранение файла", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(result == DialogResult.Yes)
                {
                    SaveFile(filename);
                    UpdateText();
                }
                if(result == DialogResult.Cancel)
                {
                    writer(filename);
                }

            }
        }
        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }
        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);

        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length - textBox1.SelectionStart);
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            CutText();
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClose(object sender, FormClosingEventArgs e)
        {
            if (FileChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Сохранение файла", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }
    }
}
