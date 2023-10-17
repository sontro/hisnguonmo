using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTranPatiTech
{
    partial class HisTranPatiTechDelete : BusinessBase
    {
        internal HisTranPatiTechDelete()
            : base()
        {

        }

        internal HisTranPatiTechDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRAN_PATI_TECH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTechCheck checker = new HisTranPatiTechCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_TECH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiTechDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRAN_PATI_TECH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiTechCheck checker = new HisTranPatiTechCheck(param);
                List<HIS_TRAN_PATI_TECH> listRaw = new List<HIS_TRAN_PATI_TECH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiTechDAO.DeleteList(listData);
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
