using AutoMapper;
using DevExpress.Utils.Menu;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.ConfigSystem;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.TransactionDebtCollect.ADO;
using HIS.Desktop.Plugins.TransactionDebtCollect.Config;
using HIS.Desktop.Print;
using Inventec.Common.Adapter;
//using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using Inventec.Fss.Client;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TransactionDebtCollect
{
    public partial class frmTransactionDebtCollect : HIS.Desktop.Utility.FormBase
    {
        private async Task FillInfoPatient(V_HIS_TREATMENT_FEE data)
        {
            try
            {
                if (data != null)
                {
                    txtPatient.Text = data.TDL_PATIENT_CODE;
                    txtPatientName.Text = data.TDL_PATIENT_NAME;
                    txtDOB.Text = Inventec.Common.DateTime.Convert.TimeNumberToDateString(data.TDL_PATIENT_DOB);
                    txtGender.Text = data.TDL_PATIENT_GENDER_NAME;
                    txtAddress.Text = data.TDL_PATIENT_ADDRESS;
                    //                    if (this.resultPatientType == null || this.resultPatientType.ID == 0)
                    //                    {
                    //                        this.resultPatientType = new BackendAdapter(new CommonParam())
                    //.Get<MOS.EFMODEL.DataModels.V_HIS_PATIENT_TYPE_ALTER>("api/HisPatientTypeAlter/GetViewLastByTreatmentId", ApiConsumers.MosConsumer, currentTreatment.ID, null);
                    //                    }
                    //                    if (resultPatientType != null)
                    //                    {
                    //                        string rightRoute = "";
                    //                        if (resultPatientType.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.TRUE)
                    //                        {
                    //                            rightRoute = Inventec.Common.Resource.Get.Value("IVT_LANGUAGE_KEY__FRM_TRANSACTION_DEBT_COLLECT__RIGHT_ROUTE_TRUE", Base.ResourceLangManager.LanguageFrmTransactionBill, LanguageManager.GetCulture());
                    //                        }
                    //                        else
                    //                        {
                    //                            rightRoute = Inventec.Common.Resource.Get.Value("IVT_LANGUAGE_KEY__FRM_TRANSACTION_DEBT_COLLECT__RIGHT_ROUTE_FALSE", Base.ResourceLangManager.LanguageFrmTransactionBill, LanguageManager.GetCulture());
                    //                        }
                    //                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static string TrimHeinCardNumber(string chucodau)
        {
            string result = "";
            try
            {
                result = System.Text.RegularExpressions.Regex.Replace(chucodau, @"[-,_ ]|[_]{2}|[_]{3}|[_]{4}|[_]{5}", "").ToUpper();
            }
            catch (Exception ex)
            {
                LogSystem.Error("Không thể tách thẻ BHYT");
            }
            return result;
        }

        private void FillInfoPatient(HIS_TRANSACTION data)
        {
            try
            {
                if (data != null)
                {
                    txtPatient.Text = data.TDL_PATIENT_CODE;
                    txtPatientName.Text = data.TDL_PATIENT_NAME;
                    txtDOB.Text = Inventec.Common.DateTime.Convert.TimeNumberToDateString(data.TDL_PATIENT_DOB ?? 0);
                    txtGender.Text = data.TDL_PATIENT_GENDER_NAME;
                    txtAddress.Text = data.TDL_PATIENT_ADDRESS;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
