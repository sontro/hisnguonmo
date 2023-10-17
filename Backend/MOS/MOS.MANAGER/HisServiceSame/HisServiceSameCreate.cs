using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceSame
{
    partial class HisServiceSameCreate : BusinessBase
    {
		private List<HIS_SERVICE_SAME> recentHisServiceSames = new List<HIS_SERVICE_SAME>();
		
        internal HisServiceSameCreate()
            : base()
        {

        }

        internal HisServiceSameCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_SAME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceSameDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceSame_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceSame that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceSames.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_SAME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceSameCheck checker = new HisServiceSameCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceSameDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceSame_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceSame that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceSames.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceSames))
            {
                if (!DAOWorker.HisServiceSameDAO.TruncateList(this.recentHisServiceSames))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceSame that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceSames", this.recentHisServiceSames));
                }
				this.recentHisServiceSames = null;
            }
        }
    }
}
