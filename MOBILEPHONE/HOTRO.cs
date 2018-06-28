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
    public partial class HOTRO : Form
    {
#region Bien
        public MucDich giaitri = new MucDich();
        public MucDich hoctap = new MucDich();
        public MucDich lamviec = new MucDich();
        public MucDich quayphimchupanh = new MucDich();
        int Wram=5,Wcpu=5,Wmanhinh=3,Whdh=4,Wpin=3,Wcamerachinh=2,Wcameraphu=1,Wbonho=4,Wthenho=1;
#endregion
        public HOTRO()
        {
            InitializeComponent();
        }
        //KN
        public static bool clicktuhotro = false;
        public SqlConnection con;
        public DataTable dt = new DataTable("dt");
        public SqlDataAdapter da = new SqlDataAdapter();
        public static DataTable top10 = new DataTable("top10");
        public static DataTable tatca=new DataTable("tatca");
        public void connect()
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
        public void disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                Dispose();
            }
        }
        private void HOTRO_Load(object sender, EventArgs e)
        {    
                connect();
                String sql = "select * from HANG";
                SqlCommand com = new SqlCommand(sql, con);
                da.SelectCommand = com;
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbohang.Items.Add(dt.Rows[i][1].ToString());
                }
                btreset.PerformClick();
        }
        private void btKQHT_Click(object sender, EventArgs e)
        {
            try
            {
                clicktuhotro = true;
                MAIN.clicktumain = false;
                try
                {
                    tatca.Clear();
                    top10.Clear();
                    connect();
                }
                catch (Exception)
                {
                }   
                String sql = "Update SP set SIM=" + (Wram * getHij(cboRAM.Text) * trackBarRAM.Value).ToString() + "*CAUHINH.DGRAM+" +
                    (Wcpu * getHij(cboCPU.Text) * trackBarCPU.Value).ToString() + "*CAUHINH.DGCPU+" +
                    (Wcamerachinh * getHij(cboCAMERACHINH.Text) * trackBarCAMERACHINH.Value).ToString() + "*CAUHINH.DGCAMERACHINH+" +
                    (Wcameraphu * getHij(cboCAMERAPHU.Text) * trackBarCAMERAPHU.Value).ToString() + "*CAUHINH.DGCAMERAPHU+" +
                    (Wmanhinh * getHij(cboMANHINH.Text) * trackBarMANHINH.Value).ToString() + "*CAUHINH.DGMANHINH+" +
                    (Whdh * getHij(cboHDH.Text) * trackBarHDH.Value).ToString() +
                    (Wpin * getHij(cboPIN.Text) * trackBarPIN.Value).ToString() + "*CAUHINH.DGPIN+" +
                    (Wbonho * getHij(cboBoNho.Text) * trackBarBONHO.Value).ToString() + "*CAUHINH.DGBONHO+" +
                    (Wthenho * getHij(cboTheNho.Text) * trackBarTHENHO.Value).ToString() + "*CAUHINH.DGTHENHO from CAUHINH where CAUHINH.MACH=SP.MACH";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                String sqltop10 = "";
                String sqltatca = "";
                if (cbohang.Text != "Tất cả" && cboHDH.Text != "Tất cả")
                {
                    sqltop10 = @"Select  top 10 SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HANG.TENHANG=N'" + cbohang.Text + "' and HDH.TENHDH=N'" + cboHDH.Text +
                                     "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                     " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                     " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                     " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                     " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                     " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                     " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                     " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                     " order by SP.SIM DESC";
                    sqltatca = @"Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HANG.TENHANG=N'" + cbohang.Text + "' and HDH.TENHDH=N'" + cboHDH.Text +
                                     "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                     " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                     " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                     " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                     " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                     " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                     " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                     " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                     " order by SP.SIM DESC";
                }
                else if (cbohang.Text == "Tất cả" && cboHDH.Text != "Tất cả")
                {
                    sqltop10 = @"Select  top 10 SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HDH.TENHDH=N'" + cboHDH.Text +
                                    "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                    sqltatca = @"Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HDH.TENHDH=N'" + cboHDH.Text +
                                    "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                }
                else if (cbohang.Text != "Tất cả" && cboHDH.Text == "Tất cả")
                {
                    sqltop10 = @"Select  top 10 SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HANG.TENHANG=N'" + cbohang.Text +
                                    "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                    sqltatca = @"Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text + ") and HANG.TENHANG=N'" + cbohang.Text +
                                    "' and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                }
                else
                {
                    sqltop10 = @"Select  top 10 SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text +
                                    ") and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                    sqltatca = @"Select SP.TENSP,SP.GIA,HANG.TENHANG,HDH.TENHDH
                                from SP,HANG, HDH, CAUHINH
                                where HANG.MAHANG=SP.MAHANG and HDH.MAHDH=SP.MAHDH and SP.MACH=CAUHINH.MACH and (GIA between " + txtgiadau.Text + " and " + txtgiacuoi.Text +
                                    ") and CAUHINH.DGMANHINH>=" + toInt(cboMANHINH.Text).ToString() +
                                    " and CAUHINH.DGRAM>=" + toInt(cboRAM.Text).ToString() +
                                    " and CAUHINH.DGCAMERACHINH>=" + toInt(cboCAMERACHINH.Text).ToString() +
                                    " and CAUHINH.DGBONHO>=" + toInt(cboBoNho.Text).ToString() +
                                    " and CAUHINH.DGCAMERAPHU>=" + toInt(cboCAMERAPHU.Text).ToString() +
                                    " and CAUHINH.DGTHENHO>=" + toInt(cboTheNho.Text).ToString() +
                                    " and CAUHINH.DGCPU>=" + toInt(cboCPU.Text).ToString() +
                                    " and CAUHINH.DGPIN>=" + toInt(cboPIN.Text).ToString() +
                                    " order by SP.SIM DESC";
                }
                SqlCommand comtop10 = new SqlCommand(sqltop10, con);
                SqlCommand comtatca = new SqlCommand(sqltatca, con);
                da.SelectCommand = comtatca;
                da.Fill(tatca);
                da.SelectCommand = comtop10;
                da.Fill(top10);
                new KetQuaHoTro().ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Giá không hợp lệ!");
            }
           
        }
        private int getHij(String s)
        {
             if(s!="Tất cả")
                 return 1;
            return 0;
        }
        private int toInt(String s)
        {
            if (s == "Rất cao")
                return 5;
            if (s == "Cao")
                return 4;
            if (s == "Vừa")
                return 3;
            if (s == "Thấp")
                return 2;
            if (s == "Rất thấp")
                return 1;
            return 0;
        }
        private String toStr(int s)
        {
            if (s == 5)
                return "Rất cao";
            if (s ==4 )
                return "Cao";
            if (s ==3)
                return "Vừa";
            if (s == 2)
                return "Thấp";
            if (s == 1)
                return "Rất thấp";
            return "Tất cả";
        }
        public void macdinh()
        {
            cboMANHINH.Text = "Tất cả";
            trackBarMANHINH.Value = 0;
            cboRAM.Text = "Tất cả";
            trackBarRAM.Value = 0;
            cboCPU.Text = "Tất cả";
            trackBarCPU.Value = 0;
            cboPIN.Text = "Tất cả";
            trackBarPIN.Value = 0;
            cboBoNho.Text = "Tất cả";
            trackBarBONHO.Value = 0;
            cboCAMERACHINH.Text = "Tất cả";
            trackBarCAMERACHINH.Value = 0;
            cboCAMERAPHU.Text = "Tất cả";
            trackBarCAMERAPHU.Value = 0;
            cboTheNho.Text = "Tất cả";
            trackBarTHENHO.Value = 0;
            cboHDH.Text = "Tất cả";
            trackBarHDH.Value = 0;
        }  
        public void setChoice()
        {
            cboMANHINH.Text = toStr(getmax(toInt(giaitri.manhinh), toInt(hoctap.manhinh), toInt(lamviec.manhinh), toInt(quayphimchupanh.manhinh)));
            cboCPU.Text = toStr(getmax(toInt(giaitri.cpu), toInt(hoctap.cpu), toInt(lamviec.cpu), toInt(quayphimchupanh.cpu)));
            cboRAM.Text = toStr(getmax(toInt(giaitri.ram), toInt(hoctap.ram), toInt(lamviec.ram), toInt(quayphimchupanh.ram)));
            cboTheNho.Text = toStr(getmax(toInt(giaitri.thenho), toInt(hoctap.thenho), toInt(lamviec.thenho), toInt(quayphimchupanh.thenho)));
            cboCAMERACHINH.Text = toStr(getmax(toInt(giaitri.camerachinh), toInt(hoctap.camerachinh), toInt(lamviec.camerachinh), toInt(quayphimchupanh.camerachinh)));
            cboCAMERAPHU.Text = toStr(getmax(toInt(giaitri.cameraphu), toInt(hoctap.cameraphu), toInt(lamviec.cameraphu), toInt(quayphimchupanh.cameraphu)));
            cboBoNho.Text = toStr(getmax(toInt(giaitri.bonho), toInt(hoctap.bonho), toInt(lamviec.bonho), toInt(quayphimchupanh.bonho)));
            cboPIN.Text = toStr(getmax(toInt(giaitri.pin), toInt(hoctap.pin), toInt(lamviec.pin), toInt(quayphimchupanh.pin)));
            //
            trackBarMANHINH.Value = getmax(giaitri.dqtmanhinh, hoctap.dqtmanhinh, lamviec.dqtmanhinh, quayphimchupanh.dqtmanhinh);
            trackBarCPU.Value = getmax(giaitri.dqtcpu, hoctap.dqtcpu, lamviec.dqtcpu, quayphimchupanh.dqtcpu);
            trackBarRAM.Value = getmax(giaitri.dqtram, hoctap.dqtram, lamviec.dqtram, quayphimchupanh.dqtram);
            trackBarTHENHO.Value = getmax(giaitri.dqtthenho, hoctap.dqtthenho, lamviec.dqtthenho, quayphimchupanh.dqtthenho);
            trackBarBONHO.Value = getmax(giaitri.dqtbonho, hoctap.dqtbonho, lamviec.dqtbonho, quayphimchupanh.dqtbonho);
            trackBarCAMERACHINH.Value = getmax(giaitri.dqtcamerachinh, hoctap.dqtcamerachinh, lamviec.dqtcamerachinh, quayphimchupanh.dqtcamerachinh);
            trackBarCAMERAPHU.Value = getmax(giaitri.dqtcameraphu, hoctap.dqtcameraphu, lamviec.dqtcameraphu, quayphimchupanh.dqtcameraphu);
            trackBarPIN.Value = getmax(giaitri.dqtpin, hoctap.dqtpin, lamviec.dqtpin, quayphimchupanh.dqtpin);
        }
        public int getmax(int x1,int x2,int x3,int x4)
        {
            int a=x1;
            if(a<x2)
                a=x2;
            if(a<x3)
                a=x3;
            if(a<x4)
                a=x4;
            return a;
        }
        private void checkBoxgiaitri_CheckedChanged(object sender, EventArgs e)
        {
            //ok
            if (checkBoxgiaitri.Checked == true)
            {
                giaitri.setTS("Vừa", "Thấp", "Vừa", "Vừa", "Tất cả", "Tất cả", "Vừa", "Tất cả");
                giaitri.setDQT(3, 3, 4, 3, 0, 0, 3, 0);
                setChoice();
            }
            else
            {
                giaitri = new MucDich();
                setChoice();
            }

        }
        private void checkBoxhoctap_CheckedChanged(object sender, EventArgs e)
        {
            //ok
            if (checkBoxhoctap.Checked == true)
            {
                hoctap.setTS("Thấp", "Thấp", "Thấp", "Thấp", "Thấp", "Tất cả", "Thấp", "Tất cả");
                hoctap.setDQT(2, 3, 3, 2, 0, 0, 3, 0);
                setChoice();
            }
            else 
            {
                hoctap = new MucDich();
                setChoice();
            }
        }
        private void checkBoxlamviec_CheckedChanged(object sender, EventArgs e)
        {
            //ok
            if (checkBoxlamviec.Checked == true)
            {
                lamviec.setTS("Cao", "Thấp", "Vừa", "Vừa", "Vừa", "Tất cả", "Vừa", "Tất cả");
                lamviec.setDQT(4, 3, 4, 4, 3, 0, 4, 0);
                setChoice();
            }
            else
            {
                lamviec = new MucDich();
                setChoice();
            }
        }
        private void checkBoxQuayphimChupAnh_CheckedChanged(object sender, EventArgs e)
        {
            //ok
            if (checkBoxQuayphimChupAnh.Checked == true)
            {
                quayphimchupanh.setTS("Cao","Thấp","Vừa","Vừa","Cao","Thấp","Cao","Tất cả");
                quayphimchupanh.setDQT(4, 4, 4, 4, 5, 4, 3, 4);
                setChoice();
            }
            else
            {
                quayphimchupanh = new MucDich();
                setChoice();
            }
        }

        private void HOTRO_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnect();
            this.Close();
        }

        private void btreset_Click(object sender, EventArgs e)
        {
            checkBoxgiaitri.Checked = false;
            checkBoxhoctap.Checked = false;
            checkBoxlamviec.Checked = false;
            checkBoxQuayphimChupAnh.Checked = false;
            txtgiacuoi.Text = "50000";
            txtgiadau.Text = "0";
            cbohang.Text=cboHDH.Text=cboMANHINH.Text=cboPIN.Text=cboRAM.Text=cboTheNho.Text=cboBoNho.Text=cboCAMERACHINH.Text=cboCAMERAPHU.Text=cboCPU.Text = "Tất cả";
            trackBarBONHO.Value = trackBarCAMERACHINH.Value = trackBarCAMERAPHU.Value = trackBarCPU.Value = trackBarHDH.Value = trackBarMANHINH.Value = trackBarPIN.Value = trackBarRAM.Value = trackBarTHENHO.Value = 0;

        }

        private void bttrolai_MouseHover(object sender, EventArgs e)
        {
            bttrolai.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
            bttrolai.ForeColor = Color.Black;
        }
        private void bttrolai_MouseLeave(object sender, EventArgs e)
        {
            bttrolai.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
            bttrolai.ForeColor = Color.White;
        }
        private void btKQHT_MouseHover(object sender, EventArgs e)
        {
            btKQHT.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
            btKQHT.ForeColor = Color.Black;
        }

        private void btKQHT_MouseLeave(object sender, EventArgs e)
        {
            btKQHT.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
            btKQHT.ForeColor = Color.White;
        }

        private void btreset_MouseHover(object sender, EventArgs e)
        {
              btreset.BackgroundImage = Image.FromFile("Image/TrangChu/button.png");
              btreset.ForeColor = Color.Black;
        }

        private void btreset_MouseLeave(object sender, EventArgs e)
        {
            btreset.BackgroundImage = Image.FromFile("Image/TrangChu/button3.png");
            btreset.ForeColor = Color.White;
        }

        private void bttrolai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class MucDich
    {
        public String manhinh, ram, cpu, pin, camerachinh, cameraphu, bonho, thenho;
        public int dqtmanhinh, dqtram, dqtcpu, dqtpin, dqtcamerachinh, dqtcameraphu, dqtbonho, dqtthenho;
        public MucDich()
        {
            this.manhinh = "Tất cả";
            this.ram = "Tất cả";
            this.cpu = "Tất cả";
            this.pin = "Tất cả";
            this.camerachinh = "Tất cả";
            this.cameraphu = "Tất cả";
            this.bonho = "Tất cả";
            this.thenho = "Tất cả";
            this.dqtmanhinh = 0;
            this.dqtram = 0;
            this.dqtcpu = 0;
            this.dqtpin = 0;
            this.dqtcamerachinh = 0;
            this.dqtcameraphu = 0;
            this.dqtbonho = 0;
            this.dqtthenho = 0;
        }
        public void setTS(String manhinh,String ram, String cpu, String pin, String camerachinh, String cameraphu, String bonho,String thenho)
        {            
            this.manhinh = manhinh;
            this.ram = ram;
            this.cpu = cpu;
            this.pin = pin;
            this.camerachinh = camerachinh;
            this.cameraphu = cameraphu;
            this.bonho = bonho;
            this.thenho = thenho;
        }
        public void setDQT(int dqtmanhinh, int dqtram, int dqtcpu, int dqtpin, int dqtcamerachinh, int dqtcameraphu, int dqtbonho, int dqtthenho)
        {
            this.dqtmanhinh = dqtmanhinh;
            this.dqtram = dqtram;
            this.dqtcpu = dqtcpu;
            this.dqtpin = dqtpin;
            this.dqtcamerachinh = dqtcamerachinh;
            this.dqtcameraphu = dqtcameraphu;
            this.dqtbonho = dqtbonho;
            this.dqtthenho = dqtthenho;
        }   
    }
}

