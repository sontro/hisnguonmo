using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PatientInfo
{
    class ValidationBHYTNumber : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtSoThe;
        internal List<MOS.EFMODEL.DataModels.HIS_BHYT_BLACKLIST> BhytBlackLists;
        internal List<MOS.EFMODEL.DataModels.HIS_BHYT_WHITELIST> BhytWhiteLists;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtSoThe != null);
                if (txtSoThe.Enabled == false)
                    return valid;
                //Neu doi tuong benh nhan la Bhyt
                valid = valid && (!String.IsNullOrEmpty(txtSoThe.Text) && !String.IsNullOrEmpty(txtSoThe.Text.Trim()));
                if (valid)
                {
                    string currentValue = txtSoThe.Text.Replace(" ", "").ToUpper();
                    string heincardNumber = HeinUtils.TrimHeinCardNumber(currentValue);
                    valid = valid && (new MOS.LibraryHein.Bhyt.BhytHeinProcessor().IsValidHeinCardNumber(heincardNumber) && heincardNumber.Length == 15);
                    if (!valid)
                    {
                        this.ErrorText = "Số thẻ BHYT không hợp lệ";
                    }
                    else //Ktra Thẻ hợp lệ là thẻ: có đầu mã thẻ nằm trong danh sách được khai báo trong HIS_BHYT_WHITE_LIST và ko nằm trong d/s các thẻ trong HIS_BHYT_BLACK_LIST
                    {
                        string heinCardNumberCode = heincardNumber.Substring(0, 3).ToString();
                        var lstWhite = BhytWhiteLists.Where(p => p.BHYT_WHITELIST_CODE == heinCardNumberCode).ToList();
                        if (lstWhite != null && lstWhite.Count() > 0)
                        {
                            if (BhytBlackLists != null)
                            {
                                foreach (var itemBlack in BhytBlackLists)
                                {
                                    if (heincardNumber.StartsWith(itemBlack.HEIN_CARD_NUMBER))
                                    {
                                        this.ErrorText = "Số thẻ BHYT nằm trong danh sách đen";
                                        valid = valid && false;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.ErrorText = "Số thẻ BHYT không hợp lệ";
                            valid = valid && false;
                        }
                    }
                }
                else
                {
                    valid = true;
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
