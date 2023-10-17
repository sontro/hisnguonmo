using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskOccupational
{
    partial class HisKskOccupationalDelete : BusinessBase
    {
        internal HisKskOccupationalDelete()
            : base()
        {

        }

        internal HisKskOccupationalDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_KSK_OCCUPATIONAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OCCUPATIONAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisKskOccupationalDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                List<HIS_KSK_OCCUPATIONAL> listRaw = new List<HIS_KSK_OCCUPATIONAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisKskOccupationalDAO.DeleteList(listData);
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
