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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class MaterialProcessor : BusinessBase
    {
        private List<HisMaterialBeanSplit> beanSpliters;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;

        internal MaterialProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.beanSpliters = new List<HisMaterialBeanSplit>();
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MATERIAL> oldMaterials, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMaterialSDO> materials, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATERIAL> refInserts, ref List<HIS_EXP_MEST_MATERIAL> refDeletes, long instructionTime, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> updates = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> beforeUpdates = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                    //Lay ra danh sach thong tin cu
                    //Ko lay du lieu co so seri (du lieu nay xu ly o phan khac)
                    List<HIS_EXP_MEST_MATERIAL> olds = IsNotNullOrEmpty(oldMaterials) ? oldMaterials.Where(o => o.SERIAL_NUMBER == null).ToList() : null;

                    //Danh sach exp_mest_material
                    List<HIS_EXP_MEST_MATERIAL> news = this.MakeData(olds, materials, expMest, instructionTime, ref newMaterialDic);
                    if (IsNotNullOrEmpty(news))
                    {
                        List<long> materailIds = news.Select(o => o.MATERIAL_ID.Value).ToList();
                        List<V_HIS_MATERIAL_2> choosenMaterials = new HisMaterialGet().GetView2ByIds(materailIds);

                        if (!new HisServiceReqPresCheck(param).IsValidMaterialWithBidDate(choosenMaterials, news, new List<HIS_EXP_MEST> { expMest }))
                        {
                            throw new Exception("IsValidMaterialWithBidDate false. Rollback du lieu");
                        }

                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, news, null, instructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(news, news[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(news, news[0].SERE_SERV_PARENT_ID, instructionTime);
                    }

                    List<long> expMestMaterialIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MATERIAL_BEAN> oldBeans = new HisMaterialBeanGet().GetByExpMestMaterialIds(expMestMaterialIds);

                    List<HIS_EXP_MEST_MATERIAL> inserts = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> deletes = new List<HIS_EXP_MEST_MATERIAL>();

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

                    this.PassResult(oldMaterials, inserts, updates, deletes, ref refInserts, ref refDeletes, ref resultData);
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

        private List<HIS_EXP_MEST_MATERIAL> MakeData(List<HIS_EXP_MEST_MATERIAL> olds, List<PresMaterialSDO> materials, HIS_EXP_MEST expMest, long instructionTime, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<ReqMaterialData> reqData = this.MakeReqMaterialData(materials, expMest);

            List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

            //Dictionary luu exp_mest_material va d/s bean tuong ung
            materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

            //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
            //  + 2 req co loai thuoc giong nhau thi can thuoc 2 lenh tach bean khac nhau
            //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau (cai nay ko
            // can xu ly vi sua don thuoc chi cho phep ke 1 kho)
            //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_material tuong ung
            List<long> useBeandIds = new List<long>();
            bool byMaterial = IsNotNullOrEmpty(reqData) && reqData.Exists(t => t.MaterialId.HasValue);

            if (IsNotNullOrEmpty(reqData))
            {
                var groups = reqData.GroupBy(o => o.IsNotPres);

                foreach (var group in groups)
                {
                    List<ReqMaterialData> lst = group.ToList();

                    while (lst != null && lst.Count > 0)
                    {
                        List<ReqMaterialData> toSplits = new List<ReqMaterialData>();
                        foreach (ReqMaterialData s in lst)
                        {
                            if (!byMaterial && !toSplits.Exists(t => t.MaterialTypeId == s.MaterialTypeId))
                            {
                                toSplits.Add(s);
                            }
                            else if ((byMaterial && !toSplits.Exists(t => t.MaterialId == s.MaterialId)))
                            {
                                toSplits.Add(s);
                            }
                        }
                        lst.RemoveAll(o => toSplits.Exists(t => t.Id == o.Id));

                        //Thuc hien lenh tach bean
                        List<HIS_EXP_MEST_MATERIAL> data = this.SplitBeanAndMakeData(byMaterial, olds, toSplits, instructionTime, useBeandIds, ref materialDic);
                        if (IsNotNullOrEmpty(data))
                        {
                            expMestMaterials.AddRange(data);
                        }
                    }
                }
            }

            return expMestMaterials;
        }

        /// <summary>
        /// Tach bean theo ReqMaterialData va tao ra exp_mest_material tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMaterialData dam bao ko co material_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MATERIAL> SplitBeanAndMakeData(bool byMaterial, List<HIS_EXP_MEST_MATERIAL> olds, List<ReqMaterialData> toSplits, long instructionTime, List<long> useMaterialBeandIds, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<HIS_MATERIAL_PATY> materialPaties = null;

            if (!byMaterial)
            {
                List<ExpMaterialTypeSDO> reqSplits = new List<ExpMaterialTypeSDO>();
                foreach (ReqMaterialData req in toSplits)
                {
                    short? isExpend = req.IsExpend ? (short?)Constant.IS_TRUE : null;
                    short? isOutParentFee = req.IsOutParentFee ? (short?)Constant.IS_TRUE : null;

                    List<long> expMaterialIds = olds != null ? olds
                        .Where(o => o.TDL_MATERIAL_TYPE_ID == req.MaterialTypeId
                            && o.PATIENT_TYPE_ID == req.PatientTypeId
                            && o.SERE_SERV_PARENT_ID == req.SereServParentId
                            && o.IS_EXPEND == isExpend
                            && o.SERVICE_CONDITION_ID == req.ServiceConditionId
                            && o.OTHER_PAY_SOURCE_ID == req.OtherPaySourceId
                            && o.IS_OUT_PARENT_FEE == isOutParentFee
                            && o.EQUIPMENT_SET_ID == req.EquipmentSetId)
                        .Select(o => o.ID).ToList() : null;

                    ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                    sdo.Amount = req.Amount;
                    sdo.ExpMestMaterialIds = expMaterialIds;
                    sdo.MaterialTypeId = req.MaterialTypeId;
                    sdo.PatientTypeId = req.PatientTypeId;
                    reqSplits.Add(sdo);
                }
                HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                this.beanSpliters.Add(spliter);

                //Truyen vao "useMaterialBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
                //(trong lan tach bean truoc do - trong cung 1 lan ke don)
                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?)instructionTime : null;

                if (!spliter.SplitByMaterialType(reqSplits, toSplits[0].MediStockId, expiredDate, null, useMaterialBeandIds, ref materialBeans, ref materialPaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }
            else
            {
                List<ExpMaterialSDO> reqSplits = new List<ExpMaterialSDO>();
                foreach (ReqMaterialData req in toSplits)
                {
                    short? isExpend = req.IsExpend ? (short?)Constant.IS_TRUE : null;
                    short? isOutParentFee = req.IsOutParentFee ? (short?)Constant.IS_TRUE : null;

                    List<long> expMaterialIds = olds != null ? olds
                        .Where(o => o.MATERIAL_ID == req.MaterialId
                            && o.PATIENT_TYPE_ID == req.PatientTypeId
                            && o.SERE_SERV_PARENT_ID == req.SereServParentId
                            && o.IS_EXPEND == isExpend
                            && o.SERVICE_CONDITION_ID == req.ServiceConditionId
                            && o.OTHER_PAY_SOURCE_ID == req.OtherPaySourceId
                            && o.IS_OUT_PARENT_FEE == isOutParentFee
                            && o.EQUIPMENT_SET_ID == req.EquipmentSetId)
                        .Select(o => o.ID).ToList() : null;

                    ExpMaterialSDO sdo = new ExpMaterialSDO();
                    sdo.Amount = req.Amount;
                    sdo.ExpMestMaterialIds = expMaterialIds;
                    sdo.MaterialId = req.MaterialId.Value;
                    sdo.PatientTypeId = req.PatientTypeId;
                    reqSplits.Add(sdo);
                }
                HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                this.beanSpliters.Add(spliter);

                //Truyen vao "useMaterialBeandIds" de viec tach bean ko lay ra ve cac bean da duoc su dung
                //(trong lan tach bean truoc do - trong cung 1 lan ke don)
                if (!spliter.SplitByMaterial(reqSplits, toSplits[0].MediStockId, useMaterialBeandIds, ref materialBeans, ref materialPaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }

            if (IsNotNullOrEmpty(materialBeans))
            {
                //Cap nhat lai danh sach bean da duoc dung
                useMaterialBeandIds.AddRange(materialBeans.Select(o => o.ID).ToList());

                List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

                //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
                foreach (ReqMaterialData req in toSplits)
                {
                    //Do danh sach mety_req dam bao ko co material_type_id nao trung nhau ==> dung material_type_id de lay ra cac bean tuong ung
                    List<HIS_MATERIAL_BEAN> reqBeans = materialBeans
                        .Where(o => (o.TDL_MATERIAL_TYPE_ID == req.MaterialTypeId && !req.MaterialId.HasValue) || (o.MATERIAL_ID == req.MaterialId))
                        .ToList();

                    if (!IsNotNullOrEmpty(reqBeans))
                    {
                        throw new Exception("Ko tach duoc bean tuong ung voi MaterialTypeId:" + req.MaterialTypeId);
                    }

                    var group = reqBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_IMP_PRICE, o.TDL_MATERIAL_IMP_VAT_RATIO });
                    foreach (var tmp in group)
                    {
                        List<HIS_MATERIAL_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                        exp.EXP_MEST_ID = req.ExpMestId;
                        exp.TDL_SERVICE_REQ_ID = req.ServiceReqId;
                        exp.TDL_TREATMENT_ID = req.TreatmentId;
                        exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                        exp.TDL_MATERIAL_TYPE_ID = req.MaterialTypeId;
                        exp.EQUIPMENT_SET_ID = req.EquipmentSetId;
                        exp.EQUIPMENT_SET_ORDER = 1;//mac dinh luc tao se dien thu tu la 1. Nguoi dung co nhu cau sua se vao sua o chuc nang bang ke
                        exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;

                        //Neu ban bang gia nhap
                        if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = tmp.Key.TDL_MATERIAL_IMP_PRICE;
                            exp.VAT_RATIO = tmp.Key.TDL_MATERIAL_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ? materialPaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MATERIAL_ID == tmp.Key.MATERIAL_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + tmp.Key.MATERIAL_ID + "va patient_type_id: " + req.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }

                        exp.TDL_MEDI_STOCK_ID = req.MediStockId;
                        exp.NUM_ORDER = req.NumOrder;
                        exp.TUTORIAL = req.Tutorial;
                        exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.SERVICE_CONDITION_ID = req.ServiceConditionId;
                        exp.OTHER_PAY_SOURCE_ID = req.OtherPaySourceId;
                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                        exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                        exp.IS_NOT_PRES = req.IsNotPres ? (short?)Constant.IS_TRUE : null;
                        //Neu phan so luong lam tron chu ko phai do BS ke va DTTT la BHYT thi tu dong chuyen sang DTTT la vien phi (de ko xuat hien trong XML day len BHYT)
                        exp.PATIENT_TYPE_ID = req.IsNotPres && req.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE : req.PatientTypeId;
                        exp.PRES_AMOUNT = req.PresAmount;
                        exp.EXCEED_LIMIT_IN_PRES_REASON = req.ExceedLimitInPresReason;
                        exp.EXCEED_LIMIT_IN_DAY_REASON = req.ExceedLimitInDayReason;
                        data.Add(exp);

                        materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }
                }
                return data;
            }
            return null;
        }

        private void GetDiff(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> news, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> newMaterialDic, List<HIS_MATERIAL_BEAN> oldBeans, ref List<HIS_EXP_MEST_MATERIAL> inserts, ref List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> updates, ref List<HIS_EXP_MEST_MATERIAL> oldOfUpdates)
        {
            if (deletes == null)
            {
                deletes = new List<HIS_EXP_MEST_MATERIAL>();
            }
            if (inserts == null)
            {
                inserts = new List<HIS_EXP_MEST_MATERIAL>();
            }
            if (updates == null)
            {
                updates = new List<HIS_EXP_MEST_MATERIAL>();
            }
            if (oldOfUpdates == null)
            {
                oldOfUpdates = new List<HIS_EXP_MEST_MATERIAL>();
            }

            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                if (IsNotNullOrEmpty(olds)) //check null, empty de tranh loi addrange
                {
                    deletes.AddRange(olds);
                }
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                if (IsNotNullOrEmpty(news)) //check null, empty de tranh loi addrange
                {
                    inserts.AddRange(news);
                }
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
                            && t.SERVICE_CONDITION_ID == newMaterial.SERVICE_CONDITION_ID
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
                            && t.IS_NOT_PRES == newMaterial.IS_NOT_PRES
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaterial);
                    }
                    else if (old.NUM_ORDER != newMaterial.NUM_ORDER || old.TUTORIAL != newMaterial.TUTORIAL || old.PRES_AMOUNT != newMaterial.PRES_AMOUNT
                        || old.EXCEED_LIMIT_IN_PRES_REASON != newMaterial.EXCEED_LIMIT_IN_PRES_REASON
                        || old.EXCEED_LIMIT_IN_DAY_REASON != newMaterial.EXCEED_LIMIT_IN_DAY_REASON)
                    {
                        HIS_EXP_MEST_MATERIAL oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MATERIAL>(old);
                        old.NUM_ORDER = newMaterial.NUM_ORDER;
                        old.TUTORIAL = newMaterial.TUTORIAL;
                        old.PRES_AMOUNT = newMaterial.PRES_AMOUNT;
                        old.EXCEED_LIMIT_IN_PRES_REASON = newMaterial.EXCEED_LIMIT_IN_PRES_REASON;
                        old.EXCEED_LIMIT_IN_DAY_REASON = newMaterial.EXCEED_LIMIT_IN_DAY_REASON;

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
                            && t.SERVICE_CONDITION_ID == old.SERVICE_CONDITION_ID
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
                            && t.IS_NOT_PRES == old.IS_NOT_PRES
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

        private void PassResult(List<HIS_EXP_MEST_MATERIAL> olds, List<HIS_EXP_MEST_MATERIAL> inserts, List<HIS_EXP_MEST_MATERIAL> updates, List<HIS_EXP_MEST_MATERIAL> deletes, ref List<HIS_EXP_MEST_MATERIAL> refInserts, ref List<HIS_EXP_MEST_MATERIAL> refDeletes, ref List<HIS_EXP_MEST_MATERIAL> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                if (resultData == null)
                {
                    resultData = new List<HIS_EXP_MEST_MATERIAL>();
                }

                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                    refInserts.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(deletes))
                {
                    refDeletes.AddRange(deletes);
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

        /// <summary>
        /// Duyet theo y/c cua client de tao ra yeu cau tach bean tuong ung
        /// Với mỗi y/c, co bao nhieu exp_mest tuong ung voi kho đó thi tao ra bấy nhiêu y/c tách bean giống nhau
        /// (do cung 1 kho ma co nhieu exp_mest ==> do chỉ định nhiều ngày khác nhau)
        /// </summary>
        /// <param name="materials"></param>
        /// <param name="expMests"></param>
        /// <returns></returns>
        private List<ReqMaterialData> MakeReqMaterialData(List<PresMaterialSDO> materials, HIS_EXP_MEST expMest)
        {
            if (IsNotNullOrEmpty(materials))
            {
                //Xu ly de tach stent trong truong hop vat tu la stent
                List<PresMaterialSDO> mt = StentUtil.MakeSingleStent(materials);

                List<ReqMaterialData> reqData = new List<ReqMaterialData>();
                int index = 0;
                foreach (PresMaterialSDO sdo in mt)
                {
                    ReqMaterialData req = new ReqMaterialData(sdo, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, ++index, expMest.TDL_INTRUCTION_TIME.Value);
                    reqData.Add(req);
                }
                return reqData;
            }
            return null;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSpliters))
            {
                foreach (HisMaterialBeanSplit spliter in this.beanSpliters)
                {
                    spliter.RollBack();
                }
            }

            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
