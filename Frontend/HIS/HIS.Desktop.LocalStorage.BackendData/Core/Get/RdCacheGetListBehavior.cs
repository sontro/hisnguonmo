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
    class RdCacheGetListBehavior : BusinessBase, IGetDataT
    {
        object entity;
        string uriRDService;
        string uriService;
        Inventec.Common.WebApiClient.ApiConsumer apiConsumer;

        internal RdCacheGetListBehavior(CommonParam param, object filter, string uriRDService, string uriService, Inventec.Common.WebApiClient.ApiConsumer apiConsumer)
            : base(param)
        {
            this.entity = filter;
            this.uriRDService = uriRDService;
            this.uriService = uriService;
            this.apiConsumer = apiConsumer;
        }

        internal RdCacheGetListBehavior(CommonParam param, object filter, string uriRDService)
            : base(param)
        {
            this.entity = filter;
            this.uriRDService = uriRDService;
        }

        object IGetDataT.Execute<T>()
        {
            try
            {
                List<T> rs = new BackendAdapter(param).GetStrong<List<T>>(uriRDService, ApiConsumers.RdCacheConsumer, param, entity, 0, null);
                if (rs == null || rs.Count == 0)
                {
                    rs = new BackendAdapter(param).GetStrong<List<T>>(this.uriService, apiConsumer, param, entity, 0, null);
                }
                return rs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
