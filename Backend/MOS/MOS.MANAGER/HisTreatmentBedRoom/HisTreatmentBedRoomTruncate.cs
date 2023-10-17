using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientObservation;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomTruncate : BusinessBase
    {
        internal HisTreatmentBedRoomTruncate()
            : base()
        {

        }

        internal HisTreatmentBedRoomTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT_BED_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_BED_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    var patientObservations = new HisPatientObservationGet().GetByTreatmentBedRoomId(raw.ID);
                    if (IsNotNullOrEmpty(patientObservations))
                    {
                        if (!DAOWorker.HisPatientObservationDAO.TruncateList(patientObservations))
                        {
                            throw new Exception("Xoa du lieu theo doi thoi gian that bai. Ket thuc nghiep vu");
                        }
                    }
                    result = DAOWorker.HisTreatmentBedRoomDAO.Truncate(data);
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
