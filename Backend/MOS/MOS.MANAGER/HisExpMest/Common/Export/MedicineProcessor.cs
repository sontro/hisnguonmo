using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisMediStockMety;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.Export
{
    partial class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanExport hisMedicineBeanExport;

        internal MedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal MedicineProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMedicineBeanExport = new HisMedicineBeanExport(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId, string loginName, string userName, long? expTime, ref List<string> sqls)
        {
            try
            {
                this.ProcessMedicineBean(hisExpMestMedicines, mediStockId, ref sqls);

                this.ProcessExpMestMedicine(hisExpMestMedicines, loginName, userName, expTime, ref sqls);
                
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
