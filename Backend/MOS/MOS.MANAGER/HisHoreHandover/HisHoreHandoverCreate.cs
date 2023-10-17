using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandover
{
    partial class HisHoreHandoverCreate : BusinessBase
    {
		private List<HIS_HORE_HANDOVER> recentHisHoreHandovers = new List<HIS_HORE_HANDOVER>();
		
        internal HisHoreHandoverCreate()
            : base()
        {

        }

        internal HisHoreHandoverCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HORE_HANDOVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisHoreHandoverDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandover_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreHandover that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHoreHandovers.Add(data);
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
		
		internal bool CreateList(List<HIS_HORE_HANDOVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisHoreHandoverDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandover_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreHandover that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisHoreHandovers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisHoreHandovers))
            {
                if (!DAOWorker.HisHoreHandoverDAO.TruncateList(this.recentHisHoreHandovers))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreHandover that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHoreHandovers", this.recentHisHoreHandovers));
                }
				this.recentHisHoreHandovers = null;
            }
        }
    }
}
