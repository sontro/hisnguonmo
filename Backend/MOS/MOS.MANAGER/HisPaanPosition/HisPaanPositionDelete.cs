using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPaanPosition
{
    partial class HisPaanPositionDelete : BusinessBase
    {
        internal HisPaanPositionDelete()
            : base()
        {

        }

        internal HisPaanPositionDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PAAN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PAAN_POSITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPaanPositionDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PAAN_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                List<HIS_PAAN_POSITION> listRaw = new List<HIS_PAAN_POSITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPaanPositionDAO.DeleteList(listData);
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
