using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineType
{
    class HisMedicineTypeTruncate : BusinessBase
    {
        internal HisMedicineTypeTruncate()
            : base()
        {

        }

        internal HisMedicineTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE_TYPE raw = null;
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw.ID);
                valid = valid && serviceChecker.CheckConstraint(raw.SERVICE_ID);
                if (valid)
                {
                    long? parentId = raw.PARENT_ID;
                    if (DAOWorker.HisMedicineTypeDAO.Truncate(raw))
                    {
                        result = new HisServiceTruncate(param).Truncate(raw.SERVICE_ID);
                        //set lai is_leaf = null cho dich vu parent cu
                        if (parentId.HasValue)
                        {
                            HIS_MEDICINE_TYPE oldParent = new HisMedicineTypeGet().GetById(parentId.Value);
                            List<HIS_MEDICINE_TYPE> children = new HisMedicineTypeGet().GetByParentId(parentId.Value);
                            if (!IsNotNullOrEmpty(children))
                            {
                                oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                                if (!new HisMedicineTypeUpdate(param).Update(oldParent))
                                {
                                    throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                                }
                            }
                        }
                    }

                    new EventLogGenerator(EventLog.Enum.HisMedicineType_XoaLoaiThuoc)
                       .MedicineTypeId(data.ID.ToString())
                       .MedicineTypeCode(data.MEDICINE_TYPE_CODE)
                       .Run();
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
