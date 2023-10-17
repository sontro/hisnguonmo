using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisDebateTemp.Validate
{
    class ValidateMaxLeng:DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtControl;
        internal int? MaxLength;
        public override bool Validate(Control control, object value)
        {
            bool success = true;
            try
            {
                success=success && (txtControl!=null);
                if (success)
                {
                    string strError = "";
                    string LengthtxtControl = txtControl.Text.Trim();
                    int? CountLangth = Inventec.Common.String.CountVi.Count(LengthtxtControl);
                    if (String.IsNullOrEmpty(LengthtxtControl))
                    {
                        success = false;
                        strError = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    }
                    else
                    {
                        if (CountLangth > MaxLength)
                        {
                            success = false;
                            strError += ((!string.IsNullOrEmpty(LengthtxtControl) ? "\r\n" : "") + String.Format("Trường dữ liệu vượt quá giới hạn.",MaxLength));
                        }
                    }
                    this.ErrorText = strError;
                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return success;
        }
    }
}
