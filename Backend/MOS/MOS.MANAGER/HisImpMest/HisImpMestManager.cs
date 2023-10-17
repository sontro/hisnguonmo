using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Aggr;
using MOS.MANAGER.HisImpMest.Aggr.Unimport;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.MANAGER.HisImpMest.Common.Delete;
using MOS.MANAGER.HisImpMest.Common.Get;
using MOS.MANAGER.HisImpMest.Common.Update;
using MOS.MANAGER.HisImpMest.Import;
using MOS.MANAGER.HisImpMest.Manu;
using MOS.MANAGER.HisImpMest.Moba;
using MOS.MANAGER.HisImpMest.Other;
using MOS.MANAGER.HisImpMest.Reusable;
using MOS.MANAGER.HisImpMest.UnImport;
using MOS.MANAGER.HisImpMest.UpdateStatus;
using MOS.MANAGER.HisImpMest.UpdateDetail;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest
{
    public partial class HisImpMestManager : BusinessBase
    {
        public HisImpMestManager()
            : base()
        {

        }

        public HisImpMestManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST>> Get(HisImpMestFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_IMP_MEST>> GetView(HisImpMestViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_IMP_MEST data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    result = this.PackSingleResult(new HisImpMestTruncate(param).Truncate(data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_IMP_MEST> UpdateStatus(HIS_IMP_MEST data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestUpdateStatus(param).UpdateStatus(data, false, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST> Import(HIS_IMP_MEST data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestImport(param).Import(data, false, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST> CancelImport(HIS_IMP_MEST data)
        {
            ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST resultData = null;
                if (valid)
                {
                    new HisImpMestCancelImport(param).Cancel(data, ref resultData);
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
        public ApiResultObject<List<V_HIS_IMP_MEST>> GetViewByDetail(HisImpMestViewDetailFilter filter)
        {
            ApiResultObject<List<V_HIS_IMP_MEST>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestGetViewByDetail(param).GetViewByDetail(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisImpMestResultSDO> UpdateDetail(HisImpMestUpdateDetailSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestUpdateDetail(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_IMP_MEST>> UpdateNationalCode(List<HIS_IMP_MEST> listData)
        {
            ApiResultObject<List<HIS_IMP_MEST>> result = new ApiResultObject<List<HIS_IMP_MEST>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    new HisImpMestUpdateNationalCode(param).Run(listData, ref resultData);
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
        public ApiResultObject<List<HIS_IMP_MEST>> CancelNationalCode(List<long> listData)
        {
            ApiResultObject<List<HIS_IMP_MEST>> result = new ApiResultObject<List<HIS_IMP_MEST>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_IMP_MEST> resultData = null;
                if (valid)
                {
                    new HisImpMestCancelNationalCode(param).Run(listData, ref resultData);
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
        public ApiResultObject<HisImpMestResultSDO> ReusableCreate(HisImpMestReuseSDO data)
        {
            ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisImpMestResultSDO resultData = null;
                if (valid)
                {
                    new HisImpMestReusableCreate(param).Run(data, ref resultData);
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
