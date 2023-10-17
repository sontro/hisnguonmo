using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Delete;
using MOS.MANAGER.HisRestRetrType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccination
{
    partial class HisVaccinationTruncate : BusinessBase
    {
        internal HisVaccinationTruncate()
            : base()
        {

        }

        internal HisVaccinationTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HisVaccinationSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_VACCINATION vaccination = null;
                HIS_EXP_MEST expMest = null;
                WorkPlaceSDO wp = null;

                HisVaccinationCheck checker = new HisVaccinationCheck(param);
                valid = valid && (data.Id > 0 && data.RequestRoomId > 0);
                valid = valid && checker.VerifyId(data.Id, ref vaccination);
                valid = valid && checker.IsUnLock(vaccination);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref wp);
                valid = valid && checker.IsStatusNew(vaccination);
                valid = valid && checker.HasNotBill(vaccination);
                valid = valid && checker.IsValidExpMestForDelete(vaccination, ref expMest);
                if (valid)
                {
                    this.ProcessExpMest(data, expMest);
                    if (!DAOWorker.HisVaccinationDAO.Truncate(vaccination))
                    {
                        throw new Exception("Xoa HIS_VACCINATION that bai. Kiem tra lai du lieu");
                    }

                    result = true;
                    string expMestCode = expMest != null ? expMest.EXP_MEST_CODE : null;
                    new EventLogGenerator(EventLog.Enum.HisVaccination_XoaYeuCauTiem)
                        .PatientCode(vaccination.TDL_PATIENT_CODE)
                        .VaccinationCode(vaccination.VACCINATION_CODE)
                        .ExpMestCode(expMestCode)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessExpMest(HisVaccinationSDO data, HIS_EXP_MEST expMest)
        {
            if (expMest != null)
            {
                HisExpMestSDO sdo = new HisExpMestSDO();
                sdo.ExpMestId = expMest.ID;
                sdo.ReqRoomId = data.RequestRoomId;
                if (!new HisExpMestTruncate(param).Truncate(sdo, true))
                {
                    throw new Exception("Xoa du lieu bang chi tiet that bai.");
                }
            }
        }
    }
}
