using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicineBean.Update;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.AggrExam.Unexport
{
    partial class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanUnexport hisMedicineBeanUnexport;

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
            this.hisMedicineBeanUnexport = new HisMedicineBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines)
        {
            try
            {
                this.ProcessExpMestMedicine(hisExpMestMedicines);
                this.ProcessMedicineBean(hisExpMestMedicines, aggrExpMest.MEDI_STOCK_ID);
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
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines) && !this.hisMedicineBeanUnexport.Run(hisExpMestMedicines, mediStockId))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        /// <summary>
        /// Huy thong tin xuat vao exp_mest_medicine
        /// </summary>
        /// <param name="expMestMedicines"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="expTime"></param>
        private void ProcessExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (IsNotNullOrEmpty(expMestMedicines))
            {
                List<long> expMestMedicineIds = expMestMedicines.Select(o => o.ID).ToList();

                string sql = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_EXPORT = NULL, EXP_LOGINNAME = NULL, EXP_USERNAME = NULL, EXP_TIME = NULL WHERE %IN_CLAUSE%", "ID");

                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisMedicineBeanUnexport.Rollback();
        }
    }
}
