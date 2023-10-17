using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Desktop.Common.Controls.ValidationRule;
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

namespace HIS.Desktop.Plugins.Library.PrintBordereau.ChooseDepartment
{
    public partial class frmBordereauChooseDepartment : Form
    {
        private void Valid__Department()
        {
            try
            {
                GridLookupEditWithTextEditValidationRule icdExam = new GridLookupEditWithTextEditValidationRule();
                icdExam.txtTextEdit = txtDepartmentCode;
                icdExam.cbo = cboDepartment;
                icdExam.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdExam.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtDepartmentCode, icdExam);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
