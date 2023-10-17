using AutoMapper;
using Inventec.Core;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisMediRecordBorrow;
using MOS.MANAGER.HisTreatment.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord.OutOfMediRecord
{
    internal class HisTreatmentOutOfMediRecord : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisTreatmentOutOfMediRecord()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisTreatmentOutOfMediRecord(CommonParam param)
            : base(param)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, ref HIS_TREATMENT resultData)
        {
            List<HIS_TREATMENT> rs = new List<HIS_TREATMENT>();

            if (this.Run(new List<long>() { treatment.ID }, ref rs))
            {
                resultData = rs[0];
                return true;
            }
            return false;
        }

        internal bool Run(List<long> treatmentIds, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TREATMENT> treatments = new List<HIS_TREATMENT>();
                List<HIS_MEDI_RECORD> mediRecords = null;
                HisTreatmentOutOfMediRecordCheck checker = new HisTreatmentOutOfMediRecordCheck(param);
                HisMediRecordCheck mediRecordChecker = new HisMediRecordCheck(param);
                HisMediRecordBorrowCheck mediBorrowChecker = new HisMediRecordBorrowCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && treatmentChecker.VerifyIds(treatmentIds, treatments);
                valid = valid && checker.HasMediRecordId(treatments);
                valid = valid && checker.IsNotRecordInspectionApproved(treatments);
                valid = valid && mediBorrowChecker.IsNotBorrowed(treatments.Select(o => o.MEDI_RECORD_ID.Value).ToList());
                valid = valid && checker.HasPermission(treatments, ref mediRecords);
                valid = valid && mediRecordChecker.IsUnLock(mediRecords);
                
                if (valid)
                {
                    this.ProcessTreatment(treatments);
                    this.ProcessMediRecord(mediRecords);
                    this.ProcessSyncEmr(treatments);
                    result = true;
                    resultData = treatments;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void ProcessSyncEmr(List<HIS_TREATMENT> treatments)
        {
            HisTreatmentUploadEmr syncEmr = new HisTreatmentUploadEmr();
            syncEmr.Run(treatments, true);
        }

        private void ProcessTreatment(List<HIS_TREATMENT> treatments)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            List<HIS_TREATMENT> befores = Mapper.Map<List<HIS_TREATMENT>>(treatments);
            foreach (var treat in treatments)
            {
                treat.MEDI_RECORD_ID = null;
                treat.PROGRAM_ID = null;
                treat.MEDI_RECORD_TYPE_ID = null;
                treat.DATA_STORE_ID = null;
                treat.STORE_TIME = null;
                //Neu bat cau hinh "giữ 'mã lưu trữ'" thì không clear thông tin này
                if (!HisTreatmentCFG.IS_KEEPING_STORE_CODE)
                {
                    treat.STORE_CODE = null;
                }
            }

            if (!this.hisTreatmentUpdate.UpdateList(treatments, befores))
            {
                throw new Exception("hisTreatmentUpdate. Ket thuc nghiep vu");
            }
        }

        private void ProcessMediRecord(List<HIS_MEDI_RECORD> mediRecords)
        {
            if (IsNotNullOrEmpty(mediRecords))
            {
                List<long> mediRecordIds = mediRecords.Select(o => o.ID).ToList();
                string idStr = string.Join(",", mediRecordIds);

                List<string> sqls = new List<string>();
                sqls.Add(String.Format("DELETE HIS_MEDI_RECORD_BORROW M WHERE MEDI_RECORD_ID IN ({0}) AND NOT EXISTS (SELECT 1 FROM HIS_TREATMENT T WHERE T.MEDI_RECORD_ID = M.ID) ", idStr));
                sqls.Add(String.Format("DELETE HIS_MEDI_RECORD M WHERE ID IN ({0}) AND NOT EXISTS (SELECT 1 FROM HIS_TREATMENT T WHERE T.MEDI_RECORD_ID = M.ID) ", idStr));
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Sql. " + sqls.ToString());
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
