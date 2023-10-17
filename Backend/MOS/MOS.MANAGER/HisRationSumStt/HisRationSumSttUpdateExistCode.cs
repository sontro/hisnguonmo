using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSumStt
{
    partial class HisRationSumSttUpdate : BusinessBase
    {
        private List<HIS_RATION_SUM_STT> beforeUpdateHisRationSumStts = new List<HIS_RATION_SUM_STT>();

        internal HisRationSumSttUpdate()
            : base()
        {

        }

        internal HisRationSumSttUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_RATION_SUM_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumSttCheck checker = new HisRationSumSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_RATION_SUM_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.RATION_SUM_STT_CODE, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisRationSumSttDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSumStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSumStt that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisRationSumStts.Add(raw);

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

        internal bool UpdateList(List<HIS_RATION_SUM_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationSumSttCheck checker = new HisRationSumSttCheck(param);
                List<HIS_RATION_SUM_STT> listRaw = new List<HIS_RATION_SUM_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.RATION_SUM_STT_CODE, data.ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRationSumSttDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSumStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSumStt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisRationSumStts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationSumStts))
            {
                if (!DAOWorker.HisRationSumSttDAO.UpdateList(this.beforeUpdateHisRationSumStts))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSumStt that bai, can kiem tra lai." + LogUtil.TraceData("HisRationSumStts", this.beforeUpdateHisRationSumStts));
                }
                this.beforeUpdateHisRationSumStts = null;
            }
        }
    }
}
