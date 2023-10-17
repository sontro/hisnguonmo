using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceHein
{
    partial class HisServiceHeinUpdate : BusinessBase
    {
		private List<HIS_SERVICE_HEIN> beforeUpdateHisServiceHeins = new List<HIS_SERVICE_HEIN>();
		
        internal HisServiceHeinUpdate()
            : base()
        {

        }

        internal HisServiceHeinUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_HEIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceHeinCheck checker = new HisServiceHeinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_HEIN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceHeinDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceHein_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceHein that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceHeins.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_HEIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceHeinCheck checker = new HisServiceHeinCheck(param);
                List<HIS_SERVICE_HEIN> listRaw = new List<HIS_SERVICE_HEIN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceHeinDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceHein_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceHein that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceHeins.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceHeins))
            {
                if (!DAOWorker.HisServiceHeinDAO.UpdateList(this.beforeUpdateHisServiceHeins))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceHein that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceHeins", this.beforeUpdateHisServiceHeins));
                }
				this.beforeUpdateHisServiceHeins = null;
            }
        }
    }
}
