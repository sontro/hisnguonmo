using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDebtCollect.TransactionDebtCollect
{
    class TransactionDebtCollectFactory
    {
        internal static ITransactionDebtCollect MakeITransactionBill(CommonParam param, object[] data)
        {
            ITransactionDebtCollect result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_TREATMENT_FEE hisTreatment = null;
            long treatmentId = 0;
            List<long> ListTransactionId = null;
            RefeshReference refeshReference = null;
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
                            else if (data[i] is List<long>)
                            {
                                ListTransactionId = (List<long>)data[i];
                            }
                            else if (data[i] is long)
                            {
                                treatmentId = (long)data[i];
                            }
                            else if (data[i] is RefeshReference)
                            {
                                refeshReference = (RefeshReference)data[i];
                            }
                        }

                        if (moduleData != null && hisTreatment != null)
                        {
                            result = new TransactionDebtCollectBehavior(moduleData, param, hisTreatment);
                        }
                        else if (moduleData != null && ListTransactionId != null && ListTransactionId.Count > 0 && treatmentId > 0)
                        {
                            result = new TransactionDebtCollectBehavior(moduleData, param, ListTransactionId, treatmentId, refeshReference);
                        }
                        else if (moduleData != null && ListTransactionId != null && ListTransactionId.Count > 0)
                        {
                            result = new TransactionDebtCollectBehavior(moduleData, param, ListTransactionId);
                        }
                        else if (moduleData != null && treatmentId > 0)
                        {
                            result = new TransactionDebtCollectBehavior(moduleData, param, treatmentId);
                        }
                        else
                        {
                            result = new TransactionDebtCollectBehavior(moduleData, param);
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
