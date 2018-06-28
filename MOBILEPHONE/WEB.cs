using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOBILEPHONE
{
    public partial class WEB : Form
    {
        public WEB()
        {
            InitializeComponent();
        }

        private void WEB_Load(object sender, EventArgs e)
        {
       
                try
                {
                    webBrowser.ScriptErrorsSuppressed = true;
                    webBrowser.Navigate(KetQuaHoTro.strAddress);
                }
                catch (Exception)
                {

                }
  
        }
  
    }
}
