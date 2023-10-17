using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Create
{
    class ExpMestProcessor : BusinessBase
    {
        private HIS_EXP_MEST recentHisExpMest = null;
        private List<HIS_MATERIAL_BEAN> materialBeans = null;

        private HisExpMestCreate hisExpMestCreate;

        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal ExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
        }

        internal bool Run(HisPackingCreateSDO data, HIS_DISPENSE hisDispense, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.ProcessHisExpMest(data, hisDispense);
                this.ProcessMaterial(data, ref materials, ref sqls);
                expMest = this.recentHisExpMest;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisExpMest(HisPackingCreateSDO data, HIS_DISPENSE hisDispense)
        {
            HIS_EXP_MEST expMest = new HIS_EXP_MEST();
            expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCT;
            expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
            expMest.REQ_ROOM_ID = data.RequestRoomId;
            expMest.MEDI_STOCK_ID = data.MediStockId;
            expMest.DISPENSE_ID = hisDispense.ID;
            expMest.TDL_DISPENSE_CODE = hisDispense.DISPENSE_CODE;
            if (!this.hisExpMestCreate.Create(expMest))
            {
                throw new Exception("hisExpMestCreate. Ket thuc nghiep vu");
            }

            this.recentHisExpMest = expMest;
        }

        private void ProcessMaterial(HisPackingCreateSDO data, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(data.MaterialTypes))
            {
                this.ProcessMaterialBean(data);
                this.ProcessExpMestMaterial(ref materials, ref sqls);
            }
        }

        private void ProcessMaterialBean(HisPackingCreateSDO data)
        {
            List<ExpMaterialTypeSDO> materialTypeSplits = new List<ExpMaterialTypeSDO>();
            List<HIS_MATERIAL_BEAN> listBean = null;
            List<HIS_MATERIAL_PATY> materialPaties = null;
            var Groups = data.MaterialTypes.GroupBy(g => g.MaterialTypeId).ToList();
            foreach (var group in Groups)
            {
                ExpMaterialTypeSDO mtSdo = new ExpMaterialTypeSDO();
                mtSdo.Amount = group.Sum(s => s.Amount);
                mtSdo.MaterialTypeId = group.Key;
                materialTypeSplits.Add(mtSdo);
            }
            if (!this.hisMaterialBeanSplit.SplitByMaterialType(materialTypeSplits, data.MediStockId, null, null, null, ref listBean, ref materialPaties))
            {
                throw new Exception("hisMaterialBeanSplit. Ket thuc nghiep vu");
            }
            this.materialBeans = listBean;
        }

        private void ProcessExpMestMaterial(ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> dicExpMestMaterial = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
            var Groups = materialBeans.GroupBy(g => g.MATERIAL_ID).ToList();
            foreach (var group in Groups)
            {
                List<HIS_MATERIAL_BEAN> listByMaterial = group.ToList();
                HIS_EXP_MEST_MATERIAL expMestMaterial = new HIS_EXP_MEST_MATERIAL();
                expMestMaterial.TDL_MATERIAL_TYPE_ID = listByMaterial[0].TDL_MATERIAL_TYPE_ID;
                expMestMaterial.TDL_MEDI_STOCK_ID = this.recentHisExpMest.MEDI_STOCK_ID;
                expMestMaterial.EXP_MEST_ID = this.recentHisExpMest.ID;
                expMestMaterial.MATERIAL_ID = group.Key;
                expMestMaterial.PRICE = listByMaterial[0].TDL_MATERIAL_IMP_PRICE;
                expMestMaterial.VAT_RATIO = listByMaterial[0].TDL_MATERIAL_IMP_VAT_RATIO;
                expMestMaterial.AMOUNT = listByMaterial.Sum(s => s.AMOUNT);
                dicExpMestMaterial[expMestMaterial] = listByMaterial.Select(s => s.ID).ToList();
            }

            if (!this.hisExpMestMaterialCreate.CreateList(dicExpMestMaterial.Select(s => s.Key).ToList()))
            {
                throw new Exception("hisExpMestMaterialCreate. Ket thuc nghiep vu");
            }

            this.SqlUpdateMaterialBean(dicExpMestMaterial, ref sqls);

            materials = dicExpMestMaterial.Select(s => s.Key).ToList();
        }

        private void SqlUpdateMaterialBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisExpMestMaterialCreate.RollbackData();
                this.hisMaterialBeanSplit.RollBack();
                this.hisExpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
