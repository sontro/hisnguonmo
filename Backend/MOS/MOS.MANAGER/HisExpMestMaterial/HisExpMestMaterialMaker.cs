using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMestMaterial
{
    class HisExpMestMaterialMaker : BusinessBase
    {
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private List<HisMaterialBeanSplit> beanSplitors = new List<HisMaterialBeanSplit>();
        private HisMaterialBeanSplitPlus beanSpliterPlus = null;

        internal HisExpMestMaterialMaker(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.beanSpliterPlus = new HisMaterialBeanSplitPlus(param);
        }

        internal bool Run(List<ExpMaterialTypeSDO> materials, HIS_EXP_MEST expMest, long? expiredDate, string loginname, string username, long? approvalTime, bool isAuto, ref List<HIS_EXP_MEST_MATERIAL> resultData, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(materials) && expMest != null)
                {
                    List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
                    {
                        Dictionary<ExpMaterialTypeSDO, List<HIS_MATERIAL_BEAN>> dicBeans = null;
                        List<HIS_MATERIAL_PATY> materialPaties = null;

                        if (isAuto && HisExpMestCFG.ALLOW_APPROVE_LESS_THAN_REQUEST)
                        {
                            if (!this.beanSpliterPlus.SplitAndDecreaseByMaterialType(materials, expMest.MEDI_STOCK_ID, expiredDate, null, ref dicBeans, ref materialPaties))
                            {
                                return false;
                            }
                            if (!IsNotNullOrEmpty(dicBeans))
                            {
                                LogSystem.Info("Khong co vat tu nao du kha dung de duyet");
                                return true;
                            }
                        }
                        else
                        {
                            if (!this.beanSpliterPlus.SplitByMaterialType(materials, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref dicBeans, ref materialPaties))
                            {
                                return false;
                            }
                        }

                        foreach (var dic in dicBeans)
                        {
                            List<HIS_MATERIAL_BEAN> materialBeans = dic.Value;
                            var GBeans = materialBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID });
                            ExpMaterialTypeSDO sdo = dic.Key;

                            foreach (var tmp in GBeans)
                            {
                                List<HIS_MATERIAL_BEAN> beans = tmp.ToList();

                                HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                                exp.EXP_MEST_ID = expMest.ID;
                                exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                                exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;
                                exp.NUM_ORDER = sdo.NumOrder;
                                exp.DESCRIPTION = sdo.Description;
                                exp.EXP_MEST_MATY_REQ_ID = sdo.ExpMestMatyReqId;
                                exp.PRICE = sdo.Price;
                                exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                                exp.VAT_RATIO = sdo.VatRatio;
                                exp.PRICE = sdo.Price;
                                exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                                exp.APPROVAL_LOGINNAME = loginname;
                                exp.APPROVAL_TIME = approvalTime;
                                exp.APPROVAL_USERNAME = username;
                                exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                                exp.IS_NOT_PRES = sdo.IsNotPres;
                                exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                                exp.TDL_TREATMENT_ID = sdo.TreatmentId;
                                data.Add(exp);
                                materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                            }
                        }
                    }
                    else
                    {
                        List<HIS_MATERIAL_BEAN> materialBeans = null;
                        List<HIS_MATERIAL_PATY> materialPaties = null;
                        HisMaterialBeanSplit spliter = new HisMaterialBeanSplit(param);
                        this.beanSplitors.Add(spliter);
                        if (!spliter.SplitByMaterialType(materials, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref materialBeans, ref materialPaties))
                        {
                            return false;
                        }

                        var group = materialBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID });
                        foreach (var tmp in group)
                        {
                            ExpMaterialTypeSDO sdo = materials.Where(o => o.MaterialTypeId == tmp.Key.TDL_MATERIAL_TYPE_ID).FirstOrDefault();

                            List<HIS_MATERIAL_BEAN> beans = tmp.ToList();

                            HIS_EXP_MEST_MATERIAL exp = new HIS_EXP_MEST_MATERIAL();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                            exp.MATERIAL_ID = tmp.Key.MATERIAL_ID;
                            exp.NUM_ORDER = sdo.NumOrder;
                            exp.DESCRIPTION = sdo.Description;
                            exp.EXP_MEST_MATY_REQ_ID = sdo.ExpMestMatyReqId;
                            exp.PRICE = sdo.Price;
                            exp.TDL_MATERIAL_TYPE_ID = beans[0].TDL_MATERIAL_TYPE_ID;
                            exp.VAT_RATIO = sdo.VatRatio;
                            exp.PRICE = sdo.Price;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_TIME = approvalTime;
                            exp.APPROVAL_USERNAME = username;
                            exp.TDL_AGGR_EXP_MEST_ID = expMest.AGGR_EXP_MEST_ID;
                            exp.IS_NOT_PRES = sdo.IsNotPres;
                            exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                            data.Add(exp);
                            materialDic.Add(exp, beans.Select(o => o.ID).ToList());
                        }

                    }
                    if (IsNotNullOrEmpty(data) && !this.hisExpMestMaterialCreate.CreateListSql(data))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(materialDic, ref sqls);

                    resultData = data;
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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> useBeandIdDic, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                foreach (HIS_EXP_MEST_MATERIAL expMestMaterial in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMaterial];
                    //cap nhat danh sach cac bean da dung
                    string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, EXP_MEST_MATERIAL_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    query = string.Format(query, expMestMaterial.ID);
                    sqls.Add(query);
                }
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSplitors))
            {
                foreach (var spliter in this.beanSplitors)
                {
                    spliter.RollBack();
                }
            }
            this.beanSpliterPlus.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
