using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMatyDepa
{
    partial class HisMestMatyDepaDelete : BusinessBase
    {
        internal HisMestMatyDepaDelete()
            : base()
        {

        }

        internal HisMestMatyDepaDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_MATY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_MATY_DEPA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestMatyDepaDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_MATY_DEPA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                List<HIS_MEST_MATY_DEPA> listRaw = new List<HIS_MEST_MATY_DEPA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestMatyDepaDAO.DeleteList(listData);
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
