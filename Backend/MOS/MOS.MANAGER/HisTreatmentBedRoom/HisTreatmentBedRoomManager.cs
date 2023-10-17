using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatmentBedRoom.SetObservedTime;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    public partial class HisTreatmentBedRoomManager : BusinessBase
    {
        public HisTreatmentBedRoomManager()
            : base()
        {

        }
        
        public HisTreatmentBedRoomManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_BED_ROOM>> Get(HisTreatmentBedRoomFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_BED_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBedRoomGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> GetView(HisTreatmentBedRoomViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBedRoomGet(param).GetView(filter);
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
        public ApiResultObject<List<L_HIS_TREATMENT_BED_ROOM>> GetLView(HisTreatmentBedRoomLViewFilterQuery filter)
        {
            ApiResultObject<List<L_HIS_TREATMENT_BED_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_TREATMENT_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBedRoomGet(param).GetLView(filter);
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
        public ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM_1>> GetView1(HisTreatmentBedRoomView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_BED_ROOM_1> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBedRoomGet(param).GetView1(filter);
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
        public ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> GetViewCurrentIn(long bedRoomId)
        {
            ApiResultObject<List<V_HIS_TREATMENT_BED_ROOM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<V_HIS_TREATMENT_BED_ROOM> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentBedRoomGet(param).GetViewCurrentInByBedRoomId(bedRoomId);
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
        public ApiResultObject<HIS_TREATMENT_BED_ROOM> Create(HIS_TREATMENT_BED_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BED_ROOM resultData = null;
                if (valid && new HisTreatmentBedRoomCreate(param).Create(data))
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
        public ApiResultObject<HisTreatmentBedRoomSDO> CreateSdo(HisTreatmentBedRoomSDO data)
        {
            ApiResultObject<HisTreatmentBedRoomSDO> result = new ApiResultObject<HisTreatmentBedRoomSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisTreatmentBedRoomSDO resultData = null;
                if (valid)
                {
                    new HisTreatmentBedRoomCreate(param).CreateSdo(data, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_BED_ROOM> Remove(HIS_TREATMENT_BED_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BED_ROOM resultData = null;
                if (valid)
                {
                    new HisTreatmentBedRoomRemove(param).Remove(data, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_BED_ROOM> ChangeLock(HIS_TREATMENT_BED_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BED_ROOM resultData = null;
                if (valid && new HisTreatmentBedRoomLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_TREATMENT_BED_ROOM data)
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
                    resultData = new HisTreatmentBedRoomTruncate(param).Truncate(data);
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
        public ApiResultObject<HIS_TREATMENT_BED_ROOM> UpdateTime(HIS_TREATMENT_BED_ROOM data)
        {
            ApiResultObject<HIS_TREATMENT_BED_ROOM> result = new ApiResultObject<HIS_TREATMENT_BED_ROOM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BED_ROOM resultData = null;
                if (valid)
                {
                    new HisTreatmentBedRoomUpdateTime(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> SetObservedTime(ObservedTimeSDO data)
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
                    resultData = new HisTreatmentBedRoomSetObservedTime(param).Run(data);
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
