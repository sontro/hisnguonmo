using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.RecoverNotTaken
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisMedicineBeanSplit> beanSpliters;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beanSpliters = new List<HisMedicineBeanSplit>();
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        Dictionary<ReqMedicineData, List<HIS_EXP_MEST_MEDICINE>> dicReqMedicine = new Dictionary<ReqMedicineData, List<HIS_EXP_MEST_MEDICINE>>();

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> olds, ref ResultMedicineData resultData, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(olds))
                {
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    List<HIS_MEDICINE_PATY> medicinePaties = null;
                    List<ReqMedicineData> reqData = this.MakeReqMedicineData(olds, expMest);

                    List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(reqData, expMest, ref newMedicineDic, ref medicinePaties);

                    List<V_HIS_MEDICINE_2> medicine2s = new HisMedicineGet().GetView2ByIds(news.Select(s => s.MEDICINE_ID.Value).Distinct().ToList());

                    List<HIS_EXP_MEST_MEDICINE> inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<HIS_EXP_MEST_MEDICINE>> dicDelete = new Dictionary<HIS_EXP_MEST_MEDICINE, List<HIS_EXP_MEST_MEDICINE>>();
                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();

                    this.GetDiff(olds, news, ref inserts, ref dicDelete, ref updates, ref beforeUpdates, ref newMedicineDic);

                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMedicineCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisExpMestMedicineUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteIds = (dicDelete.Count > 0) ? dicDelete.Select(s => s.Key.ID).ToList() : null;

                    //Cap nhat thong tin bean
                    this.SqlUpdateBean(newMedicineDic, ref sqls);

                    //Xoa cac exp_mest_medicine ko dung.
                    //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMedicine(deleteIds, ref sqls);

                    resultData = new ResultMedicineData();
                    resultData.Inserts = inserts;
                    resultData.Updates = updates;
                    resultData.Befores = beforeUpdates;
                    resultData.DicDelete = dicDelete;
                    resultData.Medicines = medicine2s;
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

        private List<ReqMedicineData> MakeReqMedicineData(List<HIS_EXP_MEST_MEDICINE> olds, HIS_EXP_MEST expMest)
        {
            List<ReqMedicineData> reqData = new List<ReqMedicineData>();

            int index = 0;

            foreach (HIS_EXP_MEST_MEDICINE exp in olds)
            {
                ReqMedicineData req = new ReqMedicineData(expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, expMest.TDL_INTRUCTION_TIME.Value, ++index);
                req.Afternoon = exp.AFTERNOON;
                req.Amount = exp.AMOUNT;
                req.Evening = exp.EVENING;
                req.HtuId = exp.HTU_ID;
                req.IsBedExpend = (exp.EXPEND_TYPE_ID.HasValue && exp.EXPEND_TYPE_ID.Value > 0);
                req.IsExpend = (exp.IS_EXPEND == Constant.IS_TRUE);
                req.IsOutParentFee = (exp.IS_OUT_PARENT_FEE == Constant.IS_TRUE);
                req.MedicineTypeId = exp.TDL_MEDICINE_TYPE_ID.Value;
                req.Morning = exp.MORNING;
                req.Noon = exp.NOON;
                req.NumOrder = exp.NUM_ORDER;
                req.PatientTypeId = exp.PATIENT_TYPE_ID.Value;
                req.SereServParentId = exp.SERE_SERV_PARENT_ID;
                req.Speed = exp.SPEED;
                req.PreviousUsingCount = exp.PREVIOUS_USING_COUNT;
                req.Tutorial = exp.TUTORIAL;
                req.UseOriginalUnitForPres = (exp.USE_ORIGINAL_UNIT_FOR_PRES == Constant.IS_TRUE);
                req.UseTimeTo = exp.USE_TIME_TO;
                req.ExpMestMedicineId = exp.ID;
                req.PriorityMedicineId = exp.MEDICINE_ID;
                reqData.Add(req);
            }
            return reqData;
        }

        private List<HIS_EXP_MEST_MEDICINE> MakeData(List<ReqMedicineData> reqData, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();

            //Dictionary luu exp_mest_medicine va d/s bean tuong ung
            medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

            //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
            //  + 2 req co loai thuoc giong nhau thi can thuoc 2 lenh tach bean khac nhau
            //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau (cai nay ko
            // can xu ly vi sua don thuoc chi cho phep ke 1 kho)
            //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_medicine tuong ung

            List<long> useBeandIds = new List<long>();

            while (reqData != null && reqData.Count > 0)
            {
                List<ReqMedicineData> toSplits = new List<ReqMedicineData>();
                foreach (ReqMedicineData s in reqData)
                {
                    if (!toSplits.Exists(t => t.MedicineTypeId == s.MedicineTypeId))
                    {
                        toSplits.Add(s);
                    }
                }
                reqData.RemoveAll(o => toSplits.Exists(t => t.Id == o.Id));

                //Thuc hien lenh tach bean
                List<HIS_EXP_MEST_MEDICINE> data = this.SplitBeanAndMakeData(toSplits, expMest, useBeandIds, ref medicineDic, ref medicinePaties);
                if (IsNotNullOrEmpty(data))
                {
                    expMestMedicines.AddRange(data);
                }
            }
            return expMestMedicines;
        }

        /// <summary>
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(List<ReqMedicineData> toSplits, HIS_EXP_MEST expMest, List<long> useBeandIds, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic, ref List<HIS_MEDICINE_PATY> medicinePaties)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;
            List<ExpMedicineTypeSDO> reqSplits = new List<ExpMedicineTypeSDO>();
            foreach (ReqMedicineData req in toSplits)
            {
                ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                sdo.Amount = req.Amount;
                sdo.MedicineTypeId = req.MedicineTypeId;
                sdo.PatientTypeId = req.PatientTypeId;
                sdo.PriorityMedicineId = req.PriorityMedicineId;
                reqSplits.Add(sdo);
            }
            HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
            this.beanSpliters.Add(spliter);

            //Truyen vao "useMedicineBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
            //(trong lan tach bean truoc do - trong cung 1 lan ke don)
            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?)expMest.TDL_INTRUCTION_TIME : null;

            if (!spliter.SplitByMedicineType(reqSplits, expMest.MEDI_STOCK_ID, expiredDate, null, useBeandIds, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            if (IsNotNullOrEmpty(medicineBeans))
            {
                //Cap nhat lai danh sach bean da duoc dung
                useBeandIds.AddRange(medicineBeans.Select(o => o.ID).ToList());

                List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

                //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
                foreach (ReqMedicineData req in toSplits)
                {
                    //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                    List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans.Where(o => o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId).ToList();

                    if (!IsNotNullOrEmpty(reqBeans))
                    {
                        throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                    }

                    var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });
                    foreach (var tmp in group)
                    {
                        List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
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

                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.NUM_ORDER = req.NumOrder;
                        exp.SPEED = req.Speed;
                        exp.PREVIOUS_USING_COUNT = req.PreviousUsingCount;
                        exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                        exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.PATIENT_TYPE_ID = req.PatientTypeId;
                        exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                        exp.TUTORIAL = req.Tutorial;
                        exp.MORNING = req.Morning;
                        exp.NOON = req.Noon;
                        exp.AFTERNOON = req.Afternoon;
                        exp.EVENING = req.Evening;
                        exp.HTU_ID = req.HtuId;
                        exp.USE_ORIGINAL_UNIT_FOR_PRES = req.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;
                        exp.USE_TIME_TO = req.UseTimeTo;

                        data.Add(exp);

                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                        if (!dicReqMedicine.ContainsKey(req))
                        {
                            dicReqMedicine[req] = new List<HIS_EXP_MEST_MEDICINE>();
                        }
                        dicReqMedicine[req].Add(exp);
                    }
                }
                return data;
            }

            return null;
        }


        private void GetDiff(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> news, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<HIS_EXP_MEST_MEDICINE>> dicDelete, ref List<HIS_EXP_MEST_MEDICINE> updates, ref List<HIS_EXP_MEST_MEDICINE> oldOfUpdates, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic)
        {

            Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();

            foreach (var dic in this.dicReqMedicine)
            {
                HIS_EXP_MEST_MEDICINE old = olds.FirstOrDefault(o => o.ID == dic.Key.ExpMestMedicineId);

                if (dic.Value.Count == 1)
                {
                    HIS_EXP_MEST_MEDICINE newExp = dic.Value[0];
                    HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                    old.MEDICINE_ID = newExp.MEDICINE_ID;
                    old.PRICE = newExp.PRICE;
                    old.VAT_RATIO = newExp.VAT_RATIO;
                    oldOfUpdates.Add(oldOfUpdate);
                    updates.Add(old);
                    List<long> beanIds = newMedicineDic[newExp];
                    newMedicineDic.Remove(newExp);
                    newMedicineDic.Add(old, beanIds);
                }
                else
                {
                    dic.Value.ForEach(o =>
                        {
                            o.APPROVAL_LOGINNAME = old.APPROVAL_LOGINNAME;
                            o.APPROVAL_TIME = old.APPROVAL_TIME;
                            o.APPROVAL_USERNAME = old.APPROVAL_USERNAME;
                            o.APPROVAL_DATE = old.APPROVAL_DATE;
                        });
                    inserts.AddRange(dic.Value);
                    dicDelete[old] = dic.Value;
                }
            }
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(newMedicineDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine
                foreach (HIS_EXP_MEST_MEDICINE key in newMedicineDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, key.ID);
                        sqls.Add(query);
                    }
                }
            }
        }

        private void SqlDeleteExpMestMedicine(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.beanSpliters))
                {
                    foreach (HisMedicineBeanSplit spliter in this.beanSpliters)
                    {
                        spliter.RollBack();
                    }
                }
                this.hisExpMestMedicineUpdate.RollbackData();
                this.hisExpMestMedicineCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }

    class ReqMedicineData : PresMedicineSDO
    {
        public int Id { get; set; } //định danh dữ liệu
        public long InstructionTime { get; set; }
        public long ExpMestId { get; set; }
        public long ServiceReqId { get; set; }
        public long TreatmentId { get; set; }
        public long? UseTimeTo { get; set; }
        public long ExpMestMedicineId { get; set; }
        public long? PriorityMedicineId { get; set; }

        public ReqMedicineData(long expMestId, long serviceReqId, long treatmentId, long instructionTime, int id)
        {
            this.ExpMestId = expMestId;
            this.InstructionTime = instructionTime;
            this.Id = id;
            this.ServiceReqId = serviceReqId;
            this.TreatmentId = treatmentId;
        }
    }

    class ResultMedicineData
    {
        public List<HIS_EXP_MEST_MEDICINE> Inserts { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Updates { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Befores { get; set; }
        public Dictionary<HIS_EXP_MEST_MEDICINE, List<HIS_EXP_MEST_MEDICINE>> DicDelete { get; set; }
        public List<V_HIS_MEDICINE_2> Medicines { get; set; }
    }
}
