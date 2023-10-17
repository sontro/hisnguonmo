using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationUpdate : BusinessBase
    {
		private List<HIS_SURG_REMUNERATION> beforeUpdateHisSurgRemunerations = new List<HIS_SURG_REMUNERATION>();
		
        internal HisSurgRemunerationUpdate()
            : base()
        {

        }

        internal HisSurgRemunerationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SURG_REMUNERATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SURG_REMUNERATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SURG_REMUNERATION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSurgRemunerationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuneration_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSurgRemuneration that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSurgRemunerations.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SURG_REMUNERATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemunerationCheck checker = new HisSurgRemunerationCheck(param);
                List<HIS_SURG_REMUNERATION> listRaw = new List<HIS_SURG_REMUNERATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SURG_REMUNERATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSurgRemunerationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuneration_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSurgRemuneration that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSurgRemunerations.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSurgRemunerations))
            {
                if (!DAOWorker.HisSurgRemunerationDAO.UpdateList(this.beforeUpdateHisSurgRemunerations))
                {
                    LogSystem.Warn("Rollback du lieu HisSurgRemuneration that bai, can kiem tra lai." + LogUtil.TraceData("HisSurgRemunerations", this.beforeUpdateHisSurgRemunerations));
                }
				this.beforeUpdateHisSurgRemunerations = null;
            }
        }
    }
}
