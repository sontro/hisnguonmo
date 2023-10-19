using ACS.MANAGER.Base;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.SdaTrouble
{
    public class SdaTroubleManager  : Inventec.Backend.MANAGER.BusinessBase
    {
        public SdaTroubleManager()
            : base()
        {

        }

        public SdaTroubleManager(CommonParam param)
            : base(param)
        {

        }

        public ApiResultObject<List<SDA_TROUBLE>> Get(SdaTroubleFilter filter)
        {
            ApiResultObject<List<SDA_TROUBLE>> result = null;
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => filter), filter) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<SDA_TROUBLE> resultData = null;
                if (valid)
                {
                    resultData = new SdaTroubleGet(param).Get(filter);
                }
                result = this.PackResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            #region Logging
            if (param.HasException)
            {
                LogInOut();
            }
            #endregion

            TroubleCheck();
            return result;
        }

        public ApiResultObject<bool> Create(string data)
        {
            ApiResultObject<bool> result = null;
            #region Logging Input Data
            try
            {
                Input = Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            #endregion

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid && new SdaTroubleCreate(param).Create(data))
                {
                    resultData = true;
                }
                result = this.PackResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            try
            {
                //LogWorker.SdaTroubleLog.LogCreate(result.Data, result.Success);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            #region Logging
            if (result == null || !result.Success)
            {
                LogInOut(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }
            #endregion

            TroubleCheck();
            return result;
        }

        public void Scan()
        {
            try
            {
                new SdaTroubleScan().Execute();
                TroubleCheck();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
        }
    }
}
