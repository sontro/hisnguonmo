using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStock.Inventory
{
    class ExpMedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal ExpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
        }

        internal bool Run(List<ExpMedicineSDO> medicines, HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    List<HIS_MEDICINE_BEAN> medicineBeans = null;
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(medicines, expMest.MEDI_STOCK_ID, ref medicineBeans))
                    {
                        return false;
                    }

                    List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                    foreach (ExpMedicineSDO sdo in medicines)
                    {
                        List<HIS_MEDICINE_BEAN> beans = medicineBeans.Where(o => o.MEDICINE_ID == sdo.MedicineId).ToList();
                        if (!IsNotNullOrEmpty(beans))
                        {
                            throw new Exception("Ko co bean tuong ung voi medicine_id " + sdo.MedicineId);
                        }

                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = sdo.Amount;
                        exp.MEDICINE_ID = sdo.MedicineId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.DESCRIPTION = sdo.Description;
                        exp.PRICE = sdo.Price;
                        exp.TDL_MEDICINE_TYPE_ID = beans[0].TDL_MEDICINE_TYPE_ID;
                        exp.VAT_RATIO = sdo.VatRatio;
                        exp.PRICE = sdo.Price;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        data.Add(exp);
                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }

                    if (!this.hisExpMestMedicineCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);

                    //resultData = data;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
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

        internal void Rollback()
        {
            this.hisMedicineBeanSplit.RollBack();
            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
