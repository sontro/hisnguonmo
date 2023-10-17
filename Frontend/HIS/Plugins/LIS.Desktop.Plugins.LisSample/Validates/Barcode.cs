using LIS.Desktop.Plugins.LisSample.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIS.Desktop.Plugins.LisSample.Validates
{
    class Barcode : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit textEdit;
        public Barcode()
        { }
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
               valid = valid && (textEdit!= null);
               if (valid)
               {
                   string str = " ";
                   string lengthtxt = textEdit.Text.Trim();

                   if (String.IsNullOrEmpty(lengthtxt))
                   {
                       valid = false;
                       str = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);

                   }
                   else
                   {
                       int LENGTH_BARCODE = 0;

                       try
                       {
                           LENGTH_BARCODE = int.Parse(LisConfigCFG.LENGTH_BARCODE);
                       }
                       catch (Exception ex)
                       {
                           Inventec.Common.Logging.LogSystem.Error(ex);
                       }


                       if (!String.IsNullOrEmpty(textEdit.Text) && Inventec.Common.String.CountVi.Count(textEdit.Text) != LENGTH_BARCODE) //Inventec.Common.String.CountVi.Count(textEdit.Text)
                       {
                           valid = false;
                           str = string.Format("Dữ liệu không đúng định dạng, mã Barcode phải {0} kí tự", LENGTH_BARCODE);

                       }

                   }
                   this.ErrorText = str;
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
