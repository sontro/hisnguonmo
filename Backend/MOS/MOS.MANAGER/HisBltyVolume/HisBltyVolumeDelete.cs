using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBltyVolume
{
    partial class HisBltyVolumeDelete : BusinessBase
    {
        internal HisBltyVolumeDelete()
            : base()
        {

        }

        internal HisBltyVolumeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BLTY_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BLTY_VOLUME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBltyVolumeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BLTY_VOLUME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                List<HIS_BLTY_VOLUME> listRaw = new List<HIS_BLTY_VOLUME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBltyVolumeDAO.DeleteList(listData);
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
