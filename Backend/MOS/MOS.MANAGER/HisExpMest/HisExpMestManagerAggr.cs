using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Aggr;
using MOS.MANAGER.HisExpMest.Aggr.Approve;
using MOS.MANAGER.HisExpMest.Aggr.Create;
using MOS.MANAGER.HisExpMest.Aggr.Delete;
using MOS.MANAGER.HisExpMest.Aggr.Export;
using MOS.MANAGER.HisExpMest.Aggr.Remove;
using MOS.MANAGER.HisExpMest.Aggr.Unapprove;
using MOS.MANAGER.HisExpMest.Aggr.Unexport;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> AggrCreate(HisExpMestAggrSDO data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrCreate(param).Create(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> AggrApprove(HisExpMestSDO data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrApprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> AggrExport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrExport(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> AggrUnapprove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrUnapprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> AggrUnexport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrUnexport(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> AggrRemove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrRemove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> AggrDelete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestAggrTruncate(param).Truncate(id);
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
    }
}
