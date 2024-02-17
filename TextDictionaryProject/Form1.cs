using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextDictionaryProject.Repositories;
using TextDictionaryProject.Services;

namespace TextDictionaryProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            autoCompleteTextBox1.Values = new string[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        // Обработчик события Click для создания словаря
        private async void созданиеСловаряToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var path = openFileDialog1.FileName;
            DictionaryService.CreateDictionary(path);
        }

        // Обработчик события Click для обновления словаря
        private async void обновлениеСловаряToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var path = openFileDialog1.FileName;
            DictionaryService.UpdateDictionary(path);
        }

        // Обработчик события Click для удаления словаря
        private async void удалениеСловаряToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            DictionaryService.CleanDictionary();
        }

    }

}
