using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyTruncate : BusinessBase
    {
        internal HisEquipmentSetMatyTruncate()
            : base()
        {

        }

        internal HisEquipmentSetMatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                HIS_EQUIPMENT_SET_MATY raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetMatyDAO.Truncate(raw);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                List<HIS_EQUIPMENT_SET_MATY> listData = new List<HIS_EQUIPMENT_SET_MATY>();
                checker.VerifyIds(ids, listData);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetMatyDAO.TruncateList(listData);
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

        private bool TruncateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisEquipmentSetMatyDAO.TruncateList(listData);
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

        internal bool TruncateByEquipmentSetId(long equipmentSetId)
        {
            bool result = true;
            try
            {
                List<HIS_EQUIPMENT_SET_MATY> equipmentSetMatys = new HisEquipmentSetMatyGet().GetByEquipmentSetId(equipmentSetId);
                if (IsNotNullOrEmpty(equipmentSetMatys))
                {
                    result = this.TruncateList(equipmentSetMatys);
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
    }
}
