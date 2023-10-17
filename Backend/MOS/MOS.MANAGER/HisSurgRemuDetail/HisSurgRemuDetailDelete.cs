using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailDelete : BusinessBase
    {
        internal HisSurgRemuDetailDelete()
            : base()
        {

        }

        internal HisSurgRemuDetailDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SURG_REMU_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMU_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSurgRemuDetailDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                List<HIS_SURG_REMU_DETAIL> listRaw = new List<HIS_SURG_REMU_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSurgRemuDetailDAO.DeleteList(listData);
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
