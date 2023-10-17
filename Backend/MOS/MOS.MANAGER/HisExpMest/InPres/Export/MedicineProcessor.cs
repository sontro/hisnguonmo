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

namespace MOS.MANAGER.HisExpMest.InPres.Export
{
    class MedicineProcessor : BusinessBase
    {
        internal MedicineProcessor()
            : base()
        {
        }

        internal MedicineProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }


        internal bool Run(HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            try
            {
                this.ProcessExpMestMedicine(expMest.ID, ref hisExpMestMedicines, loginName, userName, expTime, ref sqls);
                this.ProcessMedicineBean(hisExpMestMedicines,expMest.MEDI_STOCK_ID, ref sqls);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Cap nhat his_medicine_bean
        /// </summary>
        /// <param name="hisExpMestMedicines"></param>
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                List<long> expMestMedicineIds = hisExpMestMedicines.Select(o => o.ID).ToList();
                sqls.Add(new HisMedicineBeanExport(param).GenSql(expMestMedicineIds, mediStockId));
            }
        }

        /// <summary>
        /// Bo sung thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMedicine(long expMestId, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            //Lay cac du lieu chua duoc xuat va da duoc duyet
            HisExpMestMedicineFilterQuery filter = new HisExpMestMedicineFilterQuery();
            filter.EXP_MEST_ID = expMestId;
            filter.IS_EXPORT = false;
            filter.IS_APPROVED = true;

            expMestMedicines = new HisExpMestMedicineGet().Get(filter);

            if (IsNotNullOrEmpty(expMestMedicines))
            {
                List<long> expMestMedicineIds = expMestMedicines.Select(o => o.ID).ToList();

                string sql = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_EXPORT = {0}, EXP_LOGINNAME = '{1}', EXP_USERNAME = '{2}', EXP_TIME = {3} WHERE %IN_CLAUSE%", "ID");

                sqls.Add(string.Format(sql, MOS.UTILITY.Constant.IS_TRUE, loginName, userName, expTime));
            }
        }
    }
}
