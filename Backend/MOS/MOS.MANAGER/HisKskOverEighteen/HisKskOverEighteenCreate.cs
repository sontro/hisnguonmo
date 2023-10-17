using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOverEighteen
{
    partial class HisKskOverEighteenCreate : BusinessBase
    {
		private List<HIS_KSK_OVER_EIGHTEEN> recentHisKskOverEighteens = new List<HIS_KSK_OVER_EIGHTEEN>();
		
        internal HisKskOverEighteenCreate()
            : base()
        {

        }

        internal HisKskOverEighteenCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_OVER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskOverEighteenDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOverEighteen_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskOverEighteen that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskOverEighteens.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskOverEighteenDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOverEighteen_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskOverEighteen that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskOverEighteens.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskOverEighteens))
            {
                if (!DAOWorker.HisKskOverEighteenDAO.TruncateList(this.recentHisKskOverEighteens))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOverEighteen that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskOverEighteens", this.recentHisKskOverEighteens));
                }
				this.recentHisKskOverEighteens = null;
            }
        }
    }
}
