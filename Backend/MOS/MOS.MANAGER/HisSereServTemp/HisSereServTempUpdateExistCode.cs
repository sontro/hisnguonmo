using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServTemp
{
    partial class HisSereServTempUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_TEMP> beforeUpdateHisSereServTemps = new List<HIS_SERE_SERV_TEMP>();
		
        internal HisSereServTempUpdate()
            : base()
        {

        }

        internal HisSereServTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServTempCheck checker = new HisSereServTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERE_SERV_TEMP_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisSereServTemps.Add(raw);
					if (!DAOWorker.HisSereServTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServTemp that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERE_SERV_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTempCheck checker = new HisSereServTempCheck(param);
                List<HIS_SERE_SERV_TEMP> listRaw = new List<HIS_SERE_SERV_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERE_SERV_TEMP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisSereServTemps.AddRange(listRaw);
					if (!DAOWorker.HisSereServTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServTemp that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServTemps))
            {
                if (!DAOWorker.HisSereServTempDAO.UpdateList(this.beforeUpdateHisSereServTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServTemps", this.beforeUpdateHisSereServTemps));
                }
            }
        }
    }
}
