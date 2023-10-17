using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusionSum
{
    partial class HisInfusionSumCreate : BusinessBase
    {
		private List<HIS_INFUSION_SUM> recentHisInfusionSums = new List<HIS_INFUSION_SUM>();
		
        internal HisInfusionSumCreate()
            : base()
        {

        }

        internal HisInfusionSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_INFUSION_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInfusionSumCheck checker = new HisInfusionSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisInfusionSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusionSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInfusionSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInfusionSums.Add(data);
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
		
		internal bool CreateList(List<HIS_INFUSION_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInfusionSumCheck checker = new HisInfusionSumCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisInfusionSumDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInfusionSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInfusionSum that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisInfusionSums.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisInfusionSums))
            {
                if (!new HisInfusionSumTruncate(param).TruncateList(this.recentHisInfusionSums))
                {
                    LogSystem.Warn("Rollback du lieu HisInfusionSum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisInfusionSums", this.recentHisInfusionSums));
                }
            }
        }
    }
}
