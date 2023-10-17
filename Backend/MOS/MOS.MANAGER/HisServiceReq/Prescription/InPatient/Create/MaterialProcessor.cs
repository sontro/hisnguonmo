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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class MaterialProcessor : BusinessBase
    {
        private List<HisMaterialBeanSplit> beanSpliters;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.beanSpliters = new List<HisMaterialBeanSplit>();
        }

        internal bool Run(HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMaterialSDO> materials, List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && IsNotNullOrEmpty(expMests))
                {
                    List<ReqMaterialData> reqData = this.MakeReqMaterialData(materials, expMests);

                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

                    //Dictionary luu exp_mest_material va d/s bean tuong ung
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    //- Duyet danh sach yeu cau de tao ra cac lenh tach bean. Can thoa man:
                    //  + 2 req co loai thuoc (hoac lo thuoc) giong nhau thi can thuoc 2 lenh tach bean khac nhau
                    //  + 2 req co medi_stock_id khac nhau thi can thuoc 2 lenh tach bean khac nhau
                    //- Sau khi thuc hien tach bean, thuc hien tao exp_mest_material tuong ung
                    bool byMaterial = IsNotNullOrEmpty(reqData) && reqData.Exists(t => t.MaterialId.HasValue);

                    //Neu co cau hinh ko ke thuoc/vat tu het HSD va hinh thuc ke don ko phai la ke theo lo
                    //thi can gom nhom theo thoi gian y lenh de xu ly ko layb lo thuoc het HSD
                    if (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && !byMaterial)
                    {
                        var groups = reqData.GroupBy(o => new { o.MediStockId, o.InstructionTime, o.IsNotPres });
                        foreach (var group in groups)
                        {
                            List<ReqMaterialData> lst = group.ToList();
                            while (lst.Count > 0)
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
                                List<HIS_EXP_MEST_MATERIAL> data = this.SplitBeanAndMakeData(byMaterial, group.Key.InstructionTime, processPackage37, processPackageBirth, processPackagePttm, toSplits, ref materialDic);
                                if (IsNotNullOrEmpty(data))
                                {
                                    expMestMaterials.AddRange(data);
                                }
                            }
                        }
                    }
                    else
                    {
                        var groups = reqData.GroupBy(o => new { o.MediStockId, o.IsNotPres });
                        foreach (var group in groups)
                        {
                            List<ReqMaterialData> lst = group.ToList();
                            while (lst.Count > 0)
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
                                List<HIS_EXP_MEST_MATERIAL> data = this.SplitBeanAndMakeData(byMaterial, null, processPackage37, processPackageBirth, processPackagePttm, toSplits, ref materialDic);
                                if (IsNotNullOrEmpty(data))
                                {
                                    expMestMaterials.AddRange(data);
                                }
                            }
                        }
                    }

                    List<long> materailIds = expMestMaterials.Select(o => o.MATERIAL_ID.Value).ToList();
                    List<V_HIS_MATERIAL_2> choosenMaterials = new HisMaterialGet().GetView2ByIds(materailIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMaterialWithBidDate(choosenMaterials, expMestMaterials, expMests))
                    {
                        throw new Exception("IsValidMaterialWithBidDate false. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(expMestMaterials) && !this.hisExpMestMaterialCreate.CreateList(expMestMaterials))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(materialDic, ref sqls);
                    if (resultData == null)
                    {
                        resultData = new List<HIS_EXP_MEST_MATERIAL>();
                    }
                    resultData.AddRange(expMestMaterials);
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

        /// <summary>
        /// Tach bean theo ReqMaterialData va tao ra exp_mest_material tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMaterialData dam bao ko co material_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MATERIAL> SplitBeanAndMakeData(bool byMaterial, long? instructionTime, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<ReqMaterialData> toSplits, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<HIS_MATERIAL_PATY> materialPaties = null;

            //Neu ke theo lo
            if (byMaterial)
            {
                List<ExpMaterialSDO> reqSplits = toSplits.Select(o => new ExpMaterialSDO
                {
                    Amount = o.Amount,
                    MaterialId = o.MaterialId.Value,
                    PatientTypeId = o.PatientTypeId
                }).ToList();

                HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                this.beanSpliters.Add(spliter);

                if (!spliter.SplitByMaterial(reqSplits, toSplits[0].MediStockId, null, ref materialBeans, ref materialPaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }
            else
            {
                List<ExpMaterialTypeSDO> reqSplits = toSplits.Select(o => new ExpMaterialTypeSDO
                {
                    Amount = o.Amount,
                    MaterialTypeId = o.MaterialTypeId,
                    PatientTypeId = o.PatientTypeId
                }).ToList();

                HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                this.beanSpliters.Add(spliter);

                if (!spliter.SplitByMaterialType(reqSplits, toSplits[0].MediStockId, instructionTime, null, null, ref materialBeans, ref materialPaties))
                {
                    throw new Exception("Tach bean that bai. Rollback du lieu");
                }
            }

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

                List<HIS_EXP_MEST_MATERIAL> materials = new List<HIS_EXP_MEST_MATERIAL>();
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
                    exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                    exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                    exp.IS_NOT_PRES = req.IsNotPres ? (short?)Constant.IS_TRUE : null;
                    //Neu phan so luong lam tron chu ko phai do BS ke va DTTT la BHYT thi tu dong chuyen sang DTTT la vien phi (de ko xuat hien trong XML day len BHYT)
                    exp.PATIENT_TYPE_ID = req.IsNotPres && req.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT ? HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE : req.PatientTypeId;

                    //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                    exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;
                    exp.PRES_AMOUNT = req.PresAmount;
                    exp.EXCEED_LIMIT_IN_PRES_REASON = req.ExceedLimitInPresReason;
                    exp.EXCEED_LIMIT_IN_DAY_REASON = req.ExceedLimitInDayReason;
                    materials.Add(exp);

                    materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                }
                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                processPackage37.Apply3Day7Day(null, materials, null, req.InstructionTime);
                //Xu ly de ap dung goi de
                processPackageBirth.Run(materials, req.SereServParentId);
                //Xu ly de ap dung goi phau thuat tham my
                processPackagePttm.Run(materials, req.SereServParentId, req.InstructionTime);

                data.AddRange(materials);
            }
            return data;
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
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
        private List<ReqMaterialData> MakeReqMaterialData(List<PresMaterialSDO> materials, List<HIS_EXP_MEST> expMests)
        {
            if (IsNotNullOrEmpty(materials))
            {
                //Xu ly de tach stent trong truong hop vat tu la stent
                List<PresMaterialSDO> mt = StentUtil.MakeSingleStent(materials);

                List<ReqMaterialData> reqData = new List<ReqMaterialData>();

                int index = 0;
                foreach (PresMaterialSDO sdo in mt)
                {
                    //Lay exp_mest tuong ung voi kho xuat va thoi gian y lenh
                    List<HIS_EXP_MEST> exps = null;

                    if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                    {
                        if (HisExpMestCFG.IS_SPLIT_STAR_MARK)
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId && o.IS_STAR_MARK != Constant.IS_TRUE).ToList();
                        }
                        else
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId).ToList();
                        }
                    }
                    else
                    {
                        if (HisExpMestCFG.IS_SPLIT_STAR_MARK)
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId
                                && sdo.InstructionTimes.Contains(o.TDL_INTRUCTION_TIME.Value) && o.IS_STAR_MARK != Constant.IS_TRUE).ToList();
                        }
                        else
                        {
                            exps = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId
                            && sdo.InstructionTimes.Contains(o.TDL_INTRUCTION_TIME.Value)).ToList();
                        }
                    }

                    if (!IsNotNullOrEmpty(exps))
                    {
                        LogSystem.Error("Ko ton tai exp_mest tuong ung voi medi_stock_id: " + sdo.MediStockId);
                    }

                    foreach (HIS_EXP_MEST expMest in exps)
                    {
                        ReqMaterialData req = new ReqMaterialData(sdo, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, ++index, expMest.TDL_INTRUCTION_TIME.Value);
                        reqData.Add(req);
                    }
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
