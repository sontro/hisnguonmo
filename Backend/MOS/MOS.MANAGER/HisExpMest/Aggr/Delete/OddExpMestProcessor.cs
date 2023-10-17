using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Delete
{
    class OddExpMestProcessor : BusinessBase
    {
        internal OddExpMestProcessor()
            : base()
        {

        }

        internal OddExpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST aggExpMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST> oddExpMests = new HisExpMestGet().GetOddByAggrExpMestId(aggExpMest.ID);
                if (IsNotNullOrEmpty(oddExpMests))
                {
                    bool valid = true;
                    HisExpMestCheck checker = new HisExpMestCheck(param);
                    foreach (HIS_EXP_MEST expMest in oddExpMests)
                    {
                        valid = valid && checker.IsUnlock(expMest);
                        valid = valid && checker.VerifyStatusForDelete(expMest);
                    }
                    if (valid)
                    {
                        foreach (HIS_EXP_MEST expMest in oddExpMests)
                        {
                            this.ProcessMaterial(expMest, ref sqls);
                            this.ProcessMaterial(expMest, ref sqls);
                            sqls.Add(String.Format("DELETE HIS_EXP_MEST WHERE ID = {0}", expMest.ID));
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }


        private void ProcessMaterial(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            if (expMest != null)
            {
                List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(hisExpMestMaterials))
                {
                    foreach (var material in hisExpMestMaterials)
                    {
                        if (material.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                            throw new Exception("Ton tai HIS_EXP_MEST_MATERIAL dang bi khoa" + LogUtil.TraceData("expMestMaterial", material));
                        }
                        if (material.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Ton tai HIS_EXP_MEST_MATERIAL o trang thai da xuat IS_EXPORT = 1" + LogUtil.TraceData("expMestMaterial", material));
                        }
                    }

                    List<long> expMestMaterialIds = hisExpMestMaterials.Select(o => o.ID).ToList();

                    string updateBean = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                    string deleteMaterial = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    listSql.Add(updateBean);
                    listSql.Add(deleteMaterial);//Luu y: can update bean truoc khi xoa material de tranh loi FK
                }
            }
        }

        private void ProcessMedicine(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            if (expMest != null)
            {
                List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                if (IsNotNullOrEmpty(hisExpMestMedicines))
                {
                    foreach (var medicine in hisExpMestMedicines)
                    {
                        if (medicine.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                            throw new Exception("Ton tai HIS_EXP_MEST_MEDICINE dang bi khoa" + LogUtil.TraceData("expMestMedicine", medicine));
                        }
                        if (medicine.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Ton tai HIS_EXP_MEST_MEDICINE o trang thai da xuat IS_EXPORT = 1" + LogUtil.TraceData("expMestMedicine", medicine));
                        }
                    }

                    List<long> expMestMedicineIds = hisExpMestMedicines.Select(o => o.ID).ToList();

                    string updateBean = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                    string deleteMedicine = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    listSql.Add(updateBean);
                    listSql.Add(deleteMedicine);//Luu y: can update bean truoc khi xoa medicine de tranh loi FK
                }
            }
        }
    }
}
