using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData.Core.Get
{
    class MosGetZipListBehavior : BusinessBase, IGetDataT
    {
        object entity;
        string uri;
        internal MosGetZipListBehavior(CommonParam param, object filter, string uriParam)
            : base(param)
        {
            this.entity = filter;
            this.uri = uriParam;
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                return new BackendAdapter(param).GetZip<List<T>>(uri, ApiConsumers.MosConsumer, entity, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
