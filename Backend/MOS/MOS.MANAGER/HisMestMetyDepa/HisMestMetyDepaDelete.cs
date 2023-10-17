using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestMetyDepa
{
    partial class HisMestMetyDepaDelete : BusinessBase
    {
        internal HisMestMetyDepaDelete()
            : base()
        {

        }

        internal HisMestMetyDepaDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEST_METY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_METY_DEPA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMestMetyDepaDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEST_METY_DEPA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                List<HIS_MEST_METY_DEPA> listRaw = new List<HIS_MEST_METY_DEPA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMestMetyDepaDAO.DeleteList(listData);
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
