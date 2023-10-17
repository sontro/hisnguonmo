using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class ExpMestMedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanExport hisMedicineBeanExport;

        internal ExpMestMedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisMedicineBeanExport = new HisMedicineBeanExport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                this.ProcessMedicineBean(hisExpMestMedicines, expMest.MEDI_STOCK_ID, ref sqls);
                this.ProcessExpMestMedicine(expMest, hisExpMestMedicines);
                expMestMedicines = hisExpMestMedicines;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Cap nhat his_material_bean
        /// </summary>
        /// <param name="hisExpMestMedicines"></param>
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                List<long> expMestMedicineIds = hisExpMestMedicines.Select(o => o.ID).ToList();
                sqls.Add(this.hisMedicineBeanExport.GenSql(expMestMedicineIds, mediStockId));
            }
        }

        private void ProcessExpMestMedicine(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                hisExpMestMedicines.ForEach(o =>
                {
                    o.IS_EXPORT = MOS.UTILITY.Constant.IS_TRUE;
                    o.EXP_LOGINNAME = expMest.LAST_EXP_LOGINNAME;
                    o.EXP_USERNAME = expMest.LAST_EXP_USERNAME;
                    o.EXP_TIME = expMest.LAST_EXP_TIME;
                    o.APPROVAL_LOGINNAME = expMest.LAST_EXP_LOGINNAME;
                    o.APPROVAL_USERNAME = expMest.LAST_EXP_USERNAME;
                    o.APPROVAL_TIME = expMest.LAST_EXP_TIME;
                });

                if (!this.hisExpMestMedicineUpdate.UpdateList(hisExpMestMedicines))
                {
                    throw new Exception("hisExpMestMedicineUpdate. Ket thuc nghiep vu");
                }
            }
        }

        internal void RollbackData()
        {
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
