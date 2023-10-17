using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.TransactionBillTwoInOne
{
    class TransactionBillTwoInOneFactory
    {
        internal static ITransactionBillTwoInOne MakeITransactionBillTwoInOne(CommonParam param, object[] data)
        {
            ITransactionBillTwoInOne result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_TREATMENT_FEE hisTreatment = null;
            V_HIS_PATIENT_TYPE_ALTER resultPatientType = null;
            List<V_HIS_SERE_SERV_5> listSereServ5 = null;
            bool? isBill = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is V_HIS_TREATMENT_FEE)
                            {
                                hisTreatment = (V_HIS_TREATMENT_FEE)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is List<V_HIS_SERE_SERV_5>)
                            {
                                listSereServ5 = (List<V_HIS_SERE_SERV_5>)data[i];
                            }
                            else if (data[i] is V_HIS_PATIENT_TYPE_ALTER)
                            {
                                resultPatientType = (V_HIS_PATIENT_TYPE_ALTER)data[i];
                            }
                            else if (data[i] is bool)
                            {
                                isBill = (bool)data[i];
                            }
                        }

                        if (moduleData != null && hisTreatment != null)
                        {
                            result = new TransactionBillTwoInOneBehavior(moduleData, param, hisTreatment, listSereServ5, resultPatientType, isBill);
                        }
                        else
                        {
                            result = new TransactionBillTwoInOneBehavior(moduleData, param, isBill);
                        }
                    }
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
