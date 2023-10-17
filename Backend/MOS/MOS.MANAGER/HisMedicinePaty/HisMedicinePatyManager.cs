using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicinePaty
{
    public partial class HisMedicinePatyManager : BusinessBase
    {
        public HisMedicinePatyManager()
            : base()
        {

        }
        
        public HisMedicinePatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_PATY>> Get(HisMedicinePatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicinePatyGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_MEDICINE_PATY>> GetOfLast(long medicineTypeId)
        {
            ApiResultObject<List<HIS_MEDICINE_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_MEDICINE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicinePatyGet(param).GetOfLast(medicineTypeId);
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
        public ApiResultObject<List<V_HIS_MEDICINE_PATY>> GetView(HisMedicinePatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_PATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_PATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicinePatyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_MEDICINE_PATY> Create(HIS_MEDICINE_PATY data)
        {
            ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_PATY resultData = null;
                if (valid && new HisMedicinePatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MEDICINE_PATY>> CreateList(List<HIS_MEDICINE_PATY> data)
        {
            ApiResultObject<List<HIS_MEDICINE_PATY>> result = new ApiResultObject<List<HIS_MEDICINE_PATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDICINE_PATY> resultData = null;
                if (valid && new HisMedicinePatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_MEDICINE_PATY> Update(HIS_MEDICINE_PATY data)
        {
            ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_PATY resultData = null;
                if (valid && new HisMedicinePatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDICINE_PATY> ChangeLock(HIS_MEDICINE_PATY data)
        {
            ApiResultObject<HIS_MEDICINE_PATY> result = new ApiResultObject<HIS_MEDICINE_PATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_PATY resultData = null;
                if (valid && new HisMedicinePatyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDICINE_PATY data)
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
                    resultData = new HisMedicinePatyTruncate(param).Truncate(data);
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
