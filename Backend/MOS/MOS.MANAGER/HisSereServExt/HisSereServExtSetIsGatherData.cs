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
    partial class HisSereServExtSetIsGatherData : BusinessBase
    {
        internal HisSereServExtSetIsGatherData()
            : base()
        {
        }

        internal HisSereServExtSetIsGatherData(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HisSereServExtIsGatherDataSDO data, ref HIS_SERE_SERV_EXT resultData)
        {
            bool result = false;

            try
            {
                bool valid = true;
                HIS_SERE_SERV sereServ = null;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                valid = valid && sereServChecker.VerifyId(data.SereServId, ref sereServ);

                if (valid)
                {
                    HIS_SERE_SERV_EXT ssExt = new HisSereServExtGet().GetBySereServId(data.SereServId);
                    if (ssExt == null)
                    {
                        ssExt = new HIS_SERE_SERV_EXT();
                        ssExt.IS_GATHER_DATA = data.IsGatherData ? (short?)Constant.IS_TRUE : null;
                        HisSereServExtUtil.SetTdl(ssExt, sereServ);
                        result = DAOWorker.HisSereServExtDAO.Create(ssExt);
                    }
                    else
                    {
                        ssExt.IS_GATHER_DATA = data.IsGatherData ? (short?)Constant.IS_TRUE : null;
                        result = DAOWorker.HisSereServExtDAO.Update(ssExt);
                    }
                    resultData = result ? ssExt : null;
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
