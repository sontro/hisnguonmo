using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServReha;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Reha
{
    /// <summary>
    /// Xu ly phuc hoi chuc nang
    /// </summary>
    class HisServiceReqRehaUpdate : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqRehaUpdate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqRehaUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Update(HisRehaServiceReqUpdateSDO data, ref HisRehaServiceReqUpdateSDO resultData)
        {
            bool result = false;
            try
            {
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck();
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(data.ServiceReqId);
                HIS_TREATMENT treatment = null;

                if (treatmentChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment)
                    && treatmentChecker.IsUnLockHein(treatment))
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.ICD_TEXT = data.IcdText;
                    serviceReq.ICD_NAME = data.IcdName;
                    serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                    serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                    serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                    serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(data.IcdSubCode);
                    serviceReq.SYMPTOM_AFTER = data.SymptomAfter;
                    serviceReq.SYMPTOM_BEFORE = data.SymptomBefore;
                    serviceReq.RESPIRATORY_BEFORE = data.RespiratoryBefore;
                    serviceReq.RESPIRATORY_AFTER = data.RespiratoryAfter;
                    serviceReq.ECG_BEFORE = data.EcgBefore;
                    serviceReq.ECG_AFTER = data.EcgAfter;
                    serviceReq.ADVISE = data.Advise;

                    result = this.hisServiceReqUpdate.Update(serviceReq, beforeUpdate, false); //check treatment sau

                    if (result)
                    {
                        Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                        HIS_TREATMENT beforeUpdateTreatment = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                        bool hasUpdate = HisTreatmentUpdate.SetIcd(serviceReq, beforeUpdate, treatment);

                        //Neu co su thay doi thi moi thuc hien update
                        if (hasUpdate && !new HisTreatmentUpdate().Update(treatment, beforeUpdateTreatment))
                        {
                            LogSystem.Warn("Cap nhat thong tin ICD cho treatment dua vao ICD cua service_req that bai");
                        }
                        
                        resultData = data;
                        result = true;
                    };
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
