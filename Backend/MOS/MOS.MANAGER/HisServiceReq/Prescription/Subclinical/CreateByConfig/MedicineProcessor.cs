using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
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

        internal bool Run(HIS_EXP_MEST expMest, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, HIS_SERE_SERV sereServ, List<HIS_SERVICE_METY> serviceMeties, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<V_HIS_MEDICINE_2> choosenMedicines, ref List<string> sqls)
        {
            try
            {
                bool isExpend = HisSereServCFG.AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION == HisSereServCFG.SetExpendForAutoExpendPresOption.BHYT && sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || HisSereServCFG.AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION == HisSereServCFG.SetExpendForAutoExpendPresOption.ALL;

                List<PresMedicineSDO> medicines = new List<PresMedicineSDO>();

                if (IsNotNullOrEmpty(serviceMeties))
                {
                    foreach(HIS_SERVICE_METY m in serviceMeties)
                    {
                        //Chi lay cac thuoc duoc thiet lap den dich vu duoc ke tieu hao
                        if (m.SERVICE_ID == sereServ.SERVICE_ID && m.IS_ACTIVE == Constant.IS_TRUE)
                        {
                            HIS_MEDICINE_TYPE mt = HisMedicineTypeCFG.DATA.Where(o => o.ID == m.MEDICINE_TYPE_ID).FirstOrDefault();
                            PresMedicineSDO sdo = new PresMedicineSDO();
                            sdo.InstructionTimes = new List<long>() { expMest.TDL_INTRUCTION_TIME.Value };
                            sdo.IsExpend = isExpend;
                            sdo.MedicineTypeId = m.MEDICINE_TYPE_ID;
                            sdo.MedicineUseFormId = mt.MEDICINE_USE_FORM_ID;
                            sdo.MediStockId = expMest.MEDI_STOCK_ID;
                            sdo.PatientTypeId = sereServ.PATIENT_TYPE_ID;
                            sdo.SereServParentId = sereServ.ID;
                            sdo.Amount = m.EXPEND_AMOUNT * sereServ.AMOUNT; //lay so luong dinh muc nhan voi so luong dich vu
                            medicines.Add(sdo);
                        }
                    }
                }

                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    List<HIS_MEDICINE_PATY> medicinePaties = new List<HIS_MEDICINE_PATY>();

                    //Thuc hien lenh tach bean
                    List<HIS_MEDICINE_PATY> paties = null;
                    List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(expMest.TDL_INTRUCTION_TIME.Value, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, processPackage37, processPackageBirth, processPackagePttm, medicines, ref medicineDic, ref paties);
                    if (IsNotNullOrEmpty(data))
                    {
                        expMestMedicines.AddRange(data);
                        if (IsNotNullOrEmpty(paties))
                        {
                            medicinePaties.AddRange(paties);
                        }
                    }

                    List<long> medicineIds = expMestMedicines.Where(o => o.MEDICINE_ID.HasValue).Select(o => o.MEDICINE_ID.Value).ToList();
                    choosenMedicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMedicineWithBidDate(choosenMedicines, expMestMedicines, new List<HIS_EXP_MEST>() { expMest }))
                    {
                        throw new Exception("IsValidMedicineWithBidDate false. Rollback du lieu");
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
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(long instructionTime, long expMestId, long serviceReqId, long treatmentId, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMedicineSDO> toSplits, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
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

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?) instructionTime : null;
            if (!spliter.SplitByMedicineType(reqSplits, toSplits[0].MediStockId, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }
            
            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
            foreach (PresMedicineSDO req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                //Neu ke theo lo thi can cu vao medicine_id
                List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans
                    .Where(o => (o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId && !req.MedicineId.HasValue) || (o.MEDICINE_ID == req.MedicineId))
                    .ToList();

                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                }

                var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMestId;
                    exp.TDL_SERVICE_REQ_ID = serviceReqId;
                    exp.TDL_TREATMENT_ID = treatmentId;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MEDICINE_TYPE_ID = req.MedicineTypeId;
                    exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;
                    exp.USE_ORIGINAL_UNIT_FOR_PRES = req.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;

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
                    exp.NUM_ORDER = req.NumOrder;
                    exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                    exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                    medicines.Add(exp);
                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }

                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                processPackage37.Apply3Day7Day(null, null, medicines, instructionTime);
                //Xu ly de ap dung goi de
                processPackageBirth.Run(medicines, req.SereServParentId);
                //Xu ly de ap dung goi phau thuat tham my
                processPackagePttm.Run(medicines, req.SereServParentId, instructionTime);

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
