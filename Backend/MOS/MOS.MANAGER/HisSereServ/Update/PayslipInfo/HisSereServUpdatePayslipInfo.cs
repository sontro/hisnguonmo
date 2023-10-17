using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ.Update.PayslipInfo
{
    class UpdateData
    {
        public string FieldName { get; set; }
        public string Value { get; set; }

        public UpdateData(string fieldName, string value)
        {
            this.FieldName = fieldName;
            this.Value = value;
        }
    }

    /// <summary>
    /// Update cac truong lien quan den bang ke thanh toan
    /// - Khong thuc hien
    /// - Doi tuong thanh toan
    /// - Hao phi
    /// - Chi phi ngoai goi
    /// - Dinh kem dich vu
    /// </summary>
    partial class HisSereServUpdatePayslipInfo : BusinessBase
    {
        private List<HIS_SERE_SERV> beforeUpdateHisSereServs = new List<HIS_SERE_SERV>();

        private HisSereServUpdateSql hisSereServUpdateSql;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisSereServUpdatePayslipInfo()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdatePayslipInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
        }

        internal bool Run(HisSereServPayslipSDO data, ref List<HIS_SERE_SERV> resultData)
        {
            HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TreatmentId);
            return this.Run(data, treatment, false, ref resultData);
        }

        internal bool Run(HisSereServPayslipSDO data, HIS_TREATMENT treatment, bool isAllowUpdateNoExecutePres, ref List<HIS_SERE_SERV> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> allExists = null;
                List<HIS_SERE_SERV> affectList = null;
                List<HIS_SERVICE_REQ> affectServiceReqs = null;

                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                HisSereServCheck checker = new HisSereServCheck(param);
                HisSereServUpdatePayslipInfoCheck payslipInfoChecker = new HisSereServUpdatePayslipInfoCheck(param);

                bool valid = payslipInfoChecker.IsValidData(data, isAllowUpdateNoExecutePres, ref allExists, ref affectList, ref affectServiceReqs);
                valid = valid && payslipInfoChecker.IsAllowed(affectList);
                valid = valid && payslipInfoChecker.ValidateInCaseOfPackageService(data.Field, data.SereServs, affectList);
                if (HisSereServCFG.IS_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED)
                {
                    valid = valid && treatmentCheck.IsUnpause(treatment);
                }
                valid = valid && treatmentCheck.IsUnLock(treatment);
                valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                valid = valid && treatmentCheck.IsUnLockHein(treatment);
                valid = valid && checker.HasNoBill(affectList);
                valid = valid && checker.HasNoInvoice(affectList);
                valid = valid && checker.HasNoDebt(affectList);
                valid = valid && payslipInfoChecker.ValidateInCaseOfUpdateExecute(checker, data.Field, data.SereServs, affectList);
                valid = valid && payslipInfoChecker.ValidateNoExpendAllowed(data, allExists);
                valid = valid && payslipInfoChecker.IsValidBHYTServices(data, allExists, affectServiceReqs, false);
                valid = valid && payslipInfoChecker.IsValidServiceConditions(data, affectList, allExists);
                valid = valid && payslipInfoChecker.IsValidDoNotUseBHYT(data.SereServs);
                if (HisSereServCFG.DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH)
                {
                    valid = valid && payslipInfoChecker.ValidateWhenExpMestFinished(data, affectList, affectServiceReqs);
                }
                if (valid)
                {
                    result = this.RunWithoutChecking(allExists, affectServiceReqs, data, treatment, affectList, ref resultData);
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool RunWithoutChecking(List<HIS_SERE_SERV> allExists, List<HIS_SERVICE_REQ> affectSReqs, HisSereServPayslipSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> affectList, ref List<HIS_SERE_SERV> resultData)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                List<HIS_SERE_SERV> befores = Mapper.Map<List<HIS_SERE_SERV>>(allExists);
                List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);

                this.ChangeData(allExists, data, treatment, patientTypeAlters, affectSReqs);

                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, patientTypeAlters, false);

                List<HIS_SERE_SERV> changes = null;
                List<HIS_SERE_SERV> oldOfChanges = null;

                if (this.hisSereServUpdateHein.Update(befores, allExists, ref changes, ref oldOfChanges))
                {

                    //HisSereServUtil.GetChangeRecord(befores, allExists, ref changes, ref oldOfChanges);

                    if (IsNotNullOrEmpty(changes))
                    {
                        HisSereServUpdatePayslipInfoCheck payslipInfoChecker = new HisSereServUpdatePayslipInfoCheck(param);
                        if (!payslipInfoChecker.ValidInCaseOfUpdateExpend(data.Field, changes, treatment))
                        {
                            return false;
                        }

                        this.beforeUpdateHisSereServs.AddRange(oldOfChanges);//phuc vu rollback

                        if (this.hisSereServUpdateSql.Run(changes))
                        {
                            var dataChanges = new HisSereServGet().GetByIds(changes.Select(o=>o.ID).ToList());
                            if (IsRequiredToCreateHisTransReq(dataChanges, oldOfChanges))
                            {
                                WorkPlaceSDO workPlace = new WorkPlaceSDO();
                                var ss = dataChanges.FirstOrDefault(o => o.TDL_IS_MAIN_EXAM == Constant.IS_TRUE);
                                if (ss != null)
                                {
                                    workPlace.RoomId = ss.TDL_EXECUTE_ROOM_ID;
                                    workPlace.BranchId = ss.TDL_EXECUTE_BRANCH_ID;
                                    workPlace.DepartmentId = ss.TDL_EXECUTE_DEPARTMENT_ID;
                                }
                                else
                                {
                                    var sereServ = dataChanges.OrderByDescending(o => o.TDL_INTRUCTION_TIME).FirstOrDefault();
                                    if (sereServ != null)
                                    {
                                        workPlace.RoomId = sereServ.TDL_EXECUTE_ROOM_ID;
                                        workPlace.BranchId = sereServ.TDL_EXECUTE_BRANCH_ID;
                                        workPlace.DepartmentId = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                                    }
                                }
                                if (!new HisTransReqCreateByService(param).Run(treatment, affectSReqs, workPlace))
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                                }
                            }
                            List<string> sqls = new List<string>();
                            List<long> changeHeinCardNumberOrPatientTypeIdServiceReqIds = new List<long>();

                            //Thuc hien xu ly exp_mest cuoi cung de ko phai rollback (xu ly exp_mest su dung sql)
                            this.UpdateServiceReq(data.TreatmentId, changes, oldOfChanges, ref changeHeinCardNumberOrPatientTypeIdServiceReqIds, ref sqls);

                            //Thuc hien xu ly exp_mest cuoi cung de ko phai rollback (xu ly exp_mest su dung sql)
                            this.UpdateExpMest(data, oldOfChanges, changeHeinCardNumberOrPatientTypeIdServiceReqIds, ref sqls);

                            if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                            {
                                throw new Exception("Rollback du lieu");
                            }
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }

                    if (result)
                    {
                        resultData = new HisSereServGet().GetByTreatmentId(treatment.ID);
                        HisSereServUpdatePayslipInfoLog.Log(data, befores, treatment);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool IsRequiredToCreateHisTransReq(List<HIS_SERE_SERV> changes, List<HIS_SERE_SERV> oldOfChanges)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(changes)) return false;
                foreach (var item in changes)
                {
                    HIS_SERE_SERV oldItem = oldOfChanges.FirstOrDefault(o => o.ID == item.ID);
                    if (oldItem == null) continue;
                    if (oldItem.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        if (item.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT) return true;
                    }
                    else if (item.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || item.VIR_TOTAL_PATIENT_PRICE != oldItem.VIR_TOTAL_PATIENT_PRICE)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        private void UpdateServiceReq(long treatmentId, List<HIS_SERE_SERV> changes, List<HIS_SERE_SERV> olds, ref List<long> changeHeinCardNumberOrPatientTypeIdServiceReqIds, ref List<string> sqls)
        {
            List<long> toNoExecuteServiceReqIds = new List<long>();
            List<long> toExecuteServiceReqIds = new List<long>();
            changeHeinCardNumberOrPatientTypeIdServiceReqIds = new List<long>();
            List<long> lisServiceReqIds = new List<long>();
            bool hasNoExecuteExam = false;
            bool hasExecuteExam = false;

            foreach (HIS_SERE_SERV t in changes)
            {
                HIS_SERE_SERV old = olds.Where(o => o.ID == t.ID).FirstOrDefault();

                //lay ra danh sach update thanh "ko o thuc hien"
                if (old != null && t.IS_NO_EXECUTE == Constant.IS_TRUE && old.IS_NO_EXECUTE != Constant.IS_TRUE && old.SERVICE_REQ_ID.HasValue)
                {
                    toNoExecuteServiceReqIds.Add(old.SERVICE_REQ_ID.Value);
                    if (old.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                    {
                        hasNoExecuteExam = true;
                    }
                    else if (old.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                        || HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(old.TDL_SERVICE_REQ_TYPE_ID))
                    {
                        lisServiceReqIds.Add(old.SERVICE_REQ_ID.Value);
                    }
                }
                //lay ra danh sach update thanh "co o thuc hien"
                else if (old != null && t.IS_NO_EXECUTE != Constant.IS_TRUE && old.IS_NO_EXECUTE == Constant.IS_TRUE && old.SERVICE_REQ_ID.HasValue)
                {
                    toExecuteServiceReqIds.Add(old.SERVICE_REQ_ID.Value);
                    if (old.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                    {
                        hasExecuteExam = true;
                    }
                    else if (old.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                        || HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(old.TDL_SERVICE_REQ_TYPE_ID))
                    {
                        lisServiceReqIds.Add(old.SERVICE_REQ_ID.Value);
                    }
                }
                else if (old != null && old.SERVICE_REQ_ID.HasValue && (old.HEIN_CARD_NUMBER != t.HEIN_CARD_NUMBER || old.PATIENT_TYPE_ID != t.PATIENT_TYPE_ID))
                {
                    changeHeinCardNumberOrPatientTypeIdServiceReqIds.Add(old.SERVICE_REQ_ID.Value);
                }
            }

            //lay cac service_req_id cua cac sere_serv cap nhat "thuc hien"
            //va loai cac service_req_id cua cac sere_serv co cap nhat "khong thuc hien"
            //vi: 1 sere_serv la "thuc hien" thi service_req tuong ung luon la "thuc hien",
            //nhung 1 sere_serv la "khong thuc hien" thi chua xac dinh duoc service_req co thuc hien hay ko 
            //(can kiem tra them cac sere_serv khac trong cung service_req)
            if (IsNotNullOrEmpty(toNoExecuteServiceReqIds) && IsNotNullOrEmpty(toExecuteServiceReqIds))
            {
                toNoExecuteServiceReqIds.RemoveAll(t => toExecuteServiceReqIds.Contains(t));
            }

            if (IsNotNullOrEmpty(toExecuteServiceReqIds))
            {
                sqls.AddRange(HisServiceReqUpdateNoExecuteUtil.GenSql(toExecuteServiceReqIds, ServiceReqUpdateExecuteOption.EXECUTE, hasExecuteExam, treatmentId));
            }
            if (IsNotNullOrEmpty(toNoExecuteServiceReqIds))
            {
                sqls.AddRange(HisServiceReqUpdateNoExecuteUtil.GenSql(toNoExecuteServiceReqIds, ServiceReqUpdateExecuteOption.CHECK_SERE_SERV, hasNoExecuteExam, treatmentId));
            }

            //Neu co cap nhat ko thuc hien service_req XN/PACS thi can update lai LIS_STT_ID de danh dau phuc vu tien trinh gui lai sang he thong LIS/PACS
            if (IsNotNullOrEmpty(lisServiceReqIds))
            {
                lisServiceReqIds = lisServiceReqIds.Distinct().ToList();

                sqls.Add(DAOWorker.SqlDAO.AddInClause(lisServiceReqIds, String.Format("UPDATE HIS_SERVICE_REQ SET LIS_STT_ID = {0}, IS_UPDATED_EXT = 1 WHERE %IN_CLAUSE% AND IS_SENT_EXT = 1", LisUtil.LIS_STT_ID__UPDATE), "ID"));
            }
            //Cap nhat thong tin TDL_HEIN_CARD_NUMBER trong his_service_req khi co thay doi thong tin sere_serv
            if (IsNotNullOrEmpty(changeHeinCardNumberOrPatientTypeIdServiceReqIds))
            {
                changeHeinCardNumberOrPatientTypeIdServiceReqIds = changeHeinCardNumberOrPatientTypeIdServiceReqIds.Distinct().ToList();

                sqls.Add(DAOWorker.SqlDAO.AddInClause(changeHeinCardNumberOrPatientTypeIdServiceReqIds, "UPDATE HIS_SERVICE_REQ REQ SET REQ.TDL_HEIN_CARD_NUMBER = (SELECT HEIN_CARD_NUMBER FROM HIS_SERE_SERV S WHERE S.SERVICE_REQ_ID = REQ.ID ORDER BY S.HEIN_CARD_NUMBER, S.ID FETCH FIRST ROWS ONLY), REQ.TDL_PATIENT_TYPE_ID = (SELECT PATIENT_TYPE_ID FROM HIS_SERE_SERV S WHERE S.SERVICE_REQ_ID = REQ.ID ORDER BY S.HEIN_CARD_NUMBER, S.ID FETCH FIRST ROWS ONLY) WHERE %IN_CLAUSE% ", "ID"));
            }
        }

        private void ChangeData(List<HIS_SERE_SERV> allSereServs, HisSereServPayslipSDO data, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERVICE_REQ> affectSReqs)
        {
            HisSereServSetInfo addInfo = new HisSereServSetInfo(param, ptas);
            foreach (HIS_SERE_SERV newSs in data.SereServs)
            {
                HIS_SERE_SERV t = allSereServs.Where(o => o.ID == newSs.ID).FirstOrDefault();

                //Cac truong luon update theo du lieu client truyen len
                t.PRIMARY_PATIENT_TYPE_ID = newSs.PRIMARY_PATIENT_TYPE_ID;
                t.SERVICE_CONDITION_ID = newSs.SERVICE_CONDITION_ID;

                //Cac truong chi update theo client truyen len neu data.Field tuong ung voi truong do 
                t.PARENT_ID = data.Field == UpdateField.PARENT_ID ? newSs.PARENT_ID : t.PARENT_ID;
                t.STENT_ORDER = data.Field == UpdateField.STENT_ORDER ? newSs.STENT_ORDER : t.STENT_ORDER;
                t.PATIENT_TYPE_ID = data.Field == UpdateField.PATIENT_TYPE_ID ? newSs.PATIENT_TYPE_ID : t.PATIENT_TYPE_ID;
                t.EXPEND_TYPE_ID = data.Field == UpdateField.EXPEND_TYPE_ID ? newSs.EXPEND_TYPE_ID : t.EXPEND_TYPE_ID;
                t.OTHER_PAY_SOURCE_ID = data.Field == UpdateField.OTHER_PAY_SOURCE_ID ? newSs.OTHER_PAY_SOURCE_ID : t.OTHER_PAY_SOURCE_ID;
                t.SHARE_COUNT = data.Field == UpdateField.SHARE_COUNT ? newSs.SHARE_COUNT : t.SHARE_COUNT;
                t.USER_PRICE = data.Field == UpdateField.USER_PRICE ? newSs.USER_PRICE : t.USER_PRICE;
                t.PACKAGE_PRICE = data.Field == UpdateField.PACKAGE_PRICE ? newSs.PACKAGE_PRICE : t.PACKAGE_PRICE;
                if (newSs.PACKAGE_PRICE.HasValue && data.Field == UpdateField.PACKAGE_PRICE)
                {
                    t.IS_USER_PACKAGE_PRICE = Constant.IS_TRUE;
                }
                t.EQUIPMENT_SET_ID = data.Field == UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID ? newSs.EQUIPMENT_SET_ID : t.EQUIPMENT_SET_ID;
                t.EQUIPMENT_SET_ORDER = data.Field == UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID ? newSs.EQUIPMENT_SET_ORDER : t.EQUIPMENT_SET_ORDER;
                t.IS_NOT_USE_BHYT = data.Field == UpdateField.IS_NOT_USE_BHYT ? newSs.IS_NOT_USE_BHYT : t.IS_NOT_USE_BHYT;
                //Luu y: cac truong nay (IS_EXPEND, IS_NO_EXECUTE, IS_OUT_PARENT_FEE, IS_FUND_ACCEPTED) neu khac 1 thi luu thanh "null",
                //vi lien quan den cac truong virtual trong DB dang so sanh voi null chu ko so sanh voi khac 1
                if (data.Field == UpdateField.IS_EXPEND)
                {
                    t.IS_EXPEND = newSs.IS_EXPEND != Constant.IS_TRUE ? null : newSs.IS_EXPEND;
                }
                if (data.Field == UpdateField.IS_NO_EXECUTE)
                {
                    bool canUpdate = false;
                    var req = affectSReqs.FirstOrDefault(o => o.ID == t.SERVICE_REQ_ID);
                    //HIS_SERE_SERV_EXT sereServExt = new HisSereServExtGet().GetBySereServId(newSs.ID);
                    if (IsNotNull(req) && HisSereServCFG.MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE)
                    {
                        if (t.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN && req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                        {
                            List<HIS_SERE_SERV_TEIN> teins = new HisSereServTeinGet().GetBySereServId(t.ID);
                            if (!IsNotNullOrEmpty(teins))
                            {
                                canUpdate = true;
                            }
                            else
                            {
                                if (teins.All(o => string.IsNullOrWhiteSpace(o.VALUE)))
                                {
                                    canUpdate = true;
                                }
                            }
                        }
                        else
                        {
                            canUpdate = true;
                        }
                    }
                    else if (IsNotNull(req))
                    {
                        if (t.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN)
                        {
                            if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL || (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && t.IS_CONFIRM_NO_EXCUTE == Constant.IS_TRUE))
                            {
                                canUpdate = true;
                            }
                        }
                        else
                        {
                            if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL || (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL && t.IS_CONFIRM_NO_EXCUTE == Constant.IS_TRUE))
                            {
                                canUpdate = true;
                            }
                            else
                            {
                                List<HIS_SERE_SERV_TEIN> teins = new HisSereServTeinGet().GetBySereServId(t.ID);
                                if (!IsNotNullOrEmpty(teins))
                                {
                                    canUpdate = true;
                                }
                                else
                                {
                                    if (teins.All(o => string.IsNullOrWhiteSpace(o.VALUE)))
                                    {
                                        canUpdate = true;
                                    }
                                }
                            }
                        }
                    }
                    if (canUpdate)
                    {
                        t.IS_NO_EXECUTE = newSs.IS_NO_EXECUTE != Constant.IS_TRUE ? null : newSs.IS_NO_EXECUTE;
                        t.IS_SENT_EXT = null;
                    }
                }
                if (data.Field == UpdateField.IS_FUND_ACCEPTED)
                {
                    t.IS_FUND_ACCEPTED = newSs.IS_FUND_ACCEPTED != Constant.IS_TRUE ? null : newSs.IS_FUND_ACCEPTED;
                }

                if (data.Field == UpdateField.IS_OUT_PARENT_FEE)
                {
                    t.IS_OUT_PARENT_FEE = newSs.IS_OUT_PARENT_FEE != Constant.IS_TRUE ? null : newSs.IS_OUT_PARENT_FEE;
                }

                if (!addInfo.AddInfo(t))
                {
                    throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                }
            }

            //Cap nhat lai thong tin gia sau khi co su thay doi thong tin
            //Do co 1 so thong tin cua dich vu se anh huong den chinh sach gia
            //Luu y: can thay doi thong tin cua toan bo cac dich vu roi moi thuc hien
            //cap nhat lai thong tin gia (vi trong 1 so truong hop, gia cua dv nay duoc 
            //quyet dinh boi thong tin cua dich vu khac)
            List<long> effectedSereServIds = allSereServs.Select(o => o.ID).ToList();
            SereServPriceUtil.ReloadPrice(param, treatment, allSereServs, effectedSereServIds);
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.beforeUpdateHisSereServs) && !this.hisSereServUpdateSql.Run(this.beforeUpdateHisSereServs))
                {
                    LogSystem.Warn("Rollback sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        //Cap nhat thong tin exp_mest tuong ung
        private void UpdateExpMest(HisSereServPayslipSDO sdo, List<HIS_SERE_SERV> olds, List<long> changeHeinCardNumberOrPatientTypeIdServiceReqIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(olds) && sdo.Field != UpdateField.IS_NO_EXECUTE) //thuoc/vat tu/mau ko cap nhat truong ko thuc hien
            {
                //Lay ra cac sere_serv co thay doi lien quan den thuoc/vat tu/mau
                List<HIS_SERE_SERV> checkLists = olds.Where(o => o.MEDICINE_ID.HasValue || o.MATERIAL_ID.HasValue || o.BLOOD_ID.HasValue).ToList();

                if (IsNotNullOrEmpty(checkLists))
                {
                    this.UpdateExpMestMedicine(sdo, checkLists, ref sqls);
                    this.UpdateExpMestMaterial(sdo, checkLists, ref sqls);

                    string updateExpMestSql = DAOWorker.SqlDAO.AddInClause(changeHeinCardNumberOrPatientTypeIdServiceReqIds, "UPDATE HIS_EXP_MEST EXP SET EXP.TDL_HEIN_CARD_NUMBER = (SELECT TDL_HEIN_CARD_NUMBER FROM HIS_SERVICE_REQ REQ WHERE EXP.SERVICE_REQ_ID = REQ.ID), EXP.TDL_PATIENT_TYPE_ID = (SELECT TDL_PATIENT_TYPE_ID FROM HIS_SERVICE_REQ REQ WHERE EXP.SERVICE_REQ_ID = REQ.ID) WHERE %IN_CLAUSE% ", "SERVICE_REQ_ID");
                    sqls.Add(updateExpMestSql);
                }
            }
        }

        //Cap nhat thong tin exp_mest_medicine tuong ung
        private void UpdateExpMestMedicine(HisSereServPayslipSDO sdo, List<HIS_SERE_SERV> oldOfchangeSereServs, ref List<string> sqls)
        {
            List<long> medicineIds = oldOfchangeSereServs.Where(o => o.MEDICINE_ID.HasValue).Select(o => o.MEDICINE_ID.Value).ToList();
            if (IsNotNullOrEmpty(medicineIds))
            {
                HisExpMestMedicineFilterQuery filter = new HisExpMestMedicineFilterQuery();
                filter.TDL_SERVICE_REQ_IDs = oldOfchangeSereServs.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList();
                filter.MEDICINE_IDs = medicineIds;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().Get(filter);

                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    Dictionary<UpdateData, List<long>> updateDic = new Dictionary<UpdateData, List<long>>();

                    //<List<long> toUpdateIds = new List<long>();
                    foreach (HIS_SERE_SERV oldSs in oldOfchangeSereServs)
                    {
                        if (!oldSs.MEDICINE_ID.HasValue)
                        {
                            continue;
                        }

                        HIS_SERE_SERV newSs = sdo.SereServs.Where(o => o.ID == oldSs.ID).FirstOrDefault();
                        List<UpdateData> updateData = null;
                        bool isChange = this.GetNewValue(newSs, oldSs, sdo.Field, ref updateData);

                        HIS_EXP_MEST_MEDICINE toUpdate = null;
                        if (oldSs.EXP_MEST_MEDICINE_ID.HasValue)
                        {
                            toUpdate = expMestMedicines.Where(o => o.ID == oldSs.EXP_MEST_MEDICINE_ID.Value).FirstOrDefault();
                        }
                        else
                        {
                            toUpdate = expMestMedicines
                            .Where(o => o.TDL_SERVICE_REQ_ID == oldSs.SERVICE_REQ_ID
                                && o.MEDICINE_ID == oldSs.MEDICINE_ID
                                && o.PATIENT_TYPE_ID == oldSs.PATIENT_TYPE_ID
                                && o.IS_EXPEND == oldSs.IS_EXPEND
                                //&& o.IS_OUT_PARENT_FEE == oldSs.IS_OUT_PARENT_FEE
                                && o.SERE_SERV_PARENT_ID == oldSs.PARENT_ID
                                && (o.AMOUNT - (o.TH_AMOUNT ?? 0)) == oldSs.AMOUNT
                                )
                            .FirstOrDefault();
                        }

                        if (isChange && toUpdate != null)
                        {
                            if (IsNotNullOrEmpty(updateData))
                            {
                                foreach (UpdateData newVal in updateData)
                                {
                                    if (updateDic.ContainsKey(newVal))
                                    {
                                        updateDic[newVal].Add(toUpdate.ID);
                                    }
                                    else
                                    {
                                        updateDic.Add(newVal, new List<long>() { toUpdate.ID });
                                    }
                                }
                            }
                        }
                        else if (isChange && toUpdate == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_EXP_MEST_MEDICINE tuong ung voi HIS_SERE_SERV: " + LogUtil.TraceData("SereServ", oldSs));
                        }
                    }

                    if (IsNotNullOrEmpty(updateDic))
                    {
                        foreach (UpdateData val in updateDic.Keys)
                        {
                            List<long> toUpdateIds = updateDic[val];
                            string sql = DAOWorker.SqlDAO.AddInClause(toUpdateIds, "UPDATE HIS_EXP_MEST_MEDICINE SET {0} = {1} WHERE %IN_CLAUSE%", "ID");
                            sql = string.Format(sql, val.FieldName, val.Value);
                            sqls.Add(sql);
                        }
                    }
                }
            }
        }

        //Cap nhat thong tin exp_mest_material tuong ung
        private void UpdateExpMestMaterial(HisSereServPayslipSDO sdo, List<HIS_SERE_SERV> oldOfchangeSereServs, ref List<string> sqls)
        {
            List<long> materialIds = oldOfchangeSereServs.Where(o => o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList();
            if (IsNotNullOrEmpty(materialIds))
            {
                HisExpMestMaterialFilterQuery filter = new HisExpMestMaterialFilterQuery();
                filter.TDL_SERVICE_REQ_IDs = oldOfchangeSereServs.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList();
                filter.MATERIAL_IDs = materialIds;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new HisExpMestMaterialGet().Get(filter);

                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    Dictionary<UpdateData, List<long>> updateDic = new Dictionary<UpdateData, List<long>>();

                    //<List<long> toUpdateIds = new List<long>();
                    foreach (HIS_SERE_SERV oldSs in oldOfchangeSereServs)
                    {
                        if (!oldSs.MATERIAL_ID.HasValue)
                        {
                            continue;
                        }

                        HIS_SERE_SERV newSs = sdo.SereServs.Where(o => o.ID == oldSs.ID).FirstOrDefault();
                        List<UpdateData> updateData = null;
                        bool isChange = this.GetNewValue(newSs, oldSs, sdo.Field, ref updateData);

                        HIS_EXP_MEST_MATERIAL toUpdate = null;
                        if (oldSs.EXP_MEST_MATERIAL_ID.HasValue)
                        {
                            toUpdate = expMestMaterials.Where(o => o.ID == oldSs.EXP_MEST_MATERIAL_ID.Value).FirstOrDefault();
                        }
                        else
                        {
                            toUpdate = expMestMaterials
                            .Where(o => o.TDL_SERVICE_REQ_ID == oldSs.SERVICE_REQ_ID
                                && o.MATERIAL_ID == oldSs.MATERIAL_ID
                                && o.PATIENT_TYPE_ID == oldSs.PATIENT_TYPE_ID
                                && o.IS_EXPEND == oldSs.IS_EXPEND
                                //&& o.IS_OUT_PARENT_FEE == oldSs.IS_OUT_PARENT_FEE
                                && o.SERE_SERV_PARENT_ID == oldSs.PARENT_ID
                                && o.STENT_ORDER == oldSs.STENT_ORDER
                                && o.EQUIPMENT_SET_ID == oldSs.EQUIPMENT_SET_ID
                                && o.EQUIPMENT_SET_ORDER == oldSs.EQUIPMENT_SET_ORDER
                                && (o.AMOUNT - (o.TH_AMOUNT ?? 0)) == oldSs.AMOUNT)
                            .FirstOrDefault();
                        }

                        if (isChange && toUpdate != null)
                        {
                            if (IsNotNullOrEmpty(updateData))
                            {
                                foreach (UpdateData newVal in updateData)
                                {
                                    if (updateDic.ContainsKey(newVal))
                                    {
                                        updateDic[newVal].Add(toUpdate.ID);
                                    }
                                    else
                                    {
                                        updateDic.Add(newVal, new List<long>() { toUpdate.ID });
                                    }
                                }
                            }
                        }
                        else if (isChange && toUpdate == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong tim thay HIS_EXP_MEST_MATERIAL tuong ung voi HIS_SERE_SERV: " + LogUtil.TraceData("SereServ", oldSs));
                        }
                    }

                    if (IsNotNullOrEmpty(updateDic))
                    {
                        foreach (UpdateData val in updateDic.Keys)
                        {
                            List<long> toUpdateIds = updateDic[val];
                            string sql = DAOWorker.SqlDAO.AddInClause(toUpdateIds, "UPDATE HIS_EXP_MEST_MATERIAL SET {0} = {1} WHERE %IN_CLAUSE%", "ID");
                            sql = string.Format(sql, val.FieldName, val.Value);
                            sqls.Add(sql);
                        }
                    }
                }
            }
        }

        private string FieldName(UpdateField field)
        {
            if (field == UpdateField.PARENT_ID)
            {
                return "SERE_SERV_PARENT_ID";
            }
            else if (field == UpdateField.STENT_ORDER)
            {
                return "STENT_ORDER";
            }
            else if (field == UpdateField.IS_EXPEND)
            {
                return "IS_EXPEND";
            }
            else if (field == UpdateField.IS_OUT_PARENT_FEE)
            {
                return "IS_OUT_PARENT_FEE";
            }
            else if (field == UpdateField.PATIENT_TYPE_ID)
            {
                return "PATIENT_TYPE_ID";
            }
            else if (field == UpdateField.PRIMARY_PATIENT_TYPE_ID)
            {
                return "PRIMARY_PATIENT_TYPE_ID";
            }
            else if (field == UpdateField.SERVICE_CONDITION_ID)
            {
                return "SERVICE_CONDITION_ID";
            }
            else if (field == UpdateField.EXPEND_TYPE_ID)
            {
                return "EXPEND_TYPE_ID";
            }
            else if (field == UpdateField.OTHER_PAY_SOURCE_ID)
            {
                return "OTHER_PAY_SOURCE_ID";
            }
            return null;
        }

        private string Value(object val)
        {
            return val == null ? "NULL" : val.ToString();
        }

        private bool GetNewValue(HIS_SERE_SERV newSereServ, HIS_SERE_SERV oldSereServ, UpdateField field, ref List<UpdateData> updateData)
        {
            if (newSereServ != null && oldSereServ != null)
            {
                if (field == UpdateField.IS_EXPEND && newSereServ.IS_EXPEND != oldSereServ.IS_EXPEND)
                {
                    updateData = new List<UpdateData>() { new UpdateData("IS_EXPEND", this.Value(newSereServ.IS_EXPEND)) };
                    return true;
                }
                else if (field == UpdateField.IS_NO_EXECUTE && newSereServ.IS_NO_EXECUTE != oldSereServ.IS_NO_EXECUTE)
                {
                    updateData = new List<UpdateData>() { new UpdateData("IS_NO_EXECUTE", this.Value(newSereServ.IS_NO_EXECUTE)) };
                    return true;
                }
                else if (field == UpdateField.IS_OUT_PARENT_FEE && newSereServ.IS_OUT_PARENT_FEE != oldSereServ.IS_OUT_PARENT_FEE)
                {
                    updateData = new List<UpdateData>() { new UpdateData("IS_OUT_PARENT_FEE", this.Value(newSereServ.IS_OUT_PARENT_FEE)) };
                    return true;
                }
                else if (field == UpdateField.PARENT_ID && newSereServ.PARENT_ID != oldSereServ.PARENT_ID)
                {
                    updateData = new List<UpdateData>() { new UpdateData("SERE_SERV_PARENT_ID", this.Value(newSereServ.PARENT_ID)) };
                    return true;
                }
                else if (field == UpdateField.PATIENT_TYPE_ID && newSereServ.PATIENT_TYPE_ID != oldSereServ.PATIENT_TYPE_ID)
                {
                    updateData = new List<UpdateData>() { new UpdateData("PATIENT_TYPE_ID", this.Value(newSereServ.PATIENT_TYPE_ID)) };
                    return true;
                }
                else if (field == UpdateField.EXPEND_TYPE_ID && newSereServ.EXPEND_TYPE_ID != oldSereServ.EXPEND_TYPE_ID)
                {
                    updateData = new List<UpdateData>() { new UpdateData("EXPEND_TYPE_ID", this.Value(newSereServ.EXPEND_TYPE_ID)) };
                    return true;
                }
                else if (field == UpdateField.PATIENT_TYPE_ID && newSereServ.PRIMARY_PATIENT_TYPE_ID != oldSereServ.PRIMARY_PATIENT_TYPE_ID)
                {
                    updateData = new List<UpdateData>() { new UpdateData("PRIMARY_PATIENT_TYPE_ID", this.Value(newSereServ.PRIMARY_PATIENT_TYPE_ID)) };
                    return true;
                }
                else if (field == UpdateField.SERVICE_CONDITION_ID && newSereServ.SERVICE_CONDITION_ID != oldSereServ.SERVICE_CONDITION_ID)
                {
                    updateData = new List<UpdateData>() { new UpdateData("SERVICE_CONDITION_ID", this.Value(newSereServ.SERVICE_CONDITION_ID)) };
                    return true;
                }
                else if (field == UpdateField.STENT_ORDER && newSereServ.STENT_ORDER != oldSereServ.STENT_ORDER)
                {
                    updateData = new List<UpdateData>() { new UpdateData("STENT_ORDER", this.Value(newSereServ.STENT_ORDER)) };
                    return true;
                }
                else if (field == UpdateField.EQUIPMENT_SET_ORDER__AND__EQUIPMENT_SET_ID && (newSereServ.EQUIPMENT_SET_ORDER != oldSereServ.EQUIPMENT_SET_ORDER || newSereServ.EQUIPMENT_SET_ID != oldSereServ.EQUIPMENT_SET_ID))
                {
                    updateData = new List<UpdateData>() { 
                        new UpdateData("EQUIPMENT_SET_ORDER", this.Value(newSereServ.EQUIPMENT_SET_ORDER)),
                        new UpdateData("EQUIPMENT_SET_ID", this.Value(newSereServ.EQUIPMENT_SET_ID)) 
                    };
                    return true;
                }
            }
            return false;
        }

    }

}
