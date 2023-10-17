using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomTruncate : BusinessBase
    {
        internal HisMedicineTypeRoomTruncate()
            : base()
        {

        }

        internal HisMedicineTypeRoomTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                HIS_MEDICINE_TYPE_ROOM raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeRoomDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeRoomDAO.TruncateList(listData);
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
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                List<HIS_MEDICINE_TYPE_ROOM> listData = new List<HIS_MEDICINE_TYPE_ROOM>();
                valid = valid && checker.VerifyIds(ids, listData);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeRoomDAO.TruncateList(listData);
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
