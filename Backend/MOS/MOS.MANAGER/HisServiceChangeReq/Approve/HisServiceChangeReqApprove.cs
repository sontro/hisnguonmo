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
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceChangeReq.Approve
{
    /// <summary>
    /// Xu ly nghiep vu tao yeu cau doi dich vu
    /// </summary>
    class HisServiceChangeReqApprove : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisServiceChangeReqUpdate hisServiceChangeReqUpdate;

        internal HisServiceChangeReqApprove()
            : base()
        {
            this.Init();
        }

        internal HisServiceChangeReqApprove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisServiceChangeReqUpdate = new HisServiceChangeReqUpdate(param);
        }

        internal bool Run(HisServiceChangeReqApproveSDO data, ref HIS_SERVICE_CHANGE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV oldSereServ = null;
                HIS_SERVICE_CHANGE_REQ serviceChangeReq = null;
                HIS_TREATMENT hisTreatment = null;
                WorkPlaceSDO workPlaceSdo = null;

                HisServiceChangeReqApproveCheck checker = new HisServiceChangeReqApproveCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.IsValidData(data, ref serviceChangeReq, ref serviceReq, ref oldSereServ);
                valid = valid && sereServChecker.HasExecute(oldSereServ);
                valid = valid && sereServChecker.HasNoBill(oldSereServ);
                valid = valid && sereServChecker.HasNoInvoice(oldSereServ);
                valid = valid && serviceReqChecker.IsNotStarted(serviceReq);
                valid = valid && serviceReqChecker.HasExecute(serviceReq);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlaceSdo);
                valid = valid && checker.IsValidWorkingRoom(serviceReq, workPlaceSdo);
                valid = valid && treatmentChecker.IsUnLock(serviceReq.TREATMENT_ID, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                    
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    
                    HIS_SERE_SERV beforeUpdate = Mapper.Map<HIS_SERE_SERV>(oldSereServ); //clone phuc vu rollback
                    HIS_SERE_SERV newSereServ = Mapper.Map<HIS_SERE_SERV>(oldSereServ);

                    //Cap nhat du lieu cu thanh "khong thuc hien"
                    oldSereServ.IS_NO_EXECUTE = MOS.UTILITY.Constant.IS_TRUE;

                    //Tao du lieu moi voi service_id tuong ung voi service_id moi
                    newSereServ.SERVICE_ID = serviceChangeReq.ALTER_SERVICE_ID;
                    newSereServ.IS_EXPEND = oldSereServ.IS_EXPEND;
                    newSereServ.IS_OUT_PARENT_FEE = oldSereServ.IS_OUT_PARENT_FEE;
                    newSereServ.PATIENT_TYPE_ID = serviceChangeReq.PATIENT_TYPE_ID;
                    newSereServ.AMOUNT = oldSereServ.AMOUNT;
                    newSereServ.PACKAGE_ID = oldSereServ.PACKAGE_ID;

                    HisSereServPackageUtil.AutoAssign(newSereServ);

                    //SereServUpdateHein ko can check treatment vi da check o ham validate o phia tren
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, hisTreatment, false);

                    //Cap nhat thong tin gia theo dich vu moi
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID).FirstOrDefault() : null;
                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, hisTreatment, null, null);
                    if (!priceAdder.AddPrice(newSereServ, serviceReq.INTRUCTION_TIME, department.BRANCH_ID, oldSereServ.TDL_REQUEST_ROOM_ID, oldSereServ.TDL_REQUEST_DEPARTMENT_ID, oldSereServ.TDL_EXECUTE_ROOM_ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    bool updateOld = false;

                    //Kiem tra xem dich vu da duoc tam thu chua
                    List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new HisSereServDepositGet().GetNoCancelBySereServId(oldSereServ.ID);
                    if (IsNotNullOrEmpty(sereServDeposits))
                    {
                        List<long> ids = sereServDeposits.Select(o => o.ID).ToList();
                        List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetBySereServDepositIds(ids);

                        List<HIS_SERE_SERV_DEPOSIT> noRepay = sereServDeposits.Where(o => repays == null || !repays.Exists(t => t.SERE_SERV_DEPOSIT_ID == o.ID)).ToList();

                        //Neu co giao dich tam ung va cau hinh cho phep tick "khong thuc hien"
                        if (IsNotNullOrEmpty(noRepay) && HisSereServCFG.ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION == HisSereServCFG.AllowNoExecuteForPaidServiceOption.ALLOW_FOR_DIPOSIT)
                        {
                            updateOld = true;
                        }
                    }
                    else
                    {
                        updateOld = true;
                    }

                    if (updateOld && !this.hisSereServUpdate.Update(oldSereServ, beforeUpdate))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                        
                    if (!this.hisSereServCreate.Create(newSereServ, serviceReq, false))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    //Cap nhat ti le BHYT cho sere_serv
                    if (!this.hisSereServUpdateHein.UpdateDb())
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    serviceChangeReq.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceChangeReq.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    serviceChangeReq.ALTER_SERE_SERV_ID = newSereServ.ID;

                    if (!this.hisServiceChangeReqUpdate.Update(serviceChangeReq))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    result = true;
                    resultData = serviceChangeReq;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisSereServ_DoiDichVu, oldSereServ.TDL_SERVICE_CODE, oldSereServ.TDL_SERVICE_NAME, newSereServ.TDL_SERVICE_CODE, newSereServ.TDL_SERVICE_NAME).TreatmentCode(hisTreatment.TREATMENT_CODE).ServiceReqCode(newSereServ.TDL_SERVICE_REQ_CODE).Run();
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
    }
}
