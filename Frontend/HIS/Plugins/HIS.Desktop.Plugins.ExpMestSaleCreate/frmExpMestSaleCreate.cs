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

namespace HIS.Desktop.Plugins.ExpMestSaleCreate
{
    public partial class frmExpMestSaleCreate : FormBase
    {
        private long roomTypeId { get; set; }
        private long roomId { get; set; }
        private UCExpMestSaleCreate ucExpMestSaleCreate { get; set; }
        private long? expMestId { get; set; }

        public frmExpMestSaleCreate()
        {
            InitializeComponent();
        }

        public frmExpMestSaleCreate(long roomTypeId, long roomId, long? expMestId)
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

        private void frmExpMestSaleCreate_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(Inventec.Desktop.Common.LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));

                //ucExpMestSaleCreate = new UCExpMestSaleCreate(roomTypeId, roomId, expMestId);
                //ucExpMestSaleCreate.Dock = DockStyle.Fill;
                //panelExpMestSaleCreate.Controls.Add(ucExpMestSaleCreate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmExpMestSaleCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ucExpMestSaleCreate != null)
                {
                    ucExpMestSaleCreate.FromClosingEvent();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
