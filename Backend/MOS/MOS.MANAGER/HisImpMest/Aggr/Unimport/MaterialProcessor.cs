using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.UnImport;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unimport
{
    class MaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;

        internal MaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
        }

        internal bool Run(HIS_IMP_MEST aggrImpMest, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMestMaterials))
                {
                    List<ExpMaterialSDO> materialSDOs = new List<ExpMaterialSDO>();
                    var Groups = impMestMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        if (!this.CheckCorrectImpExp(group.Key, aggrImpMest.MEDI_STOCK_ID, aggrImpMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MaterialId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMaterialSDO sdo = new ExpMaterialSDO();
                        sdo.Amount = totalAmount;
                        sdo.MaterialId = group.Key;
                        materialSDOs.Add(sdo);
                    }
                    List<HIS_MATERIAL_BEAN> hisMaterialBeans = null;
                    if (!this.hisMaterialBeanSplit.SplitByMaterial(materialSDOs, aggrImpMest.MEDI_STOCK_ID, ref hisMaterialBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo material, co the do khong du so luong " + LogUtil.TraceData("materialSDOs", materialSDOs));
                    }

                    string sql = DAOWorker.SqlDAO.AddInClause(hisMaterialBeans.Select(s => s.ID).ToList(), "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sql);
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

        private bool CheckCorrectImpExp(long materialId, long mediStockId, long aggrImpMestId)
        {
            List<ImpExpMestMaterialData> checks = new List<ImpExpMestMaterialData>();

            string queryExp = new StringBuilder().Append("SELECT -EMMA.AMOUNT as AMOUNT, EMMA.EXP_TIME as IMP_EXP_TIME FROM HIS_EXP_MEST_MATERIAL EMMA ").Append("JOIN HIS_EXP_MEST EXP ON EMMA.EXP_MEST_ID = EXP.ID ").Append(" WHERE EXP.MEDI_STOCK_ID = :param1").Append(" AND EMMA.MATERIAL_ID = :param2").Append(" AND EMMA.IS_EXPORT = 1").ToString();
            List<ImpExpMestMaterialData> expMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestMaterialData>(queryExp, mediStockId, materialId);
            if (!IsNotNullOrEmpty(expMaterials))
            {
                return true;
            }
            checks.AddRange(expMaterials);
            string queryImp = new StringBuilder().Append("SELECT IMMA.AMOUNT as AMOUNT, IMP.IMP_TIME as IMP_EXP_TIME FROM HIS_IMP_MEST_MATERIAL IMMA ").Append("JOIN HIS_IMP_MEST IMP ON IMMA.IMP_MEST_ID = IMP.ID ").Append("WHERE IMP.MEDI_STOCK_ID = :param1").Append(" AND (IMP.AGGR_IMP_MEST_ID IS NULL OR IMP.AGGR_IMP_MEST_ID <> :param2 )").Append(" AND IMP.IMP_MEST_STT_ID = :param3 ").Append(" AND IMMA.MATERIAL_ID = :param4").ToString();

            List<ImpExpMestMaterialData> impMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestMaterialData>(queryImp, mediStockId, aggrImpMestId, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT, materialId);

            if (!IsNotNullOrEmpty(impMaterials))
            {
                return false;
            }
            checks.AddRange(impMaterials);
            checks = checks.OrderBy(o => o.IMP_EXP_TIME).ToList();
            decimal availAmount = 0;
            foreach (ImpExpMestMaterialData item in checks)
            {
                availAmount += item.AMOUNT;
                if (availAmount < 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMaterialBeanSplit.RollBack();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
