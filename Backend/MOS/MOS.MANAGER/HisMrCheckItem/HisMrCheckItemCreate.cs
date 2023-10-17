using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItem
{
    partial class HisMrCheckItemCreate : BusinessBase
    {
		private List<HIS_MR_CHECK_ITEM> recentHisMrCheckItems = new List<HIS_MR_CHECK_ITEM>();
		
        internal HisMrCheckItemCreate()
            : base()
        {

        }

        internal HisMrCheckItemCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MR_CHECK_ITEM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMrCheckItemDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItem_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrCheckItem that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMrCheckItems.Add(data);
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
		
		internal bool CreateList(List<HIS_MR_CHECK_ITEM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMrCheckItemDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItem_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrCheckItem that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMrCheckItems.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMrCheckItems))
            {
                if (!DAOWorker.HisMrCheckItemDAO.TruncateList(this.recentHisMrCheckItems))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckItem that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMrCheckItems", this.recentHisMrCheckItems));
                }
				this.recentHisMrCheckItems = null;
            }
        }
    }
}
