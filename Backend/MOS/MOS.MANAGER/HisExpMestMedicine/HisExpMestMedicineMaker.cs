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

namespace MOS.MANAGER.HisExpMestMedicine
{
    class HisExpMestMedicineMaker : BusinessBase
    {
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private List<HisMedicineBeanSplit> beanSplitors = new List<HisMedicineBeanSplit>();
        private HisMedicineBeanSplitPlus beanSpliterPlus;

        internal HisExpMestMedicineMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.beanSpliterPlus = new HisMedicineBeanSplitPlus(param);
        }

        internal bool Run(List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, long? expiredDate, string loginname, string username, long? approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                    {
                        Dictionary<ExpMedicineTypeSDO, List<HIS_MEDICINE_BEAN>> dicBeans = null;
                        List<HIS_MEDICINE_PATY> medicinePaties = null;

                        if (isAuto && HisExpMestCFG.ALLOW_APPROVE_LESS_THAN_REQUEST)
                        {
                            if (!this.beanSpliterPlus.SplitAndDecreaseByMedicineType(medicines, expMest.MEDI_STOCK_ID, expiredDate, null, ref dicBeans, ref medicinePaties))
                            {
                                return false;
                            }
                            if (!IsNotNullOrEmpty(dicBeans))
                            {
                                LogSystem.Info("Khong co thuoc nao du kha dung de duyet");
                                return true;
                            }
                        }
                        else
                        {
                            if (!this.beanSpliterPlus.SplitByMedicineType(medicines, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref dicBeans, ref medicinePaties))
                            {
                                return false;
                            }
                        }
                        foreach (var dic in dicBeans)
                        {
                            ExpMedicineTypeSDO sdo = dic.Key;
                            List<HIS_MEDICINE_BEAN> medicineBeans = dic.Value;
                            var GBeans = medicineBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID });
                            foreach (var tmp in GBeans)
                            {
                                List<HIS_MEDICINE_BEAN> beans = tmp.ToList();

                                HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                                exp.EXP_MEST_ID = expMest.ID;
                                exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                                exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;
                                exp.NUM_ORDER = sdo.NumOrder;
                                exp.DESCRIPTION = sdo.Description;
                                exp.EXP_MEST_METY_REQ_ID = sdo.ExpMestMetyReqId;
                                exp.PRICE = sdo.Price;
                                exp.TDL_MEDICINE_TYPE_ID = beans[0].TDL_MEDICINE_TYPE_ID;
                                exp.VAT_RATIO = sdo.VatRatio;
                                exp.PRICE = sdo.Price;
                                exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                                exp.APPROVAL_LOGINNAME = loginname;
                                exp.APPROVAL_TIME = approvalTime;
                                exp.APPROVAL_USERNAME = username;
                                exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                                exp.IS_NOT_PRES = sdo.IsNotPres;
                                exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                                exp.TDL_TREATMENT_ID = sdo.TreatmentId;
                                data.Add(exp);
                                medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                            }
                        }
                    }
                    else
                    {
                        List<HIS_MEDICINE_BEAN> medicineBeans = null;
                        List<HIS_MEDICINE_PATY> medicinePaties = null;
                        HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
                        this.beanSplitors.Add(spliter);
                        if (!spliter.SplitByMedicineType(medicines, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
                        {
                            return false;
                        }

                        var group = medicineBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID });
                        foreach (var tmp in group)
                        {
                            ExpMedicineTypeSDO sdo = medicines.Where(o => o.MedicineTypeId == tmp.Key.TDL_MEDICINE_TYPE_ID).FirstOrDefault();

                            List<HIS_MEDICINE_BEAN> beans = tmp.ToList();

                            HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                            exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;
                            exp.NUM_ORDER = sdo.NumOrder;
                            exp.DESCRIPTION = sdo.Description;
                            exp.EXP_MEST_METY_REQ_ID = sdo.ExpMestMetyReqId;
                            exp.PRICE = sdo.Price;
                            exp.TDL_MEDICINE_TYPE_ID = beans[0].TDL_MEDICINE_TYPE_ID;
                            exp.VAT_RATIO = sdo.VatRatio;
                            exp.PRICE = sdo.Price;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_TIME = approvalTime;
                            exp.APPROVAL_USERNAME = username;
                            exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                            exp.IS_NOT_PRES = sdo.IsNotPres;
                            exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                            data.Add(exp);
                            medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                        }
                    }

                    if (IsNotNullOrEmpty(data) && !this.hisExpMestMedicineCreate.CreateListSql(data))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }
                    this.SqlUpdateBean(medicineDic, ref sqls);

                    resultData = data;
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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMedicine.ID);
                    sqls.Add(query);
                }
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSplitors))
            {
                foreach (var spliter in this.beanSplitors)
                {
                    spliter.RollBack();
                }
            }
            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
