using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public abstract class BusinessBase : EntityBase
    {
        public BusinessBase()
            : base()
        {
            param = new CommonParam();
        }

        public BusinessBase(CommonParam paramBusiness)
            : base()
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        protected CommonParam param { get; set; }

        //protected void LogInput(object data)
        //{
        //    try
        //    {
        //        Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), Newtonsoft.Json.JsonConvert.SerializeObject(data)) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //    }
        //}

        public ApiResultObject<T> PackCollectionResult<T>(T listData)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(listData, listData != null, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<T> PackSingleResult<T>(T data)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(data, data != null, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public List<T> ProcessDataFromDB<T>(string uri, Inventec.Common.WebApiClient.ApiConsumer consumer, object rfilter)
        {
            List<T> datas = null;
            try
            {
                Type type = typeof(T);
                CommonParam param = new CommonParam();
                if (rfilter != null)
                {
                    datas = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<T>>(uri, consumer, rfilter, param);
                }
                else
                {
                    dynamic dfilter = new System.Dynamic.ExpandoObject();
                    dfilter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                    datas = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<T>>(uri, consumer, dfilter, param);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return datas;
        }

    }
}
