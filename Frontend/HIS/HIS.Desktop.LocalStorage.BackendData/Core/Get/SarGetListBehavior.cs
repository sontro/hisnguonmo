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
    class SarGetListBehavior : BusinessBase, IGetDataT
    {
        object entity;
        string uri;
        internal SarGetListBehavior(CommonParam param, object filter, string uriParam)
            : base(param)
        {
            this.entity = filter;
            this.uri = uriParam;
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                return new BackendAdapter(param).GetStrong<List<T>>(uri, ApiConsumers.SarConsumer, param, entity, 0, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
