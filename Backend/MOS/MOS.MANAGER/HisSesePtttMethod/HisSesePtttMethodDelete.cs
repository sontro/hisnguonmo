using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodDelete : BusinessBase
    {
        internal HisSesePtttMethodDelete()
            : base()
        {

        }

        internal HisSesePtttMethodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SESE_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_PTTT_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSesePtttMethodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SESE_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                List<HIS_SESE_PTTT_METHOD> listRaw = new List<HIS_SESE_PTTT_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSesePtttMethodDAO.DeleteList(listData);
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
