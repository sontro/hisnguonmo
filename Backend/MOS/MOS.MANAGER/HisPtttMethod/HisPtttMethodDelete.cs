using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttMethod
{
    partial class HisPtttMethodDelete : BusinessBase
    {
        internal HisPtttMethodDelete()
            : base()
        {

        }

        internal HisPtttMethodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisPtttMethodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                List<HIS_PTTT_METHOD> listRaw = new List<HIS_PTTT_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPtttMethodDAO.DeleteList(listData);
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
