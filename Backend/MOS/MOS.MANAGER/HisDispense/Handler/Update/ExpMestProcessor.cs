using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Update
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisDispenseUpdateSDO data, HIS_DISPENSE dispense, HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCT;
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                expMest.DISPENSE_ID = dispense.ID;
                expMest.TDL_DISPENSE_CODE = dispense.DISPENSE_CODE;

                if (ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST>(before, expMest))
                {
                    if (!this.hisExpMestUpdate.Update(expMest, before))
                    {
                        throw new Exception("hisExpMestUpdate. Rollback du lieu. Ket thuc nghiep vu");
                    }
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

        internal void RollbackData()
        {
            try
            {
                this.hisExpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
