using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.VitaminA.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisMedicineBeanSplit hisMedicineBeanSplit;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_VITAMIN_A> vitaminAs, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines,ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                List<HIS_MEDICINE_BEAN> medicineBeans = null;
                List<HIS_MEDICINE_PATY> medicinePaties = null;
                List<ExpMedicineTypeSDO> metySDOs = new List<ExpMedicineTypeSDO>();
                var Groups = vitaminAs.GroupBy(g => g.MEDICINE_TYPE_ID.Value).ToList();
                foreach (var group in Groups)
                {
                    HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                    if (medicineType == null || medicineType.IS_VITAMIN_A != Constant.IS_TRUE)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HIS_MEDICINE_TYPE hoac IS_VITAMIN_A <> TRUE");
                    }
                    ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                    sdo.Amount = group.Sum(s => s.AMOUNT ?? 0);
                    sdo.MedicineTypeId = group.Key;
                    metySDOs.Add(sdo);
                }

                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? Inventec.Common.DateTime.Get.StartDay() : null;

                if (!this.hisMedicineBeanSplit.SplitByMedicineType(metySDOs, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
                {
                    return false;
                }

                var GroupMedicines = medicineBeans.GroupBy(g => g.MEDICINE_ID).ToList();
                foreach (var group in GroupMedicines)
                {
                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.AMOUNT = group.Sum(s => s.AMOUNT);
                    exp.MEDICINE_ID = group.Key;
                    exp.TDL_MEDICINE_TYPE_ID = group.FirstOrDefault().TDL_MEDICINE_TYPE_ID;
                    exp.VAT_RATIO = group.FirstOrDefault().TDL_MEDICINE_IMP_VAT_RATIO;
                    exp.PRICE = group.FirstOrDefault().TDL_MEDICINE_IMP_PRICE;
                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    medicines.Add(exp);
                    medicineDic.Add(exp, group.Select(o => o.ID).ToList());
                }

                if (!this.hisExpMestMedicineCreate.CreateList(medicines))
                {
                    throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                }

                this.SqlUpdateBean(medicineDic, ref sqls);

                expMestMedicines = medicines;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
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

        internal void RollbackData()
        {
            try
            {
                this.hisMedicineBeanSplit.RollBack();
                this.hisExpMestMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
