using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    partial class HisMestMetyUnitCreate : BusinessBase
    {
		private List<HIS_MEST_METY_UNIT> recentHisMestMetyUnits = new List<HIS_MEST_METY_UNIT>();
		
        internal HisMestMetyUnitCreate()
            : base()
        {

        }

        internal HisMestMetyUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_METY_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMestMetyUnitDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMetyUnit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestMetyUnits.Add(data);
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
		
		internal bool CreateList(List<HIS_MEST_METY_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyUnitCheck checker = new HisMestMetyUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestMetyUnitDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyUnit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMetyUnit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestMetyUnits.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestMetyUnits))
            {
                if (!DAOWorker.HisMestMetyUnitDAO.TruncateList(this.recentHisMestMetyUnits))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMetyUnit that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestMetyUnits", this.recentHisMestMetyUnits));
                }
				this.recentHisMestMetyUnits = null;
            }
        }
    }
}
