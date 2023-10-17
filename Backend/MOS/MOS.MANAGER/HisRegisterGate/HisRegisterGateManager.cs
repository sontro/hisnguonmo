using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.Filter;
using MOS.SDO;

namespace MOS.MANAGER.HisRegisterGate
{
    public partial class HisRegisterGateManager : BusinessBase
    {
        public HisRegisterGateManager()
            : base()
        {

        }

        public HisRegisterGateManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_REGISTER_GATE>> Get(HisRegisterGateFilterQuery filter)
        {
            ApiResultObject<List<HIS_REGISTER_GATE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REGISTER_GATE> resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterGateGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REGISTER_GATE> Create(HIS_REGISTER_GATE data)
        {
            ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_GATE resultData = null;
                if (valid && new HisRegisterGateCreate(param).Create(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REGISTER_GATE> Update(HIS_REGISTER_GATE data)
        {
            ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_GATE resultData = null;
                if (valid && new HisRegisterGateUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REGISTER_GATE> ChangeLock(long id)
        {
            ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_GATE resultData = null;
                if (valid)
                {
                    new HisRegisterGateLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REGISTER_GATE> Lock(long id)
        {
            ApiResultObject<HIS_REGISTER_GATE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_GATE resultData = null;
                if (valid)
                {
                    new HisRegisterGateLock(param).Lock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_REGISTER_GATE> Unlock(long id)
        {
            ApiResultObject<HIS_REGISTER_GATE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REGISTER_GATE resultData = null;
                if (valid)
                {
                    new HisRegisterGateLock(param).Unlock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRegisterGateTruncate(param).Truncate(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HisRegisterGateSDO>> GetCurrentNumOrder(HisRegisterGateCurrentNumOrderFilter filter)
        {
            ApiResultObject<List<HisRegisterGateSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisRegisterGateSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterGateGetSql(param).GetCurrentNumOrder(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<RegisterGateDepartmentSDO>> GetDepartment()
        {
            ApiResultObject<List<RegisterGateDepartmentSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<RegisterGateDepartmentSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterGateGetSql(param).GetDepartment();
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateNumOrder(List<HisRegisterGateSDO> data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRegisterGateUpdateNumOrder(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<IssueOrderNumberSDO> IssueOrderNumber(long data)
        {
            ApiResultObject<IssueOrderNumberSDO> result = new ApiResultObject<IssueOrderNumberSDO>(null, false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                IssueOrderNumberSDO resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterGateIssueOrderNumber(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_REGISTER_REQ>> Call(RegisterGateCallSDO data)
        {
            ApiResultObject<List<HIS_REGISTER_REQ>> result = new ApiResultObject<List<HIS_REGISTER_REQ>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_REGISTER_REQ> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegisterGateCallOrReCall(param).CallOrReCall(data, false, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_REGISTER_REQ>> ReCall(RegisterGateCallSDO data)
        {
            ApiResultObject<List<HIS_REGISTER_REQ>> result = new ApiResultObject<List<HIS_REGISTER_REQ>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_REGISTER_REQ> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRegisterGateCallOrReCall(param).CallOrReCall(data, true, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
