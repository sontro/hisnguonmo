using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Update
{
    class HisPrepareUpdateSdo : BusinessBase
    {
        private PrepareProcessor prepareProcessor;
        private PrepareMatyProcessor prepareMatyProcessor;
        private PrepareMetyProcessor prepareMetyProcessor;

        private List<HIS_PREPARE_MATY> recentPrepareMatys = null;
        private List<HIS_PREPARE_METY> recentPrepareMetys = null;

        internal HisPrepareUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisPrepareUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.prepareProcessor = new PrepareProcessor(param);
            this.prepareMatyProcessor = new PrepareMatyProcessor(param);
            this.prepareMetyProcessor = new PrepareMetyProcessor(param);
        }

        internal bool Run(HisPrepareSDO data, ref HisPrepareResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HIS_PREPARE raw = null;
                HisPrepareCheck checker = new HisPrepareCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && data.Id.HasValue;
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsNotApprove(raw);
                valid = valid && checker.IsCreatorOrAdmin(raw);
                valid = valid && checker.CheckDuplicate(data);
                valid = valid && checker.CheckIsMustPrepare(data);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.prepareProcessor.Run(data, raw))
                    {
                        throw new Exception("prepareProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMatyProcessor.Run(raw, data.MaterialTypes, ref recentPrepareMatys, ref sqls))
                    {
                        throw new Exception("prepareMatyProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMetyProcessor.Run(raw, data.MedicineTypes, ref recentPrepareMetys, ref sqls))
                    {
                        throw new Exception("prepareMetyProcessor. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql: " + sqls.ToString());
                    }

                    this.PassResult(raw, ref resultData);
                    result = true;

                    HisPrepareLog.Run(treatment, raw, this.recentPrepareMatys, this.recentPrepareMetys, LibraryEventLog.EventLog.Enum.HisPrepare_SuaDuTruBenhNhan);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void PassResult(HIS_PREPARE raw, ref HisPrepareResultSDO resultData)
        {
            resultData = new HisPrepareResultSDO();
            resultData.HisPrepare = raw;
            resultData.HisPrepareMatys = this.recentPrepareMatys;
            resultData.HisPrepareMetys = this.recentPrepareMetys;
        }

        private void Rollback()
        {
            try
            {
                this.prepareMetyProcessor.RollbackData();
                this.prepareMatyProcessor.RollbackData();
                this.prepareProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
