using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Process
{
    class HisServiceReqExamProcessCheck : BusinessBase
    {
        internal HisServiceReqExamProcessCheck()
            : base()
        {

        }

        internal HisServiceReqExamProcessCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(HisServiceReqExamUpdateSDO data, ref HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (data == null
                    || (data.TreatmentFinishSDO != null && data.ExamAdditionSDO != null)
                    || (data.TreatmentFinishSDO != null && data.HospitalizeSDO != null)
                    || (data.HospitalizeSDO != null && data.ExamAdditionSDO != null))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Chi duoc phep truyen 1 trong cac du lieu bo sung sau: TreatmentFinishSDO, HospitalizeSDO hoac ExamAdditionSDO" + LogUtil.TraceData("data", data));
                    return false;
                }

                serviceReq = new HisServiceReqGet().GetById(data.Id);

                if (serviceReq == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("id khong hop le" + LogUtil.TraceData("data", data));
                    return false;
                }

                if (data.FinishTime.HasValue && serviceReq.START_TIME.HasValue && data.FinishTime.Value < serviceReq.START_TIME)
                {
                    string finishTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.FinishTime.Value);
                    string startTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(serviceReq.START_TIME.Value);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianBatDauLonHonThoiGianKetThuc, startTime, finishTime);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

    }
}
