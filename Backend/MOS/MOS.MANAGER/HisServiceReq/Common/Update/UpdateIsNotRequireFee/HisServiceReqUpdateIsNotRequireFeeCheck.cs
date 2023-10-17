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
using MOS.MANAGER.HisEmployee;

namespace MOS.MANAGER.HisServiceReq.UpdateIsNotRequireFee
{
    partial class HisServiceReqUpdateIsNotRequireFeeCheck : BusinessBase
    {
        internal HisServiceReqUpdateIsNotRequireFeeCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateIsNotRequireFeeCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidData(List<long> serviceReqIds, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = true;

            try
            {
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    serviceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);
                    List<long> treatmentIds = serviceReqs != null ? serviceReqs.Select(o => o.TREATMENT_ID).Distinct().ToList() : null;

                    if (treatmentIds.Count > 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Ton tai y lenh thuoc cac ho so dieu tri khac nhau");
                        return false;
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
