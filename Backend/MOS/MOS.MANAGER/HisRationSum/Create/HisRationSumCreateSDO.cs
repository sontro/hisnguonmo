using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Create
{
    class HisRationSumCreateSDO : BusinessBase
    {
        private List<HIS_RATION_SUM> recentRationSum;

        private RationSumProcessor rationSumProcessor;
        private ServiceReqProcessor serviceReqProcessor;
        private SereServProcessor sereServProcessor;

        internal HisRationSumCreateSDO()
            : base()
        {
            this.Init();
        }

        internal HisRationSumCreateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.rationSumProcessor = new RationSumProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HisRationSumSDO data, ref List<HIS_RATION_SUM> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                List<HIS_SERVICE_REQ> listRaw = new List<HIS_SERVICE_REQ>();
                List<HIS_TREATMENT> listTreatment = new List<HIS_TREATMENT>();
                List<HIS_SERE_SERV_RATION> sereServRations = null;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyField(data);
                valid = valid && this.HasWorkPlaceInfo(data.RoomId, ref workPlace);
                valid = valid && serviceReqChecker.VerifyIds(data.ServiceReqIds, listRaw);
                valid = valid && serviceReqChecker.IsTypeRation(listRaw);
                valid = valid && serviceReqChecker.HasNoRationSum(listRaw);
                valid = valid && treatmentChecker.VerifyIds(listRaw.Select(s => s.TREATMENT_ID).Distinct().ToList(), listTreatment);
                valid = valid && treatmentChecker.IsUnLock(listTreatment);
                valid = valid && treatmentChecker.IsUnpause(listTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(listTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(listTreatment);
                valid = valid && this.ValidData(listRaw, ref sereServRations);
                if (valid)
                {
                    if (!this.rationSumProcessor.Run(data, listRaw, workPlace, ref this.recentRationSum))
                    {
                        throw new Exception("rationSumProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.serviceReqProcessor.Run(this.recentRationSum, listRaw))
                    {
                        throw new Exception("serviceReqProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.sereServProcessor.Run(listTreatment, listRaw, sereServRations))
                    {
                        throw new Exception("rationSumProcessor. Ket thuc nghiep vu");
                    }

                    resultData = this.recentRationSum;
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rolback();
                result = false;
            }
            return result;
        }

        private bool ValidData(List<HIS_SERVICE_REQ> listRaw, ref List<HIS_SERE_SERV_RATION> sereServRations)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_RATION> ssRations = new HisSereServRationGet().GetByServiceReqIds(listRaw.Select(s => s.ID).ToList());
                var notExists = listRaw.Where(o => ssRations == null || !ssRations.Any(a => a.SERVICE_REQ_ID == o.ID)).ToList();

                if (IsNotNullOrEmpty(notExists))
                {
                    string codes = String.Join(",", notExists.Select(s => s.SERVICE_REQ_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisRationSum_YeuCauXuatAnKhongCoDichVu, codes);
                    return false;
                }

                sereServRations = ssRations;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private void Rolback()
        {
            try
            {
                this.sereServProcessor.Rollback();
                this.serviceReqProcessor.Rollback();
                this.rationSumProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
