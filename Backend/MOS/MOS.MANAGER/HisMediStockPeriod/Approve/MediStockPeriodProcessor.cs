using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class MediStockPeriodProcessor : BusinessBase
    {
        private HisMediStockPeriodUpdate hisMediStockPeriodUpdate;

        internal MediStockPeriodProcessor(CommonParam param)
            : base(param)
        {
            this.hisMediStockPeriodUpdate = new HisMediStockPeriodUpdate(param);
        }

        internal bool Run(HIS_MEDI_STOCK_PERIOD raw)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_MEDI_STOCK_PERIOD, HIS_MEDI_STOCK_PERIOD>();
                HIS_MEDI_STOCK_PERIOD before = Mapper.Map<HIS_MEDI_STOCK_PERIOD>(raw);
                raw.IS_APPROVE = Constant.IS_TRUE;

                if (!this.hisMediStockPeriodUpdate.Update(raw, before))
                {
                    throw new Exception("hisMediStockPeriodUpdate. Ket thuc nghiep vu");
                }
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


        internal void Rollback()
        {
            this.hisMediStockPeriodUpdate.RollbackData();
        }
    }
}
