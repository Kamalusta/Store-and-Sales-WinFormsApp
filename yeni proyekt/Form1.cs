using DGVPrinterHelper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using USB_Barcode_Scanner;

namespace yeni_proyekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BarcodeScanner barcode = new BarcodeScanner(textBox1);
            barcode.BarcodeScanned += Barcode_BarcodeScanned;
        }
        SQLiteConnection sqlelaqe = new SQLiteConnection(@"Data Source = C:\Users\ekber\Desktop\kohne komp screen\Anbar.db; Version=3");
        SQLiteConnection sqlelaqe2 = new SQLiteConnection(@"Data Source = C:\Users\ekber\Desktop\kohne komp screen\Medaxildb.db; Version=3");
        SQLiteConnection sqlelaqe3 = new SQLiteConnection(@"Data Source = C:\Users\ekber\Desktop\kohne komp screen\Mexaricdb.db; Version=3");

        DataTable table = new DataTable();
        string[] row;
        int secilen;
        Boolean medaxilbool = false;
        Boolean mexaricbool = false;

        //public void SenedHesabD()
        //{
        //    senedlerD = Directory.GetFiles(@"C:\Anbar app\medaxil", "*.txt");
        //    for (int i = 0; i < senedlerD.Length; i++)
        //    {
        //        DirectoryInfo snd = new DirectoryInfo(senedlerD[i]);
        //        sndAd = new string[senedlerD.Length];
        //        sndAd[i] = snd.Name;
        //        sndUnvan = new string[senedlerD.Length];
        //        sndUnvan[i] = snd.FullName;
        //    }
        //}
        //public void siyahilaX()
        //{
        //    senedlerX = Directory.GetFiles(@"C:\Anbar app\mexaric\", "*.txt");
        //    DirectoryInfo snd = new DirectoryInfo(senedlerX[senedlerX.Length - 1]);
        //    listView1.Items.Add(snd.Name);
        //}
        //public void SenedHesabX()
        //{
        //    senedlerX = Directory.GetFiles(@"C:\Anbar app\mexaric", "*.txt");
        //    for (int i = 0; i < senedlerX.Length; i++)
        //    {
        //        DirectoryInfo snd = new DirectoryInfo(senedlerX[i]);
        //        sndAd = new string[senedlerX.Length];
        //        sndAd[i] = snd.Name;
        //        sndUnvan = new string[senedlerX.Length];
        //        sndUnvan[i] = snd.FullName;
        //    }
        //}
        public void cemhesabla()
        {
            float cem2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                float cem = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                cem2 += cem;
            }
            label15.Text = cem2.ToString() + "AZN";
        }
        public void cemhesablaalis()
        {
            float cem2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                float cem = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                cem2 += cem;
            }
            label17.Text = cem2.ToString() + "AZN";
        }
        public void txbxtemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        private void Barcode_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBox1.Text = e.Barcode;
            sqlelaqe.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar, Qalis, Qsatis FROM Baza WHERE Barcode= @Barcode", sqlelaqe);
            cmd.Parameters.AddWithValue("@Barcode", textBox1.Text);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = reader["Ad"].ToString();
                textBox3.Text = reader["Miqdar"].ToString();
                textBox4.Text = reader["Qalis"].ToString();
                textBox5.Text = reader["Qsatis"].ToString();
            }
            if (reader.StepCount == 0)
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            sqlelaqe.Close();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            //if (!File.Exists(@"c:\Anbar app\baza\baza.txt"))
            //{
            //    string qovluq = @"c:\Anbar app\baza";
            //    string qovluq1 = @"d:\Anbar app\baza";
            //    string qovluq2 = @"c:\Anbar app\medaxil";
            //    string qovluq3 = @"c:\Anbar app\mexaric";
            //    Directory.CreateDirectory(qovluq);
            //    Directory.CreateDirectory(qovluq1);
            //    Directory.CreateDirectory(qovluq2);
            //    Directory.CreateDirectory(qovluq3);
            //    string file = @"c:\Anbar app\baza\baza.txt";
            //    File.Create(file);
            //}
            button2.Visible = false;
            button17.Visible = false;
            button1.Visible = false;
            button16.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button11.Visible = false;
            button7.Visible = false;
            button15.Visible = false;
            button12.Visible = false;
            listView2.Visible = false;
            listView1.Visible = false;
            textBox6.Visible = false;
            label5.Visible = false;
            label19.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;

            table.Columns.Add("S/n", typeof(int));
            table.Columns.Add("BarCode", typeof(string));
            table.Columns.Add("Adı", typeof(string));
            table.Columns.Add("Miqdarı", typeof(int));
            table.Columns.Add("Qiymət (alış)", typeof(float));
            table.Columns.Add("Qiymət (satış)", typeof(float));
            table.Columns.Add("Cəmi Məbləğ (alış)", typeof(float));
            table.Columns.Add("Cəmi Məbləğ (satış)", typeof(float));

            dataGridView1.DataSource = table;
            label13.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textBox1.Select();
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 135;
            dataGridView1.Columns[7].Width = 135;

            listView2.Columns.Add("Barcode", 100);
            listView2.Columns.Add("Adı", 300);
            listView2.Columns.Add("Miqdar", 100);
            listView2.View = View.Details;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            decimal sts, als, cms, cma;
            try
            {
                table.Clear();
                sqlelaqe.Open();
                SQLiteCommand cmd = new SQLiteCommand(" SELECT *FROM Baza ", sqlelaqe);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow row1 = table.NewRow();
                    row1["BarCode"] = reader["Barcode"];
                    row1["Adı"] = reader["Ad"];
                    row1["Miqdarı"] = reader["Miqdar"];
                    row1["Qiymət (alış)"] = reader["Qalis"];
                    row1["Qiymət (satış)"] = reader["Qsatis"];
                    row1["Cəmi Məbləğ (alış)"] = Convert.ToDecimal(reader["Qalis"].ToString()) * Convert.ToInt32( reader["Miqdar"].ToString());
                    row1["Cəmi Məbləğ (satış)"] = Convert.ToDecimal(reader["Qsatis"].ToString()) * Convert.ToInt32(reader["Miqdar"].ToString());
                    table.Rows.Add(row1);
                }
                dataGridView1.DataSource = table;
                sqlelaqe.Close();

                sirasay();
                cemhesabla();
                cemhesablaalis();
            }
            catch (Exception xeta)
            {
                MessageBox.Show(xeta.ToString());
            }
        }
        public void sirasay()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int sayi;
                sayi = i + 1;
                dataGridView1.Rows[i].Cells[0].Value = sayi;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Select();
            row = new string[8];
            try
            {
                int mqd = Convert.ToInt32(textBox3.Text);
                decimal qymtSts = decimal.Parse(textBox5.Text);
                decimal qymtAls = decimal.Parse(textBox4.Text);
                decimal cemSts = mqd * qymtSts;
                decimal cemAls = mqd * qymtAls;

                row[1] = textBox1.Text;
                row[2] = textBox2.Text;
                row[3] = textBox3.Text;
                row[4] = textBox4.Text;
                row[5] = textBox5.Text;
                row[6] = cemAls.ToString();
                row[7] = cemSts.ToString();
                table.Rows.Add(row);
            }
            catch (Exception)
            {
                MessageBox.Show("Melumatlari duzgun doldur");
            }
            txbxtemizle();
            cemhesabla();
            cemhesablaalis();
            sirasay();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (dataGridView1.SelectedRows.Count > 0 && secilen >= 0)
            {
                int mqd = Convert.ToInt32(textBox3.Text);
                float qymtSts = float.Parse(textBox5.Text);
                float qymtAls = float.Parse(textBox4.Text);
                float cemSts = mqd * qymtSts;
                float cemAls = mqd * qymtAls;
                dataGridView1.Rows[secilen].Cells[1].Value = textBox1.Text;
                dataGridView1.Rows[secilen].Cells[2].Value = textBox2.Text;
                dataGridView1.Rows[secilen].Cells[3].Value = textBox3.Text;
                dataGridView1.Rows[secilen].Cells[4].Value = textBox4.Text;
                dataGridView1.Rows[secilen].Cells[5].Value = textBox5.Text;
                dataGridView1.Rows[secilen].Cells[7].Value = cemSts.ToString();
                dataGridView1.Rows[secilen].Cells[6].Value = cemAls.ToString();
                cemhesabla();
                cemhesablaalis();
            }
            //}
            //catch (Exception xeta1)
            //{
            //    MessageBox.Show(xeta1.ToString());
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (secilen >= 0)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(secilen);
                }
                catch (Exception)
                { }
                cemhesabla();
                cemhesablaalis();
            }
        }
        Boolean mdxl = false;
        Boolean mxrc = false;

        
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    mdxl = true;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string barcod = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        int m = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        //decimal ca = decimal.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        //decimal cs = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                        sqlelaqe.Open();
                        SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO Baza(Barcode, Ad, Miqdar, Qalis, Qsatis) " +
                            "VALUES ( @Barcode1 , @Ad1 , @Miqdar1 , @Qalis1 , @Qsatis1 )" +
                            " ON CONFLICT(Barcode) DO UPDATE SET" +
                            " Ad = @Ad1 , Miqdar = Miqdar + @Miqdar1 , Qalis = @Qalis1 , Qsatis = @Qsatis1 ", sqlelaqe);
                        cmd2.Prepare();
                        cmd2.Parameters.AddWithValue("@Barcode1", barcod);
                        cmd2.Parameters.AddWithValue("@Ad1", a);
                        cmd2.Parameters.AddWithValue("@Miqdar1", m);
                        cmd2.Parameters.AddWithValue("@Qalis1", qa);
                        cmd2.Parameters.AddWithValue("@Qsatis1", qs);
                        //cmd2.Parameters.AddWithValue("@Calis1", ca);
                        //cmd2.Parameters.AddWithValue("@Csatis1", cs);
                        cmd2.ExecuteNonQuery();
                        sqlelaqe.Close();

                    }
                    MessageBox.Show("Mehsullar medaxil edildi");


                    DialogResult sorgu = MessageBox.Show("Medaxili sened kimi saxlamaq isteyirsiniz?", "Sorgu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (sorgu == DialogResult.Yes)
                        button16_Click(sender, e);
                }
            }
            // catch (Exception xeta2)           
            //  MessageBox.Show(xeta2.ToString());           
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int say;
                string qaime;
                if (dataGridView1.Rows.Count > 0)
                {
                    if (mxrc == true)
                    {
                        sqlelaqe3.Open();
                        SQLiteCommand cmd1 = new SQLiteCommand("SELECT COUNT(*) FROM Mexaric", sqlelaqe3);
                        say = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                        qaime = "Qaime" + say.ToString();
                        SQLiteCommand cmd = new SQLiteCommand(" CREATE TABLE " + qaime + " (" +
                            "Barcode TEXT," +
                            "Adı TEXT," +
                            "Miqdarı INTEGER," +
                            "Qiymətalış NUMERIC," +
                            "Qiymətsatış NUMERIC ) ", sqlelaqe3);
                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            string barcod = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            int m = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                            decimal ca = decimal.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                            decimal cs = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO " + qaime + "(Barcode,Adı,Miqdarı,Qiymətalış,Qiymətsatış) VALUES " +
                                "(@Barcode,@Adı,@Miqdarı,@Qiymətalış,@Qiymətsatış)", sqlelaqe3);
                            cmd2.Parameters.AddWithValue("@Barcode", barcod);
                            cmd2.Parameters.AddWithValue("@Adı", a);
                            cmd2.Parameters.AddWithValue("@Qiymətalış", qa);
                            cmd2.Parameters.AddWithValue("@Qiymətsatış", qs);
                            cmd2.Parameters.AddWithValue("@Miqdarı", m);
                            cmd2.ExecuteNonQuery();
                        }
                        decimal cam = 0, cas = 0;
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            cam += decimal.Parse(dataGridView1.Rows[j].Cells[6].Value.ToString());
                            cas += decimal.Parse(dataGridView1.Rows[j].Cells[7].Value.ToString());
                        }
                        SQLiteCommand cmd3 = new SQLiteCommand("INSERT INTO Mexaric (SenedAdi,Tarix,Cmalis,Cmsatis) VALUES (@senedadi,@tarix,@cma,@cms)", sqlelaqe3);
                        cmd3.Parameters.AddWithValue("@senedadi", qaime);
                        cmd3.Parameters.AddWithValue("@tarix", DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss"));
                        cmd3.Parameters.AddWithValue("@Cma", cam);
                        cmd3.Parameters.AddWithValue("@Cms", cas);
                        cmd3.ExecuteNonQuery();
                        sqlelaqe3.Close();

                        button12_Click(sender, e);
                        MessageBox.Show("Yeni Mexaric sened elave edildi");
                        mxrc = false;
                        table.Clear();
                    }
                    else
                    {
                        DialogResult sorguX = MessageBox.Show("Malları bazadan məxaric etmdən sənədi saxlamağa əminsiniz ?", "Diqqet!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (sorguX == DialogResult.Yes)
                        {
                            mxrc = true;
                            button9_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception xeta3)
            {
                MessageBox.Show(xeta3.ToString());
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    mxrc = true;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string barcod2 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        int m = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        decimal ca = decimal.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        decimal cs = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                        sqlelaqe.Open();
                        SQLiteCommand cmd = new SQLiteCommand("UPDATE Baza SET Miqdar = Miqdar - @Miqdar1 , Qalis = @Qalis1 , Qsatis = @Qsatis1 " +
                            " WHERE Barcode= @Barcode1", sqlelaqe);
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@Barcode1", barcod2);
                        //cmd.Parameters.AddWithValue("@Ad1", a);
                        cmd.Parameters.AddWithValue("@Miqdar1", m);
                        cmd.Parameters.AddWithValue("@Qalis1", qa);
                        cmd.Parameters.AddWithValue("@Qsatis1", qs);
                        cmd.ExecuteNonQuery();
                        sqlelaqe.Close();
                    }
                    MessageBox.Show("Məhsullar məxaric edildi");
                    DialogResult sorgu = MessageBox.Show("Mexarici sened kimi saxlamaq isteyirsiniz?", "Sorgu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (sorgu == DialogResult.Yes)
                    {
                        button9_Click(sender, e);
                    }
                }
            }
            catch (Exception xeta4)
            {
                MessageBox.Show(xeta4.ToString());
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (button7.Visible == false)
            {
                button11.Visible = true;
                button7.Visible = true;
            }
            else
            {
                button11.Visible = false;
                button7.Visible = false;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            label7.Text = "Baza alış:";
            label8.Text = "Baza satış:";
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            button2.Visible = true;
            button17.Visible = true;
            button18.Visible = true;
            monthCalendar1.Visible= false;
            button18.Enabled = false;
            button19.Enabled = false;
            button19.Visible = true;
            button1.Visible = false;
            button8.Visible = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button20.Visible = false;
            button21.Visible = false;
            button9.Visible = false;
            button16.Visible = false;
            button12.Visible = false;
            button15.Visible = false;
            listView1.Visible = false;
            listView2.Visible = true;
            textBox6.Visible = true;
            label5.Visible = true;
            label19.Visible = false;
            textBox6.Text = "";
            textBox1.Select();
            tarix = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");

            button4.BackColor = Color.FromArgb(206, 206, 206);
            BackColor = Color.FromArgb(206, 206, 206);
            btnMedaxil.BackColor = Color.FromArgb(224, 224, 224);
            btnMexaric.BackColor = Color.FromArgb(224, 224, 224);

            listView1.Items.Clear();
            listView2.Items.Clear();

            sqlelaqe.Open();
            SQLiteCommand emr = new SQLiteCommand("Select Barcode,Ad,Miqdar From Baza ", sqlelaqe);
            SQLiteDataReader reader = emr.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem listitem = new ListViewItem(reader[0].ToString());
                listitem.SubItems.Add(reader[1].ToString());
                listitem.SubItems.Add(reader[2].ToString());
                listView2.Items.Add(listitem);
            }

            SQLiteCommand emr2=new SQLiteCommand("SELECT SUM (Calis) FROM Baza",sqlelaqe);
            SQLiteDataReader reader1 = emr2.ExecuteReader();
            while(reader1.Read())           
                label9.Text= reader1[0].ToString() +" AZN";

            SQLiteCommand emr3 = new SQLiteCommand("SELECT SUM (Csatis) FROM Baza", sqlelaqe);
            SQLiteDataReader reader2 = emr3.ExecuteReader();
            while (reader2.Read())
                label10.Text = reader2[0].ToString() + " AZN";

            sqlelaqe.Close();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Text = "Mexaric alış:";
                label8.Text = "Mexaric satış:";
                textBox1.Select();
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                listView1.Items.Clear();
                listView2.Visible = false;
                listView1.Visible = true;
                monthCalendar1.Visible = true;
                mexaricbool = true;
                medaxilbool = false;
                button8.Visible = true;
                button9.Visible = true;
                button21.Visible = true;
                button21.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button2.Visible = false;
                button17.Visible = false;
                button18.Visible = false;
                button19.Visible = false;
                button20.Visible = false;
                button16.Visible = false;
                button1.Visible = false;
                button12.Visible = false;
                button15.Visible = true;
                textBox6.Visible = false;
                label5.Visible = false;
                label19.Visible = true;
                tarix = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");

                btnMexaric.BackColor = Color.FromArgb(206, 206, 206);
                BackColor = Color.FromArgb(206, 206, 206);
                btnMedaxil.BackColor = Color.FromArgb(224, 224, 224);
                button4.BackColor = Color.FromArgb(224, 224, 224);

                sqlelaqe3.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Id, SenedAdi, Tarix, Cmalis, Cmsatis FROM Mexaric", sqlelaqe3);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem itm = new ListViewItem(reader[0].ToString());
                    itm.SubItems.Add(reader[1].ToString());
                    itm.SubItems.Add(reader[2].ToString());
                    itm.SubItems.Add(reader[3].ToString());
                    itm.SubItems.Add(reader[4].ToString());
                    listView1.Items.Add(itm);
                }
                SQLiteCommand cmd2 = new SQLiteCommand("SELECT SUM(Cmalis) FROM Mexaric", sqlelaqe3);
                SQLiteDataReader reader1 = cmd2.ExecuteReader();
                while(reader1.Read())
                    label9.Text = reader1[0].ToString() + " AZN";
                SQLiteCommand cmd3 = new SQLiteCommand("SELECT SUM(Cmsatis) FROM Mexaric", sqlelaqe3);
                SQLiteDataReader reader2 = cmd3.ExecuteReader();
                while (reader2.Read())
                    label10.Text = reader2[0].ToString() + " AZN";

                sqlelaqe3.Close();
            }
            catch (Exception xeta6)
            {
                MessageBox.Show(xeta6.ToString());
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
           // try
            {
                label7.Text = "Medaxil alış:";
                label8.Text = "Medaxil satış:";
                textBox1.Select();
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                listView1.Items.Clear();
                listView2.Visible = false;
                listView1.Visible = true;
                monthCalendar1.Visible = true;
                medaxilbool = true;
                mexaricbool = false;
                button1.Visible = true;
                button16.Visible = true;
                button20.Visible = true;
                button20.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button8.Visible = false;
                button9.Visible = false;
                button2.Visible = false;
                button17.Visible = false;
                button18.Visible = false;
                button19.Visible = false;
                button21.Visible = false;
                button15.Visible = false;
                button12.Visible = true;
                textBox6.Visible = false;
                label5.Visible = false;
                label19.Visible = true;
                tarix = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");

                btnMedaxil.BackColor = Color.FromArgb(206, 206, 206);
                BackColor = Color.FromArgb(206, 206, 206);
                button4.BackColor = Color.FromArgb(224, 224, 224);
                btnMexaric.BackColor = Color.FromArgb(224, 224, 224);

                sqlelaqe2.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Id,SenedAdi, Tarix, Cmalis, Cmsatis FROM Medaxil", sqlelaqe2);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem itm = new ListViewItem(reader[0].ToString());
                    itm.SubItems.Add(reader[1].ToString());
                    itm.SubItems.Add(reader[2].ToString());
                    itm.SubItems.Add(reader[3].ToString());
                    itm.SubItems.Add(reader[4].ToString());
                    listView1.Items.Add(itm);
                }


                SQLiteCommand cmd1 = new SQLiteCommand("SELECT SUM(Cmalis) FROM Medaxil", sqlelaqe2);
                SQLiteDataReader reader1 = cmd1.ExecuteReader();
                while(reader1.Read())
                    label9.Text = reader1[0].ToString() + " AZN";

                SQLiteCommand cmd2 = new SQLiteCommand("SELECT SUM(Cmsatis) FROM Medaxil", sqlelaqe2);
                SQLiteDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                    label10.Text = reader2[0].ToString() + " AZN";
                sqlelaqe2.Close();

                
            }
           // catch (Exception xeta)
            {
           //     MessageBox.Show(xeta.ToString());
            }
        }
        string sened;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // try
            {
                button5.Enabled = false;
                button6.Enabled = false;
                txbxtemizle();
                button21.Enabled = true;
                button20.Enabled = true;
                table.Clear();
                if (medaxilbool == true)
                {
                    sened = listView1.FocusedItem.SubItems[1].Text;
                    sqlelaqe2.Open();
                    string emr = "SELECT *FROM " + sened;
                    SQLiteCommand cmd = new SQLiteCommand(emr, sqlelaqe2);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row1 = table.NewRow();
                        row1["BarCode"] = reader[0];
                        row1["Adı"] = reader[1];
                        row1["Miqdarı"] = reader[2];
                        row1["Qiymət (alış)"] = reader[3];
                        row1["Qiymət (satış)"] = reader[4];
                        row1["Cəmi Məbləğ (alış)"] = Convert.ToDecimal( reader[3]) * Convert.ToInt32( reader[2]);
                        row1["Cəmi Məbləğ (satış)"] = Convert.ToDecimal(reader[4]) * Convert.ToInt32(reader[2]);
                        table.Rows.Add(row1);
                    }
                    dataGridView1.DataSource = table;
                    sqlelaqe2.Close();
                    sirasay();
                    cemhesabla();
                    cemhesablaalis();
                }
                if (mexaricbool == true)
                {
                    sened = listView1.FocusedItem.SubItems[1].Text;
                    sqlelaqe3.Open();
                    string emr = "SELECT *FROM " + sened;
                    SQLiteCommand cmd = new SQLiteCommand(emr, sqlelaqe3);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row1 = table.NewRow();
                        row1["BarCode"] = reader[0];
                        row1["Adı"] = reader[1];
                        row1["Miqdarı"] = reader[2];
                        row1["Qiymət (alış)"] = reader[3];
                        row1["Qiymət (satış)"] = reader[4];
                        row1["Cəmi Məbləğ (alış)"] = Convert.ToDecimal(reader[3]) * Convert.ToInt32(reader[2]);
                        row1["Cəmi Məbləğ (satış)"] = Convert.ToDecimal(reader[4]) * Convert.ToInt32(reader[2]);
                        table.Rows.Add(row1);
                    }
                    dataGridView1.DataSource = table;
                    sqlelaqe3.Close();
                    cemhesabla();
                    cemhesablaalis();
                    sirasay();
                }
            }
            //  catch (Exception xeta7)
            {
                //     MessageBox.Show(xeta7.ToString());
            }
        }

        string tarix;
        private void button16_Click(object sender, EventArgs e)
        {
            // try
            {
                int say;
                string qaime;
                if (dataGridView1.Rows.Count > 0)
                {
                    if (mdxl == true)
                    {
                        // MessageBox.Show(tarix);

                        sqlelaqe2.Open();
                        SQLiteCommand cmd1 = new SQLiteCommand("SELECT COUNT(*) FROM Medaxil", sqlelaqe2);
                        say = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;

                        qaime = "Qaime" + say.ToString();
                        SQLiteCommand cmd = new SQLiteCommand(" CREATE TABLE " + qaime + " (" +
                            "Barcode TEXT," +
                            "Adı TEXT," +
                            "Miqdarı INTEGER," +
                            "Qiymətalış NUMERIC," +
                            "Qiymətsatış NUMERIC) ", sqlelaqe2);
                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            string barcod = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            int m = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                            SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO " + qaime + "(Barcode,Adı,Miqdarı,Qiymətalış,Qiymətsatış) VALUES " +
                                "(@Barcode,@Adı,@Miqdarı,@Qiymətalış,@Qiymətsatış)", sqlelaqe2);
                            cmd2.Parameters.AddWithValue("@Barcode", barcod);
                            cmd2.Parameters.AddWithValue("@Adı", a);
                            cmd2.Parameters.AddWithValue("@Qiymətalış", qa);
                            cmd2.Parameters.AddWithValue("@Qiymətsatış", qs);
                            cmd2.Parameters.AddWithValue("@Miqdarı", m);
                            cmd2.ExecuteNonQuery();
                        }
                        decimal cam = 0, cas = 0;
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            cam += Convert.ToDecimal(dataGridView1.Rows[j].Cells[6].Value.ToString());
                            cas += Convert.ToDecimal(dataGridView1.Rows[j].Cells[7].Value.ToString());
                        }
                        SQLiteCommand cmd3 = new SQLiteCommand("INSERT INTO Medaxil (SenedAdi,Tarix,Cmalis,Cmsatis) VALUES (@senedadi,@tarix,@cma,@cms)", sqlelaqe2);
                        cmd3.Parameters.AddWithValue("@senedadi", qaime);
                        cmd3.Parameters.AddWithValue("@tarix", tarix /* DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss")*/);
                        cmd3.Parameters.AddWithValue("@Cma", cam);
                        cmd3.Parameters.AddWithValue("@Cms", cas);
                        cmd3.ExecuteNonQuery();
                        sqlelaqe2.Close();

                        button11_Click(sender, e);
                        MessageBox.Show("Yeni Medaxil sened elave edildi");
                        mdxl = false;
                        table.Clear();
                    }
                    else
                    {
                        DialogResult sorguD = MessageBox.Show("Malları bazaya mədaxil etmdən sənədi saxlamağa əminsiniz ?", "Diqqet!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (sorguD == DialogResult.Yes)
                        {
                            mdxl = true;
                            button16_Click(sender, e);
                        }
                    }
                }
            }
            //catch (Exception xeta8)
            {
                //    MessageBox.Show(xeta8.ToString());
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            txbxtemizle();
            textBox1.Select();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            table.Clear();
            label15.Text = 0.ToString();
            label17.Text = 0.ToString();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // File.Copy(@"c:\Anbar app\baza\baza.txt", @"d:\Anbar app\baza\baza.txt", true);
            if (table.Rows.Count > 0)
            {
                DialogResult cxssorgu = MessageBox.Show("Saxlanılmamış senediniz var! Çıxmağa əminsiniz ?", "DIQQƏT!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cxssorgu == DialogResult.Yes)
                {
                    Application.Exit();

                    //if (!File.Exists(@"c:\Anbar app\baza\baza.txt"))
                    //{
                    //    string qovluq = @"c:\Anbar app\baza";
                    //    string qovluq2 = @"c:\Anbar app\medaxil";
                    //    string qovluq3 = @"c:\Anbar app\mexaric";
                    //    Directory.CreateDirectory(qovluq);
                    //    Directory.CreateDirectory(qovluq2);
                    //    Directory.CreateDirectory(qovluq3);
                    //    string file = @"c:\Anbar app\baza\baza.txt";
                    //    File.Create(file);
                    //}
                }
                else
                    e.Cancel = true;
            }
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // try
            {
                button18.Enabled = true;
                button19.Enabled = true;
                string kkk = listView2.FocusedItem.Text.ToString();
                textBox1.Text = kkk;

                sqlelaqe.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar, Qalis, Qsatis FROM Baza WHERE Barcode= @Barcode", sqlelaqe);
                cmd.Parameters.AddWithValue("@Barcode", kkk);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = (reader["Barcode"].ToString());
                    textBox2.Text = (reader["Ad"].ToString());
                    textBox3.Text = (reader["Miqdar"].ToString());
                    textBox4.Text = (reader["Qalis"].ToString());
                    textBox5.Text = (reader["Qsatis"].ToString());
                }
                sqlelaqe.Close();
            }
            //  catch (Exception xeta9)
            {
                //  MessageBox.Show(xeta9.ToString());
            }
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sirala sira = listView1.ListViewItemSorter as sirala;
            if (sira == null)
            {
                sira = new sirala(e.Column);
                sira.Order = SortOrder.Ascending;
                listView1.ListViewItemSorter = sira;
            }

            if (e.Column == sira.Column)
            {
                if (sira.Order == SortOrder.Ascending)
                    sira.Order = SortOrder.Descending;
                else
                    sira.Order = SortOrder.Ascending;
            }
            else
            {
                sira.Column = e.Column;
                sira.Order = SortOrder.Ascending;
            }
            listView1.Sort();
        }
        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sirala sira = listView2.ListViewItemSorter as sirala;
            if (sira == null)
            {
                sira = new sirala(e.Column);
                sira.Order = SortOrder.Ascending;
                listView2.ListViewItemSorter = sira;
            }
            if (e.Column == sira.Column)
            {
                if (sira.Order == SortOrder.Ascending)
                    sira.Order = SortOrder.Descending;
                else
                    sira.Order = SortOrder.Ascending;
            }
            else
            {
                sira.Column = e.Column;
                sira.Order = SortOrder.Ascending;
            }
            listView2.Sort();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                button7.Visible = false;
                button11.Visible = false;
                DataTable Tpdt = new DataTable();
                Tpdt = table.DefaultView.ToTable(true, "S/n", "BarCode", "Adı", "Miqdarı", "Qiymət (satış)", "Cəmi Məbləğ (satış)");
                dataGridView1.DataSource = Tpdt;
                DGVPrinter printer = new DGVPrinter();
                printer.Title = textBox8.Text;
                printer.SubTitle = sened + "                          Tarix: " + label13.Text;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.Footer = "                                             " + label14.Text + " " + label15.Text;
                printer.FooterSpacing = 80;
                printer.PrintPreviewDataGridView(dataGridView1);
                Tpdt.Columns.Clear();
                dataGridView1.DataSource = table;
                // dataGridView1.AutoResizeColumns();
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 250;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 135;
                dataGridView1.Columns[7].Width = 135;
            }
            catch (Exception xeta10)
            {
                MessageBox.Show(xeta10.ToString());
            }
        }
        private void button11_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                button7.Visible = false;
                button11.Visible = false;
                DataTable Tpdt = new DataTable();
                Tpdt = table.DefaultView.ToTable(true, "S/n", "BarCode", "Adı", "Miqdarı", "Qiymət (alış)", "Cəmi Məbləğ (alış)");
                dataGridView1.DataSource = Tpdt;
                DGVPrinter printer = new DGVPrinter();
                printer.Title = textBox8.Text;
                printer.SubTitle = sened + "                          Tarix: " + label13.Text;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.Footer = "                                             " + label14.Text + " " + label15.Text;
                printer.FooterSpacing = 80;
               
                printer.PrintPreviewDataGridView(dataGridView1);
                Tpdt.Columns.Clear();
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 250;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 135;
                dataGridView1.Columns[7].Width = 135;
            }
            catch (Exception xeta11)
            {
                MessageBox.Show(xeta11.ToString());
            }
        }
        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            // try
            {
                listView2.Items.Clear();
                char[] girilen = textBox6.Text.ToCharArray();
                if (girilen.Length == 1)
                {
                    sqlelaqe.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
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
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
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
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar FROM Baza WHERE Ad LIKE @girlen", sqlelaqe);
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
                    SQLiteCommand emr = new SQLiteCommand("Select Barcode,Ad,Miqdar From Baza ", sqlelaqe);
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
            // catch (Exception xeta12)
            {
                //     MessageBox.Show(xeta12.ToString());
            }
        }
        private void button12_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            sqlelaqe2.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * from Medaxil where Tarix between @tarix1 and @tarix2", sqlelaqe2);
            cmd.Parameters.AddWithValue("@tarix1", dateTimePicker1.Value.ToString("dd.MM.yyyy  HH:mm:ss"));
            cmd.Parameters.AddWithValue("@tarix2", dateTimePicker2.Value.ToString("dd.MM.yyyy  HH:mm:ss"));
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem itm = new ListViewItem(reader[0].ToString());
                itm.SubItems.Add(reader[1].ToString());
                itm.SubItems.Add(reader[2].ToString());
                itm.SubItems.Add(reader[3].ToString());
                listView1.Items.Add(itm);
            }
            sqlelaqe2.Close();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            sqlelaqe3.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT *from Mexaric where Tarix between @tarix1 and @tarix2", sqlelaqe3);
            cmd.Parameters.AddWithValue("@tarix1", dateTimePicker1.Value.ToString("dd.MM.yyyy  HH:mm:ss"));
            cmd.Parameters.AddWithValue("@tarix2", dateTimePicker2.Value.ToString("dd.MM.yyyy  HH:mm:ss"));
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem itm = new ListViewItem(reader[0].ToString());
                itm.SubItems.Add(reader[1].ToString());
                itm.SubItems.Add(reader[2].ToString());
                itm.SubItems.Add(reader[3].ToString());
                listView1.Items.Add(itm);
            }
            sqlelaqe3.Close();
            MessageBox.Show(dateTimePicker1.Value.ToString("dd.MM.yyyy  HH:mm:ss"));
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = true;
            button6.Enabled = true;
            try
            {
                secilen = e.RowIndex;
                if (secilen >= 0)
                {
                    textBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                    //textBox6.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                }
            }
            catch (Exception xeta14)
            {
                MessageBox.Show(xeta14.ToString());
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            //try
            {
                table.Clear();
                sqlelaqe.Open();
                SQLiteCommand cmd = new SQLiteCommand(" SELECT *FROM Baza WHERE Miqdar > 0", sqlelaqe);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow row1 = table.NewRow();
                    row1["BarCode"] = reader["Barcode"];
                    row1["Adı"] = reader["Ad"];
                    row1["Miqdarı"] = reader["Miqdar"];
                    row1["Qiymət (alış)"] = reader["Qalis"];
                    row1["Qiymət (satış)"] = reader["Qsatis"];
                    row1["Cəmi Məbləğ (alış)"] = Convert.ToDecimal( reader["Qalis"]) * Convert.ToInt32(reader["Miqdar"]);
                    row1["Cəmi Məbləğ (satış)"] = Convert.ToDecimal(reader["Qsatis"]) * Convert.ToInt32(reader["Miqdar"]);
                    table.Rows.Add(row1);
                }
                dataGridView1.DataSource = table;
                sqlelaqe.Close();

                sirasay();
                cemhesabla();
                cemhesablaalis();
            }
            // catch (Exception xeta15)
            {
                //     MessageBox.Show(xeta15.ToString());
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            // try
            {
                if (listView2.SelectedItems.Count > 0)
                {
                    string secilen = listView2.FocusedItem.Text.ToString();
                    string bar = textBox1.Text;
                    string ad = textBox2.Text;
                    int mq = Int32.Parse(textBox3.Text);
                    decimal qa = decimal.Parse(textBox4.Text);
                    decimal qs = decimal.Parse(textBox5.Text);
                    sqlelaqe.Open();
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Baza SET Barcode = @bar ,Ad = @Ad1 , Miqdar = @Miqdar1 , Qalis = @Qalis1 , Qsatis = @Qsatis1 " +
                        " WHERE Barcode = @secilen ", sqlelaqe);
                    cmd.Parameters.AddWithValue("@secilen", secilen);
                    cmd.Parameters.AddWithValue("@bar", bar);
                    cmd.Parameters.AddWithValue("@Ad1", ad);
                    cmd.Parameters.AddWithValue("@Miqdar1", mq);
                    cmd.Parameters.AddWithValue("@Qalis1", qa);
                    cmd.Parameters.AddWithValue("@Qsatis1", qs);
                    cmd.ExecuteNonQuery();
                    sqlelaqe.Close();
                }
                button4_Click(sender, e);
            }
            // catch (Exception xeta16)
            {
                //   MessageBox.Show(xeta16.ToString());
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            // try
            {
                if (listView2.SelectedItems.Count > 0)
                {

                    DialogResult silsorgu = MessageBox.Show("Bazadan məsulu silməyə əminsiniz ?", "DIQQƏT!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (silsorgu == DialogResult.Yes)
                    {
                        sqlelaqe.Open();
                        SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Baza WHERE Barcode=@barcode", sqlelaqe);
                        cmd.Parameters.AddWithValue("@barcode", listView2.FocusedItem.Text.ToString());
                        cmd.ExecuteNonQuery();                      
                        sqlelaqe.Close();
                    }
                }
                button4_Click(sender,e);
            }
            //  catch (Exception xeta17)
            {
                //     MessageBox.Show(xeta17.ToString());
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                decimal cma=0, cms=0;
                string sened = listView1.FocusedItem.SubItems[1].Text;
               
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string barcod = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    int m = Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    decimal ca = decimal.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    decimal cs = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    cma += ca;
                    cms += cs;

                    if (listView1.SelectedItems.Count > 0)
                    {
                        
                        sqlelaqe2.Open();
                        SQLiteCommand cmd = new SQLiteCommand("UPDATE " + sened + " SET Barcode=@Barcode,Adı=@Adı,Miqdarı=@Miqdarı,Qiymətalış=@Qiymətalış,Qiymətsatış=@Qiymətsatış WHERE Barcode=@Barcode", sqlelaqe2);
                        cmd.Parameters.AddWithValue("@Barcode", barcod);
                        cmd.Parameters.AddWithValue("@Adı", a);
                        cmd.Parameters.AddWithValue("@Miqdarı", m);
                        cmd.Parameters.AddWithValue("@Qiymətalış", qa);
                        cmd.Parameters.AddWithValue("@Qiymətsatış", qs);
                        cmd.ExecuteNonQuery();                  
                        sqlelaqe2.Close();
                    }
                }
                sqlelaqe2.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("UPDATE Medaxil SET Tarix=@tarix,Cmalis=@Cmalis,Cmsatis=@Cmsatis WHERE SenedAdi=@sened", sqlelaqe2);
                cmd1.Parameters.AddWithValue("@tarix", tarix);
                cmd1.Parameters.AddWithValue("@sened", sened);
                cmd1.Parameters.AddWithValue("@Cmalis", cma);
                cmd1.Parameters.AddWithValue("@Cmsatis", cms);
                cmd1.ExecuteNonQuery();
                sqlelaqe2.Close();
                MessageBox.Show("Sənəd uğurla dəyişildi");
                button11_Click(sender,e);
            }
            catch (Exception xeta21)
            {
                MessageBox.Show(xeta21.ToString());
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                decimal cma=0,cms=0;
                string sened = listView1.FocusedItem.SubItems[1].Text;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string barcod = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string a = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    int m = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    decimal qa = decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    decimal qs = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    decimal ca = decimal.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    decimal cs = decimal.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    cma += ca;
                    cms += cs;
                    if (listView1.SelectedItems.Count > 0)
                    {
                        
                        sqlelaqe3.Open();
                        SQLiteCommand cmd = new SQLiteCommand("UPDATE " + sened + " SET Barcode=@Barcode,Adı=@Adı,Miqdarı=@Miqdarı,Qiymətalış=@Qiymətalış,Qiymətsatış=@Qiymətsatış WHERE Barcode=@Barcode", sqlelaqe3);
                        cmd.Parameters.AddWithValue("@Barcode", barcod);
                        cmd.Parameters.AddWithValue("@Adı", a);
                        cmd.Parameters.AddWithValue("@Miqdarı", m);
                        cmd.Parameters.AddWithValue("@Qiymətalış", qa);
                        cmd.Parameters.AddWithValue("@Qiymətsatış", qs);
                        cmd.ExecuteNonQuery();
                        sqlelaqe3.Close();
                    }
                }
                sqlelaqe3.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("UPDATE Mexaric SET Tarix=@tarix,Cmalis=@Cmalis,Cmsatis=@Cmsatis WHERE SenedAdi=@sened", sqlelaqe3);
                cmd1.Parameters.AddWithValue("@tarix", tarix);
                cmd1.Parameters.AddWithValue("@sened", sened);
                cmd1.Parameters.AddWithValue("@Cmalis", cma);
                cmd1.Parameters.AddWithValue("@Cmsatis", cms);
                cmd1.ExecuteNonQuery();
                sqlelaqe3.Close();
                MessageBox.Show("Sənəd uğurla dəyişildi");
                button12_Click(sender, e);
            }
            catch (Exception xeta20)
            {
                MessageBox.Show(xeta20.ToString());
            }
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string g = textBox1.Text;
            sqlelaqe.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Barcode, Ad, Miqdar, Qalis, Qsatis FROM Baza WHERE Barcode = @Barcode ", sqlelaqe);
            cmd.Parameters.AddWithValue("@Barcode", g);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = reader["Ad"].ToString();
                textBox3.Text = reader["Miqdar"].ToString();
                textBox4.Text = reader["Qalis"].ToString();
                textBox5.Text = reader["Qsatis"].ToString();
            }
            if (reader.StepCount == 0)
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            sqlelaqe.Close();
        }
       
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            tarix = monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy  HH:mm:ss");            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}