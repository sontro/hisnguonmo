using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisEmployee;

namespace MOS.MANAGER.HisServiceReq.UpdateIsNotRequireFee
{
    partial class HisServiceReqUpdateIsNotRequireFee : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateIsNotRequireFee()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisServiceReqUpdateIsNotRequireFee(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(List<long> serviceReqIds, ref List<HIS_SERVICE_REQ> resultData)
        {
            bool result = false;

            try
            {
                bool valid = true;
                
                List<HIS_SERVICE_REQ> serviceReqs = null;
                HisServiceReqUpdateIsNotRequireFeeCheck checker = new HisServiceReqUpdateIsNotRequireFeeCheck(param);

                valid = valid && IsNotNullOrEmpty(serviceReqIds);
                valid = valid && checker.IsValidData(serviceReqIds, ref serviceReqs);

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);
                    serviceReqs.ForEach(o => o.IS_NOT_REQUIRE_FEE = Constant.IS_TRUE);

                    if (this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                    {
                        List<string> serviceReqCodes = serviceReqs.Select(o => o.SERVICE_REQ_CODE).ToList();
                        string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                        new EventLogGenerator(EventLog.Enum.HisServiceReq_CapNhatThuSau, serviceReqCodeStr)
                            .TreatmentCode(serviceReqs[0].TDL_TREATMENT_CODE)
                            .Run();
                        result = true;
                        resultData = serviceReqs;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisServiceReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
