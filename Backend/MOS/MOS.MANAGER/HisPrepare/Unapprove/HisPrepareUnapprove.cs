using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Unapprove
{
    class HisPrepareUnapprove : BusinessBase
    {
        private PrepareProcessor prepareProcessor;
        private PrepareMatyProcessor prepareMatyProcessor;
        private PrepareMetyProcessor prepareMetyProcessor;

        internal HisPrepareUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisPrepareUnapprove(CommonParam param)
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

        internal bool Run(HisPrepareSDO data, ref HIS_PREPARE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PREPARE raw = null;
                HIS_TREATMENT treatment = null;
                List<HIS_PREPARE_MATY> materials = null;
                List<HIS_PREPARE_METY> medicines = null;
                HisPrepareCheck commonChecker = new HisPrepareCheck(param);
                HisPrepareUnapproveCheck checker = new HisPrepareUnapproveCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && (data.Id.HasValue && data.Id.Value > 0);
                valid = valid && (data.ReqRoomId.HasValue && data.ReqRoomId.Value > 0);
                valid = valid && commonChecker.VerifyId(data.Id.Value, ref raw);
                valid = valid && treatChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && commonChecker.IsApproved(raw);
                valid = valid && checker.IsApproverOrAdmin(raw);
                valid = valid && checker.CheckAllowUnapprove(raw, ref materials, ref medicines);
                if (valid)
                {
                    if (!this.prepareProcessor.Run(raw))
                    {
                        throw new Exception("prepareProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMatyProcessor.Run(raw, materials))
                    {
                        throw new Exception("prepareMatyProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMetyProcessor.Run(raw, medicines))
                    {
                        throw new Exception("prepareMetyProcessor. Ket thuc nghiep vu");
                    }

                    resultData = raw;
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisPrepare_HuyDuyetDuTruBenhNhan)
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .PrepareCode(raw.PREPARE_CODE)
                    .Run();
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
