using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2
{
    public partial class frmExpMestSaleCreateV2 : FormBase
    {
        private long roomTypeId { get; set; }
        private long roomId { get; set; }
        private UCExpMestSaleCreateV2 ucExpMestSaleCreateV2 { get; set; }
        private long? expMestId { get; set; }

        public frmExpMestSaleCreateV2()
        {
            InitializeComponent();
        }

        public frmExpMestSaleCreateV2(long roomTypeId, long roomId, long? expMestId)
        {
            InitializeComponent();
            try
            {
                this.roomTypeId = roomTypeId;
                this.roomId = roomId;
                this.expMestId = expMestId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmExpMestSaleCreateV2_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(Inventec.Desktop.Common.LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));

                ucExpMestSaleCreateV2 = new UCExpMestSaleCreateV2(roomTypeId, roomId, expMestId);
                ucExpMestSaleCreateV2.Dock = DockStyle.Fill;
                panelExpMestSaleCreateV2.Controls.Add(ucExpMestSaleCreateV2);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmExpMestSaleCreateV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ucExpMestSaleCreateV2 != null)
                {
                    ucExpMestSaleCreateV2.FromClosingEvent();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
