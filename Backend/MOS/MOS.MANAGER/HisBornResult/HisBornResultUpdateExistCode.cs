using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBornResult
{
    partial class HisBornResultUpdate : BusinessBase
    {
		private List<HIS_BORN_RESULT> beforeUpdateHisBornResults = new List<HIS_BORN_RESULT>();
		
        internal HisBornResultUpdate()
            : base()
        {

        }

        internal HisBornResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BORN_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornResultCheck checker = new HisBornResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BORN_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BORN_RESULT_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBornResults.Add(raw);
					if (!DAOWorker.HisBornResultDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornResult that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BORN_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBornResultCheck checker = new HisBornResultCheck(param);
                List<HIS_BORN_RESULT> listRaw = new List<HIS_BORN_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BORN_RESULT_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBornResults.AddRange(listRaw);
					if (!DAOWorker.HisBornResultDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBornResult that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBornResults))
            {
                if (!new HisBornResultUpdate(param).UpdateList(this.beforeUpdateHisBornResults))
                {
                    LogSystem.Warn("Rollback du lieu HisBornResult that bai, can kiem tra lai." + LogUtil.TraceData("HisBornResults", this.beforeUpdateHisBornResults));
                }
            }
        }
    }
}
