using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    public partial class UcWordFull : UserControl
    {
        Action<decimal> actChangeZoom;
        public UcWordFull(Action<decimal> actChangeZoom)
        {
            InitializeComponent();
            this.actChangeZoom = actChangeZoom;
        }

        private void txtDescription_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //WordProcess.zoomFactor(txtDescription);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtDescription_ZoomChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.actChangeZoom != null)
                {
                    this.actChangeZoom((decimal)txtDescription.ActiveView.ZoomFactor);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
