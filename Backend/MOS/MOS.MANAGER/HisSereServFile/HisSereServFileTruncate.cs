using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServFile
{
    class HisSereServFileTruncate : BusinessBase
    {
        internal HisSereServFileTruncate()
            : base()
        {

        }

        internal HisSereServFileTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERE_SERV_FILE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServFileCheck checker = new HisSereServFileCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_FILE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServFileDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_SERE_SERV_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServFileCheck checker = new HisSereServFileCheck(param);
                List<HIS_SERE_SERV_FILE> listRaw = new List<HIS_SERE_SERV_FILE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSereServFileDAO.TruncateList(listData);
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
            List<HIS_SERE_SERV_FILE> listData = new HisSereServFileGet().GetBySereServIds(sereServIds);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.TruncateList(listData);
            }
            return result;
        }
    }
}
