using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Util;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepartmentTran
{
    class HisDepartmentTranTruncate : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisDepartmentTranTruncate()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisDepartmentTranTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepartmentTranCheck checker = new HisDepartmentTranCheck(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_DEPARTMENT_TRAN raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(raw.ID);
                if (valid)
                {
                    //Khong cho xoa het toan bo du lieu chuyen khoa
                    List<HIS_DEPARTMENT_TRAN> dts = new HisDepartmentTranGet().GetByTreatmentId(raw.TREATMENT_ID);
                    if (dts == null || dts.Count < 2)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepXoaToanBoDuLieuVaoKhoaCuaHoSoDieuTri);
                        return false;
                    }

                    //Can xoa du lieu vao khoa sau moi xoa duoc du lieu vao khoa truoc
                    HIS_DEPARTMENT_TRAN next = dts != null ? dts.Where(o => o.PREVIOUS_ID.HasValue && o.PREVIOUS_ID.Value == id).FirstOrDefault() : null;
                    if (next != null)
                    {
                        HIS_DEPARTMENT dep = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == next.DEPARTMENT_ID).FirstOrDefault() : null;
                        string deparmentName = dep != null ? dep.DEPARTMENT_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_CanXoaDuLieuVaoKhoaTruoc, deparmentName);
                        return false;
                    }

                    //Neu khoa da co chi dinh thi khong cho phep xoa
                    if (raw.DEPARTMENT_IN_TIME.HasValue)
                    {
                        if (!dts.Exists(e => e.DEPARTMENT_ID == raw.DEPARTMENT_ID && e.ID != raw.ID && e.DEPARTMENT_IN_TIME.HasValue))
                        {
                            HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                            serviceReqFilter.REQUEST_DEPARTMENT_ID = raw.DEPARTMENT_ID;
                            serviceReqFilter.TREATMENT_ID = raw.TREATMENT_ID;
                            serviceReqFilter.HAS_EXECUTE = true;
                            List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);
                            if (IsNotNullOrEmpty(serviceReqs))
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepXoaHoSoDaCoDichVuDoKhoaChiDinh);
                                return false;
                            }
                        }
                        else
                        {
                            HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                            serviceReqFilter.REQUEST_DEPARTMENT_ID = raw.DEPARTMENT_ID;
                            serviceReqFilter.TREATMENT_ID = raw.TREATMENT_ID;
                            serviceReqFilter.INTRUCTION_TIME_FROM = raw.DEPARTMENT_IN_TIME;
                            List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(serviceReqFilter);
                            if (IsNotNullOrEmpty(serviceReqs))
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepXoaHoSoDaCoDichVuDoKhoaChiDinh);
                                return false;
                            }
                        }
                    }

                    result = DAOWorker.HisDepartmentTranDAO.Truncate(raw);
                    if (result)
                    {
                        this.ProcessRecalcExamSereServ(raw);

                        HIS_TREATMENT treatment = null;
                        this.ProcessTreatment(raw, dts, ref treatment);
                        HisDepartmentTranLog.LogDelete(treatment, raw);
                    }
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

        private void ProcessTreatment(HIS_DEPARTMENT_TRAN raw, List<HIS_DEPARTMENT_TRAN> dts, ref HIS_TREATMENT treatment)
        {
            try
            {
                treatment = new HisTreatmentGet().GetById(raw.TREATMENT_ID);
                if (treatment != null)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                    //Neu xoa ban ghi nhap vien thi cap nhat lai thong tin "khoa nhap vien" trong ho so dieu tri
                    if (raw.IS_HOSPITALIZED == Constant.IS_TRUE)
                    {
                        treatment.HOSPITALIZE_DEPARTMENT_ID = null;
                    }
                    HisTreatmentUtil.SetDepartmentInfo(treatment, dts.Where(o => o.ID != raw.ID).ToList());

                    if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                        && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                    {
                        LogSystem.Warn("Cap nhat thong tin DEPARTMENT_IDS cho bang Treatment that bai. Rollback du lieu. TreatmentId: " + treatment.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessRecalcExamSereServ(HIS_DEPARTMENT_TRAN departmentTran)
        {
            if (departmentTran.PREVIOUS_ID.HasValue && HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_DEPARTMENT && departmentTran.DEPARTMENT_IN_TIME.HasValue)
            {
                //Kiem tra xem khoa truoc do co phai a khoa cap cuu ko
                HIS_DEPARTMENT_TRAN previous = new HisDepartmentTranGet().GetById(departmentTran.PREVIOUS_ID.Value);
                HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == previous.DEPARTMENT_ID).FirstOrDefault();
                if (previousDepartment != null && previousDepartment.IS_EMERGENCY == Constant.IS_TRUE)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(departmentTran.TREATMENT_ID);
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                    List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existsSereServs);

                    if (IsNotNullOrEmpty(ptas) && IsNotNullOrEmpty(existsSereServs))
                    {
                        List<HIS_SERE_SERV> exams = existsSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList();

                        //Chi check cac dich vu kham
                        if (IsNotNullOrEmpty(exams))
                        {
                            HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                            foreach (HIS_SERE_SERV s in exams)
                            {
                                priceAdder.AddPrice(s, existsSereServs, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID);
                            }

                            this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);
                            if (!this.hisSereServUpdateHein.UpdateDb(olds, existsSereServs))
                            {
                                throw new Exception("Cap nhat du lieu sere_serv that bai. Rollback du lieu");
                            }
                        }
                    }
                }
            }
        }
    }
}
