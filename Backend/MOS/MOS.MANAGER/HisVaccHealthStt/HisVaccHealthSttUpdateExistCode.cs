using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccHealthStt
{
    partial class HisVaccHealthSttUpdate : BusinessBase
    {
		private List<HIS_VACC_HEALTH_STT> beforeUpdateHisVaccHealthStts = new List<HIS_VACC_HEALTH_STT>();
		
        internal HisVaccHealthSttUpdate()
            : base()
        {

        }

        internal HisVaccHealthSttUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACC_HEALTH_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccHealthSttCheck checker = new HisVaccHealthSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACC_HEALTH_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.VACC_HEALTH_STT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisVaccHealthSttDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccHealthStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccHealthStt that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisVaccHealthStts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_VACC_HEALTH_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccHealthSttCheck checker = new HisVaccHealthSttCheck(param);
                List<HIS_VACC_HEALTH_STT> listRaw = new List<HIS_VACC_HEALTH_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VACC_HEALTH_STT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccHealthSttDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccHealthStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccHealthStt that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisVaccHealthStts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccHealthStts))
            {
                if (!DAOWorker.HisVaccHealthSttDAO.UpdateList(this.beforeUpdateHisVaccHealthStts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccHealthStt that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccHealthStts", this.beforeUpdateHisVaccHealthStts));
                }
				this.beforeUpdateHisVaccHealthStts = null;
            }
        }
    }
}
