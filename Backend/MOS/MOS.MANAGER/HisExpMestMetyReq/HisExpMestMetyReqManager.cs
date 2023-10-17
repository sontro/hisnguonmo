using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    public partial class HisExpMestMetyReqManager : BusinessBase
    {
        public HisExpMestMetyReqManager()
            : base()
        {

        }
        
        public HisExpMestMetyReqManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXP_MEST_METY_REQ>> Get(HisExpMestMetyReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_METY_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_METY_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestMetyReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_METY_REQ> Create(HIS_EXP_MEST_METY_REQ data)
        {
            ApiResultObject<HIS_EXP_MEST_METY_REQ> result = new ApiResultObject<HIS_EXP_MEST_METY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_METY_REQ resultData = null;
                if (valid && new HisExpMestMetyReqCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXP_MEST_METY_REQ> Update(HIS_EXP_MEST_METY_REQ data)
        {
            ApiResultObject<HIS_EXP_MEST_METY_REQ> result = new ApiResultObject<HIS_EXP_MEST_METY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_METY_REQ resultData = null;
                if (valid && new HisExpMestMetyReqUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_METY_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_METY_REQ> result = new ApiResultObject<HIS_EXP_MEST_METY_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_METY_REQ resultData = null;
                if (valid)
                {
                    new HisExpMestMetyReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_METY_REQ> Lock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_METY_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_METY_REQ resultData = null;
                if (valid)
                {
                    new HisExpMestMetyReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST_METY_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_EXP_MEST_METY_REQ> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST_METY_REQ resultData = null;
                if (valid)
                {
                    new HisExpMestMetyReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpMestMetyReqTruncate(param).Truncate(id);
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
    }
}
