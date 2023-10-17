using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class MaterialBySerialNumberProcessor : BusinessBase
    {
        private List<HisMaterialBeanTakeBySerial> beanTakers;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialBySerialNumberProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.beanTakers = new List<HisMaterialBeanTakeBySerial>();
        }

        internal bool Run(HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMaterialBySerialNumberSDO> materials, List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && IsNotNullOrEmpty(expMests))
                {
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

                    //Dictionary luu exp_mest_material va d/s bean tuong ung
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    foreach (HIS_EXP_MEST expMest in expMests)
                    {
                        if (HisExpMestCFG.IS_SPLIT_STAR_MARK && expMest.IS_STAR_MARK == Constant.IS_TRUE)
                            continue;
                        List<PresMaterialBySerialNumberSDO> reqMaterials = materials.Where(o => o.MediStockId == expMest.MEDI_STOCK_ID).ToList();
                        //Thuc hien lenh tach bean
                        List<HIS_EXP_MEST_MATERIAL> data = this.TakeBeanAndMakeData(processPackage37, processPackageBirth, processPackagePttm, reqMaterials, expMest, ref materialDic);
                        if (IsNotNullOrEmpty(data))
                        {
                            expMestMaterials.AddRange(data);
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
        private List<HIS_EXP_MEST_MATERIAL> TakeBeanAndMakeData(HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, List<PresMaterialBySerialNumberSDO> reqMaterials, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic)
        {
            List<HIS_MATERIAL_BEAN> materialBeans = null;
            List<string> serialNumbers = reqMaterials != null ? reqMaterials.Select(o => o.SerialNumber).ToList() : null;
            if (!IsNotNullOrEmpty(serialNumbers))
            {
                return null;
            }
            HisMaterialBeanTakeBySerial beanTaker = new HisMaterialBeanTakeBySerial(param);
            this.beanTakers.Add(beanTaker);
            if (!beanTaker.Run(serialNumbers, expMest.MEDI_STOCK_ID, ref materialBeans))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            //Lay ra cac vat tu ko ban bang gia nhap de lay chinh sach gia
            List<long> tmpMaterialIds = materialBeans.Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE)
                .Select(o => o.MATERIAL_ID).ToList();
            List<HIS_MATERIAL_PATY> materialPaties = new HisMaterialPatyGet().GetByMaterialIds(tmpMaterialIds);

            List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_material) tuong ung
            foreach (PresMaterialBySerialNumberSDO req in reqMaterials)
            {
                List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.SERIAL_NUMBER == req.SerialNumber).ToList();

                if (!IsNotNullOrEmpty(beans))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_VatTuKhongCoSan, req.SerialNumber);
                    throw new Exception("Ko lay duoc vat tu tuong ung voi so seri: " + req.SerialNumber);
                }

                if (beans.Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_CoNhieuHon1BeanVoiSoSeri, req.SerialNumber);
                    throw new Exception("Co nhieu hon 1 ban ghi bean voi so seri: " + req.SerialNumber);
                }

                HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                exp.EXP_MEST_ID = expMest.ID;
                exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                exp.MATERIAL_ID = beans[0].MATERIAL_ID;
                exp.TDL_MEDI_STOCK_ID = req.MediStockId;
                exp.NUM_ORDER = req.NumOrder;
                exp.SERIAL_NUMBER = beans[0].SERIAL_NUMBER;
                exp.REMAIN_REUSE_COUNT = beans[0].REMAIN_REUSE_COUNT;
                exp.IS_EXPEND = req.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                exp.IS_OUT_PARENT_FEE = req.IsOutParentFee && req.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                exp.PATIENT_TYPE_ID = req.PatientTypeId;
                exp.SERE_SERV_PARENT_ID = req.SereServParentId;
                //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                exp.EXPEND_TYPE_ID = req.IsBedExpend ? new Nullable<long>(1) : null;

                //Neu ban bang gia nhap
                if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                {
                    exp.PRICE = beans[0].TDL_MATERIAL_IMP_PRICE;
                    exp.VAT_RATIO = beans[0].TDL_MATERIAL_IMP_VAT_RATIO;
                }
                else
                {
                    HIS_MATERIAL_PATY paty = IsNotNullOrEmpty(materialPaties) ? materialPaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MATERIAL_ID == beans[0].MATERIAL_ID).FirstOrDefault() : null;
                    if (paty == null)
                    {
                        throw new Exception("Khong ton tai chinh sach gia tuong ung voi material_id: " + beans[0].MATERIAL_ID + "va patient_type_id: " + req.PatientTypeId);
                    }
                    exp.PRICE = paty.EXP_PRICE;
                    exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                }

                materialDic.Add(exp, beans.Select(o => o.ID).ToList());

                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                processPackage37.Apply3Day7Day(null, new List<HIS_EXP_MEST_MATERIAL>() { exp }, null, expMest.TDL_INTRUCTION_TIME.Value);
                //Xu ly de ap dung goi de
                processPackageBirth.Run(new List<HIS_EXP_MEST_MATERIAL>() { exp }, req.SereServParentId);
                //Xu ly de ap dung goi phau thuat tham my
                processPackagePttm.Run(new List<HIS_EXP_MEST_MATERIAL>() { exp }, req.SereServParentId, expMest.TDL_INTRUCTION_TIME.Value);

                data.Add(exp);
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
            if (IsNotNullOrEmpty(this.beanTakers))
            {
                foreach (HisMaterialBeanTakeBySerial beanTaker in this.beanTakers)
                {
                    beanTaker.Rollback();
                }
            }

            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
