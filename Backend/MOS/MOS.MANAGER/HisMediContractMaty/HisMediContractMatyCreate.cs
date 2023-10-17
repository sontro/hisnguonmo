using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMaty
{
    partial class HisMediContractMatyCreate : BusinessBase
    {
		private List<HIS_MEDI_CONTRACT_MATY> recentHisMediContractMatys = new List<HIS_MEDI_CONTRACT_MATY>();
		
        internal HisMediContractMatyCreate()
            : base()
        {

        }

        internal HisMediContractMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_CONTRACT_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMediContractMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediContractMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediContractMatys.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDI_CONTRACT_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediContractMatyCheck checker = new HisMediContractMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediContractMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediContractMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediContractMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediContractMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediContractMatys))
            {
                if (!DAOWorker.HisMediContractMatyDAO.TruncateList(this.recentHisMediContractMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediContractMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediContractMatys", this.recentHisMediContractMatys));
                }
				this.recentHisMediContractMatys = null;
            }
        }
    }
}
