using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Approve
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisExpMestMaterialCreate hisExpMestMaterialCreate;
        private HisExpMestMatyReqIncreaseDdAmount hisExpMestMatyReqIncreaseDdAmount;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisExpMestMaterialCreate = new HisExpMestMaterialCreate(param);
            this.hisExpMestMatyReqIncreaseDdAmount = new HisExpMestMatyReqIncreaseDdAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<ExpMaterialTypeSDO> materialSDOs, string loginname, string username, long approvalTime, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(materialSDOs) && IsNotNullOrEmpty(matyReqs))
                {
                    List<HIS_EXP_MEST_MATERIAL> data = new List<HIS_EXP_MEST_MATERIAL>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, List<long>> materialDic = new Dictionary<HIS_EXP_MEST_MATERIAL, List<long>>();

                    List<HIS_MATERIAL_BEAN> materialBeans = null;
                    List<HIS_MATERIAL_PATY> materialPaties = null;

                    long? expiredDate = (HisMediStockCFG.DONT_PRES_EXPIRED_ITEM && expMest.CHMS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION) ? (long?)approvalTime : null;

                    if (!hisMaterialBeanSplit.SplitByMaterialType(materialSDOs, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref materialBeans, ref materialPaties))
                    {
                        return false;
                    }

                    var group = materialBeans.GroupBy(o => new { o.MATERIAL_ID, o.TDL_MATERIAL_TYPE_ID });
                    foreach (var tmp in group)
                    {
                        ExpMaterialTypeSDO sdo = materialSDOs.Where(o => o.MaterialTypeId == tmp.Key.TDL_MATERIAL_TYPE_ID).FirstOrDefault();

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

                    if (IsNotNullOrEmpty(data) && !this.hisExpMestMaterialCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_material that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(materialDic, ref sqls);

                    this.ProcessDdAmount(matyReqs, data);

                    expMestMaterials = data;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
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

        private void ProcessDdAmount(List<HIS_EXP_MEST_MATY_REQ> matyReqs, List<HIS_EXP_MEST_MATERIAL> expMestMaterials)
        {
            if (!IsNotNullOrEmpty(matyReqs) || !IsNotNullOrEmpty(expMestMaterials)) return;

            Dictionary<long, decimal> increaseDic = new Dictionary<long, decimal>();

            //cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_MATY_REQ req in matyReqs)
            {
                decimal approvalAmount = expMestMaterials.Where(o => o.EXP_MEST_MATY_REQ_ID == req.ID).Sum(o => o.AMOUNT);
                if (approvalAmount > 0)
                {
                    increaseDic.Add(req.ID, approvalAmount);
                }
            }
            if (IsNotNullOrEmpty(increaseDic))
            {
                if (!this.hisExpMestMatyReqIncreaseDdAmount.Run(increaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMatyReqIncreaseDdAmount.Rollback();
            this.hisMaterialBeanSplit.RollBack();
            this.hisExpMestMaterialCreate.RollbackData();
        }
    }
}
