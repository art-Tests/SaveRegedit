using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Data.SqlClient;


namespace 連線字串寫入系統登錄 {

    public partial class Form1 : Form {
        public SqlConnection conn;

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            connStrSave();
        }

        private void connStrSave() {
            RegistryKey key = Registry.ClassesRoot.CreateSubKey("ADONETDB");
            key.SetValue("connStr", textBox1.Text);
        }
        private void connStrRead() {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("ADONETDB");
            if (key != null) {
                textBox1.Text = key.GetValue("connStr").ToString();
                button2.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            connStrRead();
        }

        private void Form1_Load(object sender, EventArgs e) {
            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            try {
                conn = new SqlConnection();
                conn.ConnectionString = textBox1.Text;
                conn.Open();
                button2.Enabled = false;
                button3.Enabled = true;
                button5.Enabled = true;
                SqlCommand cmd = new SqlCommand("select Convert(varchar(10),getdate(),120)", conn);
                string rtn = "";
                rtn = cmd.ExecuteScalar().ToString();
                label3.Text = "連線中，系統日期:" + rtn;
                
            }
            catch (Exception) {
                label3.Text = "連線字串錯誤，請修正";
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            button3.Enabled = false;
            button2.Enabled = true;
            button5.Enabled = false;
            conn.Close();
            label3.Text = "未連接";
        }

        private void rdo1_CheckedChanged(object sender, EventArgs e) {
            textBox1.Text = "Data Source=ART_PC; Initial Catalog=KM; User ID=web; Password=web";
        }

        private void rdo2_CheckedChanged(object sender, EventArgs e) {
            textBox1.Text = "Data Source=127.0.0.1; Initial Catalog=KM; User ID=web; Password=web";
        }

        private void button5_Click(object sender, EventArgs e) {
            DataTable dt = conn.GetSchema("Databases");
            dataGridView1.DataSource = dt;
        }


    }
}
