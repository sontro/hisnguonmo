using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServTein;
using MOS.UTILITY;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;

namespace MOS.MANAGER.HisSereServ.Update.PayslipInfo
{
    class HisSereServUpdateEachPayslipInfo : BusinessBase
    {
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;

        internal HisSereServUpdateEachPayslipInfo()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdateEachPayslipInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Run(HisSereServPayslipSDO data, ref List<HIS_SERE_SERV> resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisSereServCheck ssChecker = new HisSereServCheck(param);
                HisSereServUpdatePayslipInfoCheck payslipInfoChecker = new HisSereServUpdatePayslipInfoCheck(param);
                List<HIS_SERE_SERV> allSereServs = IsNotNullOrEmpty(data.SereServs) ? new HisSereServGet().GetByIds(data.SereServs.Select(o => o.ID).ToList()) : null;
                List<HIS_SERVICE_REQ> affectSReqs = allSereServs != null ? new HisServiceReqGet().GetByIds(allSereServs.Select(o => o.SERVICE_REQ_ID.Value).ToList()) : null;
                bool valid = true;
                valid = valid && payslipInfoChecker.VerifyRequireField(data);
                valid = valid && treatChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && payslipInfoChecker.IsValidBHYTServices(data, allSereServs, affectSReqs, true);
                if (HisSereServCFG.DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH)
                {
                    valid = valid && payslipInfoChecker.FilterValidateWhenExpMestFinished(data, allSereServs, affectSReqs);
                    if (valid && !IsNotNullOrEmpty(data.SereServs))
                    {
                        result = true;
                        return result;
                    }
                }
                if (valid)
                {
                    this.IsValidDoNotUseBHYTUpdateEveryPay(data);
                    this.GetValidPricingPolicy(data, treatment, allSereServs, affectSReqs);
                    this.EliminateNoServiceCondition(data);
                    if (IsNotNullOrEmpty(data.SereServs))
                    {
                        if (!hisSereServUpdatePayslipInfo.Run(data, ref resultData))
                        {
                            throw new Exception("Cap nhat doi tuong thanh toan, phu thu cho dich vu that bai");
                        }
                        result = true;
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

        private void GetValidPricingPolicy(HisSereServPayslipSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> allSereServs, List<HIS_SERVICE_REQ> affectSReqs)
        {
            try
            {
                if (IsNotNullOrEmpty(allSereServs) && IsNotNullOrEmpty(affectSReqs))
                {
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                    HisSereServSetInfo addInfo = new HisSereServSetInfo(param, ptas);
                    foreach (HIS_SERE_SERV newSs in data.SereServs)
                    {
                        HIS_SERE_SERV t = allSereServs.Where(o => o.ID == newSs.ID).FirstOrDefault();

                        t.PRIMARY_PATIENT_TYPE_ID = newSs.PRIMARY_PATIENT_TYPE_ID;
                        t.SERVICE_CONDITION_ID = newSs.SERVICE_CONDITION_ID;

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
                        t.IS_NOT_USE_BHYT = newSs.IS_NOT_USE_BHYT;
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
                            if (IsNotNull(req) && HisSereServCFG.MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE)
                            {
                                if (t.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN && req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                                {
                                    List<HIS_SERE_SERV_TEIN> teins = new HisSereServTeinGet().GetBySereServId(t.ID);
                                    if (IsNotNullOrEmpty(teins))
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
                                    if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                                    {
                                        canUpdate = true;
                                    }
                                }
                                else
                                {
                                    if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                                    {
                                        canUpdate = true;
                                    }
                                    else if (req.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                                    {
                                        List<HIS_SERE_SERV_TEIN> teins = new HisSereServTeinGet().GetBySereServId(t.ID);
                                        if (IsNotNullOrEmpty(teins))
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

                    //Lay ra cac dich vu co chinh sach gia hop le
                    List<HIS_SERE_SERV> validSereServ = this.GetValidPriceService(param, treatment, allSereServs);

                    //Cap nhat lai d/s dich vu can cap nhat la cac dich vu co chinh sach gia hop le
                    data.SereServs = IsNotNullOrEmpty(validSereServ) ? data.SereServs.Where(o => validSereServ.Select(s => s.ID).Contains(o.ID)).ToList() : null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Loc bo cac dich vu co doi tuong thanh toan la BHYT trong truong hop dich vu co thiet lap dieu kien, nhưng client (nguoi dung) khong truyen len thong tin dieu kien
        /// </summary>
        /// <param name="sereServs"></param>
        private void EliminateNoServiceCondition(HisSereServPayslipSDO data)
        {
            if (data != null && IsNotNullOrEmpty(data.SereServs))
            {
                //Lay cac dich vu co doi tuong thanh toan la BHYT
                List<long> serviceIds = data.SereServs.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).Select(o => o.SERVICE_ID).ToList();

                List<long> hasConditionServiceIds = HisServiceConditionCFG.DATA != null ? HisServiceConditionCFG.DATA.Where(o => serviceIds.Contains(o.SERVICE_ID)).Select(o => o.SERVICE_ID).ToList() : null;

                //Loc bo ra cac dich vu update doi tuong thanh toan sang BHYT trong truong hop dich vu co thiet lap dieu kien nhung client khong truyen len dieu kien
                if (hasConditionServiceIds != null && hasConditionServiceIds.Count > 0)
                {
                    List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();
                    foreach (HIS_SERE_SERV s in data.SereServs)
                    {
                        if (s.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                            || !hasConditionServiceIds.Contains(s.SERVICE_ID)
                            || s.SERVICE_CONDITION_ID.HasValue)
                        {
                            newSereServs.Add(s);
                        }
                    }

                    data.SereServs = newSereServs;
                }
            }
        }
        private void IsValidDoNotUseBHYTUpdateEveryPay(HisSereServPayslipSDO data)
        {
            try
            {
                if (data != null && IsNotNullOrEmpty(data.SereServs))
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisEmployeeUtil.IsAdmin(loginName))
                    {
                        List<long> serviceIds = data.SereServs.Where(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).Select(o => o.SERVICE_ID).ToList();
                        List<long> hasServiceIds = HisServiceCFG.DO_NOT_USE_BHYT_DATA_VIEW != null ? HisServiceCFG.DO_NOT_USE_BHYT_DATA_VIEW.Where(o => serviceIds.Contains(o.ID)).Select(o => o.ID).ToList() : null;
                        //Loc bo ra cac dich vu update doi tuong thanh toan sang BHYT trong truong hop dich vu co thiet lap dieu kien
                        if (hasServiceIds != null && hasServiceIds.Count > 0)
                        {
                            List<HIS_SERE_SERV> newSereServs = new List<HIS_SERE_SERV>();
                            foreach (HIS_SERE_SERV s in data.SereServs)
                            {
                                if (s.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT
                                    || !hasServiceIds.Contains(s.SERVICE_ID))
                                {
                                    newSereServs.Add(s);
                                }
                            }

                            data.SereServs = newSereServs;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
        private List<HIS_SERE_SERV> GetValidPriceService(CommonParam param, HIS_TREATMENT treatment, List<HIS_SERE_SERV> allExistSereServs)
        {
            List<HIS_SERE_SERV> resultData = new List<HIS_SERE_SERV>();
            try
            {
                if (allExistSereServs != null)
                {
                    List<long> medicineIds = allExistSereServs != null ? allExistSereServs.Where(o => o.MEDICINE_ID.HasValue).Select(o => o.MEDICINE_ID.Value).ToList() : null;
                    List<long> materialIds = allExistSereServs != null ? allExistSereServs.Where(o => o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList() : null;

                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(null, treatment, medicineIds, materialIds);

                    foreach (HIS_SERE_SERV t in allExistSereServs)
                    {
                        if (priceAdder.AddPrice(t, allExistSereServs, t.TDL_INTRUCTION_TIME, t.TDL_EXECUTE_BRANCH_ID, t.TDL_REQUEST_ROOM_ID, t.TDL_REQUEST_DEPARTMENT_ID, t.TDL_EXECUTE_ROOM_ID))
                        {
                            resultData.Add(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return resultData;
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServUpdatePayslipInfo.Rollback();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
