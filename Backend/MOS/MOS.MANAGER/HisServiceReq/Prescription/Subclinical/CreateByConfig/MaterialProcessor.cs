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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
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

        internal bool Run(HIS_EXP_MEST expMest, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, HIS_SERE_SERV sereServ, List<HIS_SERVICE_MATY> serviceMaties, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                bool isExpend = HisSereServCFG.AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION == HisSereServCFG.SetExpendForAutoExpendPresOption.BHYT && sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || HisSereServCFG.AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION == HisSereServCFG.SetExpendForAutoExpendPresOption.ALL;

                List<PresMaterialSDO> materials = new List<PresMaterialSDO>();

                if (IsNotNullOrEmpty(serviceMaties))
                {
                    foreach (HIS_SERVICE_MATY m in serviceMaties)
                    {
                        //Chi lay cac thuoc duoc thiet lap den dich vu duoc ke tieu hao
                        if (m.SERVICE_ID == sereServ.SERVICE_ID && m.IS_ACTIVE == Constant.IS_TRUE)
                        {
                            HIS_MATERIAL_TYPE mt = HisMaterialTypeCFG.DATA.Where(o => o.ID == m.MATERIAL_TYPE_ID).FirstOrDefault();
                            PresMaterialSDO sdo = new PresMaterialSDO();
                            sdo.InstructionTimes = new List<long>() { expMest.TDL_INTRUCTION_TIME.Value };
                            sdo.IsExpend = isExpend;
                            sdo.MaterialTypeId = m.MATERIAL_TYPE_ID;
                            sdo.MediStockId = expMest.MEDI_STOCK_ID;
                            sdo.PatientTypeId = sereServ.PATIENT_TYPE_ID;
                            sdo.SereServParentId = sereServ.ID;
                            sdo.Amount = m.EXPEND_AMOUNT * sereServ.AMOUNT; //lay so luong dinh muc nhan voi so luong dich vu
                            materials.Add(sdo);
                        }
                    }
                }

                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();
                    List<HIS_MATERIAL_PATY> materialPaties = new List<HIS_MATERIAL_PATY>();

                    //Thuc hien lenh tach bean
                    List<HIS_MATERIAL_PATY> paties = null;
                    List<HIS_EXP_MEST_MATERIAL> data = this.SplitBeanAndMakeData(expMest.TDL_INTRUCTION_TIME.Value, expMest.ID, expMest.SERVICE_REQ_ID.Value, expMest.TDL_TREATMENT_ID.Value, processPackage37, processPackageBirth, processPackagePttm, materials, ref materialDic, ref paties);
                    if (IsNotNullOrEmpty(data))
                    {
                        expMestMaterials.AddRange(data);
                        if (IsNotNullOrEmpty(paties))
                        {
                            materialPaties.AddRange(paties);
                        }
                    }

                    List<long> choosenMaterailIds = expMestMaterials.Where(o => o.MATERIAL_ID.HasValue).Select(o => o.MATERIAL_ID.Value).ToList();
                    List<V_HIS_MATERIAL_2> choosenMaterials = new HisMaterialGet().GetView2ByIds(choosenMaterailIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMaterialWithBidDate(choosenMaterials, expMestMaterials, new List<HIS_EXP_MEST>() { expMest }))
                    {
                        throw new Exception("IsValidMaterialWithBidDate false. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(expMestMaterials) && !this.hisExpMestMaterialCreate.CreateList(expMestMaterials))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(materialDic, ref sqls);
                    resultData = expMestMaterials;
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
        private List<HIS_EXP_MEST_MATERIAL> SplitBeanAndMakeData(long instructionTime, long expMestId, long serviceReqId, long treatmentId, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMaterialSDO> toSplits, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic, ref List<HIS_MATERIAL_PATY> materialPaties)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;

            List<ExpMaterialTypeSDO> reqSplits = toSplits.Select(o => new ExpMaterialTypeSDO
            {
                Amount = o.Amount,
                MaterialTypeId = o.MaterialTypeId,
                PatientTypeId = o.PatientTypeId
            }).ToList();

            HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
            this.beanSpliters.Add(spliter);

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? (long?)instructionTime : null;
            if (!spliter.SplitByMaterialType(reqSplits, toSplits[0].MediStockId, expiredDate, null, null, ref materialBeans, ref materialPaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
            foreach (PresMaterialSDO req in toSplits)
            {
                //Do danh sach mety_req dam bao ko co material_type_id nao trung nhau ==> dung material_type_id de lay ra cac bean tuong ung
                //Neu ke theo lo thi can cu vao material_id
                List<HIS_MATERIAL_BEAN> reqBeans = materialBeans
                    .Where(o => (o.TDL_MATERIAL_TYPE_ID == req.MaterialTypeId && !req.MaterialId.HasValue) || (o.MATERIAL_ID == req.MaterialId))
                    .ToList();

                List<HIS_EXP_MEST_MATERIAL> materials = new List<HIS_EXP_MEST_MATERIAL>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MaterialTypeId:" + req.MaterialTypeId);
                }

                var group = reqBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_IMP_PRICE, o.TDL_MATERIAL_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    List<HIS_MATERIAL_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                    exp.EXP_MEST_ID = expMestId;
                    exp.TDL_SERVICE_REQ_ID = serviceReqId;
                    exp.TDL_TREATMENT_ID = treatmentId;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MATERIAL_TYPE_ID = req.MaterialTypeId;
                    exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;

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
                    exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                    exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                    materials.Add(exp);
                    materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                }

                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                processPackage37.Apply3Day7Day(null, materials, null, instructionTime);
                //Xu ly de ap dung goi de
                processPackageBirth.Run(materials, req.SereServParentId);
                //Xu ly de ap dung goi phau thuat tham my
                processPackagePttm.Run(materials, req.SereServParentId, instructionTime);

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
