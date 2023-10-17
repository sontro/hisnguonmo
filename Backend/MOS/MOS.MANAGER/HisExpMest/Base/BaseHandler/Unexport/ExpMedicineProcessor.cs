using AutoMapper;
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

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class ExpMedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanUnexport hisMedicineBeanUnexport;

        internal ExpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisMedicineBeanUnexport = new HisMedicineBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMedicines)
        {
            bool result = false;
            try
            {
                this.ProcessExpMestMedicine(expMedicines);
                this.ProcessMedicineBean(expMedicines, expMest.MEDI_STOCK_ID);
                
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
        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> expMedicines, long mediStockId)
        {
            if (IsNotNullOrEmpty(expMedicines))
            {
                if (!this.hisMedicineBeanUnexport.Run(expMedicines, mediStockId))
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
        private void ProcessExpMestMedicine(List<HIS_EXP_MEST_MEDICINE> expMedicines)
        {
            if (IsNotNullOrEmpty(expMedicines))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(expMedicines);

                expMedicines.ForEach(o =>
                {
                    o.IS_EXPORT = null;
                    o.EXP_LOGINNAME = null;
                    o.EXP_USERNAME = null;
                    o.EXP_TIME = null;
                });

                if (!this.hisExpMestMedicineUpdate.UpdateList(expMedicines, befores))
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
