using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqUpdateJsonForm : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateJsonForm()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisServiceReqUpdateJsonForm(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ data)
        {
            bool result = false;

            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(raw);

                    raw.JSON_FORM_ID = data.JSON_FORM_ID;

                    if (this.hisServiceReqUpdate.Update(raw, before, false))
                    {
                        result = true;
                    }   
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            
            return result;
        }
    }
}
