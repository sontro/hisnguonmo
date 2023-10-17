using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialPaty
{
    public partial class HisMaterialPatyManager : BusinessBase
    {
        public HisMaterialPatyManager()
            : base()
        {

        }
        
        public HisMaterialPatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MATERIAL_PATY>> Get(HisMaterialPatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialPatyGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_MATERIAL_PATY>> GetOfLast(long materialTypeId)
        {
            ApiResultObject<List<HIS_MATERIAL_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_MATERIAL_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialPatyGet(param).GetOfLast(materialTypeId);
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
        public ApiResultObject<List<V_HIS_MATERIAL_PATY>> GetView(HisMaterialPatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialPatyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_MATERIAL_PATY> Create(HIS_MATERIAL_PATY data)
        {
            ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_PATY resultData = null;
                if (valid && new HisMaterialPatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MATERIAL_PATY>> CreateList(List<HIS_MATERIAL_PATY> data)
        {
            ApiResultObject<List<HIS_MATERIAL_PATY>> result = new ApiResultObject<List<HIS_MATERIAL_PATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MATERIAL_PATY> resultData = null;
                if (valid && new HisMaterialPatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_MATERIAL_PATY> Update(HIS_MATERIAL_PATY data)
        {
            ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_PATY resultData = null;
                if (valid && new HisMaterialPatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MATERIAL_PATY> ChangeLock(HIS_MATERIAL_PATY data)
        {
            ApiResultObject<HIS_MATERIAL_PATY> result = new ApiResultObject<HIS_MATERIAL_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_PATY resultData = null;
                if (valid && new HisMaterialPatyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MATERIAL_PATY data)
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
                    resultData = new HisMaterialPatyTruncate(param).Truncate(data);
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
