using HIS.Desktop.Common;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.DiimRoom.DiimRoom
{
    public partial class frmPacsView : HIS.Desktop.Utility.FormBase
    {
        RefeshReference refeshReference;
        V_HIS_SERE_SERV_6 sereServ6Current;

        public frmPacsView()
        {
            InitializeComponent();
        }

        public frmPacsView(Inventec.Desktop.Common.Modules.Module module, V_HIS_SERE_SERV_6 sereServ6)
            : this(module, sereServ6, null)
        {
        }

        public frmPacsView(Inventec.Desktop.Common.Modules.Module module, V_HIS_SERE_SERV_6 sereServ6, RefeshReference _refeshReference)
            : base(module)
        {
            InitializeComponent();
            this.refeshReference = _refeshReference;
            this.sereServ6Current = sereServ6;
        }

        private void frmPacsView_Load(object sender, EventArgs e)
        {
            try
            {
                UCPacsView UCPacsView = new UCPacsView(this.currentModuleBase, this.sereServ6Current);
                UCPacsView.Dock = DockStyle.Fill;
                this.Controls.Add(UCPacsView);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
    }
}
