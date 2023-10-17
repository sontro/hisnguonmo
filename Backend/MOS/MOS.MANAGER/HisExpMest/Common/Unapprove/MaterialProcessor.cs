using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Unapprove
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanUnlockByExpMest beanUnlock;
        private HisExpMestMatyReqDecreaseDdAmount hisExpMestMatyReqDecreaseDdAmount;
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
            this.beanUnlock = new HisMaterialBeanUnlockByExpMest(param);
            this.hisExpMestMatyReqDecreaseDdAmount = new HisExpMestMatyReqDecreaseDdAmount(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    //Neu phieu xuat tao lenh luc duyet ==> khi huy duyet se huy thong tin lenh
                    //Nguoc lai, chi xoa thong tin duyet chu khong xoa toan bo lenh
                    if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID) && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                    {
                        this.DeleteExpMestMaterial(expMest.ID, expMestMaterials, ref sqls);
                    }
                    else
                    {
                        this.RemoveApproveInfo(expMest, expMestMaterials);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void DeleteExpMestMaterial(long expMestId, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            List<HIS_EXP_MEST_MATY_REQ> metyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMestId);

            List<long> expMestMaterialIds = expMestMaterials.Select(o => o.ID).ToList();

            if (IsNotNullOrEmpty(expMestMaterialIds))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                string sqlUpdateBean = this.beanUnlock.GenSql(expMestMaterialIds);
                string sqlDeleteExpMestMaterial = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                //Luu y: can cap nhat bean truoc khi xoa exp_mest_material (tranh loi FK)
                sqls.Add(sqlUpdateBean);
                sqls.Add(sqlDeleteExpMestMaterial);
            }

            //Neu huy duyet thanh cong thi cap nhat so luong da duyet vao mety_req
            if (IsNotNullOrEmpty(metyReqs))
            {
                ProcessDdAmount(expMestMaterials, metyReqs);
            }
        }

        private void ProcessDdAmount(List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MATY_REQ> metyReqs)
        {
            if (!IsNotNullOrEmpty(metyReqs)) return;

            Dictionary<long, decimal> decreaseDic = new Dictionary<long, decimal>();

            //Cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_MATY_REQ req in metyReqs)
            {
                decimal unapprovalAmount = expMestMaterials
                    .Where(o => o.EXP_MEST_MATY_REQ_ID == req.ID)
                    .Sum(o => o.AMOUNT);
                if (unapprovalAmount > 0)
                {
                    decreaseDic.Add(req.ID, unapprovalAmount);
                }
            }
            if (IsNotNullOrEmpty(decreaseDic))
            {
                if (!this.hisExpMestMatyReqDecreaseDdAmount.Run(decreaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        private void RemoveApproveInfo(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (IsNotNullOrEmpty(expMestMaterials))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> befores = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(expMestMaterials);
                expMestMaterials.ForEach(o =>
                {
                    o.APPROVAL_LOGINNAME = null;
                    o.APPROVAL_TIME = null;
                    o.APPROVAL_USERNAME = null;
                });
                if (!this.hisExpMestMaterialUpdate.UpdateList(expMestMaterials, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                    && expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST_MATY_REQ> matyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMest.ID);
                    this.ProcessDdAmount(expMestMaterials, matyReqs);
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisExpMestMatyReqDecreaseDdAmount.Rollback();
        }
    }
}
