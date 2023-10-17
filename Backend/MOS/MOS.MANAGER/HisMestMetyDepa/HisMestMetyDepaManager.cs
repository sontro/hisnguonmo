using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMestMetyDepa
{
    public partial class HisMestMetyDepaManager : BusinessBase
    {
        public HisMestMetyDepaManager()
            : base()
        {

        }

        public HisMestMetyDepaManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_METY_DEPA>> Get(HisMestMetyDepaFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_METY_DEPA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_METY_DEPA> resultData = null;
                if (valid)
                {
                    resultData = new HisMestMetyDepaGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_METY_DEPA> Create(HIS_MEST_METY_DEPA data)
        {
            ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_DEPA resultData = null;
                if (valid && new HisMestMetyDepaCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEST_METY_DEPA> Update(HIS_MEST_METY_DEPA data)
        {
            ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_DEPA resultData = null;
                if (valid && new HisMestMetyDepaUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_METY_DEPA> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_METY_DEPA> result = new ApiResultObject<HIS_MEST_METY_DEPA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_DEPA resultData = null;
                if (valid)
                {
                    new HisMestMetyDepaLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_METY_DEPA> Lock(long id)
        {
            ApiResultObject<HIS_MEST_METY_DEPA> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_DEPA resultData = null;
                if (valid)
                {
                    new HisMestMetyDepaLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_METY_DEPA> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_METY_DEPA> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_METY_DEPA resultData = null;
                if (valid)
                {
                    new HisMestMetyDepaLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestMetyDepaTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> CreateByMedicine(HisMestMetyDepaByMedicineSDO data)
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
                    resultData = new HisMestMetyDepaCreateByMedicine(param).Run(data);
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
