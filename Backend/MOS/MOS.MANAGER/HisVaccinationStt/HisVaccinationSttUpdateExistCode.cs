using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationStt
{
    partial class HisVaccinationSttUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_STT> beforeUpdateHisVaccinationStts = new List<HIS_VACCINATION_STT>();
		
        internal HisVaccinationSttUpdate()
            : base()
        {

        }

        internal HisVaccinationSttUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINATION_STT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.VACCINATION_STT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationSttDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationStt that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisVaccinationStts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_VACCINATION_STT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationSttCheck checker = new HisVaccinationSttCheck(param);
                List<HIS_VACCINATION_STT> listRaw = new List<HIS_VACCINATION_STT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.VACCINATION_STT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationSttDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationStt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationStt that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisVaccinationStts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationStts))
            {
                if (!DAOWorker.HisVaccinationSttDAO.UpdateList(this.beforeUpdateHisVaccinationStts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationStt that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationStts", this.beforeUpdateHisVaccinationStts));
                }
				this.beforeUpdateHisVaccinationStts = null;
            }
        }
    }
}
