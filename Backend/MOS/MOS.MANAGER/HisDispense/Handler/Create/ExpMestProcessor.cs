using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Create
{
    class ExpMestProcessor : BusinessBase
    {
        private HIS_EXP_MEST recentHisExpMest = null;
        private List<HIS_MEDICINE_BEAN> medicineBeans = null;
        private List<HIS_MATERIAL_BEAN> materialBeans = null;

        private HisExpMestCreate hisExpMestCreate;

        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

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
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HisDispenseSDO data, HIS_DISPENSE hisDispense, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.ProcessHisExpMest(data, hisDispense);
                this.ProcessMaterial(data, ref materials, ref sqls);
                this.ProcessMedicine(data, ref medicines, ref sqls);
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

        private void ProcessHisExpMest(HisDispenseSDO data, HIS_DISPENSE hisDispense)
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

        private void ProcessMedicine(HisDispenseSDO data, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(data.MedicineTypes))
            {
                this.ProcessMedicineBean(data);
                this.ProcessExpMestMedicine(ref medicines, ref sqls);
            }
        }

        private void ProcessMedicineBean(HisDispenseSDO data)
        {
            List<ExpMedicineTypeSDO> medicineTypeSplits = new List<ExpMedicineTypeSDO>();
            List<HIS_MEDICINE_BEAN> listBean = null;
            List<HIS_MEDICINE_PATY> medicinePaties = null;
            var Groups = data.MedicineTypes.GroupBy(g => g.MedicineTypeId).ToList();
            foreach (var group in Groups)
            {
                ExpMedicineTypeSDO mtSdo = new ExpMedicineTypeSDO();
                mtSdo.Amount = group.Sum(s => s.Amount);
                mtSdo.MedicineTypeId = group.Key;
                medicineTypeSplits.Add(mtSdo);
            }
            if (!this.hisMedicineBeanSplit.SplitByMedicineType(medicineTypeSplits, data.MediStockId, null, null, null, ref listBean, ref medicinePaties))
            {
                throw new Exception("hisMaterialBeanSplit. Ket thuc nghiep vu");
            }
            this.medicineBeans = listBean;
        }

        private void ProcessExpMestMedicine(ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> dicExpMestMedicine = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
            var Groups = this.medicineBeans.GroupBy(g => g.MEDICINE_ID).ToList();
            foreach (var group in Groups)
            {
                List<HIS_MEDICINE_BEAN> listByMedicine = group.ToList();
                HIS_EXP_MEST_MEDICINE expMestMedicine = new HIS_EXP_MEST_MEDICINE();
                expMestMedicine.TDL_MEDICINE_TYPE_ID = listByMedicine[0].TDL_MEDICINE_TYPE_ID;
                expMestMedicine.TDL_MEDI_STOCK_ID = this.recentHisExpMest.MEDI_STOCK_ID;
                expMestMedicine.EXP_MEST_ID = this.recentHisExpMest.ID;
                expMestMedicine.MEDICINE_ID = group.Key;
                expMestMedicine.PRICE = listByMedicine[0].TDL_MEDICINE_IMP_PRICE;
                expMestMedicine.VAT_RATIO = listByMedicine[0].TDL_MEDICINE_IMP_VAT_RATIO;
                expMestMedicine.AMOUNT = listByMedicine.Sum(s => s.AMOUNT);
                dicExpMestMedicine[expMestMedicine] = listByMedicine.Select(s => s.ID).ToList();
            }

            if (!this.hisExpMestMedicineCreate.CreateList(dicExpMestMedicine.Select(s => s.Key).ToList()))
            {
                throw new Exception("hisExpMestMedicineCreate. Ket thuc nghiep vu");
            }

            this.SqlUpdateMedicineBean(dicExpMestMedicine, ref sqls);
            medicines = dicExpMestMedicine.Select(s => s.Key).ToList();
        }

        private void SqlUpdateMedicineBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMedicine.ID);
                    sqls.Add(query);
                }
            }
        }

        private void ProcessMaterial(HisDispenseSDO data, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(data.MaterialTypes))
            {
                this.ProcessMaterialBean(data);
                this.ProcessExpMestMaterial(ref materials, ref sqls);
            }
        }

        private void ProcessMaterialBean(HisDispenseSDO data)
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
                this.hisExpMestMedicineCreate.RollbackData();
                this.hisMaterialBeanSplit.RollBack();
                this.hisMedicineBeanSplit.RollBack();
                this.hisExpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
