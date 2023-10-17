using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineGroup
{
    partial class HisMedicineGroupCreate : BusinessBase
    {
		private List<HIS_MEDICINE_GROUP> recentHisMedicineGroups = new List<HIS_MEDICINE_GROUP>();
		
        internal HisMedicineGroupCreate()
            : base()
        {

        }

        internal HisMedicineGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICINE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineGroupCheck checker = new HisMedicineGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDICINE_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMedicineGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMedicineGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMedicineGroups))
            {
                if (!DAOWorker.HisMedicineGroupDAO.TruncateList(this.recentHisMedicineGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicineGroups", this.recentHisMedicineGroups));
                }
				this.recentHisMedicineGroups = null;
            }
        }
    }
}
