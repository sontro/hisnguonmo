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

namespace MOS.MANAGER.HisPrepare.Approve.List
{
    class HisPrepareApproveList : BusinessBase
    {
        private PrepareProcessor prepareProcessor;
        private PrepareMatyProcessor prepareMatyProcessor;
        private PrepareMetyProcessor prepareMetyProcessor;

        internal HisPrepareApproveList()
            : base()
        {
            this.Init();
        }

        internal HisPrepareApproveList(CommonParam param)
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

        internal bool Run(HisPrepareApproveListSDO data, ref List<HIS_PREPARE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_PREPARE> listRaw = new List<HIS_PREPARE>();
                List<HIS_TREATMENT> listTreatment = new List<HIS_TREATMENT>();
                WorkPlaceSDO wpSDO = null;
                HisPrepareCheck commonChecker = new HisPrepareCheck(param);
                HisPrepareApproveCheck checker = new HisPrepareApproveCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && commonChecker.VerifyIds(data.Ids, listRaw);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref wpSDO);
                valid = valid && treatChecker.VerifyIds(listRaw.Select(s => s.TREATMENT_ID).Distinct().ToList(), listTreatment);
                valid = valid && commonChecker.IsMediStock(wpSDO);
                valid = valid && commonChecker.IsNotApprove(listRaw);
                valid = valid && commonChecker.IsUnLock(listRaw);
                if (valid)
                {
                    if (!this.prepareProcessor.Run(data, listRaw))
                    {
                        throw new Exception("prepareProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMatyProcessor.Run(listRaw))
                    {
                        throw new Exception("prepareMatyProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.prepareMetyProcessor.Run(listRaw))
                    {
                        throw new Exception("prepareMetyProcessor. Ket thuc nghiep vu");
                    }

                    resultData = listRaw;
                    result = true;

                    HisPrepareLog.Run(listTreatment, listRaw, EventLog.Enum.HisPrepare_DuyetDanhSachDuTruBenhNhan);
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
                this.prepareProcessor.RolbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
