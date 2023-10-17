using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Approve
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialMaker hisExpMestMaterialMaker;
        private HisExpMestMatyReqIncreaseDdAmount hisExpMestMatyReqIncreaseDdAmount;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialMaker = new HisExpMestMaterialMaker(param);
            this.hisExpMestMatyReqIncreaseDdAmount = new HisExpMestMatyReqIncreaseDdAmount(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_MATERIAL> materials, List<ExpMaterialTypeSDO> materialSDOs, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            try
            {
                //Voi cac loai xuat co tao y/c thi luc duyet se tao ra thong tin lenh
                if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID) && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                {
                    this.CreateExpMestMaterial(expMest, matyReqs, materialSDOs, loginname, username, approvalTime, isAuto, ref expMestMaterials, ref sqls);
                }
                //Voi cac loai xuat tao ra thong tin lenh luon luc y/c thi luc duyet chi cap nhat thong tin duyet
                else
                {
                    this.UpdateExpMestMaterial(expMest, materials, matyReqs, loginname, username, approvalTime, ref expMestMaterials);
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

        private void CreateExpMestMaterial(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<ExpMaterialTypeSDO> materials, string loginname, string username, long approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {

            if (IsNotNullOrEmpty(matyReqs) && IsNotNullOrEmpty(materials))
            {
                long? expiredDate = (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    && (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && expMest.CHMS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION))
                    ? (long?)approvalTime : null;
                //Tao exp_mest_material
                if (!this.hisExpMestMaterialMaker.Run(materials, expMest, expiredDate, loginname, username, approvalTime, isAuto, ref expMestMaterials, ref sqls))
                {
                    throw new Exception("exp_mest_material: Rollback du lieu. Ket thuc nghiep vu");
                }

                //neu duyet thanh cong thi cap nhat so luong da duyet vao maty_req
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    ProcessDdAmount(matyReqs, expMestMaterials);
                }
            }
        }

        private void ProcessDdAmount(List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (!IsNotNullOrEmpty(matyReqs) || !IsNotNullOrEmpty(expMestMaterials)) return;

            Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();

            //cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_MATY_REQ req in matyReqs)
            {
                decimal approvalAmount = expMestMaterials.Where(o => o.EXP_MEST_MATY_REQ_ID == req.ID).Sum(o => o.AMOUNT);
                if (approvalAmount > 0)
                {
                    increaseDic.Add(req.ID, approvalAmount);
                }
            }
            if (IsNotNullOrEmpty(increaseDic))
            {
                if (!this.hisExpMestMatyReqIncreaseDdAmount.Run(increaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        private void UpdateExpMestMaterial(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> materials, List<HIS_EXP_MEST_MATY_REQ> matyReqs, string loginname, string username, long approvalTime, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (IsNotNullOrEmpty(materials))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(materials);
                materials.ForEach(o =>
                {
                    o.APPROVAL_TIME = approvalTime;
                    o.APPROVAL_LOGINNAME = loginname;
                    o.APPROVAL_USERNAME = username;
                });
                if (!this.hisExpMestMaterialUpdate.UpdateList(materials, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                    && expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    ProcessDdAmount(matyReqs, materials);
                }
                expMestMaterials = materials;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialMaker.Rollback();
            this.hisExpMestMatyReqIncreaseDdAmount.Rollback();
            this.hisExpMestMaterialUpdate.RollbackData();
        }
    }
}
