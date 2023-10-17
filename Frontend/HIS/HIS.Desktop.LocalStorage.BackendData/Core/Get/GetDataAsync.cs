using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    class GetDataAsync : BusinessBase, IGetDataTAsync
    {
        object filter;
        internal GetDataAsync(CommonParam param)
            : base(param)
        {
        }

        internal GetDataAsync(CommonParam param, object filterquery)
            : base(param)
        {
            filter = filterquery;
        }

        async Task<object> IGetDataTAsync.ExecuteAsync<T>()
        {
            object result = null;
            try
            {
                Type type = typeof(T);
                IGetDataTAsync behavior = GetDataBehaviorFactory.MakeIGetListAsync(param, type, filter);
                result = behavior != null ? (List<T>)System.Convert.ChangeType(await behavior.ExecuteAsync<T>(), typeof(List<T>)) : null;
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
