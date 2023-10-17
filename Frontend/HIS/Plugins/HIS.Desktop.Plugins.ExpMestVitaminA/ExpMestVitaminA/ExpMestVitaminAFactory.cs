using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestVitaminA.ExpMestVitaminA
{
    class ExpMestVitaminAFactory
    {
        internal static IExpMestVitaminA MakeIExpMestVitaminA(CommonParam param, object[] data)
        {
            IExpMestVitaminA result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            List<V_HIS_VITAMIN_A> vitaminA = null;
            DelegateSelectData dlg = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is List<V_HIS_VITAMIN_A>)
                            {
                                vitaminA = (List<V_HIS_VITAMIN_A>)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is HIS.Desktop.Common.DelegateSelectData)
                            {
                                dlg = (HIS.Desktop.Common.DelegateSelectData)data[i];
                            }
                        }

                        if (moduleData != null && vitaminA != null && dlg != null)
                        {
                            result = new ExpMestVitaminABehavior(moduleData, param, vitaminA, dlg);
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
