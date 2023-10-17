using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HIS.Desktop.Plugins.HisPtttTable.Validate
{
    class ValiDateMaxLength_TypeCode:DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtcontrol;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtcontrol != null);
                if(valid)
                {
                    string strError="";
                    string DocHoldType=txtcontrol.Text.Trim();
                    int? CoutLength=Inventec.Common.String.CountVi.Count(DocHoldType);
                    if (String.IsNullOrEmpty(DocHoldType))
                    {
                        valid = false;
                        strError = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    }
                    else
                    {
                        if (CoutLength > 4)
                        {
                            valid = false;
                            strError += ((!string.IsNullOrEmpty(strError) ? "\r\n" : "") + String.Format(ResourcesMassage.MaBanMoVuaQuaMaxLength, 4));
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
