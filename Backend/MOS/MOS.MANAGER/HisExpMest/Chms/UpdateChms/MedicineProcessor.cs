using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.UpdateChms
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private HisExpMestMetyReqUpdate hisExpMestMetyReqUpdate;


        List<HIS_EXP_MEST_METY_REQ> deletes = null;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.hisExpMestMetyReqUpdate = new HisExpMestMetyReqUpdate(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HisExpMestChmsSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> resultMetyReq, ref List<HIS_EXP_MEST_MEDICINE> resultMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_METY_REQ> requests = null;
                List<HIS_EXP_MEST_MEDICINE> approves = null;
                List<HIS_EXP_MEST_METY_REQ> oldExpMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMest.ID);
                if (expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST_MEDICINE> oldExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                    this.ProcessExpMestMedicine(sdo, expMest, oldExpMestMetyReqs, oldExpMestMedicines, ref requests, ref approves, ref sqls);
                    this.GenerateSqlDeleteExpMestReq(ref sqls);
                    resultMetyReq = requests;
                    resultMedicines = approves;
                }
                else
                {
                    this.ProcessExpMestMetyReq(sdo.Medicines, expMest, oldExpMestMetyReqs, ref requests);
                    this.GenerateSqlDeleteExpMestReq(ref sqls);
                    resultMetyReq = requests;
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

        private void ProcessExpMestMetyReq(List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> oldExpMestMetyReqs, ref List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs)
        {
            List<HIS_EXP_MEST_METY_REQ> listReq = new List<HIS_EXP_MEST_METY_REQ>();
            List<HIS_EXP_MEST_METY_REQ> newExpMestMetyReqs = null;
            List<HIS_EXP_MEST_METY_REQ> updateExpMestMetyReqs = null;
            List<HIS_EXP_MEST_METY_REQ> beforeUpdates = null;
            if (IsNotNullOrEmpty(medicines))
            {
                List<ExpMedicineTypeSDO> expMestMetyReqCreates = medicines.Where(o => !o.ExpMestMetyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMetyReqCreates))
                {
                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMetyReqMaker.Run(expMestMetyReqCreates, expMest, ref newExpMestMetyReqs))
                    {
                        throw new Exception("ExpMestMetyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }
                    listReq.AddRange(newExpMestMetyReqs);
                }

                List<ExpMedicineTypeSDO> expMestMetyReqUpdates = medicines.Where(o => o.ExpMestMetyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMetyReqUpdates))
                {
                    updateExpMestMetyReqs = new List<HIS_EXP_MEST_METY_REQ>();
                    beforeUpdates = new List<HIS_EXP_MEST_METY_REQ>();
                    Mapper.CreateMap<HIS_EXP_MEST_METY_REQ, HIS_EXP_MEST_METY_REQ>();
                    foreach (var item in expMestMetyReqUpdates)
                    {
                        HIS_EXP_MEST_METY_REQ req = oldExpMestMetyReqs != null ? oldExpMestMetyReqs.FirstOrDefault(o => o.ID == item.ExpMestMetyReqId.Value) : null;
                        if (req == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ExpMestMetyReqId Invalid: " + item.ExpMestMetyReqId.Value);
                        }
                        if (req.AMOUNT == item.Amount && req.NUM_ORDER == item.NumOrder)
                        {
                            listReq.Add(req);
                        }
                        else
                        {
                            beforeUpdates.Add(req);
                            HIS_EXP_MEST_METY_REQ reqUpdate = Mapper.Map<HIS_EXP_MEST_METY_REQ>(req);
                            reqUpdate.AMOUNT = item.Amount;
                            reqUpdate.NUM_ORDER = item.NumOrder;
                            updateExpMestMetyReqs.Add(reqUpdate);
                        }
                    }

                    if (IsNotNullOrEmpty(updateExpMestMetyReqs))
                    {
                        if (!this.hisExpMestMetyReqUpdate.UpdateList(updateExpMestMetyReqs, beforeUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollbac du lieu");
                        }
                        listReq.AddRange(updateExpMestMetyReqs);
                    }
                }
            }

            this.deletes = oldExpMestMetyReqs != null ? (listReq != null ? oldExpMestMetyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null) : null;
            if (IsNotNullOrEmpty(deletes))
            {
                bool valid = true;
                HisExpMestMetyReqCheck reqChecker = new HisExpMestMetyReqCheck(param);
                foreach (var delete in deletes)
                {
                    valid = valid && reqChecker.IsUnLock(delete);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
            if (IsNotNullOrEmpty(listReq))
            {
                expMestMetyReqs = listReq;
            }
        }

        private void ProcessExpMestMedicine(HisExpMestChmsSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> oldRequests, List<HIS_EXP_MEST_MEDICINE> oldApproves, ref List<HIS_EXP_MEST_METY_REQ> requests, ref List<HIS_EXP_MEST_MEDICINE> approves, ref List<string> sqls)
        {
            List<HIS_EXP_MEST_MEDICINE> inserts = new List<HIS_EXP_MEST_MEDICINE>();
            List<HIS_EXP_MEST_MEDICINE> deletes = new List<HIS_EXP_MEST_MEDICINE>();
            List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
            List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
            Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
            //Lay ra danh sach thong tin cu
            List<HIS_EXP_MEST_MEDICINE> olds = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);

            //Danh sach exp_mest_medicine
            List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(oldRequests, oldApproves, sdo.ExpMedicineSdos, expMest, ref requests, ref newMedicineDic);

            List<long> expMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
            List<HIS_MEDICINE_BEAN> oldBeans = new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds);

            this.GetDiff(olds, news, newMedicineDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

            if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMedicineCreate.CreateList(inserts))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            if (IsNotNullOrEmpty(updates) && !this.hisExpMestMedicineUpdate.UpdateList(updates, beforeUpdates))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;
            //Cap nhat thong tin bean
            this.SqlUpdateBean(newMedicineDic, deleteIds, ref sqls);

            //Xoa cac exp_mest_medicine ko dung.
            //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
            this.SqlDeleteExpMestMedicine(deleteIds, ref sqls);

        }

        private List<HIS_EXP_MEST_MEDICINE> MakeData(List<HIS_EXP_MEST_METY_REQ> oldExpMestMetyReqs, List<HIS_EXP_MEST_MEDICINE> olds, List<ExpMedicineSDO> expMedicineSdos, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> requests, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            //set exp_mest_medicine_id
            if (IsNotNullOrEmpty(olds) && IsNotNullOrEmpty(expMedicineSdos))
            {
                foreach (ExpMedicineSDO t in expMedicineSdos)
                {
                    HIS_EXP_MEST_MEDICINE exp = olds.Where(o => o.MEDICINE_ID == t.MedicineId).FirstOrDefault();
                    t.ExpMestMedicineIds = exp != null ? new List<long>() { exp.ID } : null;
                }
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
            if (IsNotNullOrEmpty(expMedicineSdos) && expMest != null)
            {
                List<HIS_MEDICINE_BEAN> medicineBeans = null;
                if (!this.hisMedicineBeanSplit.SplitByMedicine(expMedicineSdos, expMest.MEDI_STOCK_ID, ref medicineBeans))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (medicineDic == null)
                {
                    medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                }

                List<ExpMedicineTypeSDO> medicineTypeSdos = this.MakeMedicineTypeSdoByBean(oldExpMestMetyReqs, medicineBeans, expMedicineSdos);
                this.ProcessExpMestMetyReq(medicineTypeSdos, expMest, oldExpMestMetyReqs, ref requests);

                //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                foreach (ExpMedicineSDO s in expMedicineSdos)
                {
                    List<HIS_MEDICINE_BEAN> beans = medicineBeans.Where(o => o.MEDICINE_ID == s.MedicineId).ToList();
                    if (!IsNotNullOrEmpty(beans))
                    {
                        throw new Exception("Ko co bean tuong ung voi medicine_id " + s.MedicineId);
                    }

                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.AMOUNT = s.Amount;
                    exp.MEDICINE_ID = s.MedicineId;
                    exp.NUM_ORDER = s.NumOrder;
                    exp.DESCRIPTION = s.Description;
                    exp.PRICE = s.Price;
                    exp.TDL_MEDICINE_TYPE_ID = beans[0].TDL_MEDICINE_TYPE_ID;
                    exp.VAT_RATIO = s.VatRatio;
                    exp.PRICE = s.Price;
                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    var req = requests.FirstOrDefault(o => o.MEDICINE_TYPE_ID == exp.TDL_MEDICINE_TYPE_ID);
                    if (req == null)
                    {
                        throw new Exception("Khong tao duoc HIS_EXP_MEST_METY_REQ tuong ung voi loai thuoc: " + exp.TDL_MEDICINE_TYPE_ID);
                    }
                    data.Add(exp);

                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
            }
            return data;
        }

        private void GetDiff(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> news, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> updates, ref List<HIS_EXP_MEST_MEDICINE> oldOfUpdates)
        {

            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                deletes = olds;
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                inserts = news;
            }
            else
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_EXP_MEST_MEDICINE newMedicine in news)
                {
                    HIS_EXP_MEST_MEDICINE old = olds
                        .Where(t => !IsDiff(newMedicine, t, newMedicineDic, oldBeans)
                            && t.AMOUNT == newMedicine.AMOUNT
                            && t.MEDICINE_ID == newMedicine.MEDICINE_ID
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMedicine);
                    }
                    else if (old.NUM_ORDER != newMedicine.NUM_ORDER || old.DESCRIPTION != newMedicine.DESCRIPTION
                            || old.VAT_RATIO != newMedicine.VAT_RATIO || old.PRICE != newMedicine.PRICE)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.DESCRIPTION = newMedicine.DESCRIPTION;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.VAT_RATIO = newMedicine.VAT_RATIO;
                        old.PRICE = newMedicine.PRICE;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_EXP_MEST_MEDICINE old in olds)
                {
                    HIS_EXP_MEST_MEDICINE newMedicine = news
                        .Where(t => !IsDiff(t, old, newMedicineDic, oldBeans)
                            && t.AMOUNT == old.AMOUNT
                            && t.MEDICINE_ID == old.MEDICINE_ID
                        ).FirstOrDefault();

                    if (newMedicine == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private List<ExpMedicineTypeSDO> MakeMedicineTypeSdoByBean(List<HIS_EXP_MEST_METY_REQ> oldExpMestMetyReqs, List<HIS_MEDICINE_BEAN> medicineBeans, List<ExpMedicineSDO> medicineSdos)
        {
            List<ExpMedicineTypeSDO> typeSdos = new List<ExpMedicineTypeSDO>();
            var Groups = medicineBeans.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
            foreach (var group in Groups)
            {
                ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                sdo.Amount = group.Sum(s => s.AMOUNT);
                sdo.MedicineTypeId = group.Key;
                sdo.NumOrder = medicineSdos.FirstOrDefault(o => group.Any(a => a.MEDICINE_ID == o.MedicineId)).NumOrder;
                var req = oldExpMestMetyReqs != null ? oldExpMestMetyReqs.FirstOrDefault(o => o.MEDICINE_TYPE_ID == group.Key) : null;
                if (req != null)
                {
                    sdo.ExpMestMetyReqId = req.ID;
                }
                typeSdos.Add(sdo);
            }
            return typeSdos;
        }

        private bool IsDiff(HIS_EXP_MEST_MEDICINE newMedicine, HIS_EXP_MEST_MEDICINE oldMedicine, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MEDICINE_ID == oldMedicine.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMedicineDic != null && newMedicineDic.ContainsKey(newMedicine) ? newMedicineDic[newMedicine] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, ref List<string> sqls)
        {
            //Cap nhat cac medicine gan voi cac exp_mest_medicine bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMedicineIds))
            {
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                sqls.Add(query2);
            }

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat medicine_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_medicine bi xoa nhung sau do lai duoc gan vao 1 exp_mest_medicine khac duoc tao moi
            if (IsNotNullOrEmpty(newMedicineDic))
            {
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

        private void GenerateSqlDeleteExpMestReq(ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(this.deletes))
            {
                string deleteReq = DAOWorker.SqlDAO.AddInClause(this.deletes.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_METY_REQ WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(deleteReq);
            }
        }

        internal void Rollback()
        {
            this.hisMedicineBeanSplit.RollBack();
            this.hisExpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisExpMestMetyReqUpdate.RollbackData();
            this.hisExpMestMetyReqMaker.Rollback();
        }
    }
}
