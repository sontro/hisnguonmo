using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempUpdate : BusinessBase
    {
		private List<HIS_TRAN_PATI_TEMP> beforeUpdateHisTranPatiTemps = new List<HIS_TRAN_PATI_TEMP>();
		
        internal HisTranPatiTempUpdate()
            : base()
        {

        }

        internal HisTranPatiTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRAN_PATI_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTempCheck checker = new HisTranPatiTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRAN_PATI_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_TEMP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTranPatiTemp that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTranPatiTemps.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TRAN_PATI_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiTempCheck checker = new HisTranPatiTempCheck(param);
                List<HIS_TRAN_PATI_TEMP> listRaw = new List<HIS_TRAN_PATI_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRAN_PATI_TEMP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTranPatiTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTranPatiTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTranPatiTemps))
            {
                if (!DAOWorker.HisTranPatiTempDAO.UpdateList(this.beforeUpdateHisTranPatiTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisTranPatiTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisTranPatiTemps", this.beforeUpdateHisTranPatiTemps));
                }
				this.beforeUpdateHisTranPatiTemps = null;
            }
        }
    }
}
