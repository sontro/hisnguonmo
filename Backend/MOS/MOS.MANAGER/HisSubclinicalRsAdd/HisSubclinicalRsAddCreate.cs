using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddCreate : BusinessBase
    {
		private List<HIS_SUBCLINICAL_RS_ADD> recentHisSubclinicalRsAdds = new List<HIS_SUBCLINICAL_RS_ADD>();
		
        internal HisSubclinicalRsAddCreate()
            : base()
        {

        }

        internal HisSubclinicalRsAddCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SUBCLINICAL_RS_ADD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSubclinicalRsAddDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSubclinicalRsAdd_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSubclinicalRsAdd that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSubclinicalRsAdds.Add(data);
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
		
		internal bool CreateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSubclinicalRsAddDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSubclinicalRsAdd_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSubclinicalRsAdd that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSubclinicalRsAdds.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSubclinicalRsAdds))
            {
                if (!DAOWorker.HisSubclinicalRsAddDAO.TruncateList(this.recentHisSubclinicalRsAdds))
                {
                    LogSystem.Warn("Rollback du lieu HisSubclinicalRsAdd that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSubclinicalRsAdds", this.recentHisSubclinicalRsAdds));
                }
				this.recentHisSubclinicalRsAdds = null;
            }
        }
    }
}
