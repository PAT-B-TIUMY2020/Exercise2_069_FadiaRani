using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;
namespace ClientRest
{
    public partial class Form1 : Form
    {
        private string baseurl;
        private object label5;

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox1.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            textBox3.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            textBox4.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchData();

        }

        private void SearchData()
        {
            var json = new WebClient().DownloadString("http://localhost:9999/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = textBox1.Text;
            if (nim == null || nim == "")
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == textBox1.Text).ToList();

                dataGridView1.DataSource = item;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();
            mhs.nama = textBox2.Text;
            mhs.nim = textBox1.Text;
            mhs.prodi = textBox3.Text;
            mhs.angkatan = textBox4.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var postdata = new WebClient();
            postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = postdata.UploadString(baseurl + "Mahasiswa", data);
            Console.WriteLine(response);
            TampilData();
        }

        private void TampilData()
        {
            var json = new WebClient().DownloadString("http://localhost:9999/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dataGridView1.DataSource = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:9999/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            string nim = textBox1.Text;
            var item = data.Where(x => x.nim == textBox1.Text).FirstOrDefault();
            if (item != null)
            {
                // update logger with your textboxes data
                item.nama = textBox2.Text;
                item.nim = textBox1.Text;
                item.prodi = textBox3.Text;
                item.angkatan = textBox4.Text;

                // Save everything
                string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseurl + "UpdateMahasiswa", output);
                Console.WriteLine(response);
                TampilData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:9999/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            if (MessageBox.Show("Anda yakin menghapus data ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string nim = textBox1.Text;
                var item = data.Where(x => x.nim == textBox1.Text).FirstOrDefault();
                if (item != null)
                {
                    data.Remove(item);
                    // Save everything
                    string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                    var postdata = new WebClient();
                    postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = postdata.UploadString(baseurl + "DeleteMahasiswa", output);
                    Console.WriteLine(response);
                    TampilData();
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:9999/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            int length = data.Count();
            label5.Text = Convert.ToString(length);
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

