using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.UTILITY;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    //Duyet phieu xuat
    //Chi danh cho cac loai phieu xuat co tao y/c xuat: xuat chuyen kho, hoan co so, bu co so, hao phi khoa phong
    //Khong xu ly nghiep vu duyet phieu linh. Nghiep vu nay se duoc xu ly o phan "Phieu linh" rieng
    partial class HisExpMestApprove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private MaterialReuseProcessor materialReuseProcessor;
        private BloodProcessor bloodProcessor;
        private ExportAutoProcessor exportAutoProcessor;

        private string loginname = null;
        private string username = null;
        private long approvalTime = 0;

        internal HisExpMestApprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestApprove(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.exportAutoProcessor = new ExportAutoProcessor(param);
            this.materialReuseProcessor = new MaterialReuseProcessor(param);
        }

        internal bool Run(HisExpMestApproveSDO data, ref HisExpMestResultSDO resultData)
        {
            return this.Run(data, false, ref resultData);
        }

        internal bool Run(HisExpMestApproveSDO data, bool isAuto, ref HisExpMestResultSDO resultData)
        {
            return this.Run(data, isAuto, null, null, ref resultData);
        }

        internal bool Run(HisExpMestApproveSDO data, bool isAuto, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                List<HIS_EXP_MEST_BLTY_REQ> bltyReqs = null;
                List<HIS_EXP_MEST_MATERIAL> materials = expMaterials;
                List<HIS_EXP_MEST_MEDICINE> medicines = expMedicines;

                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestApproveCheck checker = new HisExpMestApproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, isAuto, ref expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && checker.ValidateData(data, expMest, ref metyReqs, ref matyReqs, ref bltyReqs);
                valid = valid && checker.IsNotApprovalAmountExceed(data, metyReqs, matyReqs, bltyReqs);
                valid = valid && this.CheckUnpaidOutPres(expMest, ref materials, ref medicines);
                valid = valid && commonChecker.IsValidApproveAntibioticUse(new List<HIS_EXP_MEST>() { expMest });

                if (valid)
                {
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    List<HIS_EXP_MEST_BLOOD> expMestBloods = null;
                    List<string> sqls = new List<string>();
                    string exBLoodCodes = null;

                    this.loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    this.username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    this.approvalTime = Inventec.Common.DateTime.Get.Now().Value;

                    this.ProcessExpMest(expMest, isAuto, data.Description);

                    this.ProcessServiceReq(expMest);

                    if (!this.medicineProcessor.Run(expMest, metyReqs, medicines, data.Medicines, loginname, username, approvalTime, isAuto, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(expMest, matyReqs, materials, data.Materials, loginname, username, approvalTime, isAuto, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    if (!this.materialReuseProcessor.Run(expMest, matyReqs, data.SerialNumbers, loginname, username, approvalTime, isAuto, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("materialReuseProcessor. Rollback du lieu");
                    }

                    if (!this.bloodProcessor.Run(expMest, bltyReqs, data.Bloods, loginname, username, approvalTime, data.TestResults, ref exBLoodCodes, ref expMestBloods, ref sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(expMest, expMestMaterials, expMestMedicines, expMestBloods, ref sqls);

                    sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET IS_BEING_APPROVED = NULL WHERE ID = {0}", expMest.ID));

                    //Xu ly sql de duoi cung de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    expMest.IS_BEING_APPROVED = null;
                    expMest.TDL_BLOOD_CODE = exBLoodCodes;
                    this.PassResult(expMest, expMestMaterials, expMestMedicines, expMestBloods, ref resultData);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_DuyetPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    //LogSystem.Info("Auto Export Begin");
                    this.ProcessAuto(expMest, data, expMestMedicines, expMestMaterials);

                    //LogSystem.Info("Auto Export End");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private bool CheckUnpaidOutPres(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(materials) && !IsNotNullOrEmpty(medicines))
                {
                    materials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    medicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                }
                if (HisExpMestCFG.OUT_PRES_IS_CHECK_UNPAID)
                {
                    
                    if (expMest != null && expMest.TDL_TREATMENT_ID.HasValue)
                    {
                        HIS_TREATMENT treatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                        valid = valid && new HisExpMestExportCheck(param).CheckUnpaidOutPatientPrescription(treatment, expMest, medicines, materials);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private void ProcessTdlTotalPrice(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> sqls)
        {
            try
            {
                if (!HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    return;
                }
                decimal? totalPrice = null;
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    decimal matePrice = 0;
                    foreach (HIS_EXP_MEST_MATERIAL mate in expMestMaterials)
                    {
                        if (!mate.PRICE.HasValue)
                        {
                            continue;
                        }
                        matePrice += (mate.AMOUNT * mate.PRICE.Value * (1 + (mate.VAT_RATIO ?? 0)));
                    }
                    if (matePrice > 0)
                    {
                        totalPrice = matePrice;
                    }
                }
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_MEDICINE medi in expMestMedicines)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.AMOUNT * medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (IsNotNullOrEmpty(expMestBloods))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_BLOOD medi in expMestBloods)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (totalPrice.HasValue)
                {
                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = NVL(TDL_TOTAL_PRICE,0) + {0} WHERE ID = {1}", totalPrice.Value, expMest.ID);
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        //Cap nhat trang thai service_req sang trang thai dang xu ly
        private void ProcessServiceReq(HIS_EXP_MEST expMest)
        {
            if (expMest != null)
            {
                HIS_SERVICE_REQ serviceReq = null;
                if (expMest.SERVICE_REQ_ID.HasValue)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
                }
                //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban --> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
                else if (expMest.PRESCRIPTION_ID.HasValue && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.PRESCRIPTION_ID.Value);
                }

                if (serviceReq != null && serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(serviceReq); //phuc vu rollback
                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                    if (!this.hisServiceReqUpdate.Update(serviceReq, beforeUpdate, false))
                    {
                        throw new Exception("Cap nhat trang thai service_Req sang dang xu ly that bai");
                    }
                }
            }
        }

        private void ProcessExpMest(HIS_EXP_MEST expMest, bool isAuto, string description)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
            //Neu phieu xuat chua o trang thai dang xu ly thi moi thuc hien cap nhat
            if (expMest.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE || expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
            {
                //Cap nhat trang thai cua exp_mest
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                expMest.IS_EXPORT_EQUAL_APPROVE = null;
                expMest.LAST_APPROVAL_TIME = this.approvalTime;
                expMest.LAST_APPROVAL_LOGINNAME = this.loginname;
                expMest.LAST_APPROVAL_USERNAME = this.username;
                expMest.LAST_APPROVAL_DATE = expMest.LAST_APPROVAL_TIME - expMest.LAST_APPROVAL_TIME % 1000000;

                List<long> ids = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__KHAC,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__TEST,
                };

                //Cac loai xuat: xuat ban, don thuoc, khac, ... chua co man hinh duyet cho phep nhap ghi chu, nen se ko update truong nay
                if (!isAuto && !ids.Contains(expMest.EXP_MEST_TYPE_ID))
                {
                    expMest.DESCRIPTION = description;
                }
                expMest.IS_BEING_APPROVED = Constant.IS_TRUE;
                if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
            else
            {
                expMest.IS_BEING_APPROVED = Constant.IS_TRUE;
                if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(HIS_EXP_MEST expMest, HisExpMestApproveSDO data, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            try
            {
                //Neu co cau hinh "bat buoc thanh toan truoc khi thuc xuat" thi phai kiem tra xem phieu xuat ban da co thong tin thanh toan hay chot no chua
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN
                    && (!HisExpMestCFG.EXPORT_SALE_MUST_BILL || expMest.BILL_ID.HasValue || expMest.DEBT_ID.HasValue))
                {
                    this.exportAutoProcessor.Run(expMest, data, expMestMedicines, expMestMaterials);
                }
                else
                {
                    this.exportAutoProcessor.Run(expMest, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_BLOOD> expBloods, ref HisExpMestResultSDO resultData)
        {
            resultData = new HisExpMestResultSDO();
            resultData.ExpMest = expMest;
            resultData.ExpMaterials = expMaterials;
            resultData.ExpMedicines = expMedicines;
            resultData.ExpBloods = expBloods;
        }

        internal void RollBack()
        {
            this.bloodProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisServiceReqUpdate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
