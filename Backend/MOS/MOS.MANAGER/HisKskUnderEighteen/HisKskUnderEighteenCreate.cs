using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenCreate : BusinessBase
    {
		private List<HIS_KSK_UNDER_EIGHTEEN> recentHisKskUnderEighteens = new List<HIS_KSK_UNDER_EIGHTEEN>();
		
        internal HisKskUnderEighteenCreate()
            : base()
        {

        }

        internal HisKskUnderEighteenCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_UNDER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskUnderEighteenDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUnderEighteen_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskUnderEighteen that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskUnderEighteens.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskUnderEighteenDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUnderEighteen_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskUnderEighteen that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskUnderEighteens.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskUnderEighteens))
            {
                if (!DAOWorker.HisKskUnderEighteenDAO.TruncateList(this.recentHisKskUnderEighteens))
                {
                    LogSystem.Warn("Rollback du lieu HisKskUnderEighteen that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskUnderEighteens", this.recentHisKskUnderEighteens));
                }
				this.recentHisKskUnderEighteens = null;
            }
        }
    }
}
