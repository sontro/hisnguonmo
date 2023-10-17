using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Update
{
    class ExpMestMaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisMaterialBeanSplit hisMaterialBeanSplit;

        internal ExpMestMaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
        }

        internal bool Run(HisPackingUpdateSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> dicExpMestMaterial = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                List<HIS_EXP_MEST_MATERIAL> olds = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);

                List<HIS_EXP_MEST_MATERIAL> news = this.MakeDataMaterial(olds, data, expMest, ref dicExpMestMaterial);

                List<long> expMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

                this.GetDiffMaterial(olds, news, dicExpMestMaterial, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

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

                List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;
                //Cap nhat thong tin bean
                this.SqlUpdateMaterialBean(dicExpMestMaterial, deleteIds, ref sqls);

                //Xoa cac exp_mest_medicine ko dung.
                //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
                this.SqlDeleteExpMestMaterial(deleteIds, ref sqls);

                this.PassResult(olds, inserts, updates, deletes, ref expMestMaterials);

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private List<HIS_EXP_MEST_MATERIAL> MakeDataMaterial(List<HIS_EXP_MEST_MATERIAL> olds, HisPackingUpdateSDO sdo, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> medicineDic)
        {
            List<HIS_EXP_MEST_MATERIAL> dataMaterial = new List<HIS_EXP_MEST_MATERIAL>();
            if (IsNotNullOrEmpty(sdo.MaterialTypes))
            {
                List<HIS_MATERIAL_BEAN> medicineBeans = null;
                List<ExpMaterialTypeSDO> medicineTypeSplits = new List<ExpMaterialTypeSDO>();
                List<HIS_MATERIAL_PATY> medicinePaties = null;
                var Groups = sdo.MaterialTypes.GroupBy(g => g.MaterialTypeId).ToList();
                foreach (var group in Groups)
                {
                    ExpMaterialTypeSDO mtSdo = new ExpMaterialTypeSDO();
                    mtSdo.Amount = group.Sum(s => s.Amount);
                    mtSdo.MaterialTypeId = group.Key;
                    mtSdo.ExpMestMaterialIds = olds != null ? olds.Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key).Select(s => s.ID).ToList() : null;
                    medicineTypeSplits.Add(mtSdo);
                }

                if (!this.hisMaterialBeanSplit.SplitByMaterialType(medicineTypeSplits, expMest.MEDI_STOCK_ID, null, null, null, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("hisMaterialBeanSplit. Ket thuc nghiep vu");
                }
                if (medicineDic == null)
                {
                    medicineDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                }

                var GroupBeans = medicineBeans.GroupBy(o => o.MATERIAL_ID).ToList();
                foreach (var g in GroupBeans)
                {
                    HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.AMOUNT = g.Sum(s => s.AMOUNT);
                    exp.MATERIAL_ID = g.Key;
                    exp.PRICE = g.FirstOrDefault().TDL_MATERIAL_IMP_PRICE;
                    exp.TDL_MATERIAL_TYPE_ID = g.FirstOrDefault().TDL_MATERIAL_TYPE_ID;
                    exp.VAT_RATIO = g.FirstOrDefault().TDL_MATERIAL_IMP_VAT_RATIO;
                    exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                    dataMaterial.Add(exp);

                    medicineDic.Add(exp, g.Select(o => o.ID).ToList());
                }
            }
            return dataMaterial;
        }

        private void GetDiffMaterial(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> news, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> oldOfUpdates)
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
                        .Where(t => !IsDiffMaterial(newMaterial, t, newMaterialDic, oldBeans)
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
                        .Where(t => !IsDiffMaterial(t, old, newMaterialDic, oldBeans)
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

        private bool IsDiffMaterial(HIS_EXP_MEST_MATERIAL newMaterial, HIS_EXP_MEST_MATERIAL oldMaterial, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MATERIAL_ID == oldMaterial.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMaterialDic != null && newMaterialDic.ContainsKey(newMaterial) ? newMaterialDic[newMaterial] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private void SqlUpdateMaterialBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<long> deleteExpMestMaterialIds, ref List<string> sqls)
        {
            //Cap nhat cac medicine gan voi cac exp_mest_medicine bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMaterialIds))
            {
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                sqls.Add(query2);
            }

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat medicine_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_medicine bi xoa nhung sau do lai duoc gan vao 1 exp_mest_medicine khac duoc tao moi
            if (IsNotNullOrEmpty(newMaterialDic))
            {
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

        private void PassResult(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> inserts, List<HIS_EXP_MEST_MATERIAL> updates, List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_EXP_MEST_MATERIAL>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    List<HIS_EXP_MEST_MATERIAL> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMaterialBeanSplit.RollBack();
                this.hisExpMestMaterialCreate.RollbackData();
                this.hisExpMestMaterialUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
