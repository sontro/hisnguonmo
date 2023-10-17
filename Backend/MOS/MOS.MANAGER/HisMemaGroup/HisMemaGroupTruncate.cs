using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMemaGroup
{
    partial class HisMemaGroupTruncate : BusinessBase
    {
        internal HisMemaGroupTruncate()
            : base()
        {

        }

        internal HisMemaGroupTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                HIS_MEMA_GROUP raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    List<HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetByMemaGroupId(id);
                    List<HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetByMemaGroupId(id);
                    if (IsNotNullOrEmpty(medicineTypes))
                    {
                        medicineTypes.ForEach(o => o.MEMA_GROUP_ID = null);
                        if (!new HisMedicineTypeUpdate(param).UpdateList(medicineTypes))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(materialTypes))
                    {
                        materialTypes.ForEach(o => o.MEMA_GROUP_ID = null);
                        if (!new HisMaterialTypeUpdate(param).UpdateList(materialTypes))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }

                    result = DAOWorker.HisMemaGroupDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_MEMA_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMemaGroupDAO.TruncateList(listData);
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
