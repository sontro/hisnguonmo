using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.Add;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.MessageBoxForm
{
    public partial class frmMessageBoxChooseVT : DevExpress.XtraEditors.XtraForm
    {
        ChonVTTrongKho chonVTTrongKho;
        public frmMessageBoxChooseVT(ChonVTTrongKho _chonThuocTrongKhoCungHoatChat)
        {
            InitializeComponent();
            this.chonVTTrongKho = _chonThuocTrongKhoCungHoatChat;
        }

        private void frmMessageBoxChooseMedicineTypeAcin_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho == null)
                {
                    throw new ArgumentNullException("chonThuocTrongKhoCungHoatChat is null");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnChonThuocNgoaiKho_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho != null)
                {
                    this.chonVTTrongKho(EnumOptionChonVatTuThayThe.VatTuNgoaiKho);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chonVTTrongKho != null)
                {
                    this.chonVTTrongKho(EnumOptionChonVatTuThayThe.None);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}