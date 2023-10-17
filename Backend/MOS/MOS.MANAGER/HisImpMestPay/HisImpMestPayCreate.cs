using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayCreate : BusinessBase
    {
		private List<HIS_IMP_MEST_PAY> recentHisImpMestPays = new List<HIS_IMP_MEST_PAY>();
		
        internal HisImpMestPayCreate()
            : base()
        {

        }

        internal HisImpMestPayCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_MEST_PAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisImpMestPayDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPay_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestPay that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMestPays.Add(data);
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
		
		internal bool CreateList(List<HIS_IMP_MEST_PAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestPayCheck checker = new HisImpMestPayCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestPayDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestPay_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMestPay that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisImpMestPays.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisImpMestPays))
            {
                if (!DAOWorker.HisImpMestPayDAO.TruncateList(this.recentHisImpMestPays))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestPay that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpMestPays", this.recentHisImpMestPays));
                }
				this.recentHisImpMestPays = null;
            }
        }
    }
}
