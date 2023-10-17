using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.SDO;

namespace MOS.MANAGER.HisMediRecord.Store
{
    partial class HisMediRecordStore : BusinessBase
    {		
        internal HisMediRecordStore()
            : base()
        {

        }

        internal HisMediRecordStore(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Store(HisMediRecordStoreSDO sdo, ref HIS_MEDI_RECORD resultData)
        {
            List<HIS_MEDI_RECORD> rs = new List<HIS_MEDI_RECORD>();
            bool result = this.Store(new List<HisMediRecordStoreSDO>() { sdo }, ref rs);
            if (IsNotNullOrEmpty(rs))
            {
                resultData = rs[0];
                return result;
            }
            return false;
        }

        internal bool Store(List<HisMediRecordStoreSDO> datas, ref List<HIS_MEDI_RECORD> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MEDI_RECORD> mediRecords = new List<HIS_MEDI_RECORD>();
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                HisMediRecordStoreCheck storeChecker = new HisMediRecordStoreCheck(param);
                List<long> mediRecordIds = datas.Select(o => o.id).ToList();
                valid = valid && checker.VerifyIds(mediRecordIds, mediRecords);
                valid = valid && storeChecker.IsNotStored(mediRecords);
                if (valid)
                {
                    foreach (var record in mediRecords)
                    {
                        HisMediRecordStoreSDO data = datas.First(o => o.id == record.ID);
                        if (IsNotNull(data))
                        {
                            record.IS_NOT_STORED = null;
                            record.LOCATION_STORE_ID = data.LocationStoreId;
                        }
                    }
                    if (!DAOWorker.HisMediRecordDAO.UpdateList(mediRecords))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecord that bai." + LogUtil.TraceData("mediRecords", mediRecords));
                    }
                    result = true;
                    resultData = mediRecords;

                    // Ghi nhat ky tac dong
                    this.ProcessEventLog(mediRecords, true);
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

        internal bool UnStore(long mediRecordId, ref HIS_MEDI_RECORD resultData)
        {
            bool result = false;
            try
            {
                HIS_MEDI_RECORD mediRecord = null;
                HisMediRecordCheck checker = new HisMediRecordCheck(param);
                HisMediRecordStoreCheck storeChecker = new HisMediRecordStoreCheck(param);
                bool valid = checker.VerifyId(mediRecordId, ref mediRecord);
                valid = valid && storeChecker.IsStored(mediRecord);
                if (valid)
                {
                    mediRecord.IS_NOT_STORED = Constant.IS_TRUE;
                    if (!DAOWorker.HisMediRecordDAO.Update(mediRecord))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecord_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecord that bai." + LogUtil.TraceData("mediRecord", mediRecord));
                    }
                    result = true;
                    resultData = mediRecord;

                    // Ghi nhat ky tac dong
                    this.ProcessEventLog(new List<HIS_MEDI_RECORD>() { mediRecord }, false);
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

        private void ProcessEventLog(List<HIS_MEDI_RECORD> mediRecords, bool isStore)
        {
            HisMediRecordView1FilterQuery filter = new HisMediRecordView1FilterQuery();
            filter.IDs = mediRecords.Select(s => s.ID).ToList();
            var records = new HisMediRecordGet().GetView1(filter);
            if (IsNotNullOrEmpty(records))
            {
                foreach (var record in records)
                {
                    if (isStore)
                    {
                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisMediRecord_NhapKhoBenhAn).TreatmentCode(record.TREATMENT_CODE).PatientCode(record.PATIENT_CODE).StoreCode(record.STORE_CODE).Run();
                    }
                    else
                    {
                        new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisMediRecord_HuyNhapKhoBenhAn).TreatmentCode(record.TREATMENT_CODE).PatientCode(record.PATIENT_CODE).StoreCode(record.STORE_CODE).Run();
                    }
                }
            }
        }
    }
}
