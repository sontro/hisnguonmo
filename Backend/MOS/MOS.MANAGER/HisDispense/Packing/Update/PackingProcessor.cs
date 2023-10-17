using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Update
{
    class PackingProcessor : BusinessBase
    {
        private HisDispenseUpdate hisDispenseUpdate;

        internal PackingProcessor()
            : base()
        {
            this.hisDispenseUpdate = new HisDispenseUpdate(param);
        }

        internal PackingProcessor(CommonParam param)
            : base(param)
        {
            this.hisDispenseUpdate = new HisDispenseUpdate(param);
        }

        internal bool Run(HisPackingUpdateSDO data, HIS_DISPENSE dispense)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_DISPENSE, HIS_DISPENSE>();
                HIS_DISPENSE before = Mapper.Map<HIS_DISPENSE>(dispense);

                dispense.DISPENSE_TIME = data.DispenseTime;
                dispense.IS_CONFIRM = null;

                if (ValueChecker.IsPrimitiveDiff<HIS_DISPENSE>(before, dispense))
                {
                    if (!this.hisDispenseUpdate.Update(dispense, before))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                return true;
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
                this.hisDispenseUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
