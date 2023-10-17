using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegimenHiv
{
    partial class HisRegimenHivUpdate : BusinessBase
    {
		private List<HIS_REGIMEN_HIV> beforeUpdateHisRegimenHivs = new List<HIS_REGIMEN_HIV>();
		
        internal HisRegimenHivUpdate()
            : base()
        {

        }

        internal HisRegimenHivUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REGIMEN_HIV data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REGIMEN_HIV raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REGIMEN_HIV_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRegimenHivDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegimenHiv_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegimenHiv that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRegimenHivs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_REGIMEN_HIV> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegimenHivCheck checker = new HisRegimenHivCheck(param);
                List<HIS_REGIMEN_HIV> listRaw = new List<HIS_REGIMEN_HIV>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REGIMEN_HIV_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisRegimenHivDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegimenHiv_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRegimenHiv that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisRegimenHivs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRegimenHivs))
            {
                if (!DAOWorker.HisRegimenHivDAO.UpdateList(this.beforeUpdateHisRegimenHivs))
                {
                    LogSystem.Warn("Rollback du lieu HisRegimenHiv that bai, can kiem tra lai." + LogUtil.TraceData("HisRegimenHivs", this.beforeUpdateHisRegimenHivs));
                }
				this.beforeUpdateHisRegimenHivs = null;
            }
        }
    }
}
