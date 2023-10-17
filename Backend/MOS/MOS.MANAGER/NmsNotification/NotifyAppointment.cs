using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.NmsNotification
{
    public class NotifyAppointment : BusinessBase
    {
        public NotifyAppointment()
            : base()
        {

        }
        public NotifyAppointment(Inventec.Core.CommonParam param)
            : base(param)
        {

        }

        public bool Run()
        {
            bool result = false;
            try
            {
                var appointmentDate = DateTime.Today.AddDays(TheVietCFG.DATE_BEFORE_NOTIFY_APPOINTMENT ?? 1);
                List<V_HIS_TREATMENT> treatments = GetValidTreatments(appointmentDate);
                if (IsNotNullOrEmpty(treatments))
                {
                    foreach (var item in treatments)
                    {
                        List<string> phoneNumbers = new List<string>();
                        if (!String.IsNullOrWhiteSpace(item.TDL_PATIENT_MOBILE)) phoneNumbers.Add(item.TDL_PATIENT_MOBILE);
                        if (!String.IsNullOrWhiteSpace(item.TDL_PATIENT_PHONE)) phoneNumbers.Add(item.TDL_PATIENT_PHONE);
                        if (!String.IsNullOrWhiteSpace(item.TDL_PATIENT_RELATIVE_MOBILE)) phoneNumbers.Add(item.TDL_PATIENT_RELATIVE_MOBILE);
                        if (!String.IsNullOrWhiteSpace(item.TDL_PATIENT_RELATIVE_PHONE)) phoneNumbers.Add(item.TDL_PATIENT_RELATIVE_PHONE);
                        foreach (var phoneNumber in phoneNumbers)
                        {
                            HIS_BRANCH branch = HisBranchCFG.DATA.Where(o => o.ID == item.BRANCH_ID).FirstOrDefault();
                            V_HIS_SERVICE service = item.APPOINTMENT_EXAM_SERVICE_ID.HasValue ? HisServiceCFG.DATA_VIEW.Where(o => o.ID == item.APPOINTMENT_EXAM_SERVICE_ID).FirstOrDefault() : null;
                            string content = String.Format(MOS.MANAGER.Base.MessageUtil.GetMessage(MOS.LibraryMessage.Message.Enum.AppointmentSchedule_BanCoLichHenTaiKhamVuiLongSapXepThoiGianDeCoMatDungNgay, param.LanguageCode)
                                                                                                    , service != null ? service.SERVICE_NAME : ""
                                                                                                    , Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(appointmentDate)
                                                                                                    , branch != null ? branch.BRANCH_NAME : "");
                            var category = NmsNotificationSend.Category.TAI_KHAM;
                            bool resultNmsNotification = new NmsNotificationSend(new CommonParam()).SendByIdentifierInfo(content, "", phoneNumber, category);
                            if (resultNmsNotification)
                            {
                                Inventec.Common.Logging.LogSystem.Info(String.Format("Thanh cong gui thong bao den SDT:{0}.", phoneNumber));
                                break;
                            }
                        }
                    }
                    result = true;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("__NotifyAppointment.Run() : Khong tim thay ho so dieu tri nao can thong bao lich tai kham!");
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

        private List<V_HIS_TREATMENT> GetValidTreatments(DateTime appointmentDate)
        {
            HisTreatmentViewFilterQuery filterTreatment = new HisTreatmentViewFilterQuery();
            filterTreatment.HAS_MOBILE = true;
            filterTreatment.IS_PAUSE = true;
            filterTreatment.APPOINTMENT_DATE = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(appointmentDate);
            List<V_HIS_TREATMENT> treatments = new HisTreatmentGet().GetView(filterTreatment);
            if (!IsNotNullOrEmpty(treatments)) return treatments;

            filterTreatment = new HisTreatmentViewFilterQuery();
            filterTreatment.PATIENT_IDs = treatments.Select(o => o.PATIENT_ID).ToList();
            filterTreatment.IN_DATE_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Today.AddDays(-1)).Value;
            List<V_HIS_TREATMENT> treatments_ToCheck = new HisTreatmentGet().GetView(filterTreatment);
            List<long> duplicateTreatmentIDs = new List<long>();
            var today = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Today).Value;
            var groups = treatments_ToCheck.GroupBy(o => o.PATIENT_ID).ToList();
            foreach (var group in groups)
            {
                var treatmentsCheck = treatments.Where(o => o.PATIENT_ID == group.Key).ToList();
                var lastInTreatment = treatmentsCheck.OrderByDescending(o => o.IN_DATE).FirstOrDefault();
                if (IsNotNullOrEmpty(group.Where(o => o.IN_DATE > lastInTreatment.IN_DATE).ToList()))
                    duplicateTreatmentIDs.AddRange(treatmentsCheck.Select(o => o.ID));
            }
            return treatments.Where(o => !duplicateTreatmentIDs.Contains(o.ID)).ToList();
        }
    }
}
