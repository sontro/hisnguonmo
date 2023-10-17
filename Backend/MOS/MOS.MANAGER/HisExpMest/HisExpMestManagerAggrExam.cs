using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.AggrExam.Remove;
using MOS.MANAGER.HisExpMest.AggrExam.Unapprove;
using MOS.MANAGER.HisExpMest.AggrExam.Unexport;
using MOS.MANAGER.HisExpMest.AggrExam.Create;
using MOS.MANAGER.HisExpMest.AggrExam.Approve;
using MOS.MANAGER.HisExpMest.AggrExam.Export;
using MOS.MANAGER.HisExpMest.AggrExam.Delete;
using MOS.MANAGER.HisExpMest.AggrExam.ByTreatAndStock;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> AggrExamCreate(HisExpMestAggrSDO data)
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
                    isSuccess = new HisExpMestAggrExamCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_EXP_MEST>> AggrExamApprove(HisExpMestSDO data)
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
                    isSuccess = new HisExpMestAggrExamApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> AggrExamExport(HisExpMestSDO data)
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
                    isSuccess = new HisExpMestAggrExamExport(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> AggrExamUnapprove(HisExpMestSDO data)
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
                    isSuccess = new HisExpMestAggrExamUnapprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> AggrExamUnexport(HisExpMestSDO data)
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
                    isSuccess = new HisExpMestAggrExamUnexport(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> AggrExamRemove(HisExpMestSDO data)
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
                    isSuccess = new HisExpMestAggrExamRemove(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> AggrExamDelete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestAggrExamTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_EXP_MEST> AggrExamByTreatAndStock(AggrExamByTreatAndStockSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestAggrExamByTreatAndStock(param).Run(data, ref resultData);
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
    }
}
