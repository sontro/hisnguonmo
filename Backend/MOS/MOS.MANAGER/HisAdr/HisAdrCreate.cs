using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrCreate : BusinessBase
    {
		private List<HIS_ADR> recentHisAdrs = new List<HIS_ADR>();
		
        internal HisAdrCreate()
            : base()
        {

        }

        internal HisAdrCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ADR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAdrCheck checker = new HisAdrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisAdrDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdr_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAdr that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAdrs.Add(data);
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
		
		internal bool CreateList(List<HIS_ADR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAdrCheck checker = new HisAdrCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAdrDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAdr_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAdr that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAdrs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAdrs))
            {
                if (!DAOWorker.HisAdrDAO.TruncateList(this.recentHisAdrs))
                {
                    LogSystem.Warn("Rollback du lieu HisAdr that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAdrs", this.recentHisAdrs));
                }
				this.recentHisAdrs = null;
            }
        }
    }
}
