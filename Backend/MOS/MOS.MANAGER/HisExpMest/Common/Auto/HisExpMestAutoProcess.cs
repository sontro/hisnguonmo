using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Approve;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMediStockExty;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Auto
{
    partial class HisExpMestAutoProcess : BusinessBase
    {
        private HisExpMestResultSDO recentResultData = null;

        private HisExpMestApprove hisExpMestApproveProcessor;

        internal HisExpMestAutoProcess()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAutoProcess(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestApproveProcessor = new HisExpMestApprove(param);
        }

        internal bool Run(HIS_EXP_MEST data, ref HisExpMestResultSDO resultData, bool isExpMestSale = false)
        {
            return this.Run(data, null, null, ref resultData, isExpMestSale);
        }

        internal bool Run(HIS_EXP_MEST data, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref HisExpMestResultSDO resultData, bool isExpMestSale = false)
        {
            bool result = true;
            try
            {
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new ArgumentNullException("data");
                }

                bool valid = true;
                valid = valid && (isExpMestSale || this.CheckAllowAuto(data));
                valid = valid && (isExpMestSale || this.CheckHasNoBltyReq(data));
                valid = valid && this.CheckAuto(data);
                if (valid)
                {
                    this.ProcessAutoApprove(data, medicines, materials);
                    resultData = this.recentResultData;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool CheckAllowAuto(HIS_EXP_MEST data)
        {
            bool valid = true;
            try
            {
                if (!HisExpMestConstant.NOT_HAS_REQ_EXP_MEST_TYPE_IDs.Contains(data.EXP_MEST_TYPE_ID) && !HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(data.EXP_MEST_TYPE_ID))
                {
                    return false;
                }
                if (new HisExpMestCheck().IsAutoStockTransfer(data))
                {
                    return true;
                }
                //HIS_MEDI_STOCK_EXTY mediStockExty = new HisMediStockExtyGet().GetByMediStockIdAndExpMestTypeId(data.MEDI_STOCK_ID, data.EXP_MEST_TYPE_ID);
                HIS_MEDI_STOCK_EXTY mediStockExty = HisMediStockExtyCFG.DATA.FirstOrDefault(o => o.MEDI_STOCK_ID == data.MEDI_STOCK_ID && o.EXP_MEST_TYPE_ID == data.EXP_MEST_TYPE_ID);
                if (mediStockExty == null || mediStockExty.IS_AUTO_APPROVE != MOS.UTILITY.Constant.IS_TRUE)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;

        }

        private bool CheckAuto(HIS_EXP_MEST data)
        {
            return (data.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST);
        }

        private bool CheckHasNoBltyReq(HIS_EXP_MEST data)
        {
            if (data.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                && data.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN)
            {
                List<HIS_EXP_MEST_BLTY_REQ> hisExpMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(data.ID);
                if (IsNotNullOrEmpty(hisExpMestBltyReqs))
                {
                    Inventec.Common.Logging.LogSystem.Info("Phieu xuat co chua mau khong cho phep tu dong duyet");
                    return false;
                }
            }
            return true;
        }

        private void ProcessAutoApprove(HIS_EXP_MEST data, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_MATERIAL> expMaterials)
        {
            List<ExpMedicineTypeSDO> medicines = null;
            List<ExpMaterialTypeSDO> materials = null;
            if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(data.EXP_MEST_TYPE_ID))
            {
                List<HIS_EXP_MEST_METY_REQ> hisExpMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(data.ID);
                List<HIS_EXP_MEST_MATY_REQ> hisExpMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(data.ID);

                if (IsNotNullOrEmpty(hisExpMestMatyReqs))
                {
                    materials = new List<ExpMaterialTypeSDO>();
                    foreach (var matyReq in hisExpMestMatyReqs)
                    {
                        decimal expAmount = matyReq.AMOUNT - (matyReq.DD_AMOUNT ?? 0);
                        if (expAmount <= 0)
                            continue;
                        ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                        sdo.Amount = expAmount;
                        sdo.ExpMestMatyReqId = matyReq.ID;
                        sdo.MaterialTypeId = matyReq.MATERIAL_TYPE_ID;
                        sdo.NumOrder = matyReq.NUM_ORDER;
                        //sdo.PatientTypeId = matyReq.PATIENT_TYPE_ID;
                        materials.Add(sdo);
                    }
                }

                if (IsNotNullOrEmpty(hisExpMestMetyReqs))
                {
                    medicines = new List<ExpMedicineTypeSDO>();
                    foreach (var metyReq in hisExpMestMetyReqs)
                    {
                        decimal expAmount = metyReq.AMOUNT - (metyReq.DD_AMOUNT ?? 0);
                        if (expAmount <= 0)
                            continue;
                        ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                        sdo.Amount = expAmount;
                        sdo.ExpMestMetyReqId = metyReq.ID;
                        sdo.MedicineTypeId = metyReq.MEDICINE_TYPE_ID;
                        sdo.NumOrder = metyReq.NUM_ORDER;
                        //sdo.PatientTypeId = metyReq.PATIENT_TYPE_ID;
                        medicines.Add(sdo);
                    }
                }
                if (!IsNotNullOrEmpty(medicines) && !IsNotNullOrEmpty(materials))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongDuyetThatBai);
                    throw new Exception("Loai xuat co tao Yeu cau nhung khong lay duoc yeu cau nao, Tu dong duyet that bai");
                }
            }
            HisExpMestApproveSDO appSdo = new HisExpMestApproveSDO();
            appSdo.ExpMestId = data.ID;
            appSdo.IsFinish = true;
            appSdo.Materials = materials;
            appSdo.Medicines = medicines;
            appSdo.ReqRoomId = data.REQ_ROOM_ID;//Xet reqRoomId de check du lieu bat buoc

            if (this.hisExpMestApproveProcessor == null)
                this.hisExpMestApproveProcessor = new HisExpMestApprove(new CommonParam());
            if (!this.hisExpMestApproveProcessor.Run(appSdo, true, expMedicines, expMaterials, ref recentResultData))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongDuyetThatBai);
                throw new Exception("Tu dong duyet phieu xuat that bai");
            }
        }

        internal void RollBack()
        {
            this.hisExpMestApproveProcessor.RollBack();
        }
    }
}
