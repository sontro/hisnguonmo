using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactSum
{
    partial class HisMediReactSumCreate : BusinessBase
    {
		private List<HIS_MEDI_REACT_SUM> recentHisMediReactSums = new List<HIS_MEDI_REACT_SUM>();
		
        internal HisMediReactSumCreate()
            : base()
        {

        }

        internal HisMediReactSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_REACT_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMediReactSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediReactSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediReactSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediReactSums.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDI_REACT_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediReactSumDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediReactSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediReactSum that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediReactSums.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediReactSums))
            {
                if (!new HisMediReactSumTruncate(param).TruncateList(this.recentHisMediReactSums))
                {
                    LogSystem.Warn("Rollback du lieu HisMediReactSum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediReactSums", this.recentHisMediReactSums));
                }
            }
        }
    }
}
