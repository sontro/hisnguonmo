using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.HisDebateTemp.Validate
{
    class ValidateMaxLengAllowNull :DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtcontrol;
        internal int? Maxlangth;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vali = true;
            try
            {
                vali = vali && (txtcontrol != null);
                if (vali)
                {
                    string strError = "";
                    string LengthtxtControl = txtcontrol.Text.Trim();
                    int? CountLength = Inventec.Common.String.CountVi.Count(LengthtxtControl);
                    if (CountLength > Maxlangth)
                    {
                        vali = false;
                        strError += ((!string.IsNullOrEmpty(LengthtxtControl) ? "\r\n" : "") + String.Format("Trường dữ liệu vượt quá giới hạn.", Maxlangth));
                    }
                    this.ErrorText = strError;
                }
            }
            catch (Exception ex)
            {

                LogSystem.Error(ex);
            }
            return vali;
        }
    }
}
