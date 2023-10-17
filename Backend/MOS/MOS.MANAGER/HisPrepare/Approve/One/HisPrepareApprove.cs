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

namespace MOS.MANAGER.HisPrepare.Approve.One
{
    class HisPrepareApprove : BusinessBase
    {
        private PrepareProcessor prepareProcessor;
        private PrepareMatyProcessor prepareMatyProcessor;
        private PrepareMetyProcessor prepareMetyProcessor;

        internal HisPrepareApprove()
            : base()
        {
            this.Init();
        }

        internal HisPrepareApprove(CommonParam param)
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

        internal bool Run(HisPrepareApproveSDO data, ref HisPrepareResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PREPARE raw = null;
                HIS_TREATMENT treatment = null;
                List<HIS_PREPARE_MATY> materials = null;
                List<HIS_PREPARE_METY> medicines = null;
                WorkPlaceSDO wpSDO = null;
                HisPrepareCheck commonChecker = new HisPrepareCheck(param);
                HisPrepareApproveCheck approveChecker = new HisPrepareApproveCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && approveChecker.ValidData(data);
                valid = valid && commonChecker.VerifyId(data.Id, ref raw);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref wpSDO);
                valid = valid && treatChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && commonChecker.IsMediStock(wpSDO);
                valid = valid && commonChecker.IsNotApprove(raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && approveChecker.CheckApproveDetail(data, ref materials, ref medicines);
                if (valid)
                {
                    if (!this.prepareProcessor.Run(data, raw))
                    {
                        throw new Exception("prepareProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMatyProcessor.Run(data.PrepareMatys, materials))
                    {
                        throw new Exception("prepareMatyProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMetyProcessor.Run(data.PrepareMetys, medicines))
                    {
                        throw new Exception("prepareMetyProcessor. Ket thuc nghiep vu");
                    }

                    this.PassResult(raw, materials, medicines, ref resultData);
                    result = true;

                    HisPrepareLog.Run(treatment, raw, materials, medicines, EventLog.Enum.HisPrepare_DuyetDuTruBenhNhan);
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

        private void PassResult(HIS_PREPARE prepare, List<HIS_PREPARE_MATY> materials, List<HIS_PREPARE_METY> medidinces, ref HisPrepareResultSDO resultData)
        {
            resultData = new HisPrepareResultSDO();
            resultData.HisPrepare = prepare;
            resultData.HisPrepareMatys = materials;
            resultData.HisPrepareMetys = medidinces;
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
