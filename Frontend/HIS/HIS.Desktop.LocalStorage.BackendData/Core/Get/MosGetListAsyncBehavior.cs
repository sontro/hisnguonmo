using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Get
{
    class MosGetListAsyncBehavior : BusinessBase, IGetDataTAsync
    {
        object entity;
        string uri;
        internal MosGetListAsyncBehavior(CommonParam param, object filter, string uriParam)
            : base(param)
        {
            this.entity = filter;
            this.uri = uriParam;
        }

        async Task<object> IGetDataTAsync.ExecuteAsync<T>()
        {
            try
            {
                return await new BackendAdapter(param).GetAsync<List<T>>(uri, ApiConsumers.MosConsumer, entity, param).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
