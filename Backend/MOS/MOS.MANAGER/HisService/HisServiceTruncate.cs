using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServicePackage;
using MOS.MANAGER.HisServicePaty;
using MOS.MANAGER.HisServiceRati;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisServSegr;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisService
{
    class HisServiceTruncate : BusinessBase
    {
        internal HisServiceTruncate()
            : base()
        {

        }

        internal HisServiceTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long serviceId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE data = null;
                HisServiceCheck checker = new HisServiceCheck(param);
                valid = valid && checker.VerifyId(serviceId, ref data);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.CheckConstraint(serviceId);
                if (valid)
                {
                    new HisServicePatyTruncate().TruncateByServiceId(serviceId);
                    new HisServiceRoomTruncate().TruncateByServiceId(serviceId);
                    new HisServSegrTruncate().TruncateByServiceId(serviceId);
                    new HisServicePackageTruncate().TruncateByServiceId(serviceId);
                    new HisServiceRatiTruncate().TruncateByServiceId(serviceId);

                    result = DAOWorker.HisServiceDAO.Truncate(data);
                    new EventLogGenerator(EventLog.Enum.HisServicePaty_XoaChinhSachGiaDichVu)
                         .ServiceCode(data.SERVICE_CODE)
                         .Run();
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

        internal bool TruncateList(List<HIS_SERVICE> listData)
        {
            bool result = true;
            if (IsNotNullOrEmpty(listData))
            {
                foreach (HIS_SERVICE data in listData)
                {
                    result = result && this.Truncate(data.ID);
                }
            }
            return result;
        }

        internal bool TruncateListId(List<long> listId)
        {
            bool result = true;
            if (IsNotNullOrEmpty(listId))
            {
                foreach (long id in listId)
                {
                    result = result && this.Truncate(id);
                }
            }
            return result;
        }
    }
}
