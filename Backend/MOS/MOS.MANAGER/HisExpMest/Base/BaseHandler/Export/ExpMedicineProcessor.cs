using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineBean.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class ExpMedicineProcessor : BusinessBase
    {
        private HisMedicineBeanExport hisMedicineBeanExport;

        internal ExpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMedicineBeanExport = new HisMedicineBeanExport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, string loginname, string username, long expTime, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    this.ProcessMedicineBean(expMestMedicines, expMest.MEDI_STOCK_ID, ref sqls);
                    this.ProcessExpMestMedicine(expMestMedicines, loginname, username, expTime, ref sqls);
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

        /// <summary>
        /// Cap nhat his_medicine_bean
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

        private void ProcessExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(hisExpMestMedicines.Select(s => s.ID).ToList(), String.Format("UPDATE HIS_EXP_MEST_MEDICINE SET IS_EXPORT = 1, EXP_LOGINNAME = '{0}', EXP_USERNAME = '{1}', EXP_TIME = {2} WHERE %IN_CLAUSE%", loginName ?? "", userName ?? "", expTime), "ID");

                sqls.Add(sql);
            }
        }
    }
}
