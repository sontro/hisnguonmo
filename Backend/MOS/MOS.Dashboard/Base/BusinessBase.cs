using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.Dashboard.Base
{
    public abstract class BusinessBase : EntityBase
    {
        public BusinessBase()
            : base()
        {
            param = new CommonParam();
            try
            {
                UserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            }
            catch (Exception)
            {
            }
        }

        public BusinessBase(CommonParam paramBusiness)
            : base()
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
            try
            {
                UserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            }
            catch (Exception)
            {
            }
        }

        protected CommonParam param { get; set; }

        protected void TroubleCheck()
        {
            try
            {
                if (param.HasException || (param.BugCodes != null && param.BugCodes.Count > 0))
                {
                    MethodName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
                    TroubleCache.Add(GetInfoProcess() + (param.HasException ? "param.HasException." : "") + param.GetBugCode());
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

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

        public ApiResultObject<T> PackResult<T>(T data, bool isSuccess)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(data, isSuccess, param);
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        public ApiResultObject<T> PackSuccess<T>(T data)
        {
            ApiResultObject<T> result = new ApiResultObject<T>();
            try
            {
                result.SetValue(data, true, param);
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
                if (typeof(T) == typeof(bool))
                {
                    bool t = bool.Parse(data.ToString());
                    result.SetValue(data, t, param);
                }
                else
                {
                    result.SetValue(data, data != null, param);
                }
            }
            catch (Exception ex)
            {
                Logging("Co exception khi dong goi ApiResultObject.", LogType.Error);
                LogSystem.Error(ex);
            }
            return result;
        }

        internal List<string> GetBugCodes()
        {
            return this.param != null ? this.param.BugCodes : null;
        }

        internal List<string> GetMessages()
        {
            return this.param != null ? this.param.Messages : null;
        }

        internal void Logger<T>(ApiResultObject<T> rs, object inputData)
        {
            try
            {
                string className = this.GetType().Name;
                string methodName = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;

                if (rs == null || !rs.Success || rs.Param.HasException)
                {
                    string parameterValues = "";

                    if (inputData != null)
                    {
                        parameterValues += Newtonsoft.Json.JsonConvert.SerializeObject(inputData);
                    }
                    else
                    {
                        parameterValues += "null";
                    }

                    string log = string.Format("\n--Class: {0} \n--Method: {1} \n--Input: {2} \n--Output: {3}", className, methodName, parameterValues, Newtonsoft.Json.JsonConvert.SerializeObject(rs.Data));
                    LogSystem.Error(log);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
