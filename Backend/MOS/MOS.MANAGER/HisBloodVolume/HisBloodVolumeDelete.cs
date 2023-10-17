using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodVolume
{
    partial class HisBloodVolumeDelete : BusinessBase
    {
        internal HisBloodVolumeDelete()
            : base()
        {

        }

        internal HisBloodVolumeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BLOOD_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodVolumeCheck checker = new HisBloodVolumeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLOOD_VOLUME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBloodVolumeDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_BLOOD_VOLUME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodVolumeCheck checker = new HisBloodVolumeCheck(param);
                List<HIS_BLOOD_VOLUME> listRaw = new List<HIS_BLOOD_VOLUME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBloodVolumeDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
