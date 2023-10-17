using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRemuneration
{
    partial class HisRemunerationUpdate : BusinessBase
    {
		private List<HIS_REMUNERATION> beforeUpdateHisRemunerations = new List<HIS_REMUNERATION>();
		
        internal HisRemunerationUpdate()
            : base()
        {

        }

        internal HisRemunerationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REMUNERATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRemunerationCheck checker = new HisRemunerationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REMUNERATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REMUNERATION_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisRemunerations.Add(raw);
					if (!DAOWorker.HisRemunerationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRemuneration_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRemuneration that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REMUNERATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRemunerationCheck checker = new HisRemunerationCheck(param);
                List<HIS_REMUNERATION> listRaw = new List<HIS_REMUNERATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REMUNERATION_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisRemunerations.AddRange(listRaw);
					if (!DAOWorker.HisRemunerationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRemuneration_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRemuneration that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRemunerations))
            {
                if (!new HisRemunerationUpdate(param).UpdateList(this.beforeUpdateHisRemunerations))
                {
                    LogSystem.Warn("Rollback du lieu HisRemuneration that bai, can kiem tra lai." + LogUtil.TraceData("HisRemunerations", this.beforeUpdateHisRemunerations));
                }
            }
        }
    }
}
