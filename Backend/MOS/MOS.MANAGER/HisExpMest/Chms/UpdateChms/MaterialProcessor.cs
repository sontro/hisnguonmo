using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.UpdateChms
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        private HisExpMestMatyReqUpdate hisExpMestMatyReqUpdate;


        List<HIS_EXP_MEST_MATY_REQ> deletes = null;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            this.hisExpMestMatyReqUpdate = new HisExpMestMatyReqUpdate(param);
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
        }

        internal bool Run(HisExpMestChmsSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> resultMatyReq, ref List<HIS_EXP_MEST_MATERIAL> resultMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MATY_REQ> requests = null;
                List<HIS_EXP_MEST_MATERIAL> approves = null;
                List<HIS_EXP_MEST_MATY_REQ> oldExpMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(expMest.ID);
                if (expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST_MATERIAL> oldExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    this.ProcessExpMestMaterial(sdo, expMest, oldExpMestMatyReqs, oldExpMestMaterials, ref requests, ref approves, ref sqls);
                    this.GenerateSqlDeleteExpMestReq(ref sqls);
                    resultMatyReq = requests;
                    resultMaterials = approves;
                }
                else
                {
                    this.ProcessExpMestMatyReq(sdo.Materials, expMest, oldExpMestMatyReqs, ref requests);
                    this.GenerateSqlDeleteExpMestReq(ref sqls);
                    resultMatyReq = requests;
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

        private void ProcessExpMestMatyReq(List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> oldExpMestMatyReqs, ref List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs)
        {
            List<HIS_EXP_MEST_MATY_REQ> listReq = new List<HIS_EXP_MEST_MATY_REQ>();
            List<HIS_EXP_MEST_MATY_REQ> newExpMestMatyReqs = null;
            List<HIS_EXP_MEST_MATY_REQ> updateExpMestMatyReqs = null;
            List<HIS_EXP_MEST_MATY_REQ> beforeUpdates = null;
            if (IsNotNullOrEmpty(materials))
            {
                List<ExpMaterialTypeSDO> expMestMatyReqCreates = materials.Where(o => !o.ExpMestMatyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMatyReqCreates))
                {
                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMatyReqMaker.Run(expMestMatyReqCreates, expMest, ref newExpMestMatyReqs))
                    {
                        throw new Exception("ExpMestMatyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }
                    listReq.AddRange(newExpMestMatyReqs);
                }

                List<ExpMaterialTypeSDO> expMestMatyReqUpdates = materials.Where(o => o.ExpMestMatyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMatyReqUpdates))
                {
                    updateExpMestMatyReqs = new List<HIS_EXP_MEST_MATY_REQ>();
                    beforeUpdates = new List<HIS_EXP_MEST_MATY_REQ>();
                    Mapper.CreateMap<HIS_EXP_MEST_MATY_REQ, HIS_EXP_MEST_MATY_REQ>();
                    foreach (var item in expMestMatyReqUpdates)
                    {
                        HIS_EXP_MEST_MATY_REQ req = oldExpMestMatyReqs != null ? oldExpMestMatyReqs.FirstOrDefault(o => o.ID == item.ExpMestMatyReqId.Value) : null;
                        if (req == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ExpMestMatyReqId Invalid: " + item.ExpMestMatyReqId.Value);
                        }
                        if (req.AMOUNT == item.Amount && req.NUM_ORDER == item.NumOrder)
                        {
                            listReq.Add(req);
                        }
                        else
                        {
                            beforeUpdates.Add(req);
                            HIS_EXP_MEST_MATY_REQ reqUpdate = Mapper.Map<HIS_EXP_MEST_MATY_REQ>(req);
                            reqUpdate.AMOUNT = item.Amount;
                            reqUpdate.NUM_ORDER = item.NumOrder;
                            updateExpMestMatyReqs.Add(reqUpdate);
                        }
                    }

                    if (IsNotNullOrEmpty(updateExpMestMatyReqs))
                    {
                        if (!this.hisExpMestMatyReqUpdate.UpdateList(updateExpMestMatyReqs, beforeUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollbac du lieu");
                        }
                        listReq.AddRange(updateExpMestMatyReqs);
                    }
                }
            }

            this.deletes = oldExpMestMatyReqs != null ? (listReq != null ? oldExpMestMatyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null) : null;
            if (IsNotNullOrEmpty(deletes))
            {
                bool valid = true;
                HisExpMestMatyReqCheck reqChecker = new HisExpMestMatyReqCheck(param);
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
                expMestMatyReqs = listReq;
            }
        }

        private void ProcessExpMestMaterial(HisExpMestChmsSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> oldRequests, List<HIS_EXP_MEST_MATERIAL> oldApproves, ref List<HIS_EXP_MEST_MATY_REQ> requests, ref List<HIS_EXP_MEST_MATERIAL> approves, ref List<string> sqls)
        {
            List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
            List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();
            List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
            List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
            Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

            //Danh sach exp_mest_material
            List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(oldRequests, oldApproves, sdo.ExpMaterialSdos, expMest, ref requests, ref newMaterialDic);

            List<long> expMestMaterialIds = IsNotNullOrEmpty(oldApproves) ? oldApproves.Select(o => o.ID).ToList() : null;
            List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

            this.GetDiff(oldApproves, news, newMaterialDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

            if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMaterialCreate.CreateList(inserts))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            if (IsNotNullOrEmpty(updates) && !this.hisExpMestMaterialUpdate.UpdateList(updates, beforeUpdates))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;
            //Cap nhat thong tin bean
            this.SqlUpdateBean(newMaterialDic, deleteIds, ref sqls);

            //Xoa cac exp_mest_material ko dung.
            //Luu y: can thuc hien xoa exp_mest_material sau khi da cap nhat bean (tranh bi loi fk)
            this.SqlDeleteExpMestMaterial(deleteIds, ref sqls);

        }

        private List<HIS_EXP_MEST_MATERIAL> MakeData(List<HIS_EXP_MEST_MATY_REQ> oldExpMestMatyReqs, List<HIS_EXP_MEST_MATERIAL> olds, List<ExpMaterialSDO> expMaterialSdos, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> requests, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            //set exp_mest_material_id
            if (IsNotNullOrEmpty(olds) && IsNotNullOrEmpty(expMaterialSdos))
            {
                foreach (ExpMaterialSDO t in expMaterialSdos)
                {
                    HIS_EXP_MEST_MATERIAL exp = olds.Where(o => o.MATERIAL_ID == t.MaterialId).FirstOrDefault();
                    t.ExpMestMaterialIds = exp != null ? new List<long>() { exp.ID } : null;
                }
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
            if (IsNotNullOrEmpty(expMaterialSdos) && expMest != null)
            {
                List<HIS_MATERIAL_BEAN> materialBeans = null;
                if (!this.hisMaterialBeanSplit.SplitByMaterial(expMaterialSdos, expMest.MEDI_STOCK_ID, ref materialBeans))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (materialDic == null)
                {
                    materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                }

                List<ExpMaterialTypeSDO> materialTypeSdos = this.MakeMaterialTypeSdoByBean(oldExpMestMatyReqs, materialBeans, expMaterialSdos);
                this.ProcessExpMestMatyReq(materialTypeSdos, expMest, oldExpMestMatyReqs, ref requests);

                //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                foreach (ExpMaterialSDO s in expMaterialSdos)
                {
                    List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.MATERIAL_ID == s.MaterialId).ToList();
                    if (!IsNotNullOrEmpty(beans))
                    {
                        throw new Exception("Ko co bean tuong ung voi material_id " + s.MaterialId);
                    }

                    HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.AMOUNT = s.Amount;
                    exp.MATERIAL_ID = s.MaterialId;
                    exp.NUM_ORDER = s.NumOrder;
                    exp.DESCRIPTION = s.Description;
                    exp.PRICE = s.Price;
                    exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                    exp.VAT_RATIO = s.VatRatio;
                    exp.PRICE = s.Price;
                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    var req = requests.FirstOrDefault(o => o.MATERIAL_TYPE_ID == exp.TDL_MATERIAL_TYPE_ID);
                    if (req == null)
                    {
                        throw new Exception("Khong tao duoc HIS_EXP_MEST_MATY_REQ tuong ung voi loai thuoc: " + exp.TDL_MATERIAL_TYPE_ID);
                    }
                    data.Add(exp);

                    materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
            }
            return data;
        }

        private void GetDiff(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> news, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> oldOfUpdates)
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
                Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_EXP_MEST_MATERIAL newMaterial in news)
                {
                    HIS_EXP_MEST_MATERIAL old = olds
                        .Where(t => !IsDiff(newMaterial, t, newMaterialDic, oldBeans)
                            && t.AMOUNT == newMaterial.AMOUNT
                            && t.MATERIAL_ID == newMaterial.MATERIAL_ID
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaterial);
                    }
                    else if (old.NUM_ORDER != newMaterial.NUM_ORDER || old.DESCRIPTION != newMaterial.DESCRIPTION
                            || old.VAT_RATIO != newMaterial.VAT_RATIO || old.PRICE != newMaterial.PRICE)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.DESCRIPTION = newMaterial.DESCRIPTION;
                        old.NUM_ORDER = newMaterial.NUM_ORDER;
                        old.VAT_RATIO = newMaterial.VAT_RATIO;
                        old.PRICE = newMaterial.PRICE;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_EXP_MEST_MATERIAL old in olds)
                {
                    HIS_EXP_MEST_MATERIAL newMaterial = news
                        .Where(t => !IsDiff(t, old, newMaterialDic, oldBeans)
                            && t.AMOUNT == old.AMOUNT
                            && t.MATERIAL_ID == old.MATERIAL_ID
                        ).FirstOrDefault();

                    if (newMaterial == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private List<ExpMaterialTypeSDO> MakeMaterialTypeSdoByBean(List<HIS_EXP_MEST_MATY_REQ> oldExpMestMatyReqs, List<HIS_MATERIAL_BEAN> materialBeans, List<ExpMaterialSDO> materialSdos)
        {
            List<ExpMaterialTypeSDO> typeSdos = new List<ExpMaterialTypeSDO>();
            var Groups = materialBeans.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();
            foreach (var group in Groups)
            {
                ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                sdo.Amount = group.Sum(s => s.AMOUNT);
                sdo.MaterialTypeId = group.Key;
                sdo.NumOrder = materialSdos.FirstOrDefault(o => group.Any(a => a.MATERIAL_ID == o.MaterialId)).NumOrder;
                var req = oldExpMestMatyReqs != null ? oldExpMestMatyReqs.FirstOrDefault(o => o.MATERIAL_TYPE_ID == group.Key) : null;
                if (req != null)
                {
                    sdo.ExpMestMatyReqId = req.ID;
                }
                typeSdos.Add(sdo);
            }
            return typeSdos;
        }

        private bool IsDiff(HIS_EXP_MEST_MATERIAL newMaterial, HIS_EXP_MEST_MATERIAL oldMaterial, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MATERIAL_ID == oldMaterial.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMaterialDic != null && newMaterialDic.ContainsKey(newMaterial) ? newMaterialDic[newMaterial] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<long> deleteExpMestMaterialIds, ref List<string> sqls)
        {
            //Can cap nhat cac bean ko dung truoc
            //Tranh truong hop bean duoc gan lai vao cac exp_mest_material tao moi
            if (IsNotNullOrEmpty(deleteExpMestMaterialIds))
            {
                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                sqls.Add(query2);
            }

            if (IsNotNullOrEmpty(newMaterialDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_material
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

        private void GenerateSqlDeleteExpMestReq(ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(this.deletes))
            {
                string deleteReq = DAOWorker.SqlDAO.AddInClause(this.deletes.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_MATY_REQ WHERE %IN_CLAUSE% ", "ID");
                sqls.Add(deleteReq);
            }
        }

        internal void Rollback()
        {
            this.hisMaterialBeanSplit.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisExpMestMatyReqUpdate.RollbackData();
            this.hisExpMestMatyReqMaker.Rollback();
        }
    }
}
