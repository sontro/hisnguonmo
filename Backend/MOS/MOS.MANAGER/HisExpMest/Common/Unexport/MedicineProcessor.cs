using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.MANAGER.HisMediStockMety;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    partial class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
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
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisMedicineBeanUnexport = new HisMedicineBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines)
        {
            try
            {
                this.ProcessExpMestMedicine(hisExpMestMedicines);
                this.ProcessMedicineBean(hisExpMestMedicines, expMest.MEDI_STOCK_ID);
                
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
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                if (!this.hisMedicineBeanUnexport.Run(hisExpMestMedicines, mediStockId))
                {
                    throw new Exception("Rollback du lieu");
                }
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
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(expMestMedicines);

                expMestMedicines.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                });

                if (!this.hisExpMestMedicineUpdate.UpdateList(expMestMedicines, befores))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisMedicineBeanUnexport.Rollback();
        }
    }
}
