using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeCreate : BusinessBase
    {
		private List<HIS_MR_CHECK_ITEM_TYPE> recentHisMrCheckItemTypes = new List<HIS_MR_CHECK_ITEM_TYPE>();
		
        internal HisMrCheckItemTypeCreate()
            : base()
        {

        }

        internal HisMrCheckItemTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MR_CHECK_ITEM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMrCheckItemTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItemType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrCheckItemType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMrCheckItemTypes.Add(data);
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
		
		internal bool CreateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMrCheckItemTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItemType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrCheckItemType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMrCheckItemTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMrCheckItemTypes))
            {
                if (!DAOWorker.HisMrCheckItemTypeDAO.TruncateList(this.recentHisMrCheckItemTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckItemType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMrCheckItemTypes", this.recentHisMrCheckItemTypes));
                }
				this.recentHisMrCheckItemTypes = null;
            }
        }
    }
}
