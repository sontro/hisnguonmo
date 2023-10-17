using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockPeriod
{
    class HisMediStockPeriodUpdateInventory : BusinessBase
    {
        private HisMestPeriodMediUpdate hisMestPeriodMediUpdate;

        private void Init()
        {
            this.hisMestPeriodMediUpdate = new HisMestPeriodMediUpdate(param);
            this.hisMestPeriodMediUpdate = new HisMestPeriodMediUpdate(param);
        }

        internal HisMediStockPeriodUpdateInventory()
            : base()
        {
            this.Init();
        }

        internal HisMediStockPeriodUpdateInventory(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        internal bool Run(HisMediStockPeriodInventorySDO sdo)
        {
            bool result = false;
            try
            {
                List<string> sqls = new List<string>();
                this.GenSql(sdo.MediStockPeriodId, sdo.Materials, ref sqls);
                this.GenSql(sdo.MediStockPeriodId, sdo.Medicines, ref sqls);

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Cap nhat so luong kiem ke that bai");
                }
                else
                {
                    result = true;
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

        private void GenSql(long mediStockPeriodId, List<HisMestPeriodMateSDO> materials, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(materials))
            {
                foreach (HisMestPeriodMateSDO sdo in materials)
                {
                    string sql = string.Format("UPDATE HIS_MEST_PERIOD_MATE S SET S.INVENTORY_AMOUNT = {0} WHERE S.ID = {1} AND S.MEDI_STOCK_PERIOD_ID = {2}", sdo.InventoryAmount, sdo.Id, mediStockPeriodId);
                    sqls.Add(sql);
                }
            }
        }

        private void GenSql(long mediStockPeriodId, List<HisMestPeriodMediSDO> materials, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(materials))
            {
                foreach (HisMestPeriodMediSDO sdo in materials)
                {
                    string sql = string.Format("UPDATE HIS_MEST_PERIOD_MEDI S SET S.INVENTORY_AMOUNT = {0} WHERE S.ID = {1} AND S.MEDI_STOCK_PERIOD_ID = {2}", sdo.InventoryAmount, sdo.Id, mediStockPeriodId);
                    sqls.Add(sql);
                }
            }
        }
    }
}
