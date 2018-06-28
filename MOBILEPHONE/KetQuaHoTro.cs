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
    public partial class KetQuaHoTro : Form
    {
        public KetQuaHoTro()
        {
            InitializeComponent();
        }
        public static string strAddress="";
        SqlConnection con;
        SqlDataAdapter da = new SqlDataAdapter();
        public static DataTable  sp = new DataTable("SP");
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
            try
            {
                sp.Clear();
            }
            catch (Exception)
            { }
            String sql = "Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH from SP,HANG, HDH, CAUHINH where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH";
            SqlCommand com = new SqlCommand(sql, con);
            da.SelectCommand = com;
            da.Fill(sp);
        }
        private void getdatatuhotro()
        {
            dtgrvKQ.DataSource = HOTRO.tatca;
        }
        private void getdatatumain()
        {
            sp.Clear();
            String sql = "Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH from SP,HANG, HDH, CAUHINH where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH";
            SqlCommand com = new SqlCommand(sql, con);
            da.SelectCommand = com;
            da.Fill(sp);
            dtgrvKQ.DataSource = sp;
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

        private void dtgrvKQ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void KetQuaHoTro_Load(object sender, EventArgs e)
        { 
            if (MAIN.clicktumain == true)
            {
                HOTRO.clicktuhotro = false;
                bttop10.Visible = false;
                bttatca.Visible = false;
                bttrolai.Visible = true;
                connect();
                getdatatumain();
            //    this.Height=630;
                this.StartPosition = FormStartPosition.CenterParent;
                getdata();
                lbslsp.Text = "Số lượng sản phẩm :" + dtgrvKQ.RowCount;
            }
            if (HOTRO.clicktuhotro == true) 
            {
                MAIN.clicktumain = false;
                bttop10.Visible = true;
                bttatca.Visible = true;
                bttrolai.Visible = false;
                connect();
                getdatatuhotro();
                this.Height = 670;
                getdata();
                lbslsp.Text = "Số lượng sản phẩm được đề nghị:" + dtgrvKQ.RowCount;
            }
            
           
        }

        private void bttatca_Click(object sender, EventArgs e)
        {
          
            dtgrvKQ.DataSource = HOTRO.tatca;
        }

        private void bttop10_Click(object sender, EventArgs e)
        {
            dtgrvKQ.ClearSelection();
            dtgrvKQ.DataSource = HOTRO.top10;
        }
        private DataTable GetaTable(String sql)
        {
            DataTable TBreturn = new DataTable("TB");
            connect();
            da = new SqlDataAdapter(sql, con);
            da.Fill(TBreturn);
            return TBreturn;
        }
        private void dtgrvKQ_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgrvKQ.CurrentCell != null)
                {
                    int vitri = e.RowIndex;
                    String sql = "Select * from SP,CAUHINH where SP.MACH=CAUHINH.MACH and SP.TENSP=N'" + dtgrvKQ.Rows[vitri].Cells["TENSP"].Value.ToString() + "'";
                       SPclick = GetaTable(sql);
                    pictureBox1.BackgroundImage = Image.FromFile(SPclick.Rows[0]["IMAGESRC"].ToString());
                    lbmanhinh.Text = SPclick.Rows[0]["MANHINH"].ToString();
                    lbcpu.Text = SPclick.Rows[0]["CPU"].ToString();
                    lbram.Text = SPclick.Rows[0]["RAM"].ToString() +" GB";
                    lbhedieuhanh.Text = SPclick.Rows[0]["HDH"].ToString();
                    lbcameraphu.Text = SPclick.Rows[0]["CAMERAPHU"].ToString();
                    lbcamerachinh.Text = SPclick.Rows[0]["CAMERACHINH"].ToString() ;
                    lbbonhotrong.Text = SPclick.Rows[0]["BONHO"].ToString() + " GB";
                    if (SPclick.Rows[0]["THENHO"].ToString() == "0")
                        lbthenhongoai.Text = "Không";
                    else
                        lbthenhongoai.Text = SPclick.Rows[0]["THENHO"].ToString() + " GB";
                    lbdungluongpin.Text = SPclick.Rows[0]["PIN"].ToString() + " mAh";
                    lbTENSP.Text = dtgrvKQ.Rows[vitri].Cells["TENSP"].Value.ToString();
                    lbGIA.Text = MAIN.convertGIA(dtgrvKQ.Rows[vitri].Cells["GIA"].Value.ToString()) + ".000 VND";
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
                if (dtgrvKQ.SelectedRows != null)
                {
                    strAddress = getAddress(SPclick.Rows[0]["TENSP"].ToString());
                }
                else
                {
                    MessageBox.Show("Chưa chọn sản phẩm !");
                }
                if (strAddress == "")
                    MessageBox.Show("Sản phẩm này hiện chưa có link web!");
                else
                {
                    WEB frmWEB = new WEB();
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
