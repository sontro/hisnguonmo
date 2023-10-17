using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyUpdate : BusinessBase
    {
		private List<HIS_ANTIGEN_METY> beforeUpdateHisAntigenMetys = new List<HIS_ANTIGEN_METY>();
		
        internal HisAntigenMetyUpdate()
            : base()
        {

        }

        internal HisAntigenMetyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIGEN_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntigenMetyCheck checker = new HisAntigenMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIGEN_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ANTIGEN_METY_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAntigenMetyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigenMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntigenMety that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAntigenMetys.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ANTIGEN_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntigenMetyCheck checker = new HisAntigenMetyCheck(param);
                List<HIS_ANTIGEN_METY> listRaw = new List<HIS_ANTIGEN_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ANTIGEN_METY_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntigenMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigenMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntigenMety that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAntigenMetys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntigenMetys))
            {
                if (!DAOWorker.HisAntigenMetyDAO.UpdateList(this.beforeUpdateHisAntigenMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisAntigenMety that bai, can kiem tra lai." + LogUtil.TraceData("HisAntigenMetys", this.beforeUpdateHisAntigenMetys));
                }
				this.beforeUpdateHisAntigenMetys = null;
            }
        }
    }
}
