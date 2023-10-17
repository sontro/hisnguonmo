using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    partial class HisSereServPtttTempUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_PTTT_TEMP> beforeUpdateHisSereServPtttTemps = new List<HIS_SERE_SERV_PTTT_TEMP>();
		
        internal HisSereServPtttTempUpdate()
            : base()
        {

        }

        internal HisSereServPtttTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_PTTT_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_PTTT_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERE_SERV_PTTT_TEMP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSereServPtttTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPtttTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServPtttTemp that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSereServPtttTemps.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                List<HIS_SERE_SERV_PTTT_TEMP> listRaw = new List<HIS_SERE_SERV_PTTT_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERE_SERV_PTTT_TEMP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSereServPtttTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPtttTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServPtttTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSereServPtttTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServPtttTemps))
            {
                if (!DAOWorker.HisSereServPtttTempDAO.UpdateList(this.beforeUpdateHisSereServPtttTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServPtttTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServPtttTemps", this.beforeUpdateHisSereServPtttTemps));
                }
				this.beforeUpdateHisSereServPtttTemps = null;
            }
        }
    }
}
