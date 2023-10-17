using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediOrg
{
    public partial class HisMediOrgManager : BusinessBase
    {
        public HisMediOrgManager()
            : base()
        {

        }

        public HisMediOrgManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_ORG>> Get(HisMediOrgFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_ORG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_ORG> resultData = null;
                if (valid)
                {
                    resultData = new HisMediOrgGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDI_ORG> Create(HIS_MEDI_ORG data)
        {
            ApiResultObject<HIS_MEDI_ORG> result = new ApiResultObject<HIS_MEDI_ORG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_ORG resultData = null;
                if (valid && new HisMediOrgCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDI_ORG> Update(HIS_MEDI_ORG data)
        {
            ApiResultObject<HIS_MEDI_ORG> result = new ApiResultObject<HIS_MEDI_ORG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_ORG resultData = null;
                if (valid && new HisMediOrgUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_ORG> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDI_ORG> result = new ApiResultObject<HIS_MEDI_ORG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_ORG resultData = null;
                if (valid)
                {
                    new HisMediOrgLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_ORG> Lock(long id)
        {
            ApiResultObject<HIS_MEDI_ORG> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_ORG resultData = null;
                if (valid)
                {
                    new HisMediOrgLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDI_ORG> Unlock(long id)
        {
            ApiResultObject<HIS_MEDI_ORG> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_ORG resultData = null;
                if (valid)
                {
                    new HisMediOrgLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMediOrgTruncate(param).Truncate(id);
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
