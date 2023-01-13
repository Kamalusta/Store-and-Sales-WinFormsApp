using System;
using System.Data.SQLite;
using System.Windows.Forms;
using USB_Barcode_Scanner;

namespace yeni_proyekt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            BarcodeScanner barcode = new BarcodeScanner(textBox1);
            barcode.BarcodeScanned += Barcode_BarcodeScanned;
        }

        Form1 frm1 = new Form1();
        SQLiteConnection sqlelaqe = new SQLiteConnection(@"Data Source = C:\Users\ekber\Desktop\kohne komp screen\Anbar.db; Version=3");

        private void Barcode_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBox1.Text = e.Barcode;
            sqlelaqe.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza WHERE Barcode= @Barcode", sqlelaqe);
            cmd.Parameters.AddWithValue("@Barcode", textBox1.Text);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = reader["Ad"].ToString();
                textBox5.Text = reader["Qsatis"].ToString();
            }
            if (reader.StepCount == 0)
            {
                textBox2.Clear();
                textBox4.Clear();
            }
            sqlelaqe.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            listView2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView2.Visible = true;
            flowLayoutPanel1.Visible = false;
            listView2.Items.Clear();
            sqlelaqe.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza", sqlelaqe);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem();
                item.Text = reader[0].ToString();
                item.SubItems.Add(reader[1].ToString());
                item.SubItems.Add(reader[2].ToString());
                listView2.Items.Add(item);
            }
            sqlelaqe.Close();

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string g = textBox1.Text;

            sqlelaqe.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza WHERE Barcode = @Barcode ", sqlelaqe);
            cmd.Parameters.AddWithValue("@Barcode", g);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = reader["Ad"].ToString();
                textBox4.Text = reader["Qsatis"].ToString();
            }
            if (reader.StepCount == 0)
            {
                textBox2.Clear();
                textBox4.Clear();
            }
            sqlelaqe.Close();
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            {
                listView2.Items.Clear();
                char[] girilen = textBox7.Text.ToCharArray();
                if (girilen.Length == 1)
                {
                    sqlelaqe.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
                    cmd.Parameters.AddWithValue("@girlen", girilen[0] + "%");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem items = new ListViewItem(reader[0].ToString());
                        items.SubItems.Add(reader[1].ToString());
                        items.SubItems.Add(reader[2].ToString());
                        listView2.Items.Add(items);
                    }
                    sqlelaqe.Close();
                }
                else if (girilen.Length == 2)
                {
                    string gir = girilen[0].ToString() + girilen[1].ToString();

                    sqlelaqe.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
                    cmd.Parameters.AddWithValue("@girlen", gir + "%");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem items = new ListViewItem(reader[0].ToString());
                        items.SubItems.Add(reader[1].ToString());
                        items.SubItems.Add(reader[2].ToString());
                        listView2.Items.Add(items);
                    }
                    sqlelaqe.Close();
                }
                else if (girilen.Length > 2)
                {
                    string gir = girilen[0].ToString() + girilen[1].ToString();

                    sqlelaqe.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Qsatis FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
                    cmd.Parameters.AddWithValue("@girlen", gir + girilen[2].ToString() + "%");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem items = new ListViewItem(reader[0].ToString());
                        items.SubItems.Add(reader[1].ToString());
                        items.SubItems.Add(reader[2].ToString());
                        listView2.Items.Add(items);
                    }
                    sqlelaqe.Close();
                }

                else
                {
                    sqlelaqe.Open();
                    SQLiteCommand emr = new SQLiteCommand("Select Barcode,Ad,Qsatis From Baza ", sqlelaqe);
                    SQLiteDataReader reader = emr.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem listitem = new ListViewItem(reader[0].ToString());
                        listitem.SubItems.Add(reader[1].ToString());
                        listitem.SubItems.Add(reader[2].ToString());
                        listView2.Items.Add(listitem);
                    }
                    sqlelaqe.Close();
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listView2.FocusedItem.Text.ToString();
            textBox2.Text = listView2.FocusedItem.SubItems[1].Text.ToString();
            textBox4.Text = listView2.FocusedItem.SubItems[2].Text.ToString();

        }

        private void button24_Click(object sender, EventArgs e)
        {
            bool girilen = int.TryParse(textBox3.Text, out int num);
            if (num > 0)
            {

                string bc = textBox1.Text.ToString();
                string ad = textBox2.Text.ToString();
                int mq = Convert.ToInt32(textBox3.Text);
                decimal qt = decimal.Parse(textBox4.Text);
                decimal mb = mq * qt;
                decimal umb = 0;
                string[] sutunlar = { bc, ad, mq.ToString(), qt.ToString(), mb.ToString() };
                dataGridView1.Rows.Add(sutunlar);
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    umb += decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                }
                label5.Text = umb.ToString("#.00");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            else
            {
                MessageBox.Show("Miqdar daxil edilmemişdir !");
            }

            textBox3.Select();
        }

        private void button26_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string bc = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string ad = dataGridView1.Rows[i].Cells[1].Value.ToString();
                int mq = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());

                sqlelaqe.Open();
                SQLiteCommand cmd = new SQLiteCommand("UPDATE Baza SET Miqdar = Miqdar - @Miqdar1 WHERE Barcode = @Barcode1", sqlelaqe);
                cmd.Parameters.AddWithValue("@Miqdar1", mq);
                cmd.Parameters.AddWithValue("@Barcode1", bc);
                cmd.ExecuteNonQuery();
                SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO alsat (Barcode, Ad, Miqdar, Qalis, Qsatis, Tarix, Qaime) VALUES (@barcode,@ad,@miqdar,@qalis,@qsatis,@tarix,@qaime)", sqlelaqe);
                cmd2.Parameters.AddWithValue("@barcode", bc);
                cmd2.Parameters.AddWithValue("@ad", ad);
                cmd2.Parameters.AddWithValue("@miqdar", mq);
                cmd2.Parameters.AddWithValue("@qalis", qa);
                cmd2.Parameters.AddWithValue("@qsatis", qs);
                cmd2.Parameters.AddWithValue("@tarix", DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss"));
                cmd2.Parameters.AddWithValue("@qaime", "satis");
                // cmd2.Parameters.AddWithValue("@qaime", "");
                cmd2.ExecuteNonQuery();                
                sqlelaqe.Close();
            }

            dataGridView1.Rows.Clear();
            MessageBox.Show("Satış yerinə yetrildi");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectedRows.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox3.Select();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // button25.Text = button25.Text.Substring(1) + button25.Text.Substring(0,1);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            // timer1.Start();
        }
    }
}
