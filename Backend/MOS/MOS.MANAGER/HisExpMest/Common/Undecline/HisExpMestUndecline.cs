using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisPrepare.Check;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Undecline
{
    class HisExpMestUndecline : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestUndecline()
            : base()
        {
            this.Init();
        }

        internal HisExpMestUndecline(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestUndeclineCheck checker = new HisExpMestUndeclineCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && this.CheckPrepare(expMest);
                if (valid)
                {
                    //Cap nhat trang thai cua exp_mest
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.ProcessServiceReq(expMest);

                    resultData = expMest;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
                this.RollBack();
            }
            return result;
        }

        private bool CheckPrepare(HIS_EXP_MEST exp)
        {
            bool result = true;
            try
            {
                if (!exp.TDL_TREATMENT_ID.HasValue)
                {
                    return true;
                }

                if (exp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT
                    && exp.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT)
                {
                    return true;
                }

                if (exp.TDL_TREATMENT_ID.HasValue
                    && exp.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(exp.TDL_TREATMENT_ID.Value);
                    if (!(treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU))
                    {
                        return true;
                    }
                }

                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(exp.ID);
                List<HIS_EXP_MEST_MATERIAL> expMestMateirals = new HisExpMestMaterialGet().GetByExpMestId(exp.ID);

                HisPrepareCheckAmount prepareChecker = new HisPrepareCheckAmount(param);
                List<PrepareData> errorDatas = new List<PrepareData>();

                List<HIS_EXP_MEST_MEDICINE> medicines = expMestMedicines != null ? expMestMedicines.Where(o => HisMedicineTypeCFG.DATA.Any(a => a.ID == o.TDL_MEDICINE_TYPE_ID && a.IS_MUST_PREPARE == Constant.IS_TRUE)).ToList() : null;
                List<HIS_EXP_MEST_MATERIAL> materials = expMestMateirals != null ? expMestMateirals.Where(o => HisMedicineTypeCFG.DATA.Any(a => a.ID == o.TDL_MATERIAL_TYPE_ID && a.IS_MUST_PREPARE == Constant.IS_TRUE)).ToList() : null;

                if (IsNotNullOrEmpty(medicines))
                {
                    var Groups = medicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID ?? 0).ToList();
                    foreach (var group in Groups)
                    {
                        decimal appAmount = 0;
                        decimal presAmount = 0;
                        decimal newAmount = group.Sum(s => s.AMOUNT);
                        if (!prepareChecker.CheckAmountMedicine(exp.TDL_TREATMENT_ID.Value, group.Key, newAmount, ref appAmount, ref presAmount))
                        {
                            PrepareData pd = new PrepareData();
                            pd.TypeId = group.Key;
                            pd.TypeName = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key).MEDICINE_TYPE_NAME;
                            pd.ApprovalAmount = appAmount;
                            pd.PresAmount = presAmount + newAmount;
                            errorDatas.Add(pd);
                        }
                    }
                }

                if (IsNotNullOrEmpty(materials))
                {
                    var Groups = materials.GroupBy(g => g.TDL_MATERIAL_TYPE_ID ?? 0).ToList();
                    foreach (var group in Groups)
                    {
                        decimal appAmount = 0;
                        decimal presAmount = 0;
                        decimal newAmount = group.Sum(s => s.AMOUNT);
                        if (!prepareChecker.CheckAmountMaterial(exp.TDL_TREATMENT_ID.Value, group.Key, newAmount, ref appAmount, ref presAmount))
                        {
                            PrepareData pd = new PrepareData();
                            pd.TypeId = group.Key;
                            pd.TypeName = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key).MATERIAL_TYPE_NAME;
                            pd.ApprovalAmount = appAmount;
                            pd.PresAmount = presAmount + newAmount;
                            errorDatas.Add(pd);
                        }
                    }
                }

                if (IsNotNullOrEmpty(errorDatas))
                {
                    string duyet = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_Duyet, param.LanguageCode);
                    string ke = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common_Ke, param.LanguageCode);
                    string message = "";
                    foreach (var item in errorDatas)
                    {
                        if (String.IsNullOrWhiteSpace(message))
                        {
                            message = String.Format("{0}({1}={2}.{3}={4})", item.TypeName, duyet, item.ApprovalAmount, ke, item.PresAmount);
                        }
                        else
                        {
                            message = String.Format(";{0}({1}={2}.{3}={4})", item.TypeName, duyet, item.ApprovalAmount, ke, item.PresAmount);
                        }
                    }
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacThuocVatTuSauCoSoLuongKeLonHonDuTru, message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

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

                if (serviceReq != null && serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq); //phuc vu rollback
                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    if (!new HisServiceReqUpdate(param).Update(serviceReq, beforeServiceReq, false))
                    {
                        throw new Exception("Cap nhat trang thai service_Req sang hoan thanh that bai");
                    }
                }
            }
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }

    public class PrepareData
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal ApprovalAmount { get; set; }
        public decimal PresAmount { get; set; }

    }
}
