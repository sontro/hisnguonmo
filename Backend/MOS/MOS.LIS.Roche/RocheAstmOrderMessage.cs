using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public enum OrderType
    {
        ADD,
        DELETE
    }

    public enum PatientFormatOption
    {
        HNI_TIM = 1, //1: Vien Tim HN
        TBH_TTYT = 2,//2: Trung tam y te thai binh
        TNH_CVC = 3 //3: Cao Van Chi
    }

    public class RocheAstmOrderMessage : RocheAstmBaseMessage
    {
        private const string PATIENT_FORMAT__TBH_TTYT = "{0}|1||{1}||{2} {3}||{4}|{5}|{6}^{7},{8}||{9}|";
        private const string PATIENT_FORMAT__TNH_CVC = "{0}|1||{1}||{2} {3}||{4}|{5}|{6},{7},{8}||{9}|";
        private const string PATIENT_FORMAT__HNI_TIM = "{0}|1||{1}||{2} {3}||{4}|{5}||||";
        private const string ORDER_FORMAT = "{0}|1|{1}||{2}|R|{3}|||||{4}|||||{5}||{6}|||||||{7}|{8}";
        private const string BODY_FORMAT = "{0}\r{1}";
        private const string START_TEST_INDEX_CODES = "^^^";
        private const string TEST_INDEX_CODES_DELIMITER = "\\^^^";

        private RocheAstmPatientData patientData;
        private RocheAstmOrderData orderData;
        private OrderType orderType;
        private int? patientFormatOption;

        public RocheAstmOrderMessage(int? option, string sendingAppCode, string receivingAppCode, RocheAstmPatientData patientData, RocheAstmOrderData orderData, OrderType orderType)
            : base(sendingAppCode, receivingAppCode)
        {
            this.patientData = patientData;
            this.orderData = orderData;
            this.orderType = orderType;
            this.patientFormatOption = option;
        }

        protected override string BodyMessage()
        {
            string patient = "";
            if (this.patientFormatOption == null || this.patientFormatOption == (int)PatientFormatOption.HNI_TIM)
            {
                patient = string.Format(PATIENT_FORMAT__HNI_TIM,
                RocheAstmConstants.IDENTIFIER__PATIENT_RECORD,
                "HIS" + patientData.PatientId,
                Inventec.Common.String.Convert.UnSignVNese(patientData.LastName),
                Inventec.Common.String.Convert.UnSignVNese(patientData.FirstName),
                this.DateOfBirthToString(patientData.DateOfBirth),
                GenderUtil.ToString(patientData.Gender)
                );
            }
            else if (this.patientFormatOption.HasValue && this.patientFormatOption.Value == (int)PatientFormatOption.TNH_CVC)
            {
                patient = string.Format(PATIENT_FORMAT__TNH_CVC,
                RocheAstmConstants.IDENTIFIER__PATIENT_RECORD,
                "HIS" + patientData.PatientId,
                patientData.LastName,
                patientData.FirstName,
                this.DateOfBirthToString(patientData.DateOfBirth),
                GenderUtil.ToString(patientData.Gender),
                    patientData.Commune,
                    patientData.District,
                    patientData.Province,
                    patientData.HeinCardNumber
                );
            }
            else
            {
                patient = string.Format(PATIENT_FORMAT__TBH_TTYT,
                RocheAstmConstants.IDENTIFIER__PATIENT_RECORD,
                "HIS" + patientData.PatientId,
                patientData.LastName,
                patientData.FirstName,
                this.DateOfBirthToString(patientData.DateOfBirth),
                GenderUtil.ToString(patientData.Gender),
                    patientData.Commune,
                    patientData.District,
                    patientData.Province,
                    patientData.HeinCardNumber
                );
            }


            string doctorName = "";
            if (this.patientFormatOption == (int)PatientFormatOption.TBH_TTYT || this.patientFormatOption == (int)PatientFormatOption.TNH_CVC)
            {
                doctorName = orderData.DoctorName;
            }

            string orderType = this.orderType == OrderType.ADD ? RocheAstmConstants.ACTION_CODE__ADD : RocheAstmConstants.ACTION_CODE__DELETE;
            string reportType = this.orderType == OrderType.DELETE ? RocheAstmConstants.REPORT_TYPE__DELETE_SAMPLE : "";
            string order = string.Format(ORDER_FORMAT,
                RocheAstmConstants.IDENTIFIER__ORDER_RECORD,
                orderData.OrderCode,
                this.FormatTestIndexCodes(),
                this.OrderDateToString(orderData.OrderDate),
                orderType,
                doctorName,
                orderData.DepartmentCode,
                reportType,
                RocheAstmConstants.IDENTIFIER__ORDER_RECORD);
            return string.Format(BODY_FORMAT, patient, order);
        }

        protected override void FromBodyMessage(string[] bodyMessage)
        {
            throw new NotImplementedException();
        }

        private string FormatTestIndexCodes()
        {
            if (this.orderData != null && this.orderData.TestIndexCodes != null && this.orderData.TestIndexCodes.Count > 0)
            {
                string result = START_TEST_INDEX_CODES;
                foreach (string testIndexCode in this.orderData.TestIndexCodes)
                {
                    result = string.Format("{0}{1}{2}", result, testIndexCode, TEST_INDEX_CODES_DELIMITER);
                }
                return result.Substring(0, result.Length - TEST_INDEX_CODES_DELIMITER.Length);
            }
            return null;
        }

        private string DateOfBirthToString(DateTime date)
        {
            if (date != null)
            {
                return date.ToString("yyyyMMdd");
            }
            return "";
        }

        private string OrderDateToString(DateTime date)
        {
            if (date != null)
            {
                return date.ToString("yyyyMMddHHmmss");
            }
            return "";
        }
    }
}
