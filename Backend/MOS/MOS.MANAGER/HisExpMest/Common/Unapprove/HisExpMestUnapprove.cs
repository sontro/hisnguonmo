using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
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
using Newtonsoft.Json;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisBlood.Update;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.Common.Unapprove
{
    //Huy duyet phieu xuat
    //Chi danh cho cac loai phieu xuat co tao y/c xuat: xuat chuyen kho, hoan co so, bu co so, hao phi khoa phong
    //Khong xu ly nghiep vu huy duyet phieu linh. Nghiep vu nay se duoc xu ly o phan "Phieu linh" rieng
    partial class HisExpMestUnapprove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private BloodProcessor bloodProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestUnapprove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestUnapprove(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                List<HIS_EXP_MEST_BLOOD> bloods = null;
                List<HIS_EXP_MEST_MEDICINE> approveMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> approveMaterials = null;
                List<HIS_EXP_MEST_BLOOD> approveBloods = null;
                HIS_SERVICE_REQ serviceReq = null;
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestUnapproveCheck checker = new HisExpMestUnapproveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.IsNotBeingApproved(expMest);
                valid = valid && checker.IsExists(expMest, ref medicines, ref materials, ref bloods, ref approveMedicines, ref approveMaterials, ref approveBloods);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    this.ProcessExpMest(expMest, medicines, materials, bloods);

                    this.ProcessServiceReq(expMest, ref serviceReq);

                    if (!this.medicineProcessor.Run(approveMedicines, expMest, ref sqls))
                    {
                        throw new Exception("Rollback");
                    }
                    if (!this.materialProcessor.Run(approveMaterials, expMest, ref sqls))
                    {
                        throw new Exception("Rollback");
                    }
                    if (!this.bloodProcessor.Run(approveBloods, expMest, ref sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(expMest, approveMaterials, approveMedicines, approveBloods, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    
                    resultData = expMest;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyDuyetPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();
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

        private void ProcessExpMest(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_BLOOD> bloods)
        {
            //Kiem tra xem phieu xuat co du lieu da thuc xuat hay chua
            bool existExport = (IsNotNullOrEmpty(medicines) && medicines.Exists(t => t.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE))
                || (IsNotNullOrEmpty(materials) && materials.Exists(t => t.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE))
                || (IsNotNullOrEmpty(bloods) && bloods.Exists(t => t.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE));

            //Neu khong co du lieu da thuc xuat thi moi thuc hien cap nhat
            if (!existExport)
            {
                //Cap nhat trang thai cua exp_mest
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                expMest.LAST_APPROVAL_TIME = null;
                expMest.LAST_APPROVAL_LOGINNAME = null;
                expMest.LAST_APPROVAL_USERNAME = null;
                expMest.LAST_APPROVAL_DATE = null;
                expMest.TDL_BLOOD_CODE = null;
                if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        //Cap nhat trang thai service_req sang trang thai chua xu ly
        private void ProcessServiceReq(HIS_EXP_MEST expMest, ref HIS_SERVICE_REQ serviceReq)
        {
            if (expMest != null && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
            {
                if (expMest.SERVICE_REQ_ID.HasValue)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
                }
                //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban --> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
                else if (expMest.PRESCRIPTION_ID.HasValue && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.PRESCRIPTION_ID.Value);
                }

                if (serviceReq != null && serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(serviceReq); //phuc vu rollback
                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    if (!this.hisServiceReqUpdate.Update(serviceReq, beforeUpdate, false))
                    {
                        throw new Exception("Cap nhat trang thai service_Req sang dang xu ly that bai");
                    }
                }
            }
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
                    string updateSql = "";
                    if (expMest.TDL_TOTAL_PRICE.HasValue && expMest.TDL_TOTAL_PRICE.Value >= totalPrice.Value)
                    {
                        updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = NVL(TDL_TOTAL_PRICE,0) - {0} WHERE ID = {1}", totalPrice.Value, expMest.ID);
                    }
                    else
                    {
                        updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = 0 WHERE ID = {0}", expMest.ID);
                    }
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
