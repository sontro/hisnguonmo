using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeCreate : BusinessBase
    {
		private List<HIS_IMP_MEST_PROPOSE> recentHisImpMestProposes = new List<HIS_IMP_MEST_PROPOSE>();
		
        internal HisImpMestProposeCreate()
            : base()
        {

        }

        internal HisImpMestProposeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_PROPOSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisImpMestProposeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPropose_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestPropose that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestProposes.Add(data);
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
		
		internal bool CreateList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestProposeCheck checker = new HisImpMestProposeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestProposeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPropose_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestPropose that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestProposes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestProposes))
            {
                if (!DAOWorker.HisImpMestProposeDAO.TruncateList(this.recentHisImpMestProposes))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestPropose that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestProposes", this.recentHisImpMestProposes));
                }
				this.recentHisImpMestProposes = null;
            }
        }
    }
}
