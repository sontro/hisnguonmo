using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServicePaty
{
    class HisServicePatyTruncate : BusinessBase
    {
        internal HisServicePatyTruncate()
            : base()
        {

        }

        internal HisServicePatyTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_PATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisServicePatyDAO.Truncate(data);
                    try
                    {
                        V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.SERVICE_ID);
                        new EventLogGenerator(EventLog.Enum.HisServicePaty_XoaChinhSachGiaDichVu)
                         .ServiceCode(hisService.SERVICE_CODE)
                         .Run();
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
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

        internal bool TruncateList(List<HIS_SERVICE_PATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServicePatyCheck checker = new HisServicePatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServicePatyDAO.TruncateList(listData);
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

        internal bool TruncateByPatientTypeId(long patientTypeId)
        {
            bool result = true;
            try
            {
                List<HIS_SERVICE_PATY> servicePaties = new HisServicePatyGet().GetByPatientTypeId(patientTypeId);
                if (IsNotNullOrEmpty(servicePaties))
                {
                    result = this.TruncateList(servicePaties);
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

        internal bool TruncateByServiceId(long serviceId)
        {
            bool result = true;
            try
            {
                List<HIS_SERVICE_PATY> servicePaties = new HisServicePatyGet().GetByServiceId(serviceId);
                if (IsNotNullOrEmpty(servicePaties))
                {
                    result = this.TruncateList(servicePaties);
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
