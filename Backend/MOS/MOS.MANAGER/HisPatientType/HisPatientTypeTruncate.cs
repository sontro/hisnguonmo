using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMestPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisServicePaty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientType
{
    partial class HisPatientTypeTruncate : BusinessBase
    {
        internal HisPatientTypeTruncate()
            : base()
        {

        }

        internal HisPatientTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PATIENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    new HisMestPatientTypeTruncate(param).TruncateByPatientTypeId(data.ID);
                    new HisPatientTypeAllowTruncate(param).TruncateByPatientTypeIdOrPatientTypeAllowId(data.ID);
                    new HisServicePatyTruncate(param).TruncateByPatientTypeId(data.ID);
                    new HisMedicinePatyTruncate(param).TruncateByPatientTypeId(data.ID);
                    new HisMaterialPatyTruncate(param).TruncateByPatientTypeId(data.ID);

                    result = DAOWorker.HisPatientTypeDAO.Truncate(data);

                    new EventLogGenerator(EventLog.Enum.HisPatientType_Xoa, data.PATIENT_TYPE_CODE, data.PATIENT_TYPE_NAME).Run();
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

        internal bool TruncateList(List<HIS_PATIENT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeCheck checker = new HisPatientTypeCheck(param);
                List<HIS_PATIENT_TYPE> listRaw = new List<HIS_PATIENT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisPatientTypeDAO.TruncateList(listData);
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
