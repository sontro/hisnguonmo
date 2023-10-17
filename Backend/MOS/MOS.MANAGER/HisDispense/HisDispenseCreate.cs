using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense
{
    partial class HisDispenseCreate : BusinessBase
    {
		private List<HIS_DISPENSE> recentHisDispenses = new List<HIS_DISPENSE>();
		
        internal HisDispenseCreate()
            : base()
        {

        }

        internal HisDispenseCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DISPENSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDispenseCheck checker = new HisDispenseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDispenseDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispense_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDispense that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDispenses.Add(data);
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
		
		internal bool CreateList(List<HIS_DISPENSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDispenseCheck checker = new HisDispenseCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDispenseDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispense_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDispense that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDispenses.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDispenses))
            {
                if (!DAOWorker.HisDispenseDAO.TruncateList(this.recentHisDispenses))
                {
                    LogSystem.Warn("Rollback du lieu HisDispense that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDispenses", this.recentHisDispenses));
                }
				this.recentHisDispenses = null;
            }
        }
    }
}
