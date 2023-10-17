using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServPttt
{
    partial class HisSereServPtttTruncate : BusinessBase
    {
        internal HisSereServPtttTruncate()
            : base()
        {

        }

        internal HisSereServPtttTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERE_SERV_PTTT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServPtttDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERE_SERV_PTTT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
                List<HIS_SERE_SERV_PTTT> listRaw = new List<HIS_SERE_SERV_PTTT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSereServPtttDAO.TruncateList(listData);
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

        internal bool TruncateBySereServIds(List<long> sereServIds)
        {
            bool result = false;
            List<HIS_SERE_SERV_PTTT> listData = new HisSereServPtttGet().GetBySereServIds(sereServIds);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.TruncateList(listData);
            }
            return result;
        }
    }
}
