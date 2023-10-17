using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaSum
{
    partial class HisRehaSumUpdate : BusinessBase
    {
        private List<HIS_REHA_SUM> beforeUpdateHisRehaSums = new List<HIS_REHA_SUM>();

        internal HisRehaSumUpdate()
            : base()
        {

        }

        internal HisRehaSumUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REHA_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaSumCheck checker = new HisRehaSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REHA_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisRehaSums.Add(raw);
                    if (!DAOWorker.HisRehaSumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaSum that bai." + LogUtil.TraceData("data", data));
                    }

                    result = true;
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

        internal bool UpdateList(List<HIS_REHA_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaSumCheck checker = new HisRehaSumCheck(param);
                List<HIS_REHA_SUM> listRaw = new List<HIS_REHA_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisRehaSums.AddRange(listRaw);
                    if (!DAOWorker.HisRehaSumDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaSum that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisRehaSums))
            {
                if (!new HisRehaSumUpdate(param).UpdateList(this.beforeUpdateHisRehaSums))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaSum that bai, can kiem tra lai." + LogUtil.TraceData("HisRehaSums", this.beforeUpdateHisRehaSums));
                }
            }
        }
    }
}