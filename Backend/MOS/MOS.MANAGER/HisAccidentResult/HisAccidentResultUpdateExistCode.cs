using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentResult
{
    partial class HisAccidentResultUpdate : BusinessBase
    {
		private List<HIS_ACCIDENT_RESULT> beforeUpdateHisAccidentResults = new List<HIS_ACCIDENT_RESULT>();
		
        internal HisAccidentResultUpdate()
            : base()
        {

        }

        internal HisAccidentResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCIDENT_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentResultCheck checker = new HisAccidentResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ACCIDENT_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCIDENT_RESULT_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisAccidentResults.Add(raw);
					if (!DAOWorker.HisAccidentResultDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentResult that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentResultCheck checker = new HisAccidentResultCheck(param);
                List<HIS_ACCIDENT_RESULT> listRaw = new List<HIS_ACCIDENT_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCIDENT_RESULT_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisAccidentResults.AddRange(listRaw);
					if (!DAOWorker.HisAccidentResultDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAccidentResult that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentResults))
            {
                if (!new HisAccidentResultUpdate(param).UpdateList(this.beforeUpdateHisAccidentResults))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentResult that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentResults", this.beforeUpdateHisAccidentResults));
                }
            }
        }
    }
}
