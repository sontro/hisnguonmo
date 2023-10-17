using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiUpdate : BusinessBase
    {
		private List<HIS_ANTIBIOTIC_MICROBI> beforeUpdateHisAntibioticMicrobis = new List<HIS_ANTIBIOTIC_MICROBI>();
		
        internal HisAntibioticMicrobiUpdate()
            : base()
        {

        }

        internal HisAntibioticMicrobiUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTIBIOTIC_MICROBI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTIBIOTIC_MICROBI raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ANTIBIOTIC_MICROBI_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAntibioticMicrobiDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticMicrobi_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticMicrobi that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAntibioticMicrobis.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticMicrobiCheck checker = new HisAntibioticMicrobiCheck(param);
                List<HIS_ANTIBIOTIC_MICROBI> listRaw = new List<HIS_ANTIBIOTIC_MICROBI>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ANTIBIOTIC_MICROBI_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAntibioticMicrobiDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntibioticMicrobi_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAntibioticMicrobi that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAntibioticMicrobis.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAntibioticMicrobis))
            {
                if (!DAOWorker.HisAntibioticMicrobiDAO.UpdateList(this.beforeUpdateHisAntibioticMicrobis))
                {
                    LogSystem.Warn("Rollback du lieu HisAntibioticMicrobi that bai, can kiem tra lai." + LogUtil.TraceData("HisAntibioticMicrobis", this.beforeUpdateHisAntibioticMicrobis));
                }
				this.beforeUpdateHisAntibioticMicrobis = null;
            }
        }
    }
}
