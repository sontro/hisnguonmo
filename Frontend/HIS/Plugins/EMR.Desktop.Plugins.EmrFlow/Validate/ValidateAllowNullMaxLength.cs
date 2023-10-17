using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrFlow.Validate
{
    class ValidateAllowNullMaxLength:DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtControl;
        internal int? Maxlangth;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool Vali = true;
            try
            {
                Vali = Vali && (txtControl!=null);
                if (Vali)
                {
                    string strError = "";
                    string LenthtxtComtrol = txtControl.Text.Trim();
                    int? CoutLength = Inventec.Common.String.CountVi.Count(LenthtxtComtrol);
                    if (CoutLength > Maxlangth)
                    {
                        Vali=false;
                        strError +=((!string.IsNullOrEmpty(LenthtxtComtrol) ? "\r\n" :"")+ String.Format(ResourcesMassage.TruongDuLieuVuotQuaMaxLength,Maxlangth));
                    }
                    this.ErrorText=strError;
                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return Vali;
        }
    }
}
