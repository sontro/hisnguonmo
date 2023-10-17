using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEquipmentSetMaty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEquipmentSet
{
    partial class HisEquipmentSetUpdate : BusinessBase
    {
        private List<HIS_EQUIPMENT_SET> beforeUpdateHisEquipmentSets = new List<HIS_EQUIPMENT_SET>();

        internal HisEquipmentSetUpdate()
            : base()
        {

        }

        internal HisEquipmentSetUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EQUIPMENT_SET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EQUIPMENT_SET raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EQUIPMENT_SET_CODE, data.ID);
                if (valid)
                {
                    if (new HisEquipmentSetMatyTruncate(param).TruncateByEquipmentSetId(raw.ID))
                    {
                        List<HIS_EQUIPMENT_SET_MATY> ekipTempUsers = data.HIS_EQUIPMENT_SET_MATY.ToList();
                        ekipTempUsers.ForEach(t => t.EQUIPMENT_SET_ID = raw.ID);

                        if (new HisEquipmentSetMatyCreate(param).CreateList(ekipTempUsers))
                        {
                            data.HIS_EQUIPMENT_SET_MATY = null;
                            if (!DAOWorker.HisEquipmentSetDAO.Update(data))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSet_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin HisEquipmentSet that bai." + LogUtil.TraceData("data", data));
                            }
                        }
                    }

                    this.beforeUpdateHisEquipmentSets.Add(raw);

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

        internal bool UpdateList(List<HIS_EQUIPMENT_SET> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetCheck checker = new HisEquipmentSetCheck(param);
                List<HIS_EQUIPMENT_SET> listRaw = new List<HIS_EQUIPMENT_SET>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EQUIPMENT_SET_CODE, data.ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEquipmentSetDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSet_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEquipmentSet that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisEquipmentSets.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEquipmentSets))
            {
                if (!DAOWorker.HisEquipmentSetDAO.UpdateList(this.beforeUpdateHisEquipmentSets))
                {
                    LogSystem.Warn("Rollback du lieu HisEquipmentSet that bai, can kiem tra lai." + LogUtil.TraceData("HisEquipmentSets", this.beforeUpdateHisEquipmentSets));
                }
                this.beforeUpdateHisEquipmentSets = null;
            }
        }
    }
}
