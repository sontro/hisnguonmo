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
    class HisMedicinePatyUpdate : BusinessBase
    {
        private HIS_MEDICINE_PATY beforeUpdateHisMedicinePatyDTO;
        private List<HIS_MEDICINE_PATY> beforeUpdateHisMedicinePatyDTOs = new List<HIS_MEDICINE_PATY>();
        private static string FORMAT_EDIT = "{0}: {1} ==> {2}";
        internal HisMedicinePatyUpdate()
            : base()
        {

        }

        internal HisMedicinePatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE_PATY raw = null;
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
                    this.beforeUpdateHisMedicinePatyDTO = raw;
                    if (!DAOWorker.HisMedicinePatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicinePaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicinePaty that bai." + LogUtil.TraceData("data", data));
                    }
                    try
                    {
                        HIS_MEDICINE hisMedicine = new HisMedicineGet().GetById(raw.MEDICINE_ID);
                        HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == hisMedicine.MEDICINE_TYPE_ID);
                        string eventLog = "";
                        ProcessEventLog(raw, data, ref eventLog);
                        new EventLogGenerator(EventLog.Enum.HisMedicinePaty_SuaChinhSachGiaThuoc, eventLog)
                            .MedicineTypeCode(medicineType.MEDICINE_TYPE_CODE)
                            .Run();
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                    }
                   
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

        private void ProcessEventLog(HIS_MEDICINE_PATY raw,HIS_MEDICINE_PATY data, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();
                
                if (IsDiffLong(raw.PATIENT_TYPE_ID, data.PATIENT_TYPE_ID))
                {
                    HIS_PATIENT_TYPE patientTypeRaw = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == raw.PATIENT_TYPE_ID);
                    HIS_PATIENT_TYPE patientTypeData = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == data.PATIENT_TYPE_ID);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToanCu);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientTypeRaw.PATIENT_TYPE_NAME, patientTypeData.PATIENT_TYPE_NAME));
                }

                if (IsDiffDecimal(raw.EXP_PRICE, data.EXP_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Gia);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.EXP_PRICE, data.EXP_PRICE));
                }
                if (IsDiffDecimal(raw.EXP_VAT_RATIO, data.EXP_VAT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VAT);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.EXP_VAT_RATIO, data.EXP_VAT_RATIO));
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
        private static bool IsDiffDecimal(decimal? oldValue, decimal? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private static bool IsDiffString(string oldValue, string newValue)
        {
            return (oldValue ?? "") != (newValue ?? "");
        }

        internal bool UpdateList(List<HIS_MEDICINE_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicinePatyCheck checker = new HisMedicinePatyCheck(param);
                List<HIS_MEDICINE_PATY> listRaw = new List<HIS_MEDICINE_PATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicinePatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicinePaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicinePaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMedicinePatyDTOs.AddRange(listRaw);
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
            if (this.beforeUpdateHisMedicinePatyDTO != null)
            {
                if (!new HisMedicinePatyUpdate(param).Update(this.beforeUpdateHisMedicinePatyDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicinePaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicinePatyDTO", this.beforeUpdateHisMedicinePatyDTO));
                }
            }

            if (this.beforeUpdateHisMedicinePatyDTOs != null)
            {
                if (!new HisMedicinePatyUpdate(param).UpdateList(this.beforeUpdateHisMedicinePatyDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicinePaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicinePatyDTOs", this.beforeUpdateHisMedicinePatyDTOs));
                }
            }
        }
    }
}
