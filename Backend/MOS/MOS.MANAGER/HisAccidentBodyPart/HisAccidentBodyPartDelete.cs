using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentBodyPart
{
    partial class HisAccidentBodyPartDelete : BusinessBase
    {
        internal HisAccidentBodyPartDelete()
            : base()
        {

        }

        internal HisAccidentBodyPartDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_BODY_PART data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_BODY_PART raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentBodyPartDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_BODY_PART> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentBodyPartCheck checker = new HisAccidentBodyPartCheck(param);
                List<HIS_ACCIDENT_BODY_PART> listRaw = new List<HIS_ACCIDENT_BODY_PART>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentBodyPartDAO.DeleteList(listData);
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
