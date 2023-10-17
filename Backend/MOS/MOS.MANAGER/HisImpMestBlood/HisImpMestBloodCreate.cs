using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestBlood
{
    partial class HisImpMestBloodCreate : BusinessBase
    {
		private List<HIS_IMP_MEST_BLOOD> recentHisImpMestBloods = new List<HIS_IMP_MEST_BLOOD>();
		
        internal HisImpMestBloodCreate()
            : base()
        {

        }

        internal HisImpMestBloodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisImpMestBloodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestBlood that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestBloods.Add(data);
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
		
		internal bool CreateList(List<HIS_IMP_MEST_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestBloodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestBloods.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestBloods))
            {
                if (!new HisImpMestBloodTruncate(param).TruncateList(this.recentHisImpMestBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestBlood that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestBloods", this.recentHisImpMestBloods));
                }
            }
        }
    }
}
