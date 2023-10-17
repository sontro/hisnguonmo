using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEquipmentSet
{
    partial class HisEquipmentSetCreate : BusinessBase
    {
		private List<HIS_EQUIPMENT_SET> recentHisEquipmentSets = new List<HIS_EQUIPMENT_SET>();
		
        internal HisEquipmentSetCreate()
            : base()
        {

        }

        internal HisEquipmentSetCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EQUIPMENT_SET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisEquipmentSetDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSet_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEquipmentSet that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEquipmentSets.Add(data);
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
		
		internal bool CreateList(List<HIS_EQUIPMENT_SET> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEquipmentSetDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSet_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEquipmentSet that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEquipmentSets.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEquipmentSets))
            {
                if (!DAOWorker.HisEquipmentSetDAO.TruncateList(this.recentHisEquipmentSets))
                {
                    LogSystem.Warn("Rollback du lieu HisEquipmentSet that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEquipmentSets", this.recentHisEquipmentSets));
                }
				this.recentHisEquipmentSets = null;
            }
        }
    }
}
