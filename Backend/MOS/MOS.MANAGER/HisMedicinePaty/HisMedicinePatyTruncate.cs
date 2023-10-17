using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMedicine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicinePaty
{
    class HisMedicinePatyTruncate : BusinessBase
    {
        internal HisMedicinePatyTruncate()
            : base()
        {

        }

        internal HisMedicinePatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDICINE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMedicinePatyDAO.Truncate(data);

                    try
                    {
                        HIS_MEDICINE hisMedicine = new HisMedicineGet().GetById(data.MEDICINE_ID);
                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == hisMedicine.MEDICINE_TYPE_ID);
                        string eventLog = "";
                        ProcessEventLog(data, ref eventLog);
                        new EventLogGenerator(EventLog.Enum.HisMedicinePaty_XoaChinhSachGiaThuoc, eventLog)
                            .MedicineTypeCode(medicineType.MEDICINE_TYPE_CODE)
                            .Run();
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                    }
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

        private void ProcessEventLog(HIS_MEDICINE_PATY data, ref string eventLog)
        {
            try
            {
                if (data.PATIENT_TYPE_ID > 0)
                {
                    List<string> editFields = new List<string>();
                    HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == data.PATIENT_TYPE_ID);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinDoiTuongThanhToan);
                    editFields.Add(String.Format("{0}: {1}",fieldName, patientType.PATIENT_TYPE_NAME));
                    eventLog = String.Join(". ", editFields);
                }
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

        internal bool TruncateList(List<HIS_MEDICINE_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicinePatyDAO.TruncateList(listData);
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

        internal bool TruncateByPatientTypeId(long patientTypeId)
        {
            bool result = false;
            try
            {
                List<HIS_MEDICINE_PATY> medicinePaties = new HisMedicinePatyGet().GetByPatientTypeId(patientTypeId);
                if (IsNotNullOrEmpty(medicinePaties))
                {
                    result = this.TruncateList(medicinePaties);
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

        internal bool TruncateByMedicineId(long medicineId)
        {
            bool result = false;
            try
            {
                List<HIS_MEDICINE_PATY> medicinePaties = new HisMedicinePatyGet().GetByMedicineId(medicineId);
                if (IsNotNullOrEmpty(medicinePaties))
                {
                    result = this.TruncateList(medicinePaties);
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

        internal bool TruncateByMedicineId(List<long> medicineIds)
        {
            bool result = false;
            try
            {
                List<HIS_MEDICINE_PATY> medicinePaties = new HisMedicinePatyGet().GetByMedicineIds(medicineIds);
                if (IsNotNullOrEmpty(medicinePaties))
                {
                    result = this.TruncateList(medicinePaties);
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
