using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.UnImport;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Unimport
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;

        internal MedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HIS_IMP_MEST aggrImpMest, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMestMedicines))
                {
                    List<ExpMedicineSDO> medicineSDOs = new List<ExpMedicineSDO>();
                    var Groups = impMestMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        if (!this.CheckCorrectImpExp(group.Key, aggrImpMest.MEDI_STOCK_ID, aggrImpMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MedicineId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMedicineSDO sdo = new ExpMedicineSDO();
                        sdo.Amount = totalAmount;
                        sdo.MedicineId = group.Key;
                        medicineSDOs.Add(sdo);
                    }
                    List<HIS_MEDICINE_BEAN> hisMedicineBeans = null;
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(medicineSDOs, aggrImpMest.MEDI_STOCK_ID, ref hisMedicineBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo medicine, co the do khong du so luong " + LogUtil.TraceData("medicineSDOs", medicineSDOs));
                    }

                    string sql = DAOWorker.SqlDAO.AddInClause(hisMedicineBeans.Select(s => s.ID).ToList(), "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
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

        private bool CheckCorrectImpExp(long medicineId, long mediStockId, long aggrImpMestId)
        {
            List<ImpExpMestMedicineData> checks = new List<ImpExpMestMedicineData>();

            string queryExp = new StringBuilder().Append("SELECT -EMMA.AMOUNT as AMOUNT, EMMA.EXP_TIME as IMP_EXP_TIME FROM HIS_EXP_MEST_MEDICINE EMMA ").Append("JOIN HIS_EXP_MEST EXP ON EMMA.EXP_MEST_ID = EXP.ID ").Append(" WHERE EXP.MEDI_STOCK_ID = :param1").Append(" AND EMMA.MEDICINE_ID = :param2").Append(" AND EMMA.IS_EXPORT = 1").ToString();
            List<ImpExpMestMedicineData> expMedicines = DAOWorker.SqlDAO.GetSql<ImpExpMestMedicineData>(queryExp, mediStockId, medicineId);
            if (!IsNotNullOrEmpty(expMedicines))
            {
                return true;
            }
            checks.AddRange(expMedicines);
            string queryImp = new StringBuilder().Append("SELECT IMMA.AMOUNT as AMOUNT, IMP.IMP_TIME as IMP_EXP_TIME FROM HIS_IMP_MEST_MEDICINE IMMA ").Append("JOIN HIS_IMP_MEST IMP ON IMMA.IMP_MEST_ID = IMP.ID ").Append("WHERE IMP.MEDI_STOCK_ID = :param1").Append(" AND (IMP.AGGR_IMP_MEST_ID IS NULL OR IMP.AGGR_IMP_MEST_ID <> :param2 )").Append(" AND IMP.IMP_MEST_STT_ID = :param3 ").Append(" AND IMMA.MEDICINE_ID = :param4").ToString();

            List<ImpExpMestMedicineData> impMedicines = DAOWorker.SqlDAO.GetSql<ImpExpMestMedicineData>(queryImp, mediStockId, aggrImpMestId, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT, medicineId);

            if (!IsNotNullOrEmpty(impMedicines))
            {
                return false;
            }
            checks.AddRange(impMedicines);
            checks = checks.OrderBy(o => o.IMP_EXP_TIME).ToList();
            decimal availAmount = 0;
            foreach (ImpExpMestMedicineData item in checks)
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
                this.hisMedicineBeanSplit.RollBack();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
