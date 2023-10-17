using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Appointment
{
    class HisVaccinationExamAppointmentCheck : BusinessBase
    {
        internal HisVaccinationExamAppointmentCheck()
            : base()
        {

        }

        internal HisVaccinationExamAppointmentCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidData(HisVaccinationAppointmentSDO data, WorkPlaceSDO workPlace, ref HIS_VACCINATION_EXAM vaccinationExam)
        {
            try
            {
                if (data.AppointmentTime <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.AppointmentTime ko hop le");
                    return false;
                }

                if (data.AppointmentTime <= Inventec.Common.DateTime.Get.Now().Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccinationExam_ThoiGianHenKhamNhoHonHoacBanThoiGianHienTai);
                    return false;
                }

                vaccinationExam = new HisVaccinationExamGet().GetById(data.VaccinationExamId);

                if (vaccinationExam == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.VaccinationExamId ko hop le");
                    return false;
                }
                if (vaccinationExam.EXECUTE_ROOM_ID != data.RequestRoomId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccinationExam_KhongLamViecTaiPhongKham);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Details) && data.Details.Select(o => o.VaccineTypeId).Distinct().ToList().Count != data.Details.Count)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccinationExam_TonTaiHaiMuiTiemGiongNhau);
                    return false;
                }
                if (IsNotNullOrEmpty(data.Details) && data.Details.Exists(t => t.VaccineTurn <= 0))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisVaccinationExam_ChuaNhapThongTinMuiThu);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }
    }
}
