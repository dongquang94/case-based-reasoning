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
    public partial class WEBTK : Form
    {
        public WEBTK()
        {
            InitializeComponent();
        }

        private void WEBTK_Load(object sender, EventArgs e)
        {

            try
            {
                webBrowser.ScriptErrorsSuppressed = true;
                webBrowser.Navigate(TimKiem.strAddressTK);
            }
            catch (Exception)
            {

            }
        }

   
    }
}
