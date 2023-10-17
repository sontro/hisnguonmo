using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Other.Update
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        internal bool Run(HisExpMestOtherSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> materialOlds, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                    //Lay ra danh sach thong tin cu
                    materialOlds = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    //Chi xu ly cac vat tu thuong. Vat tu tai su dung xu ly o ham khac
                    List<HIS_EXP_MEST_MATERIAL> olds = materialOlds != null ? materialOlds.Where(o => String.IsNullOrWhiteSpace(o.SERIAL_NUMBER)).ToList() : null;
                    //Danh sach exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(olds, sdo, expMest, ref newMaterialDic);

                    List<long> expMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

                    this.GetDiff(olds, news, newMaterialDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

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
                    this.SqlUpdateBean(newMaterialDic, deleteIds, ref sqls);

                    //Xoa cac exp_mest_material ko dung.
                    //Luu y: can thuc hien xoa exp_mest_material sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMaterial(deleteIds, ref sqls);

                    this.PassResult(olds, inserts, updates, deletes, ref resultData);
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
                            && t.PRICE == newMaterial.PRICE
                            && t.VAT_RATIO == newMaterial.VAT_RATIO
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaterial);
                    }
                    else if (old.NUM_ORDER != newMaterial.NUM_ORDER || old.DESCRIPTION != newMaterial.DESCRIPTION)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.DESCRIPTION = newMaterial.DESCRIPTION;
                        old.NUM_ORDER = newMaterial.NUM_ORDER;

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
                            && t.PRICE == old.PRICE
                            && t.VAT_RATIO == old.VAT_RATIO
                        ).FirstOrDefault();

                    if (newMaterial == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private bool IsDiff(HIS_EXP_MEST_MATERIAL newMaterial, HIS_EXP_MEST_MATERIAL oldMaterial, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MATERIAL_ID == oldMaterial.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMaterialDic != null && newMaterialDic.ContainsKey(newMaterial) ? newMaterialDic[newMaterial] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private List<HIS_EXP_MEST_MATERIAL> MakeData(List<HIS_EXP_MEST_MATERIAL> olds, HisExpMestOtherSDO sdo, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            //set exp_mest_material_id
            if (IsNotNullOrEmpty(olds) && IsNotNullOrEmpty(sdo.Materials))
            {
                foreach (ExpMaterialSDO t in sdo.Materials)
                {
                    HIS_EXP_MEST_MATERIAL exp = olds.Where(o => o.MATERIAL_ID == t.MaterialId).FirstOrDefault();
                    t.ExpMestMaterialIds = exp != null ? new List<long>() { exp.ID } : null;
                }
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
            if (IsNotNullOrEmpty(sdo.Materials) && expMest != null)
            {
                List<HIS_MATERIAL_BEAN> materialBeans = null;
                if (!this.hisMaterialBeanSplit.SplitByMaterial(sdo.Materials, expMest.MEDI_STOCK_ID, ref materialBeans))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (materialDic == null)
                {
                    materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                }

                //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                foreach (ExpMaterialSDO s in sdo.Materials)
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
                    data.Add(exp);

                    materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
            }
            return data;
        }

        internal void Rollback()
        {
            this.hisMaterialBeanSplit.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
