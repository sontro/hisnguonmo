using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    partial class HisMediContractMetyCreate : BusinessBase
    {
		private List<HIS_MEDI_CONTRACT_METY> recentHisMediContractMetys = new List<HIS_MEDI_CONTRACT_METY>();
		
        internal HisMediContractMetyCreate()
            : base()
        {

        }

        internal HisMediContractMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_CONTRACT_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMediContractMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediContractMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediContractMetys.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDI_CONTRACT_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMetyCheck checker = new HisMediContractMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediContractMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediContractMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediContractMetys))
            {
                if (!DAOWorker.HisMediContractMetyDAO.TruncateList(this.recentHisMediContractMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediContractMety that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediContractMetys", this.recentHisMediContractMetys));
                }
				this.recentHisMediContractMetys = null;
            }
        }
    }
}
