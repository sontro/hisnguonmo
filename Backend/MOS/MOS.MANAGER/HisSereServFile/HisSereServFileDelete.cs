using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServFile
{
    class HisSereServFileDelete : BusinessBase
    {
        private List<HIS_SERE_SERV_FILE> recentDeleteds = new List<HIS_SERE_SERV_FILE>();

        internal HisSereServFileDelete()
            : base()
        {

        }

        internal HisSereServFileDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_FILE data)
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
                    this.recentDeleteds.Add(raw);
                    result = DAOWorker.HisSereServFileDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERE_SERV_FILE> listData)
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
                    this.recentDeleteds.AddRange(listRaw);
                    result = DAOWorker.HisSereServFileDAO.DeleteList(listData);
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

        internal bool DeleteBySereServIds(List<long> sereServIds)
        {
            bool result = true;
            List<HIS_SERE_SERV_FILE> listData = new HisSereServFileGet().GetBySereServIds(sereServIds);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.DeleteList(listData);
            }
            return result;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentDeleteds))
            {
                this.recentDeleteds.ForEach(o => o.IS_DELETE = 0);
                if (!DAOWorker.HisSereServFileDAO.UpdateList(this.recentDeleteds))
                {
                    LogSystem.Error("Rollback sau khi delete HIS_SERE_SERV_FILE that bai");
                }
            }
        }
    }
}
