using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Update
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(HisExpMestSaleSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls, AutoEnum en = AutoEnum.NONE, long? axTime = null, string loginname = null, string username = null)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);

                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MATERIAL> olds = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    List<long> expMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

                    //Danh sach exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(sdo, expMest, olds, sessionKey, en, axTime, loginname, username, ref materialDic);

                    this.GetDiff(olds, news, materialDic, oldBeans, en, ref inserts, ref deletes, ref updates, ref beforeUpdates);

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

                    this.SqlUpdateBean(expMest, materialDic, deleteIds, sessionKey, en, ref sqls);

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

        private void SqlDeleteExpMestMaterial(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        private void SqlUpdateBean(HIS_EXP_MEST expMest, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<long> deleteExpMestMaterialIds, string sessionKey, AutoEnum en, ref List<string> sqls)
        {
            //Cap nhat danh sach cac bean gan voi cac exp_mest_material bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMaterialIds))
            {
                string sql2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                sqls.Add(sql2);
            }

            List<long> useBeanIds = new List<long>();

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_material cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat material_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_material bi xoa nhung sau do lai duoc gan vao 1 exp_mest_material khac duoc tao moi
            if (IsNotNullOrEmpty(newMaterialDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_material
                foreach (HIS_EXP_MEST_MATERIAL key in newMaterialDic.Keys)
                {
                    if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(newMaterialDic[key], "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }
                    else
                    {
                        if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                        {
                            string sql = DAOWorker.SqlDAO.AddInClause(newMaterialDic[key], "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                            sql = string.Format(sql, key.ID);
                            sqls.Add(sql);
                        }
                    }
                    useBeanIds.AddRange(newMaterialDic[key]);
                }
            }

            //cap nhat danh sach cac bean ko dung nhung bi khoa trong qua trinh "take bean"
            string sql3 = DAOWorker.SqlDAO.AddNotInClause(useBeanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
            sql3 = string.Format(sql3, sessionKey);
            sqls.Add(sql3);
        }

        private void GetDiff(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> news, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans, AutoEnum en, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> oldOfUpdates)
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
                    else if (en != AutoEnum.NONE || old.NUM_ORDER != newMaterial.NUM_ORDER || old.DESCRIPTION != newMaterial.DESCRIPTION)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.NUM_ORDER = newMaterial.NUM_ORDER;
                        old.DESCRIPTION = newMaterial.DESCRIPTION;

                        old.APPROVAL_TIME = newMaterial.APPROVAL_TIME;
                        old.APPROVAL_LOGINNAME = newMaterial.APPROVAL_LOGINNAME;
                        old.APPROVAL_USERNAME = newMaterial.APPROVAL_USERNAME;
                        old.IS_EXPORT = newMaterial.IS_EXPORT;
                        old.EXP_TIME = newMaterial.EXP_TIME;
                        old.EXP_LOGINNAME = newMaterial.EXP_LOGINNAME;
                        old.EXP_USERNAME = newMaterial.EXP_USERNAME;

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

        private List<HIS_EXP_MEST_MATERIAL> MakeData(HisExpMestSaleSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> olds, string sessionKey, AutoEnum en, long? axTime, string loginname, string username, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
            if (IsNotNullOrEmpty(sdo.Materials) && expMest != null)
            {
                List<HIS_MATERIAL_BEAN> materialBeans = IsNotNullOrEmpty(sdo.MaterialBeanIds) ? new HisMaterialBeanGet().GetByIds(sdo.MaterialBeanIds) : null;
                if (!IsNotNullOrEmpty(materialBeans))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("material_bean_id ko hop le");
                }

                List<long> oldExpMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                List<HIS_MATERIAL_BEAN> unavailables = materialBeans
                    .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE && o.SESSION_KEY != sessionKey && (oldExpMestMaterialIds == null || !o.EXP_MEST_MATERIAL_ID.HasValue || !oldExpMestMaterialIds.Contains(o.EXP_MEST_MATERIAL_ID.Value)))
                    .ToList();

                if (IsNotNullOrEmpty(unavailables))
                {
                    throw new Exception("Ton tai material_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                }

                //List<long> materialIds = materialBeans.Select(o => o.MATERIAL_ID).ToList();
                List<HIS_MATERIAL_PATY> materialPaties = null;
                if (sdo.PatientTypeId.HasValue && materialBeans.Any(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE))
                {
                    materialPaties = new HisMaterialPatyGet().GetAppliedMaterialPaty(materialBeans.Where(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MATERIAL_ID).ToList(), sdo.PatientTypeId.Value);
                }

                var groupSdos = sdo.Materials.GroupBy(g => g.MaterialTypeId).ToList();

                //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                foreach (var group in groupSdos)
                {
                    List<HIS_MATERIAL_BEAN> beans = materialBeans
                        .Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key && o.MEDI_STOCK_ID == expMest.MEDI_STOCK_ID)
                        .ToList();

                    ExpMaterialTypeSDO firstSdo = group.FirstOrDefault();

                    decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;
                    decimal sdoAmount = group.ToList().Sum(s => s.Amount);

                    //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                    if (sdoAmount != beanAmount)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai meidicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                    }

                    var groupMaterials = beans.Select(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID }).Distinct().ToList();
                    foreach (var material in groupMaterials)
                    {
                        List<HIS_MATERIAL_BEAN> usedBeans = beans.Where(o => o.MATERIAL_ID == material.MATERIAL_ID).ToList();
                        HIS_MATERIAL_BEAN first = usedBeans.FirstOrDefault();
                        decimal amount = usedBeans.Sum(o => o.AMOUNT);
                        decimal? discount = null;
                        decimal price = 0;
                        decimal vatRatio = 0;
                        bool isUseClientPrice = false;

                        //neu co thong tin doi tuong 
                        if (firstSdo.Price.HasValue)
                        {
                            price = firstSdo.Price.Value;
                            vatRatio = firstSdo.VatRatio.HasValue ? firstSdo.VatRatio.Value : 0;
                            discount = !firstSdo.DiscountRatio.HasValue ?
                        0 : firstSdo.DiscountRatio.Value * amount * price * (1 + vatRatio);
                            isUseClientPrice = true;
                        }
                        else
                        {
                            if (first.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && first.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == UTILITY.Constant.IS_TRUE)
                            {
                                price = first.TDL_MATERIAL_IMP_PRICE;
                                vatRatio = first.TDL_MATERIAL_IMP_VAT_RATIO;
                            }
                            else
                            {
                                HIS_MATERIAL_PATY mp = IsNotNullOrEmpty(materialPaties) && sdo.PatientTypeId.HasValue ?
                                    materialPaties.Where(o => o.MATERIAL_ID == material.MATERIAL_ID && o.PATIENT_TYPE_ID == sdo.PatientTypeId.Value).FirstOrDefault() : null;
                                if (mp == null)
                                {
                                    V_HIS_MATERIAL m = new HisMaterialGet().GetViewById(material.MATERIAL_ID);
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialPaty_KhongTonTaiTuongUngVoiLoaiVatTu, m.MATERIAL_TYPE_NAME);
                                    throw new Exception("Khong co thong tin gia ban tuong ung voi MATERIAL_ID:" + material.MATERIAL_ID + "; patientTypeId:" + sdo.PatientTypeId);
                                }
                                price = mp.EXP_PRICE;
                                vatRatio = mp.EXP_VAT_RATIO;
                            }
                        }

                        HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = amount;
                        exp.MATERIAL_ID = material.MATERIAL_ID;
                        exp.TDL_MATERIAL_TYPE_ID = material.TDL_MATERIAL_TYPE_ID;
                        exp.NUM_ORDER = firstSdo.NumOrder;
                        exp.PRICE = price;
                        exp.VAT_RATIO = vatRatio;
                        exp.DISCOUNT = discount;
                        exp.DESCRIPTION = firstSdo.Description;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.IS_USE_CLIENT_PRICE = isUseClientPrice ? (short?)Constant.IS_TRUE : null;

                        if (en == AutoEnum.APPROVE)
                        {
                            exp.APPROVAL_TIME = axTime;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_USERNAME = username;
                        }
                        else if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                        {
                            exp.APPROVAL_TIME = axTime;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_USERNAME = username;

                            exp.IS_EXPORT = Constant.IS_TRUE;
                            exp.EXP_TIME = axTime;
                            exp.EXP_LOGINNAME = loginname;
                            exp.EXP_USERNAME = username;
                        }

                        expMestMaterials.Add(exp);
                        materialDic.Add(exp, usedBeans.Select(o => o.ID).ToList());
                    }
                }
            }
            return expMestMaterials;
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
        }
    }
}
