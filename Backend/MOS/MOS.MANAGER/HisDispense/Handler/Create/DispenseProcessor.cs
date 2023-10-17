using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Create
{
    class DispenseProcessor : BusinessBase
    {
        private HisDispenseCreate hisDispenseCreate;

        internal DispenseProcessor()
            : base()
        {
            this.hisDispenseCreate = new HisDispenseCreate(param);
        }

        internal DispenseProcessor(CommonParam param)
            : base(param)
        {
            this.hisDispenseCreate = new HisDispenseCreate(param);
        }

        internal bool Run(HisDispenseSDO data, ref HIS_DISPENSE hisDispense)
        {
            bool result = false;
            try
            {
                HIS_DISPENSE dispense = new HIS_DISPENSE();
                dispense.DISPENSE_TIME = data.DispenseTime;
                dispense.IS_CONFIRM = null;
                dispense.MEDI_STOCK_ID = data.MediStockId;
                dispense.DISPENSE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_DISPENSE_TYPE.ID__DISPENSE_MEDICINE;
                if (!this.hisDispenseCreate.Create(dispense))
                {
                    throw new Exception("hisDispenseCreate . Ket thuc nghiep vu");
                }
                hisDispense = dispense;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisDispenseCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
