using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class ExpMaterialProcessor : BusinessBase
    {
        private long expMestId;

        internal ExpMaterialProcessor(CommonParam param)
            : base(param)
        {
        
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    this.expMestId = expMest.ID;
                    List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestMaterials))
                    {
                        foreach (var material in hisExpMestMaterials)
                        {
                            if (material.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_MATERIAL dang bi khoa" + LogUtil.TraceData("expMestMaterial", material));
                                return false;
                            }
                            if (material.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_MATERIAL o trang thai da xuat IS_EXPORT = 1" + LogUtil.TraceData("expMestMaterial", material));
                                return false;
                            }
                        }

                        List<long> expMestMaterialIds = hisExpMestMaterials.Select(o => o.ID).ToList();

                        string updateBean = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
                        string deleteMaterial = DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(updateBean);
                        sqls.Add(deleteMaterial);//Luu y: can update bean truoc khi xoa material de tranh loi FK
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
    }
}
