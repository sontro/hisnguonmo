using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineMaker hisExpMestMedicineMaker;
        private HisExpMestMetyReqIncreaseDdAmount hisExpMestMetyReqIncreaseDdAmount;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineMaker = new HisExpMestMedicineMaker(param);
            this.hisExpMestMetyReqIncreaseDdAmount = new HisExpMestMetyReqIncreaseDdAmount(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> metyReqs, List<HIS_EXP_MEST_MEDICINE> medicines, List<ExpMedicineTypeSDO> medicineSDOs, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                //Voi cac loai xuat co tao y/c thi luc duyet se tao ra thong tin lenh
                if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID) && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                {
                    this.CreateExpMestMedicine(expMest, metyReqs, medicineSDOs, loginname, username, approvalTime, isAuto, ref expMestMedicines, ref sqls);
                }
                //Voi cac loai xuat tao ra thong tin lenh luon luc y/c thi luc duyet chi cap nhat thong tin duyet
                else
                {
                    this.UpdateExpMestMedicine(expMest, medicines, metyReqs, loginname, username, approvalTime, ref expMestMedicines);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        private void CreateExpMestMedicine(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> metyReqs, List<ExpMedicineTypeSDO> medicines, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {

            if (IsNotNullOrEmpty(metyReqs) && IsNotNullOrEmpty(medicines))
            {
                long? expiredDate = (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    && (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && expMest.CHMS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION)) 
                    ? (long?)approvalTime : null;
                //Tao exp_mest_material
                if (!this.hisExpMestMedicineMaker.Run(medicines, expMest, expiredDate, loginname, username, approvalTime, isAuto, ref expMestMedicines, ref sqls))
                {
                    throw new Exception("exp_mest_material: Rollback du lieu. Ket thuc nghiep vu");
                }
                //neu duyet thanh cong thi cap nhat so luong da duyet vao maty_req
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    ProcessDdAmount(metyReqs, expMestMedicines);
                }
            }
        }

        private void ProcessDdAmount(List<HIS_EXP_MEST_METY_REQ> metyReqs, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (!IsNotNullOrEmpty(metyReqs) || !IsNotNullOrEmpty(expMestMedicines)) return;

            Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();
            //cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_METY_REQ req in metyReqs)
            {
                decimal approvalAmount = expMestMedicines.Where(o => o.EXP_MEST_METY_REQ_ID == req.ID).Sum(o => o.AMOUNT);
                if (approvalAmount > 0)
                {
                    increaseDic.Add(req.ID, approvalAmount);
                }
            }
            if (IsNotNullOrEmpty(increaseDic))
            {
                if (!this.hisExpMestMetyReqIncreaseDdAmount.Run(increaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        private void UpdateExpMestMedicine(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_METY_REQ> metyReqs, string loginname, string username, long approvalTime, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (IsNotNullOrEmpty(medicines))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(medicines);
                medicines.ForEach(o =>
                {
                    o.APPROVAL_TIME = approvalTime;
                    o.APPROVAL_LOGINNAME = loginname;
                    o.APPROVAL_USERNAME = username;
                });
                if (!this.hisExpMestMedicineUpdate.UpdateList(medicines, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK && expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    ProcessDdAmount(metyReqs, medicines);
                }
                expMestMedicines = medicines;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineMaker.Rollback();
            this.hisExpMestMetyReqIncreaseDdAmount.Rollback();
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
