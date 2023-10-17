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
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
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
        internal bool Run(SubclinicalPresSDO sdo, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    if (inserts == null)
                    {
                        inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    }
                    if (deletes == null)
                    {
                        deletes = new List<HIS_EXP_MEST_MATERIAL>();
                    }

                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);

                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MATERIAL> olds = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    List<long> expMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

                    //Danh sach exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(sdo, expMest, olds, sessionKey, ref materialDic);

                    if (IsNotNullOrEmpty(news))
                    {
                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, news, null, sdo.InstructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(news, news[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(news, news[0].SERE_SERV_PARENT_ID, sdo.InstructionTime);
                    }

                    this.GetDiff(olds, news, materialDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                    List<long> choosenMaterailIds = news != null ? news.Where(o => o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList() : null;
                    List<V_HIS_MATERIAL_2> choosenMaterials = new HisMaterialGet().GetView2ByIds(choosenMaterailIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMaterialWithBidDate(choosenMaterials, news, new List<HIS_EXP_MEST>() { expMest }))
                    {
                        throw new Exception("IsValidMaterialWithBidDate false. Rollback du lieu");
                    }

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

                    this.SqlUpdateBean(materialDic, deleteIds, sessionKey, ref sqls);

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
                    Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MATERIAL>();
                    //clone, tranh thay doi du lieu tra ve qua bien ref
                    List<HIS_EXP_MEST_MATERIAL> remains = Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(olds);
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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<long> deleteExpMestMaterialIds, string sessionKey, ref List<string> sqls)
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
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(newMaterialDic[key], "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        sql = string.Format(sql, key.ID);
                        sqls.Add(sql);
                    }
                    useBeanIds.AddRange(newMaterialDic[key]);
                }
            }

            //cap nhat danh sach cac bean ko dung nhung bi khoa trong qua trinh "take bean"
            string sql3 = DAOWorker.SqlDAO.AddNotInClause(useBeanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
            sql3 = string.Format(sql3, sessionKey);
            sqls.Add(sql3);
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
                            && t.STENT_ORDER == newMaterial.STENT_ORDER
                            && t.EQUIPMENT_SET_ID == newMaterial.EQUIPMENT_SET_ID
                            && t.EQUIPMENT_SET_ORDER == newMaterial.EQUIPMENT_SET_ORDER
                            && t.EXPEND_TYPE_ID == newMaterial.EXPEND_TYPE_ID
                            && t.SERIAL_NUMBER == newMaterial.SERIAL_NUMBER
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaterial);
                    }
                    else if (old.NUM_ORDER != newMaterial.NUM_ORDER || old.PRES_AMOUNT != newMaterial.PRES_AMOUNT)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.NUM_ORDER = newMaterial.NUM_ORDER;
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
                            && t.STENT_ORDER == old.STENT_ORDER
                            && t.EQUIPMENT_SET_ID == old.EQUIPMENT_SET_ID
                            && t.EQUIPMENT_SET_ORDER == old.EQUIPMENT_SET_ORDER
                            && t.EXPEND_TYPE_ID == old.EXPEND_TYPE_ID
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

        private List<HIS_EXP_MEST_MATERIAL> MakeData(SubclinicalPresSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> olds, string sessionKey, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            if (IsNotNullOrEmpty(sdo.Materials) && expMest != null)
            {
                List<long> materialBeanIds = new List<long>();
                foreach (PresMaterialSDO m in sdo.Materials)
                {
                    materialBeanIds.AddRange(m.MaterialBeanIds);
                }

                List<HIS_MATERIAL_BEAN> materialBeans = IsNotNullOrEmpty(materialBeanIds) ? new HisMaterialBeanGet().GetByIds(materialBeanIds) : null;
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
                    throw new Exception("Ton tai meidicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa nhung ko gan vao exp_mest_material cu~" + LogUtil.TraceData("unavailables", unavailables));
                }

                //Neu trong d/s bean co bean ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia,
                //phuc vu lay chinh sach gia
                List<long> materialIds = materialBeans
                    .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.MATERIAL_ID).Distinct().ToList();

                List<HIS_MATERIAL_PATY> materialPaties = null;
                if (IsNotNullOrEmpty(materialIds))
                {
                    List<long> patientTypeIds = sdo.Materials.Select(o => o.PatientTypeId).Distinct().ToList();

                    HisMaterialPatyFilterQuery filter = new HisMaterialPatyFilterQuery();
                    filter.MATERIAL_IDs = materialIds;
                    filter.PATIENT_TYPE_IDs = patientTypeIds;
                    materialPaties = new HisMaterialPatyGet().Get(filter);
                }

                List<HIS_EXP_MEST_MATERIAL> result = new List<HIS_EXP_MEST_MATERIAL>();
                //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                foreach (PresMaterialSDO s in sdo.Materials)
                {
                    List<HIS_MATERIAL_BEAN> beans = materialBeans
                        //neu co ko thong tin medicine_id thi lay theo medicine_id
                        .Where(o => ((o.TDL_MATERIAL_TYPE_ID == s.MaterialTypeId && !s.MaterialId.HasValue) || o.MATERIAL_ID == s.MaterialId)
                            && s.MaterialBeanIds.Contains(o.ID))
                        .OrderBy(o => o.MATERIAL_ID)
                        .ToList();

                    decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;

                    //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                    if (s.Amount != beanAmount)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai material_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                    }

                    //Group theo material_id de tao ra exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    var groupMaterials = beans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID, o.SERIAL_NUMBER, o.REMAIN_REUSE_COUNT });

                    foreach (var material in groupMaterials)
                    {
                        HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                        exp.AMOUNT = material.Sum(o => o.AMOUNT);
                        exp.IS_EXPEND = s.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.PATIENT_TYPE_ID = s.PatientTypeId;
                        exp.SERE_SERV_PARENT_ID = s.SereServParentId;
                        exp.IS_OUT_PARENT_FEE = s.IsOutParentFee && s.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.MATERIAL_ID = material.Key.MATERIAL_ID;
                        exp.SERIAL_NUMBER = material.Key.SERIAL_NUMBER;
                        exp.REMAIN_REUSE_COUNT = material.Key.REMAIN_REUSE_COUNT;
                        exp.NUM_ORDER = s.NumOrder;
                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = s.IsBedExpend ? new Nullable<long>(1) : null;
                        if (s.FailedAmount.HasValue)
                        {
                            exp.FAILED_AMOUNT = (s.FailedAmount / s.Amount) * exp.AMOUNT;
                        }
                        //Neu ban bang gia nhap
                        if (material.ToList()[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = material.ToList()[0].TDL_MATERIAL_IMP_PRICE;
                            exp.VAT_RATIO = material.ToList()[0].TDL_MATERIAL_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ? materialPaties.Where(o => o.PATIENT_TYPE_ID == s.PatientTypeId && o.MATERIAL_ID == material.Key.MATERIAL_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + material.Key.MATERIAL_ID + "va patient_type_id: " + s.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TDL_MATERIAL_TYPE_ID = material.Key.TDL_MATERIAL_TYPE_ID;
                        exp.EQUIPMENT_SET_ID = s.EquipmentSetId;
                        exp.EQUIPMENT_SET_ORDER = 1;//mac dinh luc tao se dien thu tu la 1. Nguoi dung co nhu cau sua se vao sua o chuc nang bang ke
                        exp.PRES_AMOUNT = s.PresAmount;

                        expMestMaterials.Add(exp);
                        materialDic.Add(exp, material.Select(o => o.ID).ToList());
                    }
                    result.AddRange(expMestMaterials);
                }
                return result;
            }
            return null;
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialCreate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
        }
    }
}
