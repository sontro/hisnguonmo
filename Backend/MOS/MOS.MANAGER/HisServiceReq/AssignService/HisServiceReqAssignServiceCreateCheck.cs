using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService
{
    class HisServiceReqAssignServiceCreateCheck : BusinessBase
    {
        internal HisServiceReqAssignServiceCreateCheck()
            : base()
        {

        }

        internal HisServiceReqAssignServiceCreateCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool AllowUseBhytByParent(AssignServiceSDO data, ref HIS_SERVICE_REQ parent)
        {
            bool valid = true;
            try
            {
                if (data.ParentServiceReqId.HasValue)
                {
                    parent = new HisServiceReqGet().GetById(data.ParentServiceReqId.Value);
                    if (parent == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("data.ParentServiceReqId ko ton tai");
                        return false;
                    }

                    if (parent.IS_NOT_USE_BHYT == Constant.IS_TRUE && data.ServiceReqDetails != null && data.ServiceReqDetails.Exists(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || o.PrimaryPatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhPhatSinhTuChiDinhDuocCheckKhongHuongBhyt, parent.SERVICE_REQ_CODE);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidEkipInfoForService(AssignServiceSDO data)
        {
            bool valid = true;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.ServiceReqDetails))
                {
                    List<ServiceReqDetailSDO> serviceDetailsHasEkips = data.ServiceReqDetails.Where(o => IsNotNullOrEmpty(o.EkipInfos)).ToList();
                    if (IsNotNullOrEmpty(serviceDetailsHasEkips))
                    {
                        List<EkipSDO> ekipInfos = serviceDetailsHasEkips.SelectMany(o => o.EkipInfos).ToList();
                        if (IsNotNullOrEmpty(ekipInfos) && (ekipInfos.Exists(o => o.ExecuteRoleId <= 0) || ekipInfos.Exists(o => string.IsNullOrWhiteSpace(o.LoginName))))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            LogSystem.Warn("data.ServiceReqDetails.ekipInfos loi du lieu dau vao");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
