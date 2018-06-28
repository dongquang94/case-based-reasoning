using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MOBILEPHONE
{
    public partial class MAIN : Form
    {
        public static String strTK="";
        public static bool clicktumain = false;
        public MAIN()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable sphot = new DataTable("SPHOT");
        DataTable sphot1 = new DataTable("SPHOT1");
        private void connect()
        {
            String kn = "Data Source=(local);Initial Catalog=MOBILEPHONE;Integrated Security=True";
            try
            {
                con = new SqlConnection(kn);
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối CSDL thất bại");
            }
        }
        private void getdata()
        {
            String sql = "select * from SPHOT";
            SqlCommand com = new SqlCommand(sql, con);
            da.SelectCommand = com;
            da.Fill(sphot);
            for (int i = 0; i < sphot.Rows.Count; i++)
            {
                String sqlgetSP = "select * from CAUHINH,SP,SPHOT where CAUHINH.MACH=SP.MACH and SPHOT.MASP=SP.MASP and SP.MASP=N'"+sphot.Rows[i][0].ToString()+"'";
                SqlCommand comsp = new SqlCommand(sqlgetSP, con);
                da.SelectCommand = comsp;
                da.Fill(sphot1);
                lstsphot.Items.Add(sphot1.Rows[i]["TENSP"].ToString());
            }

        }
        private void disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                Dispose();
            }
        }
         private void btexit_Click(object sender, EventArgs e)
         {
             Application.Exit();
             disconnect();
         }

     
         private void MAIN_Load(object sender, EventArgs e)
         {
             connect();
             getdata();
             lstsphot.SelectedIndex = 0;
            // hinhanh.Image = Image.FromFile("SAMSUNG/samsung-galaxy-s6-400x534.png");      
         }
         public static String convertGIA(string gia)
         {
             int hangtrieu = Convert.ToInt32(gia) / 1000;
             int hangnghin = Convert.ToInt32(gia) - hangtrieu * 1000;
             String trieu = hangtrieu.ToString();
             String nghin = hangnghin.ToString();
             if (hangnghin < 10)
                 nghin = "00" + hangnghin.ToString();
             else if (hangnghin < 100)
                 nghin = "0" + hangnghin.ToString();
             return trieu + "." + nghin;
         }
         private void lstsphot_SelectedIndexChanged(object sender, EventArgs e)
         {
             for(int i=0;i<sphot1.Rows.Count;i++)
             {
                 if (lstsphot.SelectedItem.ToString() == sphot1.Rows[i]["TENSP"].ToString())
                 {
                     hinhanh.BackgroundImage = Image.FromFile(sphot1.Rows[i]["IMAGESRC"].ToString());
                     hinhanh.BackgroundImageLayout = ImageLayout.Stretch;
                     lbmanhinh.Text = sphot1.Rows[i]["MANHINH"].ToString();
                     lbcpu.Text = sphot1.Rows[i]["CPU"].ToString();
                     lbram.Text = sphot1.Rows[i]["RAM"].ToString()+" GB";
                     lbhedieuhanh.Text = sphot1.Rows[i]["HDH"].ToString();
                     lbcameraphu.Text = sphot1.Rows[i]["CAMERAPHU"].ToString();
                     lbcamerachinh.Text = sphot1.Rows[i]["CAMERACHINH"].ToString();
                     lbbonhotrong.Text = sphot1.Rows[i]["BONHO"].ToString()+" GB";
                     if (sphot1.Rows[i]["THENHO"].ToString() == "0")
                         lbthenhongoai.Text = "Không";
                     else
                     lbthenhongoai.Text = sphot1.Rows[i]["THENHO"].ToString()+" GB";
                     lbdungluongpin.Text = sphot1.Rows[i]["PIN"].ToString()+" mAh";
                     lbgia.Text = "Giá: "+convertGIA(sphot1.Rows[i]["GIA"].ToString()) + ".000 VND";
                 }

              }
         }

         private void bttk_Click(object sender, EventArgs e)
         {
             if (txttk.Text != "" && txttk.Text != "Tên sản phẩm tìm kiếm ...")
             {
                 strTK = txttk.Text;
                 TimKiem frm = new TimKiem();
                 frm.ShowDialog();
             }
             else
                 MessageBox.Show("Hãy nhập tên sản phẩm muốn tìm kiếm");
         }

         private void txttk_MouseClick(object sender, MouseEventArgs e)
         {
             if (txttk.Text == "Tên sản phẩm tìm kiếm ...")
             {
                 txttk.Text = "";
                 txttk.ForeColor = Color.Black;
             }
         }

         private void btht_Click(object sender, EventArgs e)
         {
             HOTRO frm = new HOTRO();
             frm.ShowDialog();
         }

         private void bttcsp_Click(object sender, EventArgs e)
         {
             clicktumain = true;
             KetQuaHoTro frm = new KetQuaHoTro();
             frm.ShowDialog();
             HOTRO.clicktuhotro = false;          
         }

         private void btht_MouseHover(object sender, EventArgs e)
         {
             btht.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
             btht.ForeColor = Color.Black;
            
         }

         private void btht_MouseLeave(object sender, EventArgs e)
         {
             btht.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
             btht.ForeColor = Color.White;
         }

         private void bttcsp_MouseHover(object sender, EventArgs e)
         {
             bttcsp.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
             bttcsp.ForeColor = Color.Black;
         }

         private void bttcsp_MouseLeave(object sender, EventArgs e)
         {
             bttcsp.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
             bttcsp.ForeColor = Color.White;
         }

         private void btgopy_MouseHover(object sender, EventArgs e)
         {
             btgopy.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
             btgopy.ForeColor = Color.Black;
         }

         private void btgopy_MouseLeave(object sender, EventArgs e)
         {
             btgopy.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
             btgopy.ForeColor = Color.White;
         }

         private void btlienhe_MouseHover(object sender, EventArgs e)
         {
             btlienhe.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
             btlienhe.ForeColor = Color.Black;
         }

         private void btlienhe_MouseLeave(object sender, EventArgs e)
         {
             btlienhe.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
             btlienhe.ForeColor = Color.White;
         }

         private void btexit_MouseHover(object sender, EventArgs e)
         {
             btexit.BackgroundImage = Image.FromFile("Image/TrangChu/buttonexit2.png");
       
         }

         private void btexit_MouseLeave(object sender, EventArgs e)
         {
             btexit.BackgroundImage = Image.FromFile("Image/TrangChu/buttonexit.png");
         }

         private void txttk_MouseLeave(object sender, EventArgs e)
         {
             if (txttk.Text == "")
             {
                 txttk.Text = "Tên sản phẩm tìm kiếm ...";
             }
         }

         private void txttk_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.KeyCode == Keys.Enter)
                 bttk.PerformClick();
         }       
     }
}
