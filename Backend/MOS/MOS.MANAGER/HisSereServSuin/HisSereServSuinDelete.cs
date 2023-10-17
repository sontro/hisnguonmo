using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServSuin
{
    partial class HisSereServSuinDelete : BusinessBase
    {
        private List<HIS_SERE_SERV_SUIN> recentDeleteds = new List<HIS_SERE_SERV_SUIN>();

        internal HisSereServSuinDelete()
            : base()
        {

        }

        internal HisSereServSuinDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_SUIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_SUIN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.recentDeleteds.Add(raw);
                    result = DAOWorker.HisSereServSuinDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERE_SERV_SUIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                List<HIS_SERE_SERV_SUIN> listRaw = new List<HIS_SERE_SERV_SUIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    this.recentDeleteds.AddRange(listRaw);
                    result = DAOWorker.HisSereServSuinDAO.DeleteList(listData);
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
            List<HIS_SERE_SERV_SUIN> listData = new HisSereServSuinGet().GetBySereServIds(sereServIds);
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
                if (!DAOWorker.HisSereServSuinDAO.UpdateList(this.recentDeleteds))
                {
                    LogSystem.Error("Rollback sau khi delete HIS_SERE_SERV_SUIN that bai");
                }
            }
        }
    }
}
