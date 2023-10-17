using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create.SaleExpMest
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
        }

        internal bool Run(List<PresOutStockMetySDO> serviceReqMeties, HIS_EXP_MEST expMest, long patientTypeId, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReqMeties) && expMest != null)
                {
                    List<ExpMedicineTypeSDO> medicines = serviceReqMeties.Where(o => o.MedicineTypeId.HasValue)
                        .Select(o => new ExpMedicineTypeSDO
                        {
                            Amount = o.Amount,
                            NumOrder = o.NumOrder,
                            PatientTypeId = patientTypeId,
                            MedicineTypeId = o.MedicineTypeId.Value,
                            Tutorial = o.Tutorial,
                            Morning = o.Morning,
                            Noon = o.Noon,
                            Afternoon = o.Afternoon,
                            Evening = o.Evening,
                            HtuId = o.HtuId,
                            PresAmount = o.PresAmount,
                            UseTimeTo = o.UseTimeTo
                        }).ToList();

                    List<HIS_MEDICINE_BEAN> medicineBeans = null;
                    List<HIS_MEDICINE_PATY> medicinePaties = null;

                    long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? expMest.TDL_INTRUCTION_TIME : null; //lay TDL_INTRUCTION_TIME chu ko lay TDL_INTRUCTION_DATE, vi truong nay do trigger trong DB xu ly --> tai thoi diem nay, chua co gia tri
                    if (!this.hisMedicineBeanSplit.SplitByMedicineType(medicines, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
                    {
                        return false;
                    }

                    List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung

                    foreach (ExpMedicineTypeSDO sdo in medicines)
                    {
                        var group = medicineBeans
                            .Where(o => o.TDL_MEDICINE_TYPE_ID == sdo.MedicineTypeId)
                            .GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });

                        foreach (var tmp in group)
                        {
                            List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                            HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                            exp.TDL_MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                            exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;

                            //Neu ban bang gia nhap
                            if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                                exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                            }
                            else
                            {
                                HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ?
                                    medicinePaties.Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                                if (paty == null)
                                {
                                    throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + sdo.PatientTypeId);
                                }
                                exp.PRICE = paty.EXP_PRICE;
                                exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                            }

                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.NUM_ORDER = sdo.NumOrder;
                            exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                            exp.USE_TIME_TO = sdo.UseTimeTo;
                            exp.TUTORIAL = sdo.Tutorial;
                            exp.MORNING = sdo.Morning;
                            exp.NOON = sdo.Noon;
                            exp.AFTERNOON = sdo.Afternoon;
                            exp.EVENING = sdo.Evening;
                            exp.HTU_ID = sdo.HtuId;
                            exp.PRES_AMOUNT = sdo.PresAmount;
                            data.Add(exp);
                            medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                        }
                    }

                    if (!this.hisExpMestMedicineCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);

                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
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
