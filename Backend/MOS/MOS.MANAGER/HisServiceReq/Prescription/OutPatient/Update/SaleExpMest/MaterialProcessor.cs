using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update.SaleExpMest
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

        internal bool Run(List<PresOutStockMatySDO> serviceReqMaties, HIS_EXP_MEST expMest, long patientTypeId, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
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
                    List<HIS_EXP_MEST_MATERIAL> olds = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);

                    //Danh sach exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(olds, serviceReqMaties, expMest, patientTypeId, ref newMaterialDic);

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
            //Cap nhat cac material gan voi cac exp_mest_material bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMaterialIds))
            {
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                sqls.Add(query2);
            }

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_material cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat material_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_material bi xoa nhung sau do lai duoc gan vao 1 exp_mest_material khac duoc tao moi
            if (IsNotNullOrEmpty(newMaterialDic))
            {
                foreach (HIS_EXP_MEST_MATERIAL key in newMaterialDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_material insert moi (duoc insert vao DB ==> co ID)
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
                            && t.IS_EXPEND == newMaterial.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == newMaterial.IS_OUT_PARENT_FEE
                            && t.MATERIAL_ID == newMaterial.MATERIAL_ID
                            && t.PATIENT_TYPE_ID == newMaterial.PATIENT_TYPE_ID
                            && t.PRICE == newMaterial.PRICE
                            && t.SERE_SERV_PARENT_ID == newMaterial.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == newMaterial.VAT_RATIO
                            && t.IS_USE_CLIENT_PRICE == newMaterial.IS_USE_CLIENT_PRICE
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaterial);
                    }
                    else if (old.NUM_ORDER != newMaterial.NUM_ORDER || old.DESCRIPTION != newMaterial.DESCRIPTION || old.TUTORIAL != newMaterial.TUTORIAL || old.PRES_AMOUNT != newMaterial.PRES_AMOUNT)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.NUM_ORDER = newMaterial.NUM_ORDER;
                        old.TUTORIAL = newMaterial.TUTORIAL;
                        old.DESCRIPTION = newMaterial.DESCRIPTION;
                        old.PRES_AMOUNT = newMaterial.PRES_AMOUNT;

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
                            && t.IS_EXPEND == old.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == old.IS_OUT_PARENT_FEE
                            && t.MATERIAL_ID == old.MATERIAL_ID
                            && t.PATIENT_TYPE_ID == old.PATIENT_TYPE_ID
                            && t.PRICE == old.PRICE
                            && t.SERE_SERV_PARENT_ID == old.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == old.VAT_RATIO
                            && t.IS_USE_CLIENT_PRICE == old.IS_USE_CLIENT_PRICE
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

        private List<HIS_EXP_MEST_MATERIAL> MakeData(List<HIS_EXP_MEST_MATERIAL> olds, List<PresOutStockMatySDO> serviceReqMaties, HIS_EXP_MEST expMest, long patientTypeId, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<ExpMaterialTypeSDO> materials = new List<ExpMaterialTypeSDO>();
            //set exp_mest_material_id
            if (IsNotNullOrEmpty(serviceReqMaties))
            {
                foreach (PresOutStockMatySDO t in serviceReqMaties)
                {
                    if (t.MaterialTypeId.HasValue)
                    {
                        ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                        sdo.Amount = t.Amount;
                        sdo.Tutorial = t.Tutorial;
                        sdo.NumOrder = t.NumOrder;
                        sdo.PatientTypeId = patientTypeId;
                        sdo.MaterialTypeId = t.MaterialTypeId.Value;
                        sdo.ExpMestMaterialIds = olds != null ? olds.Where(o => o.TDL_MATERIAL_TYPE_ID == sdo.MaterialTypeId).Select(o => o.ID).ToList() : null;
                        sdo.PresAmount = t.PresAmount;
                        materials.Add(sdo);
                    }
                }
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
            if (IsNotNullOrEmpty(materials) && expMest != null)
            {
                List<HIS_MATERIAL_BEAN> materialBeans = null;
                List<HIS_MATERIAL_PATY> materialPaties = null;

                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? expMest.TDL_INTRUCTION_TIME : null; //lay TDL_INTRUCTION_TIME chu ko lay TDL_INTRUCTION_DATE, vi truong nay do trigger trong DB xu ly --> tai thoi diem nay, chua co gia tri

                if (!this.hisMaterialBeanSplit.SplitByMaterialType(materials, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref materialBeans, ref materialPaties))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (materialDic == null)
                {
                    materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                }

                foreach (ExpMaterialTypeSDO sdo in materials)
                {
                    var group = materialBeans
                            .Where(o => o.TDL_MATERIAL_TYPE_ID == sdo.MaterialTypeId)
                            .GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_IMP_PRICE, o.TDL_MATERIAL_IMP_VAT_RATIO });

                    foreach (var tmp in group)
                    {
                        List<HIS_MATERIAL_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                        exp.TDL_MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                        exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;

                        //Neu ban bang gia nhap
                        if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = tmp.Key.TDL_MATERIAL_IMP_PRICE;
                            exp.VAT_RATIO = tmp.Key.TDL_MATERIAL_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ?
                                materialPaties.Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.MATERIAL_ID == tmp.Key.MATERIAL_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + tmp.Key.MATERIAL_ID + "va patient_type_id: " + sdo.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }

                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.TUTORIAL = sdo.Tutorial;
                        exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                        exp.PRES_AMOUNT = sdo.PresAmount;
                        data.Add(exp);
                        materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }
                }
            }
            return data;
        }

        internal void Rollback()
        {
            this.hisMaterialBeanSplit.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
        }
    }
}
