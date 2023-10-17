using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    partial class HisHoreHohaCreate : BusinessBase
    {
		private List<HIS_HORE_HOHA> recentHisHoreHohas = new List<HIS_HORE_HOHA>();
		
        internal HisHoreHohaCreate()
            : base()
        {

        }

        internal HisHoreHohaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HORE_HOHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHohaCheck checker = new HisHoreHohaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisHoreHohaDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHoha_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreHoha that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHoreHohas.Add(data);
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
		
		internal bool CreateList(List<HIS_HORE_HOHA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHohaCheck checker = new HisHoreHohaCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHoreHohaDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHoha_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreHoha that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHoreHohas.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHoreHohas))
            {
                if (!DAOWorker.HisHoreHohaDAO.TruncateList(this.recentHisHoreHohas))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreHoha that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHoreHohas", this.recentHisHoreHohas));
                }
				this.recentHisHoreHohas = null;
            }
        }
    }
}
