using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;

namespace Lab16
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ShowData(openFileDialog1.FileName);
            }
        }

        private void ShowData(String datapath)
        {
            DataStorage data = new DataStorage();
            try
            {
                data = DataStorage.DataCreator(datapath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("При загрузке данных что-то сломалось");
            }

            dgvSummary.DataSource = data.GetSummaryData();
            dgvSummary.ReadOnly = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataStorage data;

            try
            {
                data = DataStorage.DataCreator(openFileDialog1.FileName);
                RawDataItem nn = new RawDataItem();
                nn.Name = textBox1.Text;
                nn.fLine = textBox5.Text;
                nn.id = Convert.ToInt32(textBox2.Text);
                nn.date = textBox3.Text;
                if (textBox4.Text == "УДАЛИТЬ" || textBox4.Text == "REMOVE")
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.UTF8);
                    String line;
                    List<RawDataItem> rawdata;
                    rawdata = new List<RawDataItem>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] items = line.Split('*');
                        var item = new RawDataItem()
                        {
                            Name = items[0].Trim(),
                            fLine = items[1].Trim(),
                            date = items[2].Trim(),
                            id = Convert.ToInt32(items[3].Trim()),
                        };
                        rawdata.Add(item);
                    }
                    sr.Close();

                    using (StreamWriter sw = new StreamWriter(openFileDialog1.FileName, false, System.Text.Encoding.UTF8))
                    {
                        foreach (var item in rawdata)
                            if(item.date != nn.date || item.Name != nn.Name || item.id != nn.id || item.fLine != nn.fLine)
                                sw.WriteLine(item.Name + " * " + item.fLine + " * " + item.date + " * " + Convert.ToString(item.id));
                    }
                    ShowData(openFileDialog1.FileName);
                }
                else if (textBox4.Text == "ДОБАВИТЬ" || textBox4.Text == "ADD")
                {
                    using (StreamWriter sw = new StreamWriter(openFileDialog1.FileName, true, System.Text.Encoding.UTF8))
                    {
                        string s1 = nn.Name + " * " + nn.fLine + " * " + nn.date + " * " + Convert.ToString(nn.id);
                        sw.WriteLine(s1);
                    }
                    ShowData(openFileDialog1.FileName);
                }
                else
                    MessageBox.Show("Вы неправильно ввели действие");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте корректность данных");
            }         
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text[0] == 'S')
            {
                button2.Text = "Русский";
                btnLoad.Text = "Load data";
                button3.Text = "Version for the visually impaired";
                label1.Text = "Author name";
                label2.Text = "id";
                label3.Text = "Date";
                label5.Text = "Title";
                label4.Text = "Enter 'REMOVE' or 'ADD'";
            }
            else
            {
                button2.Text = "Switch to English";
                btnLoad.Text = "Загрузить данные";
                button3.Text = "Версия для слабовидящих";
                label1.Text = "Имя автора";
                label2.Text = "id публикации";
                label3.Text = "Дата публикации";
                label5.Text = "Название публикации";
                label4.Text = "'УДАЛИТЬ' или 'ДОБАВИТЬ'";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.BackColor != System.Drawing.Color.Yellow)
            {
                this.BackColor = System.Drawing.Color.Yellow;
                this.btnLoad.BackColor = System.Drawing.Color.White;
                this.button1.BackColor = System.Drawing.Color.White;
                this.button2.BackColor = System.Drawing.Color.White;
                this.button3.BackColor = System.Drawing.Color.White;
                label1.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                label2.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                label3.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                label4.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                label5.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            }
            else
            {
                this.BackColor = System.Drawing.Color.WhiteSmoke;
                this.btnLoad.BackColor = System.Drawing.Color.Transparent;
                this.button1.BackColor = System.Drawing.Color.Transparent;
                this.button2.BackColor = System.Drawing.Color.Transparent;
                this.button3.BackColor = System.Drawing.Color.Transparent;
                label1.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                label2.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                label3.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                label4.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                label5.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            }
        }
    }
}
