using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMedicineBean.Handle;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(SubclinicalPresSDO presSdo, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                string sessionKey = SessionUtil.SessionKey(presSdo.ClientSessionKey);

                if (IsNotNullOrEmpty(presSdo.Materials) && IsNotNullOrEmpty(expMests))
                {
                    List<long> materialBeanIds = new List<long>();
                    foreach (PresMaterialSDO m in presSdo.Materials)
                    {
                        materialBeanIds.AddRange(m.MaterialBeanIds);
                    }

                    List<HIS_MATERIAL_BEAN> materialBeans = IsNotNullOrEmpty(materialBeanIds) ? new HisMaterialBeanGet().GetByIds(materialBeanIds) : null;
                    if (!IsNotNullOrEmpty(materialBeans))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("material_bean_id ko hop le");
                        return false;
                    }

                    List<HIS_MATERIAL_BEAN> unavailables = materialBeans
                        .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE && o.SESSION_KEY != sessionKey)
                        .ToList();

                    if (IsNotNullOrEmpty(unavailables))
                    {
                        LogSystem.Warn("Ton tai material_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                        return false;
                    }

                    //Neu trong d/s bean co bean ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia,
                    //phuc vu lay chinh sach gia
                    List<long> materialIds = materialBeans
                        .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE)
                        .Select(o => o.MATERIAL_ID).Distinct().ToList();

                    List<HIS_MATERIAL_PATY> materialPaties = null;
                    if (IsNotNullOrEmpty(materialIds))
                    {
                        List<long> patientTypeIds = presSdo.Materials.Select(o => o.PatientTypeId).Distinct().ToList();

                        HisMaterialPatyFilterQuery filter = new HisMaterialPatyFilterQuery();
                        filter.MATERIAL_IDs = materialIds;
                        filter.PATIENT_TYPE_IDs = patientTypeIds;
                        materialPaties = new HisMaterialPatyGet().Get(filter);
                    }

                    List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeanIdDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung
                    foreach (PresMaterialSDO sdo in presSdo.Materials)
                    {
                        //Lay exp_mest tuong ung voi kho xuat
                        HIS_EXP_MEST expMest = expMests.Where(o => o.MEDI_STOCK_ID == sdo.MediStockId).FirstOrDefault();
                        if (expMest == null)
                        {
                            LogSystem.Error("Ko ton tai exp_mest tuong ung voi medi_stock_id: " + sdo.MediStockId);
                            return false;
                        }

                        List<HIS_MATERIAL_BEAN> beans = materialBeans
                            //Neu ke theo lo (MaterialId) thi chi lay theo MaterialId
                            .Where(o => ((o.TDL_MATERIAL_TYPE_ID == sdo.MaterialTypeId && !sdo.MaterialId.HasValue) || o.MATERIAL_ID == sdo.MaterialId)
                                && o.MEDI_STOCK_ID == sdo.MediStockId && sdo.MaterialBeanIds != null && sdo.MaterialBeanIds.Contains(o.ID))
                            .OrderBy(o => o.MATERIAL_ID)
                            .ToList();

                        decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;

                        //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                        if (sdo.Amount != beanAmount)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("Ton tai material_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                            return false;
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
                            exp.IS_EXPEND = sdo.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                            exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                            exp.SERE_SERV_PARENT_ID = sdo.SereServParentId;
                            exp.IS_OUT_PARENT_FEE = sdo.IsOutParentFee && sdo.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                            exp.MATERIAL_ID = material.Key.MATERIAL_ID;
                            exp.SERIAL_NUMBER = material.Key.SERIAL_NUMBER;
                            exp.REMAIN_REUSE_COUNT = material.Key.REMAIN_REUSE_COUNT;
                            exp.NUM_ORDER = sdo.NumOrder;
                            if (sdo.FailedAmount.HasValue)
                            {
                                exp.FAILED_AMOUNT = (sdo.FailedAmount / sdo.Amount) * exp.AMOUNT;
                            }
                            
                            //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                            exp.EXPEND_TYPE_ID = sdo.IsBedExpend ? new Nullable<long>(1) : null;

                            //Neu ban bang gia nhap
                            if (material.ToList()[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                exp.PRICE = material.ToList()[0].TDL_MATERIAL_IMP_PRICE;
                                exp.VAT_RATIO = material.ToList()[0].TDL_MATERIAL_IMP_VAT_RATIO;
                            }
                            else
                            {
                                HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ? materialPaties.Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.MATERIAL_ID == material.Key.MATERIAL_ID).FirstOrDefault() : null;
                                if (paty == null)
                                {
                                    throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + material.Key.MATERIAL_ID + "va patient_type_id: " + sdo.PatientTypeId);
                                }
                                exp.PRICE = paty.EXP_PRICE;
                                exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                            }
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.TDL_MATERIAL_TYPE_ID = material.Key.TDL_MATERIAL_TYPE_ID;
                            exp.EQUIPMENT_SET_ID = sdo.EquipmentSetId;
                            exp.EQUIPMENT_SET_ORDER = 1;//mac dinh luc tao se dien thu tu la 1. Nguoi dung co nhu cau sua se vao sua o chuc nang bang ke
                            exp.PRES_AMOUNT = sdo.PresAmount;
                            expMestMaterials.Add(exp);

                            //Luu material_bean tuong ung voi tung exp_mest_material
                            useBeanIdDic.Add(exp, material.Select(o => o.ID).ToList());
                        }

                        data.AddRange(expMestMaterials);
                    }

                    if (IsNotNullOrEmpty(data))
                    {
                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, data, null, presSdo.InstructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(data, data[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(data, data[0].SERE_SERV_PARENT_ID, presSdo.InstructionTime);
                    }

                    List<long> choosenMaterailIds = data != null ? data.Where(o => o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList() : null;
                    List<V_HIS_MATERIAL_2> choosenMaterials = new HisMaterialGet().GetView2ByIds(choosenMaterailIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMaterialWithBidDate(choosenMaterials, data, expMests))
                    {
                        throw new Exception("IsValidMaterialWithBidDate false. Rollback du lieu");
                    }

                    if (!this.hisExpMestMaterialCreate.CreateList(data))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.SqlUpdateBean(sessionKey, useBeanIdDic, ref sqls);

                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void SqlUpdateBean(string sessionKey, Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                List<long> materialBeanIds = new List<long>();
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);

                    materialBeanIds.AddRange(beanIds);
                }

                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddNotInClause(materialBeanIds, "UPDATE HIS_MATERIAL_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
                query2 = string.Format(query2, sessionKey);
                sqls.Add(query2);
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
