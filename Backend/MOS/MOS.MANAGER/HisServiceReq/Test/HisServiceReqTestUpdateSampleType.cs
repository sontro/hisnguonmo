using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    class HisServiceReqTestUpdateSampleType : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        internal HisServiceReqTestUpdateSampleType()
            : base()
        {

        }

        internal HisServiceReqTestUpdateSampleType(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal HIS_SERVICE_REQ Update(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            HIS_SERVICE_REQ result = null;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ serviceReq = null;
                this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref serviceReq);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                    serviceReq.TEST_SAMPLE_TYPE_ID = data.TEST_SAMPLE_TYPE_ID;

                    if (this.hisServiceReqUpdate.Update(serviceReq, before, false))
                    {
                        result = serviceReq;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
