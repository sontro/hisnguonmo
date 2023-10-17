using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediContractMaty
{
    partial class HisMediContractMatyUpdate : BusinessBase
    {
        private List<HIS_MEDI_CONTRACT_MATY> beforeUpdateHisMediContractMatys = new List<HIS_MEDI_CONTRACT_MATY>();

        internal HisMediContractMatyUpdate()
            : base()
        {

        }

        internal HisMediContractMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_CONTRACT_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_CONTRACT_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMediContractMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                List<HIS_MEDI_CONTRACT_MATY> listRaw = new List<HIS_MEDI_CONTRACT_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMaty that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMediContractMatys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_MEDI_CONTRACT_MATY> listData, List<HIS_MEDI_CONTRACT_MATY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediContractMaty that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMediContractMatys.AddRange(listBefore);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediContractMatys))
            {
                if (!DAOWorker.HisMediContractMatyDAO.UpdateList(this.beforeUpdateHisMediContractMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediContractMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMediContractMatys", this.beforeUpdateHisMediContractMatys));
                }
                this.beforeUpdateHisMediContractMatys = null;
            }
        }
    }
}
