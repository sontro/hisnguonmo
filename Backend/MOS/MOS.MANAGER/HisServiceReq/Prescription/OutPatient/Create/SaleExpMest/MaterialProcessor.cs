using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create.SaleExpMest
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;

        internal MaterialProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
        }

        internal bool Run(List<PresOutStockMatySDO> serviceReqMaties, HIS_EXP_MEST expMest, long patientTypeId, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReqMaties) && expMest != null)
                {
                    List<ExpMaterialTypeSDO> materials = serviceReqMaties.Where(o => o.MaterialTypeId.HasValue)
                        .Select(o => new ExpMaterialTypeSDO
                        {
                            Amount = o.Amount,
                            NumOrder = o.NumOrder,
                            PatientTypeId = patientTypeId,
                            MaterialTypeId = o.MaterialTypeId.Value,
                            Tutorial = o.Tutorial,
                            PresAmount = o.PresAmount
                        }).ToList();

                    List<HIS_MATERIAL_BEAN> materialBeans = null;
                    List<HIS_MATERIAL_PATY> materialPaties = null;

                    long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? expMest.TDL_INTRUCTION_TIME : null; //lay TDL_INTRUCTION_TIME chu ko lay TDL_INTRUCTION_DATE, vi truong nay do trigger trong DB xu ly --> tai thoi diem nay, chua co gia tri
                    if (!this.hisMaterialBeanSplit.SplitByMaterialType(materials, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref materialBeans, ref materialPaties))
                    {
                        return false;
                    }

                    List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    //Duyet theo y/c cua client de tao ra exp_mest_material tuong ung

                    foreach (ExpMaterialTypeSDO sdo in materials)
                    {
                        //List<HIS_MATERIAL_BEAN> beans = materialBeans.Where(o => o.MATERIAL_ID == sdo.MaterialId).ToList();
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
                 
                    if (!this.hisExpMestMaterialCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(materialDic, ref sqls);

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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

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
            this.hisMaterialBeanSplit.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
