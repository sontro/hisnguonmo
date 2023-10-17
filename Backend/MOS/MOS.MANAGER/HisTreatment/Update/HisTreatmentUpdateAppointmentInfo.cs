using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdateAppointmentInfo : BusinessBase
    {
        internal HisTreatmentUpdateAppointmentInfo()
            : base()
        {

        }

        internal HisTreatmentUpdateAppointmentInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Run(TreatmentAppointmentInfoSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                if (valid)
                {
                    if (raw.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN
                        && raw.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN
                        && raw.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Chi cho phep nhap thong tin hen kham voi loai la Hen kham, Ra vien, Xin ra vien");
                        return false;
                    }

                    //neu ko co cac truong bat buoc nhap thi canh bao
                    if ((!raw.APPOINTMENT_TIME.HasValue || raw.APPOINTMENT_TIME.Value == 0) && raw.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        LogSystem.Warn("Ket thuc la hen kham, bat buoc truyen thong tin thoi gian hen kham");
                        return false;
                    }

                    //cap nhat thong tin vao treatment
                    raw.APPOINTMENT_TIME = data.AppointmentTime;
                    raw.APPOINTMENT_EXAM_ROOM_IDS = IsNotNullOrEmpty(data.AppointmentExamRoomIds) ? string.Join(",", data.AppointmentExamRoomIds) : null;
                    raw.APPOINTMENT_PERIOD_ID = data.AppointmentPeriodId;
                    raw.ADVISE = data.Advise;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = raw;
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
