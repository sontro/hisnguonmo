using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMestPatientType;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisRoom;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStock
{
    class HisMediStockTruncate : BusinessBase
    {
        internal HisMediStockTruncate()
            : base()
        {

        }

        internal HisMediStockTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDI_STOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockCheck checker = new HisMediStockCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    new HisMestPatientTypeTruncate(param).TruncateByMediStockId(data.ID);
                    new HisMestRoomTruncate(param).TruncateByMediStockId(data.ID);

                    if (DAOWorker.HisMediStockDAO.Truncate(data))
                    {
                        result = new HisRoomTruncate(param).Truncate(data.ROOM_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
