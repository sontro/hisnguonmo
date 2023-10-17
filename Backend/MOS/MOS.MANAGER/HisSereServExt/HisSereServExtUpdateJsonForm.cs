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

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtUpdateJsonForm : BusinessBase
    {
        internal HisSereServExtUpdateJsonForm()
            : base()
        {
        }

        internal HisSereServExtUpdateJsonForm(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HIS_SERE_SERV_EXT data)
        {
            bool result = false;

            try
            {
                bool valid = true;
                HIS_SERE_SERV_EXT raw = null;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                    HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(raw);

                    raw.JSON_FORM_ID = data.JSON_FORM_ID;

                    if (DAOWorker.HisSereServExtDAO.Update(raw))
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
