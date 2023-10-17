using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockCreate : BusinessBase
    {
		private List<HIS_NUM_ORDER_BLOCK> recentHisNumOrderBlocks = new List<HIS_NUM_ORDER_BLOCK>();
		
        internal HisNumOrderBlockCreate()
            : base()
        {

        }

        internal HisNumOrderBlockCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_NUM_ORDER_BLOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
					if (!DAOWorker.HisNumOrderBlockDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderBlock_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNumOrderBlock that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisNumOrderBlocks.Add(data);
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
		
		internal bool CreateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisNumOrderBlockDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderBlock_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNumOrderBlock that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisNumOrderBlocks.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisNumOrderBlocks))
            {
                if (!DAOWorker.HisNumOrderBlockDAO.TruncateList(this.recentHisNumOrderBlocks))
                {
                    LogSystem.Warn("Rollback du lieu HisNumOrderBlock that bai, can kiem tra lai." + LogUtil.TraceData("recentHisNumOrderBlocks", this.recentHisNumOrderBlocks));
                }
				this.recentHisNumOrderBlocks = null;
            }
        }
    }
}
