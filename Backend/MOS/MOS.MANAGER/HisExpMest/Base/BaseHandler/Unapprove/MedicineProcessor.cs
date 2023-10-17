using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unapprove
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanUnlockByExpMest beanUnlock;
        private HisExpMestMetyReqDecreaseDdAmount decreaseDdAmount;


        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beanUnlock = new HisMedicineBeanUnlockByExpMest(param);
            this.decreaseDdAmount = new HisExpMestMetyReqDecreaseDdAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<string> sqls)
        {
            try
            {
                this.ProcessMetyReq(expMest, expMedicines);
                this.ProcessMedicineBean(expMedicines, expMest.MEDI_STOCK_ID, ref sqls);
                this.ProcessExpMestMedicine(expMest, expMedicines, ref sqls);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessMetyReq(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines)
        {
            if (IsNotNullOrEmpty(expMedicines))
            {
                Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();
                var Groups = expMedicines.GroupBy(g => g.EXP_MEST_METY_REQ_ID ?? 0).ToList();
                foreach (var group in Groups)
                {
                    dicDecrease[group.Key] = group.Sum(s => s.AMOUNT);
                }

                if (!this.decreaseDdAmount.Run(dicDecrease))
                {
                    throw new Exception("decreaseDdAmount. Ket thuc nghiep vu");
                }
            }
        }


        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMedicines"></param>
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> expMedicines, long mediStockId, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMedicines))
            {
                string sqlUpdate = this.beanUnlock.GenSql(expMedicines.Select(s => s.ID).ToList());
                sqls.Add(sqlUpdate);
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMedicine(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMedicines))
            {
                string sqlDelete = String.Format("UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE EXP_MEST_ID = {0}", expMest.ID);
                sqls.Add(sqlDelete);
            }
        }

        internal void Rollback()
        {
            this.decreaseDdAmount.Rollback();
        }
    }
}
