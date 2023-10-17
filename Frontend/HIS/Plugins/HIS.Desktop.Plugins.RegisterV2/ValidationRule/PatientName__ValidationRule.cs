using DevExpress.XtraEditors.Controls;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.RegisterV2.ValidationRule
{
    class PatientName__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtPatientName;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtPatientName != null);
                if (valid)
                {
                    string strError = "";
                    string patientName = txtPatientName.Text.Trim();
                    if (String.IsNullOrEmpty(patientName))
                    {
                        valid = false;
                        strError = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    }
                    else
                    {
                        string firstName = "";
                        string lastName = "";
                        int idx = patientName.LastIndexOf(" ");
                        if (idx > -1)
                        {
                            firstName = patientName.Substring(idx).Trim();
                            lastName = patientName.Substring(0, idx).Trim();
                        }
                        else
                        {
                            firstName = patientName;
                            lastName = "";
                        }
                        if (!String.IsNullOrEmpty(firstName) && firstName.Length > 30)
                        {
                            valid = false;
                            strError += ((!String.IsNullOrEmpty(strError) ? "\r\n" : "") + String.Format(ResourceMessage.TenBNVuotQuaMaxLength, 30));
                        }
                        if (!String.IsNullOrEmpty(lastName) && lastName.Length > 70)
                        {
                            valid = false;
                            strError += ((!String.IsNullOrEmpty(strError) ? "\r\n" : "") + String.Format(ResourceMessage.HoDemBNVuotQuaMaxLength, 70));
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