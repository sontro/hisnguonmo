using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.TransactionList
{
    class TransactionListFactory
    {
        internal static ITransactionList MakeITransactionList(CommonParam param, object[] data)
        {
            ITransactionList result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_TREATMENT_FEE hisTreatment = null;
            V_HIS_ACCOUNT_BOOK hisAccountBook = null;
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
                            else if (data[i] is V_HIS_ACCOUNT_BOOK)
                            {
                                hisAccountBook = (V_HIS_ACCOUNT_BOOK)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null)
                        {
                            if (hisTreatment != null)
                            {
                                result = new TransactionListBehavior(moduleData, param, hisTreatment);
                            }
                            else if (hisAccountBook != null)
                            {
                                result = new TransactionListBehavior(moduleData, param, hisAccountBook);
                            }
                            else
                            {
                                result = new TransactionListBehavior(moduleData, param);
                            }
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
