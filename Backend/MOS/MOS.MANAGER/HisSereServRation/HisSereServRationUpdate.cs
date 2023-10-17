using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServRation
{
    partial class HisSereServRationUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_RATION> beforeUpdateHisSereServRations = new List<HIS_SERE_SERV_RATION>();
		
        internal HisSereServRationUpdate()
            : base()
        {

        }

        internal HisSereServRationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_RATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServRationCheck checker = new HisSereServRationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_RATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSereServRationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServRation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServRation that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSereServRations.Add(raw);
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

        internal bool UpdateList(List<HIS_SERE_SERV_RATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServRationCheck checker = new HisSereServRationCheck(param);
                List<HIS_SERE_SERV_RATION> listRaw = new List<HIS_SERE_SERV_RATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSereServRationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServRation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServRation that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSereServRations.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServRations))
            {
                if (!DAOWorker.HisSereServRationDAO.UpdateList(this.beforeUpdateHisSereServRations))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServRation that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServRations", this.beforeUpdateHisSereServRations));
                }
				this.beforeUpdateHisSereServRations = null;
            }
        }
    }
}
