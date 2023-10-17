using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceFollow;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    /// <summary>
    /// Xu ly nghiep vu ke ma'u
    /// </summary>
    class HisServiceReqBloodPresCreate : BusinessBase
    {
        private HisServiceReqMaker hisServiceReqMaker;
        private HisExpMestMaker hisExpMestMaker;
        private HisExpMestBltyReqMaker hisExpMestBltyReqMaker;
        private HisTreatmentUpdate hisTreatmentUpdate;

        private HIS_SERVICE_REQ recentHisSerivceReq;
        private HIS_EXP_MEST recentHisExpMest;
        private List<HIS_EXP_MEST_BLTY_REQ> recentHisExpMestBltyReqs;
        private HisServiceReqAssignServiceCreate hisServiceReqAssignServiceCreate;

        internal HisServiceReqBloodPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqBloodPresCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqMaker = new HisServiceReqMaker(param);
            this.hisExpMestMaker = new HisExpMestMaker(param);
            this.hisExpMestBltyReqMaker = new HisExpMestBltyReqMaker(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisServiceReqAssignServiceCreate = new HisServiceReqAssignServiceCreate(param);
        }

        internal bool Create(PatientBloodPresSDO data, ref PatientBloodPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                V_HIS_MEDI_STOCK mediStock = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                string sessionCode = Guid.NewGuid().ToString();
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqBloodPresCheck checker = new HisServiceReqBloodPresCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                valid = valid && checker.VerifydData(data);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.ValidMediStockId(data, ref mediStock);
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && checker.IsValidPatientType(data, ref ptas);
                valid = valid && checker.CheckServicePaty(data, mediStock, treatment);
                if (valid)
                {
                    if (!this.hisServiceReqMaker.Run(treatment, data, mediStock, ptas, sessionCode, ref this.recentHisSerivceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestMaker.Run(this.recentHisSerivceReq, mediStock, ref this.recentHisExpMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestBltyReqMaker.Run(data, this.recentHisExpMest, ref this.recentHisExpMestBltyReqs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    this.ProcessServiceFlow(this.recentHisExpMestBltyReqs, this.recentHisSerivceReq);
                    this.PassResult(ref resultData);
                    //tao tien trinh moi de update thong tin treatment
                    this.UpdateTreatmentThreadInit(treatment, data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessServiceFlow(List<HIS_EXP_MEST_BLTY_REQ> hisExpMestBltyReqs, HIS_SERVICE_REQ serivceReq)
        {
            // Chi dinh dich vu ki thuat moi
            if (!HisServiceReqBloodPresUltil.AssignServiceForTestBlood(this.hisServiceReqAssignServiceCreate, hisExpMestBltyReqs, serivceReq))
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

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, PatientBloodPresSDO data)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateTreatment));
                thread.Priority = ThreadPriority.Highest;
                UpdateIcdTreatmentThreadData threadData = new UpdateIcdTreatmentThreadData();
                threadData.Treatment = treatment;
                threadData.Treatment = treatment;
                threadData.IcdCode = data.IcdCode;
                threadData.IcdName = data.IcdName;
                threadData.IcdSubCode = data.IcdSubCode;
                threadData.IcdText = data.IcdText;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void ThreadProcessUpdateTreatment(object threadData)
        {
            try
            {
                //Tien trinh xu ly cap nhat thong tin ICD cua treatment
                UpdateIcdTreatmentThreadData td = (UpdateIcdTreatmentThreadData)threadData;
                HIS_TREATMENT treatment = td.Treatment;

                bool hasUpdate = false;
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                //Neu treatment chua co thong tin benh chinh thi cap nhat thong tin benh chinh cho treatment
                if (string.IsNullOrWhiteSpace(treatment.ICD_CODE) && string.IsNullOrWhiteSpace(treatment.ICD_NAME))
                {
                    if (!string.IsNullOrWhiteSpace(td.IcdCode) || !string.IsNullOrWhiteSpace(td.IcdName))
                    {
                        treatment.ICD_CODE = td.IcdCode;
                        treatment.ICD_NAME = td.IcdName;
                        hasUpdate = true;
                    }
                }

                if (HisTreatmentUpdate.AddIcd(treatment, td.IcdSubCode, td.IcdText))
                {
                    hasUpdate = true;
                }

                //Neu co su thay doi thong tin ICD thi moi thuc hien update treatment
                if (hasUpdate && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    LogSystem.Warn("Cap nhat thong tin ICD cho treatment dua vao thong tin ICD cua dich vu kham that bai.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public void RollbackData()
        {
            this.hisServiceReqAssignServiceCreate.RollbackData();
            this.hisExpMestBltyReqMaker.Rollback();
            this.hisExpMestMaker.Rollback();
            this.hisServiceReqMaker.Rollback();
        }
    }
}
