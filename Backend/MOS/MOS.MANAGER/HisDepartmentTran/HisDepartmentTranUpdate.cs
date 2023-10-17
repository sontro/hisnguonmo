using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepartmentTran
{
    class HisDepartmentTranUpdate : BusinessBase
    {
        private List<HIS_DEPARTMENT_TRAN> beforeUpdateHisDepartmentTrans = new List<HIS_DEPARTMENT_TRAN>();

        private HIS_DEPARTMENT_TRAN recentHisDepartmentTran;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisDepartmentTranUpdate()
            : base()
        {
            this.Init();
        }

        internal HisDepartmentTranUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        private void ProcessHisTreatment(HIS_DEPARTMENT_TRAN data, ref HIS_TREATMENT treatment)
        {
            treatment = new HisTreatmentGet().GetById(data.TREATMENT_ID);
            //Neu sua lai thoi gian vao khoa dau tien thi thuc hien cap nhat lai du lieu in_time cua treatment
            HIS_DEPARTMENT_TRAN departmentTran = new HisDepartmentTranGet().GetFirstByTreatmentId(treatment.ID);
            if (treatment.IN_TIME != departmentTran.DEPARTMENT_IN_TIME)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                treatment.IN_TIME = departmentTran.DEPARTMENT_IN_TIME.Value;
                if (!this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        internal bool Update(HIS_DEPARTMENT_TRAN data)
        {
            HIS_DEPARTMENT_TRAN raw = new HisDepartmentTranGet().GetById(data.ID);
            return this.Update(data, raw);
        }

        internal bool Update(HIS_DEPARTMENT_TRAN data, HIS_DEPARTMENT_TRAN beforeUpdate)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisDepartmentTranCheck checker = new HisDepartmentTranCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckInTime(data);
                valid = valid && checker.CheckPatientTypeLogTime(data);
                valid = valid && checker.CheckCoTreatmentFinishTime(data);
                valid = valid && checker.IsUnLock(beforeUpdate);
                valid = valid && checker.IsValidWithReqDepartment(data.DEPARTMENT_ID, data.TREATMENT_ID);
                if (valid)
                {
                    this.beforeUpdateHisDepartmentTrans.Add(beforeUpdate);
                    if (!DAOWorker.HisDepartmentTranDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepartmentTran_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDepartmentTran that bai." + LogUtil.TraceData("data", data));
                    }
                    this.ProcessHisTreatment(data, ref treatment);
                    this.recentHisDepartmentTran = data;
                    this.ProcessRecalcSereServ(treatment, this.recentHisDepartmentTran, beforeUpdate);
                    result = true;
                    HisDepartmentTranLog.LogUpdate(data, beforeUpdate, treatment);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessRecalcSereServ(HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN departmentTran, HIS_DEPARTMENT_TRAN beforeUpdate)
        {
            //Neu co su update thoi gian vao khoa thi xu ly nghiep vu ve chinh sach cong kham cap cuu
            if (treatment != null && departmentTran.PREVIOUS_ID.HasValue && HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_DEPARTMENT && departmentTran.DEPARTMENT_IN_TIME.HasValue
                && departmentTran.DEPARTMENT_IN_TIME != beforeUpdate.DEPARTMENT_IN_TIME)
            {
                //Kiem tra xem khoa truoc do co phai a khoa cap cuu ko
                HIS_DEPARTMENT_TRAN previous = new HisDepartmentTranGet().GetById(departmentTran.PREVIOUS_ID.Value);
                HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == previous.DEPARTMENT_ID).FirstOrDefault();
                if (previousDepartment != null && previousDepartment.IS_EMERGENCY == Constant.IS_TRUE)
                {
                    List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);

                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existsSereServs);

                    //Luu y can set thoi gian vao truoc khi goi ham update hisSereServUpdateHein.UpdateDb
                    //vi o nghiep vu tinh toan lai sere_serv co su dung out_time
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false, previous.DEPARTMENT_ID, previous.DEPARTMENT_IN_TIME, departmentTran.DEPARTMENT_IN_TIME);
                    this.hisSereServUpdateHein.UpdateDb(olds, existsSereServs);
                }
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisDepartmentTrans))
            {
                if (!DAOWorker.HisDepartmentTranDAO.UpdateList(this.beforeUpdateHisDepartmentTrans))
                {
                    LogSystem.Warn("Rollback du lieu HisDepartmentTran that bai" + LogUtil.TraceData("HisDepartmentTran", this.beforeUpdateHisDepartmentTrans));
                }
            }
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
