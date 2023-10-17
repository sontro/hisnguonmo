using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.RegisterExamKiosk
{
    class NameForm
    {
        public const string frmCheckHeinCardGOV = "frmCheckHeinCardGOV";
        public const string frmDetail = "frmDetail";
        public const string frmRegisterExamKiosk = "frmRegisterExamKiosk";
        public const string frmWaitingScreen = "frmWaitingScreen";
        public const string frmInputSave1 = "frmInputSave1";
        public const string frmServiceRoom = "frmServiceRoom";
        public const string frmInputSave = "frmInputSave";
        public const string frmRegisteredExam = "frmRegisteredExam";
        public const string frmChooseObject = "frmChooseObject";
        public const string frmInformationObject = "frmInformationObject";
        public static void CloseAllForm()
        {
            try
            {
                List<String> lsNameForm = new List<string>();
                lsNameForm.Add(NameForm.frmDetail);
                lsNameForm.Add(NameForm.frmInputSave1);
                lsNameForm.Add(NameForm.frmInputSave);
                lsNameForm.Add(NameForm.frmServiceRoom);
                lsNameForm.Add(NameForm.frmCheckHeinCardGOV);
                lsNameForm.Add(NameForm.frmRegisterExamKiosk);
                lsNameForm.Add(NameForm.frmRegisteredExam);
                lsNameForm.Add(NameForm.frmChooseObject);
                lsNameForm.Add(NameForm.frmInformationObject);
                foreach (var item in lsNameForm)
                {
                    Form fc = Application.OpenForms[item];
                    if (fc != null)
                    {
                        fc.Dispose();
                        fc.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        public static void CloseOtherForm()
        {
            try
            {
                List<String> lsNameForm = new List<string>();
                lsNameForm.Add(NameForm.frmDetail);
                lsNameForm.Add(NameForm.frmRegisterExamKiosk);
                lsNameForm.Add(NameForm.frmInputSave1);
                lsNameForm.Add(NameForm.frmInputSave);
                lsNameForm.Add(NameForm.frmServiceRoom);
                lsNameForm.Add(NameForm.frmCheckHeinCardGOV);
                lsNameForm.Add(NameForm.frmRegisteredExam);
                lsNameForm.Add(NameForm.frmChooseObject);
                lsNameForm.Add(NameForm.frmInformationObject);
                foreach (var item in lsNameForm)
                {
                    Form fc = Application.OpenForms[item];
                    if (fc != null)
                    {
                        fc.Dispose();
                        fc.Close();
                    }
                }

                Form Mc = Application.OpenForms[NameForm.frmWaitingScreen];
                if (Mc != null)
                {
                    Mc.Show();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
