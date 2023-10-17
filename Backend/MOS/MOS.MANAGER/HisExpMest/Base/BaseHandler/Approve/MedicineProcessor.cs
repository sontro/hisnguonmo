using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Approve
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMetyReqIncreaseDdAmount hisExpMestMetyReqIncreaseDdAmount;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMetyReqIncreaseDdAmount = new HisExpMestMetyReqIncreaseDdAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> metyReqs, List<ExpMedicineTypeSDO> medicineSDOs, string loginname, string username, long approvalTime, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(medicineSDOs) && IsNotNullOrEmpty(metyReqs))
                {
                    List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    List<HIS_MEDICINE_BEAN> medicineBeans = null;
                    List<HIS_MEDICINE_PATY> medicinePaties = null;

                    long? expiredDate = (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && expMest.CHMS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION) ? (long?)approvalTime : null;

                    if (!hisMedicineBeanSplit.SplitByMedicineType(medicineSDOs, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
                    {
                        return false;
                    }

                    var group = medicineBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID });
                    foreach (var tmp in group)
                    {
                        ExpMedicineTypeSDO sdo = medicineSDOs.Where(o => o.MedicineTypeId == tmp.Key.TDL_MEDICINE_TYPE_ID).FirstOrDefault();

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

                    if (IsNotNullOrEmpty(data) && !this.hisExpMestMedicineCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(medicineDic, ref sqls);

                    this.ProcessDdAmount(metyReqs, data);

                    expMestMedicines = data;
                }
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

        private void ProcessDdAmount(List<HIS_EXP_MEST_METY_REQ> metyReqs, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (!IsNotNullOrEmpty(metyReqs) || !IsNotNullOrEmpty(expMestMedicines)) return;

            Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();

            //cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_METY_REQ req in metyReqs)
            {
                decimal approvalAmount = expMestMedicines.Where(o => o.EXP_MEST_METY_REQ_ID == req.ID).Sum(o => o.AMOUNT);
                if (approvalAmount > 0)
                {
                    increaseDic.Add(req.ID, approvalAmount);
                }
            }
            if (IsNotNullOrEmpty(increaseDic))
            {
                if (!this.hisExpMestMetyReqIncreaseDdAmount.Run(increaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMetyReqIncreaseDdAmount.Rollback();
            this.hisMedicineBeanSplit.RollBack();
            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
