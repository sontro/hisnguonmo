using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestUpdate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_REQUEST> beforeUpdateHisAntibioticRequests = new List<HIS_ANTIBIOTIC_REQUEST>();
		
        internal HisAntibioticRequestUpdate()
            : base()
        {

        }

        internal HisAntibioticRequestUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIBIOTIC_REQUEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIBIOTIC_REQUEST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_REQUEST_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticRequestDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticRequest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticRequest that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAntibioticRequests.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                List<HIS_ANTIBIOTIC_REQUEST> listRaw = new List<HIS_ANTIBIOTIC_REQUEST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ANTIBIOTIC_REQUEST_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntibioticRequestDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticRequest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticRequest that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAntibioticRequests.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntibioticRequests))
            {
                if (!DAOWorker.HisAntibioticRequestDAO.UpdateList(this.beforeUpdateHisAntibioticRequests))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticRequest that bai, can kiem tra lai." + LogUtil.TraceData("HisAntibioticRequests", this.beforeUpdateHisAntibioticRequests));
                }
				this.beforeUpdateHisAntibioticRequests = null;
            }
        }
    }
}
