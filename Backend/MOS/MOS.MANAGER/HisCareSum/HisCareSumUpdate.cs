using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareSum
{
    partial class HisCareSumUpdate : BusinessBase
    {
        private List<HIS_CARE_SUM> beforeUpdateHisCareSums = new List<HIS_CARE_SUM>();

        internal HisCareSumUpdate()
            : base()
        {

        }

        internal HisCareSumUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARE_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareSumCheck checker = new HisCareSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARE_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisCareSums.Add(raw);
                    if (!DAOWorker.HisCareSumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareSum that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_CARE_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareSumCheck checker = new HisCareSumCheck(param);
                List<HIS_CARE_SUM> listRaw = new List<HIS_CARE_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisCareSums.AddRange(listRaw);
                    if (!DAOWorker.HisCareSumDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareSum that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCareSums))
            {
                if (!DAOWorker.HisCareSumDAO.UpdateList(this.beforeUpdateHisCareSums))
                {
                    LogSystem.Warn("Rollback du lieu HisCareSum that bai, can kiem tra lai." + LogUtil.TraceData("HisCareSums", this.beforeUpdateHisCareSums));
                }
            }
        }
    }
}