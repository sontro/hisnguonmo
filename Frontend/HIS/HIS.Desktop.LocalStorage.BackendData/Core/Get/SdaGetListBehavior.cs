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
    class SdaGetListBehavior : BusinessBase, IGetDataT
    {
        object entity;
        string uri;
        internal SdaGetListBehavior(CommonParam param, object filter, string uriParam)
            : base(param)
        {
            this.entity = filter;
            this.uri = uriParam;
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                return new BackendAdapter(param).GetStrong<List<T>>(uri, ApiConsumers.SdaConsumer, param, entity, 0, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }

    class SdaGetZipListBehavior : BusinessBase, IGetDataT
    {
        object entity;
        string uri;
        internal SdaGetZipListBehavior(CommonParam param, object filter, string uriParam)
            : base(param)
        {
            this.entity = filter;
            this.uri = uriParam;
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                return new BackendAdapter(param).GetZip<List<T>>(uri, ApiConsumers.SdaConsumer, entity, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
