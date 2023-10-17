using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyCreate : BusinessBase
    {
		private List<HIS_EQUIPMENT_SET_MATY> recentHisEquipmentSetMatys = new List<HIS_EQUIPMENT_SET_MATY>();
		
        internal HisEquipmentSetMatyCreate()
            : base()
        {

        }

        internal HisEquipmentSetMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EQUIPMENT_SET_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisEquipmentSetMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSetMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEquipmentSetMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEquipmentSetMatys.Add(data);
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
		
		internal bool CreateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEquipmentSetMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSetMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEquipmentSetMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEquipmentSetMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEquipmentSetMatys))
            {
                if (!DAOWorker.HisEquipmentSetMatyDAO.TruncateList(this.recentHisEquipmentSetMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisEquipmentSetMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEquipmentSetMatys", this.recentHisEquipmentSetMatys));
                }
				this.recentHisEquipmentSetMatys = null;
            }
        }
    }
}
