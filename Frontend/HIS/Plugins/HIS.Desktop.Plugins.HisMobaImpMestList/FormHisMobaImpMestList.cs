using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisMobaImpMestList
{
    public partial class FormHisMobaImpMestList : HIS.Desktop.Utility.FormBase
    {
        UCHisMobaImpMestList ucMobaImpMestList;
        long roomId = 0;
        long roomTypeId = 0;
        string expMestCode = "";
        string treatmentCode = "";

        public FormHisMobaImpMestList()
        {
            InitializeComponent();
        }

        public FormHisMobaImpMestList(Inventec.Desktop.Common.Modules.Module module, ADO.MobaImpMestListADO ado)
            : this()
        {
            try
            {
                this.Text = module.text;
                this.roomId = module.RoomId;
                this.roomTypeId = module.RoomTypeId;
                this.expMestCode = ado.ExpMestCode;
                this.treatmentCode = ado.TreatmentCode;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void FormHisMobaImpMestList_Load(object sender, EventArgs e)
        {
            try
            {
                SetIcon();
                if (!String.IsNullOrEmpty(expMestCode))
                {
                    this.ucMobaImpMestList = new UCHisMobaImpMestList(roomId, roomTypeId, expMestCode);
                    if (this.ucMobaImpMestList != null)
                    {
                        this.panelControl.Controls.Add(this.ucMobaImpMestList);
                        this.ucMobaImpMestList.Dock = DockStyle.Fill;
                    }
                }
                else if (!string.IsNullOrEmpty(treatmentCode))
                {
                    this.ucMobaImpMestList = new UCHisMobaImpMestList(roomId, treatmentCode, roomTypeId);
                    if (this.ucMobaImpMestList != null)
                    {
                        this.panelControl.Controls.Add(this.ucMobaImpMestList);
                        this.ucMobaImpMestList.Dock = DockStyle.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ucMobaImpMestList != null)
                {
                    ucMobaImpMestList.Search();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ucMobaImpMestList != null)
                {
                    ucMobaImpMestList.Refreshs();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void barButtonItemFocus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (ucMobaImpMestList != null)
                {
                    ucMobaImpMestList.FocusCode();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
