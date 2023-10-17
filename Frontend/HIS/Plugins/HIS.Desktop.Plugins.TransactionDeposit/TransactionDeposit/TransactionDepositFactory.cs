using HIS.Desktop.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDeposit.TransactionDeposit
{
    class TransactionDepositFactory
    {
        internal static ITransactionDeposit MakeITransactionDeposit(CommonParam param, object[] data)
        {
            ITransactionDeposit result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                TransactionDepositADO ado = null;
                    if (data.GetType() == typeof(object[]))
                    {
                        if (data != null && data.Count() > 0)
                        {
                            for (int i = 0; i < data.Count(); i++)
                            {
                                if (data[i] is TransactionDepositADO)
                                {
                                    ado = (TransactionDepositADO)data[i];
                                }
                                else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                                {
                                    moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                                }
                            }

                            if (moduleData != null && ado != null)
                            {
                                result = new TransactionDepositBehavior(moduleData, param, ado);
                            }
                            else
                            {
                                result = new TransactionDepositInMenuBehavior(moduleData, param);
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
