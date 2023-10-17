using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMaterial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialPaty
{
    class HisMaterialPatyUpdate : BusinessBase
    {
        private HIS_MATERIAL_PATY beforeUpdateHisMaterialPatyDTO;
        private List<HIS_MATERIAL_PATY> beforeUpdateHisMaterialPatyDTOs = new List<HIS_MATERIAL_PATY>();
        private static string FORMAT_EDIT = "{0}: {1} ==> {2}";

        internal HisMaterialPatyUpdate()
            : base()
        {

        }

        internal HisMaterialPatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MATERIAL_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MATERIAL_PATY raw = null;
                HisMaterialPatyCheck checker = new HisMaterialPatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {
                    this.beforeUpdateHisMaterialPatyDTO = raw;
                    if (!DAOWorker.HisMaterialPatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialPaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialPaty that bai." + LogUtil.TraceData("data", data));
                    }
                    try
                    {
                        HIS_MATERIAL hisMaterial = new HisMaterialGet().GetById(raw.MATERIAL_ID);
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == hisMaterial.MATERIAL_TYPE_ID);
                        string eventLog = "";
                        ProcessEventLog(raw, data, ref eventLog);
                        new EventLogGenerator(EventLog.Enum.HisMaterialPaty_SuaChinhSachGiaVatTu, eventLog)
                            .MaterialTypeCode(materialType.MATERIAL_TYPE_CODE)
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

        private void ProcessEventLog(HIS_MATERIAL_PATY raw, HIS_MATERIAL_PATY data, ref string eventLog)
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

        internal bool UpdateList(List<HIS_MATERIAL_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialPatyCheck checker = new HisMaterialPatyCheck(param);
                List<HIS_MATERIAL_PATY> listRaw = new List<HIS_MATERIAL_PATY>();
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
                    if (!DAOWorker.HisMaterialPatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialPaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialPaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisMaterialPatyDTOs.AddRange(listRaw);
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
            if (this.beforeUpdateHisMaterialPatyDTO != null)
            {
                if (!new HisMaterialPatyUpdate(param).Update(this.beforeUpdateHisMaterialPatyDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialPaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialPatyDTO", this.beforeUpdateHisMaterialPatyDTO));
                }
            }

            if (this.beforeUpdateHisMaterialPatyDTOs != null)
            {
                if (!new HisMaterialPatyUpdate(param).UpdateList(this.beforeUpdateHisMaterialPatyDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialPaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialPatyDTOs", this.beforeUpdateHisMaterialPatyDTOs));
                }
            }
        }
    }
}
