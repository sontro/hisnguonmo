using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    class HisMaterialTypeTruncate : BusinessBase
    {
        internal HisMaterialTypeTruncate()
            : base()
        {

        }

        internal HisMaterialTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MATERIAL_TYPE raw = null;
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                valid = valid && serviceChecker.CheckConstraint(raw.SERVICE_ID);
                if (valid)
                {
                    long? parentId = raw.PARENT_ID;
                    if (DAOWorker.HisMaterialTypeDAO.Truncate(raw))
                    {
                        result = new HisServiceTruncate(param).Truncate(raw.SERVICE_ID);
                        //set lai is_leaf = null cho dich vu parent cu
                        if (parentId.HasValue)
                        {
                            HIS_MATERIAL_TYPE oldParent = new HisMaterialTypeGet().GetById(parentId.Value);
                            List<HIS_MATERIAL_TYPE> children = new HisMaterialTypeGet().GetByParentId(parentId.Value);
                            if (!IsNotNullOrEmpty(children))
                            {
                                oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                                if (!new HisMaterialTypeUpdate(param).Update(oldParent))
                                {
                                    throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                                }
                            }
                        }
                    }

                    string eventLog = "";
                    ProcessEventLog(data, ref eventLog);

                    new EventLogGenerator(EventLog.Enum.HisMaterialType_XoaLoaiVatTu, eventLog)
                    .MaterialTypeId(data.ID.ToString())
                    .MaterialTypeCode(data.MATERIAL_TYPE_CODE)
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

        private void ProcessEventLog(HIS_MATERIAL_TYPE data, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();
                if (IsNotNullOrEmpty(data.HIS_SERVICE.HIS_PATIENT_TYPE.PATIENT_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinDoiTuongThanhToan);
                    editFields.Add(String.Format(fieldName, ": ", data.HIS_SERVICE.HIS_PATIENT_TYPE.PATIENT_TYPE_NAME));
                }
                eventLog = String.Join(". ", editFields);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool IsDiffLong(long? oldValue, long? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
    }
}
