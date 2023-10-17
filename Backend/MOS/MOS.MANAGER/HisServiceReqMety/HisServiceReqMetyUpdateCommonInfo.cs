using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReqMety
{
    class HisServiceReqMetyUpdateCommonInfo : BusinessBase
    {

        private HisServiceReqMetyUpdate hisServiceReqMetyUpdate;

        internal HisServiceReqMetyUpdateCommonInfo()
            : base()
        {
            this.hisServiceReqMetyUpdate = new HisServiceReqMetyUpdate(param);
        }

        internal HisServiceReqMetyUpdateCommonInfo(CommonParam param)
            : base(param)
        {
            this.hisServiceReqMetyUpdate = new HisServiceReqMetyUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ_METY data, ref HIS_SERVICE_REQ_METY resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ_METY raw=null;
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ_METY, HIS_SERVICE_REQ_METY>();
                    HIS_SERVICE_REQ_METY before = Mapper.Map<HIS_SERVICE_REQ_METY>(raw);
                    raw.TUTORIAL = data.TUTORIAL;
					raw.SPEED = data.SPEED;
                    raw.USE_TIME_TO = data.USE_TIME_TO;
                    if (!this.hisServiceReqMetyUpdate.Update(raw,before))
                    {
                        throw new Exception("hisServiceReqMetyUpdate. Ket thuc nghiep vu");
                    }

                    result = true;
                    resultData = raw;

                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(raw.SERVICE_REQ_ID);
                    new EventLogGenerator(EventLog.Enum.HisServiceReqMety_SuaThongTinChung, raw.MEDICINE_TYPE_NAME, before.TUTORIAL, raw.TUTORIAL, before.SPEED, raw.SPEED).TreatmentCode(serviceReq.TDL_TREATMENT_CODE).ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
