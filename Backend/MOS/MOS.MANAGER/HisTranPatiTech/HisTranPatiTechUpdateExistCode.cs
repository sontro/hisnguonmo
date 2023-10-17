using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTranPatiTech
{
    partial class HisTranPatiTechUpdate : BusinessBase
    {
		private List<HIS_TRAN_PATI_TECH> beforeUpdateHisTranPatiTechs = new List<HIS_TRAN_PATI_TECH>();
		
        internal HisTranPatiTechUpdate()
            : base()
        {

        }

        internal HisTranPatiTechUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRAN_PATI_TECH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTechCheck checker = new HisTranPatiTechCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRAN_PATI_TECH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_TECH_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTechDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTech_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTranPatiTech that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTranPatiTechs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TRAN_PATI_TECH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiTechCheck checker = new HisTranPatiTechCheck(param);
                List<HIS_TRAN_PATI_TECH> listRaw = new List<HIS_TRAN_PATI_TECH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_TECH_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTechDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTech_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTranPatiTech that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTranPatiTechs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTranPatiTechs))
            {
                if (!DAOWorker.HisTranPatiTechDAO.UpdateList(this.beforeUpdateHisTranPatiTechs))
                {
                    LogSystem.Warn("Rollback du lieu HisTranPatiTech that bai, can kiem tra lai." + LogUtil.TraceData("HisTranPatiTechs", this.beforeUpdateHisTranPatiTechs));
                }
				this.beforeUpdateHisTranPatiTechs = null;
            }
        }
    }
}
