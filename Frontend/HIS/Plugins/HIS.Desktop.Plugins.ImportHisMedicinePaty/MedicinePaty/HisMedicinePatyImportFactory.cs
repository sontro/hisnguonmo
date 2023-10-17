using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImportHisMedicinePaty.MedicinePaty;

namespace HIS.Desktop.Plugins.ImportHisMedicinePaty.MedicinePaty.Run
{
    class HisMedicineImportFactory
    {
        internal static IHisImportMedicinePaty MakeIHisImportMedicinePaty(CommonParam param, object[] data)
        {
            IHisImportMedicinePaty result = null;
            try
            {
                result = new HisImportMedicinePatyBehavior(param, data);
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
