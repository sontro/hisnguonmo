using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    class GetData : BusinessBase, IGetDataT
    {
        object filter;
        internal GetData(CommonParam param)
            : base(param)
        {
        }

        internal GetData(CommonParam param, object filterquery)
            : base(param)
        {
            filter = filterquery;
        }

        object IGetDataT.Execute<T>()
        {
            object result = null;
            try
            {
                Type type = typeof(T);
                IGetDataT behavior = GetDataBehaviorFactory.MakeIGetList(param, type, filter);
                result = behavior != null ? (List<T>)System.Convert.ChangeType(behavior.Execute<T>(), typeof(List<T>)) : null;
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
