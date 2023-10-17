using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class TheVietCFG
    {
        private const string THE_VIET__NOTIFY_EXAM_NUM_ORDER_BEFORE = "MOS.THE_VIET.NOTIFY.EXAM.NUM_ORDER_BEFORE";
        private const string THE_VIET__NOTIFY_SUBCLINICAL_RESULT_CFG = "MOS.THE_VIET.NOTIFY.SUBCLINICAL_RESULT";
        private const string THE_VIET__NOTIFY_APPOINTMENT_DATE_BEFORE_CFG = "MOS.THE_VIET.NOTIFY.APPOINTMENT.APPOINTMENT_DATE_BEFORE";

        private static int? numOrderBeforeNotifyExam;
        public static int? NUM_ORDER_BEFORE_NOTIFY_EXAM
        {
            get
            {
                if (!numOrderBeforeNotifyExam.HasValue)
                {
                    numOrderBeforeNotifyExam = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_EXAM_NUM_ORDER_BEFORE);
                }

                return numOrderBeforeNotifyExam;
            }
        }

        private static bool? SubclinicalResult;
        public static bool SUBCLINICAL_RESULT
        {
            get
            {
                if (!SubclinicalResult.HasValue)
                {
                    SubclinicalResult = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_SUBCLINICAL_RESULT_CFG) == 1;
                }
                return SubclinicalResult.Value;
            }
        }

        private static int? dateBeforeNotifyAppointment;
        public static int? DATE_BEFORE_NOTIFY_APPOINTMENT
        {
            get
            {
                if (!dateBeforeNotifyAppointment.HasValue)
                {
                    dateBeforeNotifyAppointment = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_APPOINTMENT_DATE_BEFORE_CFG);
                }
                return dateBeforeNotifyAppointment;
            }
        }

        public static void Reload()
        {
            numOrderBeforeNotifyExam = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_EXAM_NUM_ORDER_BEFORE);
            SubclinicalResult = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_SUBCLINICAL_RESULT_CFG) == 1;
            dateBeforeNotifyAppointment = ConfigUtil.GetIntConfig(THE_VIET__NOTIFY_APPOINTMENT_DATE_BEFORE_CFG);
        }
    }
}
