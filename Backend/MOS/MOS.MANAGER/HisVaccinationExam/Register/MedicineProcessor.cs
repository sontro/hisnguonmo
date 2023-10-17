using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisMedicineBeanSplit> beanSpliters;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.beanSpliters = new List<HisMedicineBeanSplit>();
        }

        internal bool Run(List<VaccinationMetySDO> vaccinationMeties, List<HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(vaccinationMeties) && IsNotNullOrEmpty(expMests))
                {
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
                    // 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau
                    //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_medicine tuong ung
                    var groups = vaccinationMeties.GroupBy(o => o.MediStockId);
                    foreach (var group in groups)
                    {
                        List<VaccinationMetySDO> toSplits = group.ToList();
                        //Thuc hien lenh tach bean
                        List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(toSplits, vaccinations, expMests, ref medicineDic);
                        if (IsNotNullOrEmpty(data))
                        {
                            expMestMedicines.AddRange(data);
                        }
                    }

                    if (IsNotNullOrEmpty(expMestMedicines) && !this.hisExpMestMedicineCreate.CreateList(expMestMedicines))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);
                    resultData = expMestMedicines;
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

        /// <summary>
        /// Tach bean theo VaccinationMetySDO va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach VaccinationMetySDO dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(List<VaccinationMetySDO> toSplits, List<HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;
            List<ExpMedicineTypeSDO> reqSplits = toSplits.Select(o => new ExpMedicineTypeSDO
            {
                Amount = o.Amount,
                MedicineTypeId = o.MedicineTypeId,
                PatientTypeId = o.PatientTypeId
            }).ToList();

            HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
            this.beanSpliters.Add(spliter);

            List<HIS_MEDICINE_PATY> medicinePaties = null;

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? Inventec.Common.DateTime.Get.StartDay() : null;

            if (!spliter.SplitByMedicineType(reqSplits, toSplits[0].MediStockId, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
            foreach (VaccinationMetySDO req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans.Where(o => o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId).ToList();

                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                }

                var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    //Luu y: vaccination, exp_mest duoc tao theo group medi_stock_id va patient_type_id
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == req.MediStockId).FirstOrDefault();
                    HIS_VACCINATION vaccination = vaccinations
                        .Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId 
                            && o.EXECUTE_ROOM_ID == mediStock.ROOM_ID).FirstOrDefault();
                    HIS_EXP_MEST expMest = expMests.Where(o => o.VACCINATION_ID == vaccination.ID).FirstOrDefault();

                    List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.TDL_VACCINATION_ID = expMest.VACCINATION_ID;
                    exp.VACCINATION_RESULT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_RESULT.ID__UNINJECT;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MEDICINE_TYPE_ID = req.MedicineTypeId;
                    exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;

                    //Neu ban bang gia nhap
                    if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                    {
                        exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                        exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                    }
                    else
                    {
                        HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + req.PatientTypeId);
                        }
                        exp.PRICE = paty.EXP_PRICE;
                        exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                    }

                    exp.TDL_MEDI_STOCK_ID = req.MediStockId;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;
                    
                    medicines.Add(exp);
                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }

                data.AddRange(medicines);
            }
            return data;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
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
            if (IsNotNullOrEmpty(this.beanSpliters))
            {
                foreach (HisMedicineBeanSplit spliter in this.beanSpliters)
                {
                    spliter.RollBack();
                }
            }

            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
