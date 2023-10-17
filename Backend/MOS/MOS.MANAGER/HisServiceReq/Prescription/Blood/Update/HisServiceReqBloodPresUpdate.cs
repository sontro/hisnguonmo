using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.AssignService.TruncateReq;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood.Update
{
    class HisServiceReqBloodPresUpdate : BusinessBase
    {

        private ServiceReqProcessor serviceReqProcessor;
        private ExpMestProcessor expMestProcessor;
        private ExpMestBltyReqProcessor expMestBltyProcessor;

        private HIS_EXP_MEST recentHisExpMest;
        private HIS_SERVICE_REQ recentHisSerivceReq;
        private List<HIS_EXP_MEST_BLTY_REQ> recentHisExpMestBltyReqs;

        private ServiceReqTruncate serviceReqTruncate;
        private HisServiceReqAssignServiceCreate hisServiceReqAssignServiceCreate;

        internal HisServiceReqBloodPresUpdate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqBloodPresUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestBltyProcessor = new ExpMestBltyReqProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
            this.expMestBltyProcessor = new ExpMestBltyReqProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.serviceReqTruncate = new ServiceReqTruncate(param);
            this.hisServiceReqAssignServiceCreate = new HisServiceReqAssignServiceCreate(param);
        }

        internal bool Update(PatientBloodPresSDO data, ref PatientBloodPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                V_HIS_MEDI_STOCK mediStock = null;
                HIS_EXP_MEST expMest = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                List<HIS_SERVICE_REQ> oldDVKTs = null;

                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqBloodPresCheck checker = new HisServiceReqBloodPresCheck(param);
                valid = valid && checker.VerifydData(data);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.IsValidForUpdateDifferentBloodType(data);
                valid = valid && checker.ValidMediStockId(data, ref mediStock);
                valid = valid && serviceReqChecker.VerifyId(data.Id ?? 0, ref serviceReq);
                valid = valid && serviceReqChecker.IsUnLock(serviceReq);
                valid = valid && serviceReqChecker.IsNotStarted(serviceReq);
                valid = valid && checker.VerifyExpMest(data, serviceReq, ref expMest);
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && checker.IsValidPatientType(data, ref ptas);
                valid = valid && checker.CheckServicePaty(data, mediStock, treatment);
                valid = valid && checker.IsValidForParentServiceReq(data, serviceReq, expMest, ref oldDVKTs);
                if (valid)
                {
                    if (!serviceReqProcessor.Run(data, mediStock, serviceReq))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.recentHisSerivceReq = serviceReq;

                    if (!expMestProcessor.Run(serviceReq, expMest))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.recentHisExpMest = expMest;

                    if (!expMestBltyProcessor.Run(data, expMest, ref this.recentHisExpMestBltyReqs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    this.ProcessServiceFlow(this.recentHisExpMestBltyReqs, this.recentHisSerivceReq, oldDVKTs, treatment);
                    this.PassResult(ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessServiceFlow(List<HIS_EXP_MEST_BLTY_REQ> recentHisExpMestBltyReqs, HIS_SERVICE_REQ recentHisSerivceReq, List<HIS_SERVICE_REQ> oldDVKTs, HIS_TREATMENT treatment)
        {
            // Xoa dich vu ky thuat cu
            if (IsNotNullOrEmpty(oldDVKTs))
            {
                List<HIS_SERE_SERV> allSSByTreatment = new HisSereServGet().GetByTreatmentId(treatment.ID);
                var lstSereServDelete = allSSByTreatment != null ? allSSByTreatment.Where(o => oldDVKTs.Any(a => a.ID == o.SERVICE_REQ_ID)).ToList() : null;
                var existedSereServs = allSSByTreatment != null ? allSSByTreatment.Where(o => !oldDVKTs.Any(a => a.ID == o.SERVICE_REQ_ID)).ToList() : null;
                if (!this.serviceReqTruncate.Run(oldDVKTs, lstSereServDelete, allSSByTreatment, treatment, true))
                {
                    throw new Exception("serviceReqTruncate. Ket thuc nghiep vu");
                }
            }

            // Chi dinh dich vu ki thuat moi
            if (!HisServiceReqBloodPresUltil.AssignServiceForTestBlood(this.hisServiceReqAssignServiceCreate, recentHisExpMestBltyReqs, recentHisSerivceReq))
            {
                throw new Exception("Chi dinh dich vu ki thuat cho don mau that bai. Rollback");
            }
        }

        private void PassResult(ref PatientBloodPresResultSDO resultData)
        {
            resultData = new PatientBloodPresResultSDO();
            resultData.ExpMest = this.recentHisExpMest;
            resultData.ServiceReq = this.recentHisSerivceReq;
            resultData.Bloods = this.recentHisExpMestBltyReqs;
        }

        private void Rollback()
        {
            this.hisServiceReqAssignServiceCreate.RollbackData();
            this.expMestBltyProcessor.Rollback();
            this.expMestProcessor.Rollback();
            this.serviceReqProcessor.Rollback();
        }
    }
}
