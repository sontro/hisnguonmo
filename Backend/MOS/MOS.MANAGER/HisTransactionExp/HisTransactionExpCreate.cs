using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionExp
{
    partial class HisTransactionExpCreate : BusinessBase
    {
		private List<HIS_TRANSACTION_EXP> recentHisTransactionExps = new List<HIS_TRANSACTION_EXP>();
		
        internal HisTransactionExpCreate()
            : base()
        {

        }

        internal HisTransactionExpCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRANSACTION_EXP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisTransactionExpDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransactionExp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransactionExp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTransactionExps.Add(data);
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
		
		internal bool CreateList(List<HIS_TRANSACTION_EXP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransactionExpCheck checker = new HisTransactionExpCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTransactionExpDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransactionExp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTransactionExp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTransactionExps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTransactionExps))
            {
                if (!DAOWorker.HisTransactionExpDAO.TruncateList(this.recentHisTransactionExps))
                {
                    LogSystem.Warn("Rollback du lieu HisTransactionExp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTransactionExps", this.recentHisTransactionExps));
                }
				this.recentHisTransactionExps = null;
            }
        }
    }
}
