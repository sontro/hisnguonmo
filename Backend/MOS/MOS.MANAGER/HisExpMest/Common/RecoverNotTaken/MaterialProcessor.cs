using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.RecoverNotTaken
{
    class MaterialProcessor : BusinessBase
    {
        private List<HisMaterialBeanSplit> beanSpliters;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beanSpliters = new List<HisMaterialBeanSplit>();
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        Dictionary<ReqMaterialData, List<HIS_EXP_MEST_MATERIAL>> dicReqMaterial = new Dictionary<ReqMaterialData, List<HIS_EXP_MEST_MATERIAL>>();

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> olds, ref ResultMaterialData resultData, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(olds))
                {
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                    List<HIS_MATERIAL_PATY> medicinePaties = null;
                    List<ReqMaterialData> reqData = this.MakeReqMaterialData(olds, expMest);

                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(reqData, expMest, ref newMaterialDic, ref medicinePaties);


                    List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<HIS_EXP_MEST_MATERIAL>> dicDelete = new Dictionary<HIS_EXP_MEST_MATERIAL, List<HIS_EXP_MEST_MATERIAL>>();
                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();

                    this.GetDiff(olds, news, ref inserts, ref dicDelete, ref updates, ref beforeUpdates, ref newMaterialDic);

                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMaterialCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisExpMestMaterialUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteIds = (dicDelete.Count > 0) ? dicDelete.Select(s => s.Key.ID).ToList() : null;

                    //Cap nhat thong tin bean
                    this.SqlUpdateBean(newMaterialDic, ref sqls);

                    //Xoa cac exp_mest_medicine ko dung.
                    //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMaterial(deleteIds, ref sqls);

                    resultData = new ResultMaterialData();
                    resultData.Inserts = inserts;
                    resultData.Updates = updates;
                    resultData.Befores = beforeUpdates;
                    resultData.DicDelete = dicDelete;
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

        private List<ReqMaterialData> MakeReqMaterialData(List<HIS_EXP_MEST_MATERIAL> olds, HIS_EXP_MEST expMest)
        {
            List<ReqMaterialData> reqData = new List<ReqMaterialData>();

            int index = 0;

            foreach (HIS_EXP_MEST_MATERIAL exp in olds)
            {
                ReqMaterialData req = new ReqMaterialData(expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, expMest.TDL_INTRUCTION_TIME.Value, ++index);
                req.Amount = exp.AMOUNT;
                req.IsBedExpend = (exp.EXPEND_TYPE_ID.HasValue && exp.EXPEND_TYPE_ID.Value > 0);
                req.IsExpend = (exp.IS_EXPEND == Constant.IS_TRUE);
                req.IsOutParentFee = (exp.IS_OUT_PARENT_FEE == Constant.IS_TRUE);
                req.MaterialTypeId = exp.TDL_MATERIAL_TYPE_ID.Value;
                req.NumOrder = exp.NUM_ORDER;
                req.PatientTypeId = exp.PATIENT_TYPE_ID.Value;
                req.SereServParentId = exp.SERE_SERV_PARENT_ID;
                req.Tutorial = exp.TUTORIAL;
                req.ExpMestMaterialId = exp.ID;
                req.PriorityMaterialId = exp.MATERIAL_ID;
                reqData.Add(req);
            }
            return reqData;
        }

        private List<HIS_EXP_MEST_MATERIAL> MakeData(List<ReqMaterialData> reqData, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> medicineDic, ref List<HIS_MATERIAL_PATY> medicinePaties)
        {
            List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

            //Dictionary luu exp_mest_medicine va d/s bean tuong ung
            medicineDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

            //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
            //  + 2 req co loai thuoc giong nhau thi can thuoc 2 lenh tach bean khac nhau
            //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau (cai nay ko
            // can xu ly vi sua don thuoc chi cho phep ke 1 kho)
            //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_medicine tuong ung

            List<long> useBeandIds = new List<long>();

            while (reqData != null && reqData.Count > 0)
            {
                List<ReqMaterialData> toSplits = new List<ReqMaterialData>();
                foreach (ReqMaterialData s in reqData)
                {
                    if (!toSplits.Exists(t => t.MaterialTypeId == s.MaterialTypeId))
                    {
                        toSplits.Add(s);
                    }
                }
                reqData.RemoveAll(o => toSplits.Exists(t => t.Id == o.Id));

                //Thuc hien lenh tach bean
                List<HIS_EXP_MEST_MATERIAL> data = this.SplitBeanAndMakeData(toSplits, expMest, useBeandIds, ref medicineDic, ref medicinePaties);
                if (IsNotNullOrEmpty(data))
                {
                    expMestMaterials.AddRange(data);
                }
            }
            return expMestMaterials;
        }

        /// <summary>
        /// Tach bean theo ReqMaterialData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMaterialData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MATERIAL> SplitBeanAndMakeData(List<ReqMaterialData> toSplits, HIS_EXP_MEST expMest, List<long> useBeandIds, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> medicineDic, ref List<HIS_MATERIAL_PATY> medicinePaties)
        {
            List<HIS_MATERIAL_BEAN> medicineBeans = null;
            List<ExpMaterialTypeSDO> reqSplits = new List<ExpMaterialTypeSDO>();
            foreach (ReqMaterialData req in toSplits)
            {
                ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                sdo.Amount = req.Amount;
                sdo.MaterialTypeId = req.MaterialTypeId;
                sdo.PatientTypeId = req.PatientTypeId;
                sdo.PriorityMaterialId = req.PriorityMaterialId;
                reqSplits.Add(sdo);
            }
            HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
            this.beanSpliters.Add(spliter);

            //Truyen vao "useMaterialBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
            //(trong lan tach bean truoc do - trong cung 1 lan ke don)
            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?)expMest.TDL_INTRUCTION_TIME : null;

            if (!spliter.SplitByMaterialType(reqSplits, expMest.MEDI_STOCK_ID, expiredDate, null, useBeandIds, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            if (IsNotNullOrEmpty(medicineBeans))
            {
                //Cap nhat lai danh sach bean da duoc dung
                useBeandIds.AddRange(medicineBeans.Select(o => o.ID).ToList());

                List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

                //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
                foreach (ReqMaterialData req in toSplits)
                {
                    //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                    List<HIS_MATERIAL_BEAN> reqBeans = medicineBeans.Where(o => o.TDL_MATERIAL_TYPE_ID == req.MaterialTypeId).ToList();

                    if (!IsNotNullOrEmpty(reqBeans))
                    {
                        throw new Exception("Ko tach duoc bean tuong ung voi MaterialTypeId:" + req.MaterialTypeId);
                    }

                    var group = reqBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_IMP_PRICE, o.TDL_MATERIAL_IMP_VAT_RATIO });
                    foreach (var tmp in group)
                    {
                        List<HIS_MATERIAL_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                        exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                        exp.TDL_MATERIAL_TYPE_ID = req.MaterialTypeId;
                        exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;

                        //Neu ban bang gia nhap
                        if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = tmp.Key.TDL_MATERIAL_IMP_PRICE;
                            exp.VAT_RATIO = tmp.Key.TDL_MATERIAL_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MATERIAL_ID == tmp.Key.MATERIAL_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MATERIAL_ID + "va patient_type_id: " + req.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }

                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.NUM_ORDER = req.NumOrder;
                        exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                        exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.PATIENT_TYPE_ID = req.PatientTypeId;
                        exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                        exp.TUTORIAL = req.Tutorial;

                        data.Add(exp);

                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                        if (!dicReqMaterial.ContainsKey(req))
                        {
                            dicReqMaterial[req] = new List<HIS_EXP_MEST_MATERIAL>();
                        }
                        dicReqMaterial[req].Add(exp);
                    }
                }
                return data;
            }

            return null;
        }

        private void GetDiff(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> news, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<HIS_EXP_MEST_MATERIAL>> dicDelete, ref List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> oldOfUpdates, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic)
        {

            Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();

            foreach (var dic in this.dicReqMaterial)
            {
                HIS_EXP_MEST_MATERIAL old = olds.FirstOrDefault(o => o.ID == dic.Key.ExpMestMaterialId);

                if (dic.Value.Count == 1)
                {
                    HIS_EXP_MEST_MATERIAL newExp = dic.Value[0];
                    HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                    old.MATERIAL_ID = newExp.MATERIAL_ID;
                    old.PRICE = newExp.PRICE;
                    old.VAT_RATIO = newExp.VAT_RATIO;
                    oldOfUpdates.Add(oldOfUpdate);
                    updates.Add(old);
                    List<long> beanIds = newMaterialDic[newExp];
                    newMaterialDic.Remove(newExp);
                    newMaterialDic.Add(old, beanIds);
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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(newMaterialDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine
                foreach (HIS_EXP_MEST_MATERIAL key in newMaterialDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(newMaterialDic[key], "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, key.ID);
                        sqls.Add(query);
                    }
                }
            }
        }

        private void SqlDeleteExpMestMaterial(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.beanSpliters))
                {
                    foreach (HisMaterialBeanSplit spliter in this.beanSpliters)
                    {
                        spliter.RollBack();
                    }
                }
                this.hisExpMestMaterialUpdate.RollbackData();
                this.hisExpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }

    class ReqMaterialData : PresMaterialSDO
    {
        public int Id { get; set; } //định danh dữ liệu
        public long InstructionTime { get; set; }
        public long ExpMestId { get; set; }
        public long ServiceReqId { get; set; }
        public long TreatmentId { get; set; }
        public long? UseTimeTo { get; set; }
        public long ExpMestMaterialId { get; set; }
        public long? PriorityMaterialId { get; set; }

        public ReqMaterialData(long expMestId, long serviceReqId, long treatmentId, long instructionTime, int id)
        {
            this.ExpMestId = expMestId;
            this.InstructionTime = instructionTime;
            this.Id = id;
            this.ServiceReqId = serviceReqId;
            this.TreatmentId = treatmentId;
        }
    }


    class ResultMaterialData
    {
        public List<HIS_EXP_MEST_MATERIAL> Inserts { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> Updates { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> Befores { get; set; }
        public Dictionary<HIS_EXP_MEST_MATERIAL, List<HIS_EXP_MEST_MATERIAL>> DicDelete { get; set; }
    }
}
