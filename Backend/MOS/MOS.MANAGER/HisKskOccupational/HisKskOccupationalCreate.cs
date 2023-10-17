using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    partial class HisKskOccupationalCreate : BusinessBase
    {
		private List<HIS_KSK_OCCUPATIONAL> recentHisKskOccupationals = new List<HIS_KSK_OCCUPATIONAL>();
		
        internal HisKskOccupationalCreate()
            : base()
        {

        }

        internal HisKskOccupationalCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_OCCUPATIONAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskOccupationalDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOccupational_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskOccupational that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskOccupationals.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_OCCUPATIONAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOccupationalCheck checker = new HisKskOccupationalCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskOccupationalDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOccupational_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskOccupational that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskOccupationals.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskOccupationals))
            {
                if (!DAOWorker.HisKskOccupationalDAO.TruncateList(this.recentHisKskOccupationals))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOccupational that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskOccupationals", this.recentHisKskOccupationals));
                }
				this.recentHisKskOccupationals = null;
            }
        }
    }
}
