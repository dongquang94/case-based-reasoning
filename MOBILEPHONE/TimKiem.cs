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
    public partial class TimKiem : Form
    {
        public TimKiem()
        {
            InitializeComponent();
        }
        public static string strAddressTK = "";
        SqlConnection con;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable sptk = new DataTable("SPTK");
        DataTable SPclick = new DataTable("spclick");
        private void connect()
        {
            String kn = "Data Source=.;Initial Catalog=MOBILEPHONE;Integrated Security=True";
            try
            {
                con = new SqlConnection(kn);
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối CSDL thất bại");
                throw;
            }
        }
        private void getdata()
        {
            String sql = "select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH from SP,HANG,HDH where SP.MAHDH=HDH.MAHDH and SP.MAHANG=HANG.MAHANG and SP.TENSP like N'%"+MAIN.strTK+"%'";
            SqlCommand com = new SqlCommand(sql, con);
            da.SelectCommand = com;
            da.Fill(sptk);
            if (sptk.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm được sản phẩm nào phù hợp");
                this.Close();
            }
            else
                dtgrvTK.DataSource = sptk;
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
        private void TimKiem_Load(object sender, EventArgs e)
        {
            
            connect();
            getdata();
            lbslsp.Text = "Số lượng sản phẩm tìm được: " + sptk.Rows.Count.ToString();
        }
        private DataTable GetaTable(String sql)
        {
            DataTable TBreturn = new DataTable("TB");
            connect();
            da = new SqlDataAdapter(sql, con);
            da.Fill(TBreturn);
            return TBreturn;
        }
        private void dtgrvTK_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgrvTK.CurrentCell != null)
                {
                    int vitri = e.RowIndex;
                    String sql = "Select * from SP,CAUHINH where SP.MACH=CAUHINH.MACH and SP.TENSP=N'" + dtgrvTK.Rows[vitri].Cells["TENSP"].Value.ToString() + "'";
                  
                    SPclick = GetaTable(sql);
                    pictureBox1.BackgroundImage = Image.FromFile(SPclick.Rows[0]["IMAGESRC"].ToString());
                    lbmanhinh.Text = SPclick.Rows[0]["MANHINH"].ToString();
                    lbcpu.Text = SPclick.Rows[0]["CPU"].ToString();
                    lbram.Text = SPclick.Rows[0]["RAM"].ToString() + " GB";
                    lbhedieuhanh.Text = SPclick.Rows[0]["HDH"].ToString();
                    lbcameraphu.Text = SPclick.Rows[0]["CAMERAPHU"].ToString();
                    lbcamerachinh.Text = SPclick.Rows[0]["CAMERACHINH"].ToString();
                    lbbonhotrong.Text = SPclick.Rows[0]["BONHO"].ToString() + " GB";
                    if (SPclick.Rows[0]["THENHO"].ToString() == "0")
                        lbthenhongoai.Text = "Không";
                    else
                        lbthenhongoai.Text = SPclick.Rows[0]["THENHO"].ToString() + " GB";
                    lbdungluongpin.Text = SPclick.Rows[0]["PIN"].ToString() + " mAh";
                    lbTENSP.Text = dtgrvTK.Rows[vitri].Cells["TENSP"].Value.ToString();
                    lbGIA.Text = dtgrvTK.Rows[vitri].Cells["GIA"].Value.ToString() + "000 VND";
                }
            }
            catch (Exception)
            { }
        }
        private String getAddress(string TENSP)
        {
            string address = "";
            try
            {
                connect();
                string sql = "select ADDRESS.ADDRESS from SP,ADDRESS where SP.MASP=ADDRESS.MASP and SP.TENSP=N'" + TENSP + "'";
                SqlCommand com = new SqlCommand(sql, con);
                object kq = com.ExecuteScalar();
                if (kq != null)
                    address = kq.ToString();
            }
            catch (Exception)
            {
            }
            return address;
        }

        private void btWeb_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrvTK.SelectedRows != null)
                {
                    strAddressTK = getAddress(SPclick.Rows[0]["TENSP"].ToString());
                }
                else
                {
                    MessageBox.Show("Chưa chọn sản phẩm !");
                }
                if (strAddressTK == "")
                    MessageBox.Show("Sản phẩm này hiện chưa có link web!");
                else
                {
                    WEBTK frmWEB = new WEBTK();
                    frmWEB.ShowDialog();
                }
            }
            catch (Exception)
            {
            }         
        }

        private void bttrolai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
