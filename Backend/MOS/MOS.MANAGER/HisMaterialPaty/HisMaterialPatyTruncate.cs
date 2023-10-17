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
    class HisMaterialPatyTruncate : BusinessBase
    {
        internal HisMaterialPatyTruncate()
            : base()
        {

        }

        internal HisMaterialPatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MATERIAL_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialPatyCheck checker = new HisMaterialPatyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMaterialPatyDAO.Truncate(data);

                    try
                    {
                        HIS_MATERIAL hisMaterial = new HisMaterialGet().GetById(data.MATERIAL_ID);
                        HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == hisMaterial.MATERIAL_TYPE_ID);
                        string eventLog = "";
                        ProcessEventLog(data, ref eventLog);
                        new EventLogGenerator(EventLog.Enum.HisMaterialPaty_XoaChinhSachGiaVatTu, eventLog)
                            .MaterialTypeCode(materialType.MATERIAL_TYPE_CODE)
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

        internal bool TruncateList(List<HIS_MATERIAL_PATY> listData)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    foreach (HIS_MATERIAL_PATY data in listData)
                    {
                        result = result && this.Truncate(data);
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

        internal bool TruncateByPatientTypeId(long patientTypeId)
        {
            bool result = false;
            try
            {
                List<HIS_MATERIAL_PATY> materialPaties = new HisMaterialPatyGet().GetByPatientTypeId(patientTypeId);
                if (IsNotNullOrEmpty(materialPaties))
                {
                    result = this.TruncateList(materialPaties);
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

        internal bool TruncateByMaterialId(long materialId)
        {
            bool result = false;
            try
            {
                List<HIS_MATERIAL_PATY> materialPaties = new HisMaterialPatyGet().GetByMaterialId(materialId);
                if (IsNotNullOrEmpty(materialPaties))
                {
                    result = this.TruncateList(materialPaties);
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

        private void ProcessEventLog(HIS_MATERIAL_PATY data, ref string eventLog)
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
    }
}
