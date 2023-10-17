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

namespace HIS.Desktop.Plugins.HoreHandover
{
    public partial class frmHoreHandover : FormBase
    {
        UCHoreHandover uc = null;
        public frmHoreHandover(Inventec.Desktop.Common.Modules.Module module)
            : base(module)
        {
            InitializeComponent();
            uc = new UCHoreHandover(this.currentModuleBase);
            layoutControl1.Controls.Add(uc);
        }

        public frmHoreHandover(Inventec.Desktop.Common.Modules.Module module, long horeHandoverId)
            : base(module)
        {
            InitializeComponent();
            uc = new UCHoreHandover(this.currentModuleBase, horeHandoverId);
            layoutControl1.Controls.Add(uc);
        }

        private void frmHoreHandover_Load(object sender, EventArgs e)
        {

        }

        private void barBtnHoreFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc!=null)
                {
                    this.uc.FIND();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barBtnHoreRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc != null)
                {
                    this.uc.REFRESH();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc != null)
                {
                    this.uc.SAVE();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc != null)
                {
                    this.uc.PRINT();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barBtnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc != null)
                {
                    this.uc.NEW();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barBtnFocus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.uc != null)
                {
                    this.uc.FOCUS();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
