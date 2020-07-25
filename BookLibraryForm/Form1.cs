using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace BookLibraryForm
{
    public partial class Form1 : Form
    {
        public List<Book> bookList = new List<Book>();

        public Form1()
        {
            InitializeComponent();
            ListViewSetup();
            updateList();
        }

        public void ListViewSetup()
        {
            listView1.Columns.Add("Title", 200);
            listView1.Columns.Add("Author", 150);
            listView1.Columns.Add("Location", 100);
            listView1.Columns.Add("Status", 100);

        }

        public void updateList()
        {
            listView1.Items.Clear();
            foreach (Book T in bookList)
            {
                /*ListViewItem bookItem = new ListViewItem();
                bookItem.SubItems.Add(T.title);
                bookItem.SubItems.Add(T.author);
                bookItem.SubItems.Add(T.status);
                bookItem.SubItems.Add(T.location);
                listView1.Items.Add(bookItem);*/
                string[] bookItem = { T.title, T.author, T.location, T.status };
                var listViewItem = new ListViewItem(bookItem);
                listView1.Items.Add(listViewItem);

            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            bookList.RemoveAt(listBox1.SelectedIndex - 1);
            titleBox.Text = "Title...";
            authorBox.Text = "Author...";
            locationBox.Text = "Location...";
            statusBox.Text = "Status...";
            updateList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = listBox1.SelectedIndex <= 0 ? false : true;
            addButton.Enabled = listBox1.SelectedIndex > 0 ? false : true;
            if(listBox1.SelectedIndex > 0)
            {
                titleBox.Text = bookList[listBox1.SelectedIndex - 1].title;
                locationBox.Text = bookList[listBox1.SelectedIndex - 1].location;
                statusBox.Text = bookList[listBox1.SelectedIndex - 1].status;
            } else
            {
                titleBox.Text = "Title...";
                authorBox.Text = "Author...";
                locationBox.Text = "Location...";
                statusBox.Text = "Status...";
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {

                bookList[listBox1.SelectedIndex - 1].title = titleBox.Text;
                bookList[listBox1.SelectedIndex - 1].author = authorBox.Text;
                bookList[listBox1.SelectedIndex - 1].location = locationBox.Text;
                bookList[listBox1.SelectedIndex - 1].status = statusBox.Text;
                listBox1.Items[listBox1.SelectedIndex] = bookList[listBox1.SelectedIndex - 1].toString();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    using (TextWriter tw = new StreamWriter(filePath))
                    {
                        foreach (Book T in bookList)
                        {
                            tw.WriteLine(T.toString());
                        }
                    }
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            bookList.Clear();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (true)
                        {
                            fileContent = reader.ReadLine();
                            if(fileContent != null)
                            {
                                string[] data = System.Text.RegularExpressions.Regex.Split(fileContent, @" \| ");
                                bookList.Add(new Book(data[0], data[1], data[2], data[3]));
                            } else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            updateList();

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            bool duplicate = false;
            foreach(Book T in bookList)
            {
                if(T.title == titleBox.Text)
                {
                    duplicate = true;
                    break;
                }
            }
            if (!duplicate)
            {
                if (!(titleBox.Text == "Title..." && authorBox.Text == "Author..." && locationBox.Text == "Location..." &&  statusBox.Text == "Status..."))
                    {
                    bookList.Add(new Book(titleBox.Text, authorBox.Text, locationBox.Text, statusBox.Text));
                    updateList();
                    }
            }
        }

        private void titleBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void locationBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void statusBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
