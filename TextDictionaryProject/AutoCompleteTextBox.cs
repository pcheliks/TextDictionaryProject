using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TextDictionaryProject.Services;

namespace TextDictionaryProject
{
    public class AutoCompleteTextBox : TextBox
    {
        private ListBox listBox;
        private bool isAdded;
        private String[] values;
        private String formerValue = String.Empty;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            ResetListBox();
        }

        private void InitializeComponent()
        {
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(120, 95);
            this.listBox.TabIndex = 0;
            // 
            // AutoCompleteTextBox
            // 
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AutoCompleteTextBox_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AutoCompleteTextBox_KeyUp);
            this.ResumeLayout(false);

        }

        private void ShowListBox()
        {
            if (!isAdded)
            {
                Parent.Controls.Add(listBox);
                listBox.Left = Left;
                listBox.Top = Top + Height;
                isAdded = true;
            }
            listBox.Visible = true;
            listBox.BringToFront();
        }

        private void ResetListBox()
        {
            listBox.Visible = false;
        }


        private void DeleteWhiteSpace()
        {
            var text = Text;
            var position = SelectionStart;
            if (text.EndsWith(" ") && text[position - 2] != ' ')
            {
                var posEnd = text.LastIndexOf(' ', (position < 1) ? 0 : position - 1);
                Text = text.Substring(0, posEnd);
                SelectionStart = Text.Length;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == formerValue) return;
            formerValue = Text;
            var word = GetWord();

            if (values != null && word.Length > 0)
            {
                var matches = DictionaryService.GetWords(word);
                if (matches.Count > 0)
                {
                    ShowListBox();
                    listBox.Items.Clear();

                    foreach (var match in matches)
                        listBox.Items.Add(match);

                    listBox.SelectedIndex = 0;
                    listBox.Height = 0;
                    listBox.Width = 0;
                    Focus();
                    using (Graphics graphics = listBox.CreateGraphics())
                    {
                        for (var i = 0; i < listBox.Items.Count; i++)
                        {
                            listBox.Height += listBox.GetItemHeight(i);
                            // it item width is larger than the current one
                            // set it to the new max item width
                            // GetItemRectangle does not work for me
                            // we add a little extra space by using '_'
                            var itemWidth = (int)graphics.MeasureString(((String)listBox.Items[i]) + "_", listBox.Font).Width;
                            listBox.Width = (listBox.Width < itemWidth) ? itemWidth : listBox.Width;
                        }
                    }
                }
                else
                {
                    ResetListBox();
                }
            }
            else
            {
                ResetListBox();
            }
        }

        private String GetWord()
        {
            var text = Text;
            var pos = SelectionStart;

            var posStart = text.LastIndexOf(' ', (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            var posEnd = text.IndexOf(' ', pos);
            posEnd = (posEnd == -1) ? text.Length : posEnd;

            var length = ((posEnd - posStart) < 0) ? 0 : posEnd - posStart;

            return text.Substring(posStart, length);
        }

        private void InsertWord(String newTag)
        {
            var text = Text;
            var pos = SelectionStart;

            var posStart = text.LastIndexOf(' ', (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            var posEnd = text.IndexOf(' ', pos);

            var firstPart = text.Substring(0, posStart) + newTag;
            var updatedText = firstPart + ((posEnd == -1) ? "" : text.Substring(posEnd, text.Length - posEnd)) + " ";


            Text = updatedText;
            SelectionStart = updatedText.Length;
        }

        public String[] Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }

        public List<String> SelectedValues
        {
            get
            {
                var result = Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new List<String>(result);
            }
        }

        private void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    {
                        if (listBox.Visible)
                        {
                            InsertWord((String)listBox.SelectedItem);
                            ResetListBox();
                            formerValue = Text;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if ((listBox.Visible) && (listBox.SelectedIndex < listBox.Items.Count - 1))
                            listBox.SelectedIndex++;

                        break;
                    }
                case Keys.Up:
                    {
                        if ((listBox.Visible) && (listBox.SelectedIndex > 0))
                            listBox.SelectedIndex--;

                        break;
                    }
                case Keys.Oemcomma:
                    {
                        DeleteWhiteSpace();
                        break;
                    }
                case Keys.OemPeriod:
                    {
                        DeleteWhiteSpace();
                        break;
                    }
            }
        }

        private void AutoCompleteTextBox_KeyUp(object sender, KeyEventArgs e)
        {
                UpdateListBox();
        }
    }
}