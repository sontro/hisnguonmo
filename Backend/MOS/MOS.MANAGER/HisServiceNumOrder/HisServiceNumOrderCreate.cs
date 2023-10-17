using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    partial class HisServiceNumOrderCreate : BusinessBase
    {
		private List<HIS_SERVICE_NUM_ORDER> recentHisServiceNumOrders = new List<HIS_SERVICE_NUM_ORDER>();
		
        internal HisServiceNumOrderCreate()
            : base()
        {

        }

        internal HisServiceNumOrderCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_NUM_ORDER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExsists(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceNumOrderDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceNumOrder_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceNumOrder that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceNumOrders.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_NUM_ORDER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExsists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceNumOrderDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceNumOrder_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceNumOrder that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceNumOrders.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceNumOrders))
            {
                if (!DAOWorker.HisServiceNumOrderDAO.TruncateList(this.recentHisServiceNumOrders))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceNumOrder that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceNumOrders", this.recentHisServiceNumOrders));
                }
				this.recentHisServiceNumOrders = null;
            }
        }
    }
}
