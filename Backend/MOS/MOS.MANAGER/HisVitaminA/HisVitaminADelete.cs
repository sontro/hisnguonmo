using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVitaminA
{
    partial class HisVitaminADelete : BusinessBase
    {
        internal HisVitaminADelete()
            : base()
        {

        }

        internal HisVitaminADelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_VITAMIN_A data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVitaminACheck checker = new HisVitaminACheck(param);
                valid = valid && IsNotNull(data);
                HIS_VITAMIN_A raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisVitaminADAO.Delete(data);
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

        internal bool DeleteList(List<HIS_VITAMIN_A> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVitaminACheck checker = new HisVitaminACheck(param);
                List<HIS_VITAMIN_A> listRaw = new List<HIS_VITAMIN_A>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisVitaminADAO.DeleteList(listData);
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
