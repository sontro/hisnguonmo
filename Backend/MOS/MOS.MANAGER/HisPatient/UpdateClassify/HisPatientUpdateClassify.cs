using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient.UpdateClassify
{
    class HisPatientUpdateClassify : BusinessBase
    {
        private HIS_PATIENT beforeUpdate;

        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private List<HisSereServUpdateHein> updateHeinprocessors = new List<HisSereServUpdateHein>();

        internal HisPatientUpdateClassify()
            : base()
        {
            this.Init();
        }

        internal HisPatientUpdateClassify(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HisPatientUpdateClassifySDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PATIENT raw = null;
                List<HIS_TREATMENT> treatmentsToUpdate = null;
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.PatientId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                    this.beforeUpdate = Mapper.Map<HIS_PATIENT>(raw);

                    raw.PATIENT_CLASSIFY_ID = data.PatientClassifyId;

                    if (!DAOWorker.HisPatientDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", data));
                    }

                    this.ProcessTreatment(data,ref treatmentsToUpdate);
                    this.ProcessServiceReq(data, treatmentsToUpdate);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessServiceReq(HisPatientUpdateClassifySDO data, List<HIS_TREATMENT> treatmentsToUpdate)
        {
            if (IsNotNullOrEmpty(treatmentsToUpdate))
            {
                var serviceReqs = new HisServiceReqGet().GetByTreatmentIds(treatmentsToUpdate.Where(o => o.IS_PAUSE != Constant.IS_TRUE).Select(o => o.ID).ToList());

                if (IsNotNullOrEmpty(serviceReqs))
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                   
                    serviceReqs.ForEach(o => o.TDL_PATIENT_CLASSIFY_ID = data.PatientClassifyId);

                    if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                    {
                        throw new Exception("Khong cap nhat phan loai benh nhan cho serviceReqs");
                    }
                }
            }
        }

        private void ProcessTreatment(HisPatientUpdateClassifySDO data,ref List<HIS_TREATMENT> treatmentsToUpdate)
        {
            HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
            filter.PATIENT_ID = data.PatientId;
            filter.IS_ACTIVE = Constant.IS_TRUE;
            filter.IS_PAUSE = false;
            treatmentsToUpdate = new HisTreatmentGet().Get(filter);

            if (IsNotNullOrEmpty(treatmentsToUpdate))
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                List<HIS_TREATMENT> befores = Mapper.Map<List<HIS_TREATMENT>>(treatmentsToUpdate);

                treatmentsToUpdate.ForEach(o => o.TDL_PATIENT_CLASSIFY_ID = data.PatientClassifyId);

                if (!this.hisTreatmentUpdate.UpdateList(treatmentsToUpdate, befores))
                {
                    throw new Exception("Khong cap nhat phan loai benh nhan cho treatment");
                }

                if (HisServicePatyCFG.HAS_PATIENT_CLASSIFY && befores.Exists(o => o.TDL_PATIENT_CLASSIFY_ID != data.PatientClassifyId))
                {
                    List<HIS_PATIENT_TYPE_ALTER> allptas = new HisPatientTypeAlterGet().GetByTreatmentIds(treatmentsToUpdate.Select(o => o.ID).ToList());

                    foreach (var treatment in treatmentsToUpdate)
                    {
                        var ptas = IsNotNullOrEmpty(allptas) ? allptas.Where(o => o.TREATMENT_ID == treatment.ID).ToList() : null;
                        HisSereServUpdateHein updateHeinprocessor = new HisSereServUpdateHein(param, treatment, ptas, false);
                        this.updateHeinprocessors.Add(updateHeinprocessor);
                        //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang
                        if (!updateHeinprocessor.UpdateDb())
                        {
                            throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                        }
                    }
                }
            }
        }

        private void RollbackData()
        {
            this.hisServiceReqUpdate.RollbackData();
            if (IsNotNullOrEmpty(this.updateHeinprocessors))
            {
                foreach (HisSereServUpdateHein p in this.updateHeinprocessors)
                {
                    p.RollbackData();
                }
            }
            this.hisTreatmentUpdate.RollbackData();
            if (this.beforeUpdate != null)
            {
                if (!DAOWorker.HisPatientDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback du lieu HisPatient that bai, can kiem tra lai.");
                }
            }
        }
    }
}
