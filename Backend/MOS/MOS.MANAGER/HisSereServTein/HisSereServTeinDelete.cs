using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTein
{
    class HisSereServTeinDelete : BusinessBase
    {
        private List<HIS_SERE_SERV_TEIN> recentDeleteds = new List<HIS_SERE_SERV_TEIN>();

        internal HisSereServTeinDelete()
            : base()
        {

        }

        internal HisSereServTeinDelete(Inventec.Core.CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_TEIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    this.recentDeleteds.Add(data);
                    result = DAOWorker.HisSereServTeinDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERE_SERV_TEIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                List<HIS_SERE_SERV_TEIN> listRaw = new List<HIS_SERE_SERV_TEIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    this.recentDeleteds.AddRange(listRaw);
                    result = DAOWorker.HisSereServTeinDAO.DeleteList(listRaw);
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

        internal bool DeleteBySereServIds(List<long> ids)
        {
            bool result = true;
            List<HIS_SERE_SERV_TEIN> listData = new HisSereServTeinGet().GetBySereServIds(ids);
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
                if (!DAOWorker.HisSereServTeinDAO.UpdateList(this.recentDeleteds))
                {
                    LogSystem.Error("Rollback sau khi delete HIS_SERE_SERV_TEIN that bai");
                }
            }
        }
    }
}
