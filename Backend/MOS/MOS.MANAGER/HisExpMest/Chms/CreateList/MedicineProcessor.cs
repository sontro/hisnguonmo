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

namespace MOS.MANAGER.HisExpMest.Chms.CreateList
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisExpMestMetyReqMaker> metyReqMakers;
        private List<HisMedicineBeanSplit> beanSplits;
        private List<HisExpMestMedicineCreate> medicineCreates;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicineCreates = new List<HisExpMestMedicineCreate>();
            this.metyReqMakers = new List<HisExpMestMetyReqMaker>();
            this.beanSplits = new List<HisMedicineBeanSplit>();
        }

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestChmsDetailSDO> dicExpMest, ref List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                medicines = new List<HIS_EXP_MEST_MEDICINE>();
                expMestMetyReqs = new List<HIS_EXP_MEST_METY_REQ>();

                foreach (var dic in dicExpMest)
                {
                    ExpMestChmsDetailSDO sdo = dic.Value;
                    HIS_EXP_MEST expMest = dic.Key;

                    List<HIS_EXP_MEST_MEDICINE> approves = null;
                    List<HIS_EXP_MEST_METY_REQ> requests = null;

                    if (IsNotNullOrEmpty(sdo.MedicineTypes))
                    {
                        HisExpMestMetyReqMaker maker = new HisExpMestMetyReqMaker(param);
                        this.metyReqMakers.Add(maker);
                        if (!maker.Run(sdo.MedicineTypes, expMest, ref requests))
                        {
                            throw new Exception("hisExpMestMetyReqMaker 1. Ket thuc nghiep vu");
                        }
                        expMestMetyReqs.AddRange(requests);
                    }
                    else if (IsNotNullOrEmpty(sdo.Medicines))
                    {
                        List<HIS_MEDICINE_BEAN> medicineBeans = null;
                        HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                        if (!spliter.SplitByMedicine(sdo.Medicines, expMest.MEDI_STOCK_ID, ref medicineBeans))
                        {
                            return false;
                        }

                        List<ExpMedicineTypeSDO> medicineTypeSdos = this.MakeMedicineTypeSdoByBean(medicineBeans, sdo.Medicines);
                        HisExpMestMetyReqMaker maker = new HisExpMestMetyReqMaker(param);
                        this.metyReqMakers.Add(maker);
                        if (!maker.Run(medicineTypeSdos, expMest, ref requests))
                        {
                            throw new Exception("hisExpMestMetyReqMaker 2. Ket thuc nghiep vu");
                        }

                        approves = new List<HIS_EXP_MEST_MEDICINE>();
                        Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                        //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                        foreach (ExpMedicineSDO medi in sdo.Medicines)
                        {
                            List<HIS_MEDICINE_BEAN> beans = medicineBeans.Where(o => o.MEDICINE_ID == medi.MedicineId).ToList();
                            if (!IsNotNullOrEmpty(beans))
                            {
                                throw new Exception("Ko co bean tuong ung voi medicine_id " + medi.MedicineId);
                            }

                            HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = medi.Amount;
                            exp.MEDICINE_ID = medi.MedicineId;
                            exp.NUM_ORDER = medi.NumOrder;
                            exp.DESCRIPTION = medi.Description;
                            exp.PRICE = medi.Price;
                            exp.TDL_MEDICINE_TYPE_ID = beans[0].TDL_MEDICINE_TYPE_ID;
                            exp.VAT_RATIO = medi.VatRatio;
                            exp.PRICE = medi.Price;
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

                        HisExpMestMedicineCreate mediCreate = new HisExpMestMedicineCreate(param);
                        this.medicineCreates.Add(mediCreate);

                        if (!mediCreate.CreateList(approves))
                        {
                            throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                        }

                        this.SqlUpdateBean(medicineDic, ref sqls);

                        expMestMetyReqs.AddRange(requests);
                        medicines.AddRange(approves);
                    }
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
            if (IsNotNullOrEmpty(this.beanSplits))
            {
                foreach (var spliter in this.beanSplits.AsEnumerable().Reverse())
                {
                    try
                    {
                        spliter.RollBack();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }

            if (IsNotNullOrEmpty(this.medicineCreates))
            {
                foreach (var creater in this.medicineCreates.AsEnumerable().Reverse())
                {
                    try
                    {
                        creater.RollbackData();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }

            if (IsNotNullOrEmpty(this.metyReqMakers))
            {
                foreach (var maker in this.metyReqMakers.AsEnumerable().Reverse())
                {
                    try
                    {
                        maker.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
            }
        }
    }
}
