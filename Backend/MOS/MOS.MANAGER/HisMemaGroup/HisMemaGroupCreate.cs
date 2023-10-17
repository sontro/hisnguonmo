using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    partial class HisMemaGroupCreate : BusinessBase
    {
		private List<HIS_MEMA_GROUP> recentHisMemaGroups = new List<HIS_MEMA_GROUP>();
		
        internal HisMemaGroupCreate()
            : base()
        {

        }

        internal HisMemaGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEMA_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMemaGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMemaGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMemaGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMemaGroups.Add(data);
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
		
		internal bool CreateList(List<HIS_MEMA_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMemaGroupDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMemaGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMemaGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMemaGroups.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMemaGroups))
            {
                if (!new HisMemaGroupTruncate(param).TruncateList(this.recentHisMemaGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisMemaGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMemaGroups", this.recentHisMemaGroups));
                }
            }
        }
    }
}
