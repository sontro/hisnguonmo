using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Sync;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {

        [Logger]
        public ApiResultObject<HisTreatmentCheckDataStoreSDO> CheckDataStore(List<long> ids)
        {
            ApiResultObject<HisTreatmentCheckDataStoreSDO> result = new ApiResultObject<HisTreatmentCheckDataStoreSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisTreatmentCheckDataStoreSDO resultData = null;
                if (valid)
                {
                    new HisTreatmentCheckDataStore(param).Run(ids, ref resultData);
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
        public ApiResultObject<HisTreatmentOrderSDO> CheckExistsTreatmentOrder(HisTreatmentOrderSDO data)
        {
            ApiResultObject<HisTreatmentOrderSDO> result = new ApiResultObject<HisTreatmentOrderSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool success = false;
                if (valid)
                {
                    success = new HisTreatmentCheck(param).IsNotExistsTreatmentOrder(data);
                }
                result = this.PackResult(data, success);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UploadEmr(long data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool success = false;
                if (valid)
                {
                    success = new HisTreatmentUploadEmr(param).Run(data, false);
                }
                result = this.PackSingleResult(success);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> SyncDeath(List<DeathSyncSDO> data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool success = false;
                if (valid)
                {
                    success = new HisTreatmentSyncDeath(param).Run(data);
                }
                result = this.PackSingleResult(success);
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
