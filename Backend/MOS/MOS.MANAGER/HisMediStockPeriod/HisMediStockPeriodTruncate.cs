using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMaty;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.MANAGER.HisMestPeriodMety;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockPeriod
{
    class HisMediStockPeriodTruncate : BusinessBase
    {
        internal HisMediStockPeriodTruncate()
            : base()
        {

        }

        internal HisMediStockPeriodTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_PERIOD raw = null;
                HisMediStockPeriodCheck checker = new HisMediStockPeriodCheck(param);
                valid = valid && checker.CheckConstraint(id);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotApprove(raw);
                if (valid)
                {
                    string deleteMestPeriodMedi = string.Format("DELETE FROM HIS_MEST_PERIOD_MEDI WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestPeriodMate = string.Format("DELETE FROM HIS_MEST_PERIOD_MATE WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestPeriodMety = string.Format("DELETE FROM HIS_MEST_PERIOD_METY WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestPeriodMaty = string.Format("DELETE FROM HIS_MEST_PERIOD_MATY WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestPeriodBlty = string.Format("DELETE FROM HIS_MEST_PERIOD_BLTY WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestPeriodBlood = string.Format("DELETE FROM HIS_MEST_PERIOD_BLOOD WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMestInveUser = string.Format("DELETE FROM HIS_MEST_INVE_USER U WHERE EXISTS (SELECT 1 FROM HIS_MEST_INVENTORY M WHERE U.MEST_INVENTORY_ID = M.ID AND M.MEDI_STOCK_PERIOD_ID = {0})", id);
                    string deleteInventory = string.Format("DELETE FROM HIS_MEST_INVENTORY WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string resetExpMestMedicine = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET MEDI_STOCK_PERIOD_ID = NULL WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string resetExpMestBlood = string.Format("UPDATE HIS_EXP_MEST_BLOOD SET MEDI_STOCK_PERIOD_ID = NULL WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string resetExpMestMaterial = string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET MEDI_STOCK_PERIOD_ID = NULL WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string resetImpMest = string.Format("UPDATE HIS_IMP_MEST SET MEDI_STOCK_PERIOD_ID = NULL WHERE MEDI_STOCK_PERIOD_ID = {0}", id);
                    string deleteMediStockPeriod = string.Format("DELETE HIS_MEDI_STOCK_PERIOD WHERE ID = {0}", id);

                    List<string> sqls = new List<string>() {
                        deleteMestPeriodMedi,
                        deleteMestPeriodMate,
                        deleteMestPeriodMety,
                        deleteMestPeriodMaty,
                        deleteMestPeriodBlty,
                        deleteMestPeriodBlood,
                        deleteMestInveUser,
                        deleteInventory,
                        resetExpMestMedicine,
                        resetExpMestBlood,
                        resetExpMestMaterial,
                        resetImpMest,
                        deleteMediStockPeriod
                    };
                    result = DAOWorker.SqlDAO.Execute(sqls);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
