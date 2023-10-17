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

namespace MOS.MANAGER.HisPrepare.Create
{
    class HisPrepareCreateSdo : BusinessBase
    {
        private PrepareProcessor prepareProcessor;
        private PrepareMatyProcessor prepareMatyProcessor;
        private PrepareMetyProcessor prepareMetyProcessor;


        private HIS_PREPARE recentPrepare = null;
        private List<HIS_PREPARE_MATY> recentPrepareMatys = null;
        private List<HIS_PREPARE_METY> recentPrepareMetys = null;

        internal HisPrepareCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisPrepareCreateSdo(CommonParam param)
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
                HisPrepareCheck checker = new HisPrepareCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && checker.CheckTreatmentType(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && checker.CheckIsMustPrepare(data);
                valid = valid && checker.CheckDuplicate(data);
                if (valid)
                {
                    if (!this.prepareProcessor.Run(data, ref this.recentPrepare))
                    {
                        throw new Exception("prepareProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMatyProcessor.Run(this.recentPrepare, data.MaterialTypes, ref this.recentPrepareMatys))
                    {
                        throw new Exception("prepareMatyProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMetyProcessor.Run(this.recentPrepare, data.MedicineTypes, ref this.recentPrepareMetys))
                    {
                        throw new Exception("prepareMetyProcessor. Ket thuc nghiep vu");
                    }

                    this.PassResult(ref resultData);
                    result = true;

                    HisPrepareLog.Run(treatment, this.recentPrepare, this.recentPrepareMatys, this.recentPrepareMetys, LibraryEventLog.EventLog.Enum.HisPrepare_TaoDuTruBenhNhan);
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

        private void PassResult(ref HisPrepareResultSDO resultData)
        {
            resultData = new HisPrepareResultSDO();
            resultData.HisPrepare = this.recentPrepare;
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
