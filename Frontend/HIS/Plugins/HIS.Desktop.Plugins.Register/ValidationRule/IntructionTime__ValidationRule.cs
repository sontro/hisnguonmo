using DevExpress.XtraEditors.Controls;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.ValidationRule
{
    class IntructionTime__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtIntructionTime;
        //internal DevExpress.XtraEditors.DateEdit dtIntructionTime;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtIntructionTime != null);
                if (valid)
                {
                    string strError = "";
                    if (string.IsNullOrEmpty(this.txtIntructionTime.Text))
                    {
                        //strError = ResourceMessage.TruongDuLieuBatBuoc;
                        return false;
                    }
                    else
                    {
                        DateTime? dt = DateTimeHelper.ConvertDateStringToSystemDate(this.txtIntructionTime.Text);
                        if (dt == null || dt.Value == DateTime.MinValue)
                        {
                            valid = false;
                            strError = ResourceMessage.NhapNgayThangKhongDungDinhDang;
                        }
                        else if (this.txtIntructionTime.Text.ToString().Substring(6, 1) == "0")
                        {
                            valid = false;
                            strError = ResourceMessage.NhapNgayThangKhongDungDinhDang;
                        }
                        else
                            try
                            {
                                DateTime.ParseExact(this.txtIntructionTime.Text, "dd/MM/yyyy HH:mm", null);
                            }
                            catch (Exception ex)
                            {
                                valid = false;
                                strError = ResourceMessage.NhapNgayThangKhongDungDinhDang;
                                Inventec.Common.Logging.LogSystem.Error(ex);
                            }
                    }
                    this.ErrorText = strError;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
