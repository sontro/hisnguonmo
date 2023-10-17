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

namespace MOS.MANAGER.HisDispense.Handler.UnConfirm
{
    class ExpMestMedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanUnexport hisMedicineBeanUnexport;

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
            this.hisMedicineBeanUnexport = new HisMedicineBeanUnexport(param);
        }

        internal bool Run(HIS_EXP_MEST expMest,List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            bool result = false;
            try
            {
                this.ProcessMedicineBean(expMestMedicines, expMest.MEDI_STOCK_ID);
                this.ProcessExpMestMedicine(expMestMedicines);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessMedicineBean(List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines, long mediStockId)
        {
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                if (!this.hisMedicineBeanUnexport.Run(hisExpMestMedicines, mediStockId))
                {
                    throw new Exception("hisMedicineBeanUnexport. Ket thuc nghiep vu");
                }
            }
        }
        
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
                    o.APPROVAL_LOGINNAME = null;
                    o.APPROVAL_USERNAME = null;
                    o.APPROVAL_TIME = null;
                });

                if (!this.hisExpMestMedicineUpdate.UpdateList(expMestMedicines, befores))
                {
                    throw new Exception("hisExpMestMedicineUpdate. Ket thuc nghiep vu");
                }
            }
        }

        internal void RollbackData()
        {
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisMedicineBeanUnexport.Rollback();
        }
    }
}
