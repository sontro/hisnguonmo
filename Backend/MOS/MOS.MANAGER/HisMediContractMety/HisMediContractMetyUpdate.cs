using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediContractMety
{
    partial class HisMediContractMetyUpdate : BusinessBase
    {
		private List<HIS_MEDI_CONTRACT_METY> beforeUpdateHisMediContractMetys = new List<HIS_MEDI_CONTRACT_METY>();
		
        internal HisMediContractMetyUpdate()
            : base()
        {

        }

        internal HisMediContractMetyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_CONTRACT_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_CONTRACT_METY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMediContractMetyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMety that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMediContractMetys.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                List<HIS_MEDI_CONTRACT_METY> listRaw = new List<HIS_MEDI_CONTRACT_METY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMediContractMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMety that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMediContractMetys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_MEDI_CONTRACT_METY> listData, List<HIS_MEDI_CONTRACT_METY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMetyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMety_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMety that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMediContractMetys.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediContractMetys))
            {
                if (!DAOWorker.HisMediContractMetyDAO.UpdateList(this.beforeUpdateHisMediContractMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediContractMety that bai, can kiem tra lai." + LogUtil.TraceData("HisMediContractMetys", this.beforeUpdateHisMediContractMetys));
                }
				this.beforeUpdateHisMediContractMetys = null;
            }
        }
    }
}
