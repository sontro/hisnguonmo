using HIS.Desktop.ADO;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay.NumOrderUpdate
{
    class NumOrderUpdateFactory
    {
        internal static INumOrderUpdate MakeINumOrderUpdate(CommonParam param, object[] data)
        {
            INumOrderUpdate result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_TRANSACTION transaction = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is V_HIS_TRANSACTION)
                            {
                                transaction = (V_HIS_TRANSACTION)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null && transaction != null)
                        {
                            result = new NumOrderUpdateBehavior(moduleData, param, transaction);
                        }
                    }
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
               LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + LogUtil.TraceData("data", data), ex);
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
