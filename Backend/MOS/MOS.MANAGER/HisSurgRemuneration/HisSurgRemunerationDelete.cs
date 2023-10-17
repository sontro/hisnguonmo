using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationDelete : BusinessBase
    {
        internal HisSurgRemunerationDelete()
            : base()
        {

        }

        internal HisSurgRemunerationDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SURG_REMUNERATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SURG_REMUNERATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSurgRemunerationDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SURG_REMUNERATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                List<HIS_SURG_REMUNERATION> listRaw = new List<HIS_SURG_REMUNERATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSurgRemunerationDAO.DeleteList(listData);
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
