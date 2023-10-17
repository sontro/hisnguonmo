using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaexVaer
{
    partial class HisVaexVaerUpdate : BusinessBase
    {
		private List<HIS_VAEX_VAER> beforeUpdateHisVaexVaers = new List<HIS_VAEX_VAER>();
		
        internal HisVaexVaerUpdate()
            : base()
        {

        }

        internal HisVaexVaerUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VAEX_VAER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VAEX_VAER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaexVaerDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaexVaer_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaexVaer that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaexVaers.Add(raw);
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

        internal bool UpdateList(List<HIS_VAEX_VAER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaexVaerCheck checker = new HisVaexVaerCheck(param);
                List<HIS_VAEX_VAER> listRaw = new List<HIS_VAEX_VAER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaexVaerDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaexVaer_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaexVaer that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaexVaers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaexVaers))
            {
                if (!DAOWorker.HisVaexVaerDAO.UpdateList(this.beforeUpdateHisVaexVaers))
                {
                    LogSystem.Warn("Rollback du lieu HisVaexVaer that bai, can kiem tra lai." + LogUtil.TraceData("HisVaexVaers", this.beforeUpdateHisVaexVaers));
                }
				this.beforeUpdateHisVaexVaers = null;
            }
        }
    }
}
