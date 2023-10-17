using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.CreateChms
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HisExpMestChmsSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> approves = null;
                List<HIS_EXP_MEST_METY_REQ> requests = null;

                if (IsNotNullOrEmpty(data.Medicines))
                {
                    if (!this.hisExpMestMetyReqMaker.Run(data.Medicines, expMest, ref requests))
                    {
                        throw new Exception("hisExpMestMetyReqMaker 1. Ket thuc nghiep vu");
                    }
                    expMestMetyReqs = requests;
                }
                else if (IsNotNullOrEmpty(data.ExpMedicineSdos))
                {
                    List<HIS_MEDICINE_BEAN> medicineBeans = null;
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(data.ExpMedicineSdos, expMest.MEDI_STOCK_ID, ref medicineBeans))
                    {
                        return false;
                    }

                    List<ExpMedicineTypeSDO> medicineTypeSdos = this.MakeMedicineTypeSdoByBean(medicineBeans, data.ExpMedicineSdos);
                    if (!this.hisExpMestMetyReqMaker.Run(medicineTypeSdos, expMest, ref requests))
                    {
                        throw new Exception("hisExpMestMetyReqMaker 2. Ket thuc nghiep vu");
                    }

                    approves = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                    foreach (ExpMedicineSDO sdo in data.ExpMedicineSdos)
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

                        var req = requests.FirstOrDefault(o => o.MEDICINE_TYPE_ID == exp.TDL_MEDICINE_TYPE_ID);
                        if (req == null)
                        {
                            throw new Exception("Khong tao duoc HIS_EXP_MEST_METY_REQ tuong ung voi loai thuoc: " + exp.TDL_MEDICINE_TYPE_ID);
                        }
                        exp.EXP_MEST_METY_REQ_ID = req.ID;
                        approves.Add(exp);
                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }

                    if (!this.hisExpMestMedicineCreate.CreateList(approves))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);

                    expMestMetyReqs = requests;
                    medicines = approves;
                }
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

        private List<ExpMedicineTypeSDO> MakeMedicineTypeSdoByBean(List<HIS_MEDICINE_BEAN> medicineBeans, List<ExpMedicineSDO> medicineSdos)
        {
            List<ExpMedicineTypeSDO> typeSdos = new List<ExpMedicineTypeSDO>();
            var Groups = medicineBeans.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
            foreach (var group in Groups)
            {
                ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                sdo.Amount = group.Sum(s => s.AMOUNT);
                sdo.MedicineTypeId = group.Key;
                sdo.NumOrder = medicineSdos.FirstOrDefault(o => group.Any(a => a.MEDICINE_ID == o.MedicineId)).NumOrder;
                typeSdos.Add(sdo);
            }
            return typeSdos;
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
            this.hisExpMestMetyReqMaker.Rollback();
        }
    }
}
