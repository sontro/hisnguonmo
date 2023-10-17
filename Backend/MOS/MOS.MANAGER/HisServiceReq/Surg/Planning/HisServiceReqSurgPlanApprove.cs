using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPtttCalendar;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Planning
{
    /// <summary>
    /// Duyet lap ke hoach cho phau thuat
    /// </summary>
    class HisServiceReqSurgPlanApprove : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        internal HisServiceReqSurgPlanApprove()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal HisServiceReqSurgPlanApprove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Approve(HisServiceReqPlanApproveSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HisServiceReqSurgPlanCheck planChecker = new HisServiceReqSurgPlanCheck(param);
                HisPtttCalendarCheck calendarChecker = new HisPtttCalendarCheck(param);

                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && planChecker.IsValidData(data);
                valid = valid && serviceReqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && serviceReqCheck.HasExecute(serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && planChecker.IsValidData(data);
                valid = valid && planChecker.IsValidType(serviceReq);
                valid = valid && planChecker.IsNotApproved(serviceReq);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__APPROVED;
                    serviceReq.PTTT_APPROVAL_TIME = data.Time;
                    serviceReq.PTTT_APPROVAL_LOGINNAME = data.Loginname;
                    serviceReq.PTTT_APPROVAL_USERNAME = data.Username;
                    if (this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        result = true;
                        resultData = serviceReq;
                    }
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

        internal bool Reject(HisServiceReqPlanApproveSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HisServiceReqSurgPlanCheck planChecker = new HisServiceReqSurgPlanCheck(param);
                HisPtttCalendarCheck calendarChecker = new HisPtttCalendarCheck(param);

                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && planChecker.IsValidData(data);
                valid = valid && serviceReqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && serviceReqCheck.IsStatusNotExecute(serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && planChecker.IsValidType(serviceReq);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__REJECTED;
                    serviceReq.PTTT_APPROVAL_TIME = data.Time;
                    serviceReq.PTTT_APPROVAL_LOGINNAME = data.Loginname;
                    serviceReq.PTTT_APPROVAL_USERNAME = data.Username;
                    serviceReq.IS_NO_EXECUTE = Constant.IS_TRUE;
                    if (!this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
                    }

                    //Luu y: nghiep vu nay can xu ly sau, de tranh rollback (do xu ly sere_serv ko rollback)
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                    if (IsNotNullOrEmpty(sereServs))
                    {
                        //tu choi duyet thi cap nhat dich vu thanh ko thuc hien
                        sereServs.ForEach(o => o.IS_NO_EXECUTE = Constant.IS_TRUE);
                        this.ProcessNoExecuteSereServ(serviceReq.TREATMENT_ID, sereServs);
                    }
                    
                    result = true;
                    resultData = serviceReq;
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

        internal bool Unreject(HisServiceReqPlanApproveSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HisServiceReqSurgPlanCheck planChecker = new HisServiceReqSurgPlanCheck(param);
                HisPtttCalendarCheck calendarChecker = new HisPtttCalendarCheck(param);

                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && planChecker.IsValidData(data);
                valid = valid && serviceReqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && planChecker.IsValidType(serviceReq);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW;
                    serviceReq.PTTT_APPROVAL_TIME = null;
                    serviceReq.PTTT_APPROVAL_LOGINNAME = null;
                    serviceReq.PTTT_APPROVAL_USERNAME = null;
                    serviceReq.IS_NO_EXECUTE = null;
                    if (!this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
                    }

                    //Luu y: nghiep vu nay can xu ly sau, de tranh rollback (do xu ly sere_serv ko rollback)
                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
                    if (IsNotNullOrEmpty(sereServs))
                    {
                        //tu choi duyet thi cap nhat dich vu thanh ko thuc hien
                        sereServs.ForEach(o => o.IS_NO_EXECUTE = null);
                        this.ProcessNoExecuteSereServ(serviceReq.TREATMENT_ID, sereServs);
                    }

                    result = true;
                    resultData = serviceReq;
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

        internal bool Unapprove(HisServiceReqPlanApproveSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck serviceReqCheck = new HisServiceReqCheck(param);
                HisServiceReqSurgPlanCheck planChecker = new HisServiceReqSurgPlanCheck(param);
                HisPtttCalendarCheck calendarChecker = new HisPtttCalendarCheck(param);

                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && serviceReqCheck.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtDepartment(serviceReq.EXECUTE_DEPARTMENT_ID, workPlace.DepartmentId);
                valid = valid && planChecker.IsValidType(serviceReq);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.PTTT_APPROVAL_STT_ID = IMSys.DbConfig.HIS_RS.HIS_PTTT_APPROVAL_STT.ID__NEW;
                    serviceReq.PTTT_APPROVAL_TIME = null;
                    serviceReq.PTTT_APPROVAL_LOGINNAME = null;
                    serviceReq.PTTT_APPROVAL_USERNAME = null;
                    if (this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        result = true;
                        resultData = serviceReq;
                    }
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

        private void ProcessNoExecuteSereServ(long treatmentId, List<HIS_SERE_SERV> sereServs)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                HisSereServPayslipSDO sdo = new HisSereServPayslipSDO();
                sdo.SereServs = sereServs;
                sdo.TreatmentId = treatmentId;
                sdo.Field = UpdateField.IS_NO_EXECUTE;
                List<HIS_SERE_SERV> resultData = null;

                if (!this.hisSereServUpdatePayslipInfo.Run(sdo, ref resultData))
                {
                    throw new Exception("Tu dong thiet lap 'ko thuc hien dich vu (is_no_execute) that bai. Rollback du lieu");
                }
            }
        }

        private void RollbackData()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
