using HIS.Desktop.ADO;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PackingMaterial.PackingMaterial
{
    class PackingMaterialFactory
    {
        internal static IPackingMaterial MakeIPackingMaterial(CommonParam param, object[] data)
        {
            IPackingMaterial result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            HIS_DISPENSE packingMate = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is HIS_DISPENSE)
                            {
                                packingMate = (HIS_DISPENSE)data[i];
                            }
                        }
                        if (packingMate != null)
                        {
                            result = new PackingMaterialUpdateBehavior(param, moduleData, packingMate);
                        }
                        else
                        {
                            result = new PackingMaterialBehavior(param, moduleData);
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
