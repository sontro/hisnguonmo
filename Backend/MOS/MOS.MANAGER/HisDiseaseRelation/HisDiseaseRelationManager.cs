using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDiseaseRelation
{
    public class HisDiseaseRelationManager : BusinessBase
    {
        public HisDiseaseRelationManager()
            : base()
        {

        }
        
        public HisDiseaseRelationManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DISEASE_RELATION>> Get(HisDiseaseRelationFilterQuery filter)
        {
            ApiResultObject<List<HIS_DISEASE_RELATION>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DISEASE_RELATION> resultData = null;
                if (valid)
                {
                    resultData = new HisDiseaseRelationGet(param).Get(filter);
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
        public ApiResultObject<HIS_DISEASE_RELATION> GetById(long id)
        {
            ApiResultObject<HIS_DISEASE_RELATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_DISEASE_RELATION resultData = null;
                if (valid)
                {
                    HisDiseaseRelationFilterQuery filter = new HisDiseaseRelationFilterQuery();
                    resultData = new HisDiseaseRelationGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
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
        public ApiResultObject<HIS_DISEASE_RELATION> Create(HIS_DISEASE_RELATION data)
        {
            ApiResultObject<HIS_DISEASE_RELATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DISEASE_RELATION resultData = null;
                if (valid && new HisDiseaseRelationCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_DISEASE_RELATION> Update(HIS_DISEASE_RELATION data)
        {
            ApiResultObject<HIS_DISEASE_RELATION> result = new ApiResultObject<HIS_DISEASE_RELATION>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DISEASE_RELATION resultData = null;
                if (valid && new HisDiseaseRelationUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_DISEASE_RELATION> ChangeLock(HIS_DISEASE_RELATION data)
        {
            ApiResultObject<HIS_DISEASE_RELATION> result = new ApiResultObject<HIS_DISEASE_RELATION>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DISEASE_RELATION resultData = null;
                if (valid && new HisDiseaseRelationLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_DISEASE_RELATION data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisDiseaseRelationTruncate(param).Truncate(data);
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
