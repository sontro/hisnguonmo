using HIS.Desktop.ADO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisImportMestMedicine
{
    public partial class frmHisImportMestMedicine : HIS.Desktop.Utility.FormBase
    {
        long roomId = 0;
        long roomTypeId = 0;
        long impMestTypeId = 0;
        MobaImpMestListADO mobaImpMestListADO = null;

        Inventec.Desktop.Common.Modules.Module currentModule;

        public frmHisImportMestMedicine()
        {
            InitializeComponent();
        }

        public frmHisImportMestMedicine(Inventec.Desktop.Common.Modules.Module _module)
            : base(_module)
        {
            InitializeComponent();
            try
            {
                this.roomId = _module.RoomId;
                this.roomTypeId = _module.RoomTypeId;
                this.currentModule = _module;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        public frmHisImportMestMedicine(Inventec.Desktop.Common.Modules.Module _module, long impMestTypeId, MobaImpMestListADO mobaImpMestListADO)
        {
            InitializeComponent();
            try
            {
                this.roomId = _module.RoomId;
                this.roomTypeId = _module.RoomTypeId;
                this.currentModule = _module;

                this.impMestTypeId = impMestTypeId;
                this.mobaImpMestListADO = mobaImpMestListADO;
                UCHisImportMestMedicine uCHisImportMestMedicine = new UCHisImportMestMedicine(this.currentModule, this.impMestTypeId, this.mobaImpMestListADO);
                this.panelControl1.Controls.Add(uCHisImportMestMedicine);
                uCHisImportMestMedicine.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void frmHisImportMestMedicine_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
