using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LIS.RocheV2;
using MOS.LIS.RocheV3;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche
{
    class PatientMaker
    {
        public static RocheAstmPatientData MakePatientData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> inserts)
        {
            string address = serviceReq.TDL_PATIENT_ADDRESS;
            string administrativeDivision = null;

            if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADMINISTRATIVE_DIVISION)
            {
                PatientMaker.SplitAddressElement(serviceReq.TDL_PATIENT_ADDRESS, ref address, ref administrativeDivision);
            }

            RocheAstmPatientData patient = new RocheAstmPatientData();
            if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_ADDRESS)
            {
                patient.Address = new RocheAstmAddressData(address, administrativeDivision);
            }

            if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION == PatientNameFormatOption.NORMAL)
            {
                patient.LastName = serviceReq.TDL_PATIENT_LAST_NAME;
                patient.FirstName = serviceReq.TDL_PATIENT_FIRST_NAME;
            }
            else if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION == PatientNameFormatOption.MERGE)
            {
                patient.LastName = serviceReq.TDL_PATIENT_NAME;
                patient.FirstName = "";
            }
            else if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_NAME_OPTION == PatientNameFormatOption.UNSIGNED_AS_FIRST_NAME)
            {
                patient.LastName = serviceReq.TDL_PATIENT_NAME;
                patient.FirstName = Inventec.Common.String.Convert.UnSignVNese(serviceReq.TDL_PATIENT_NAME);
            }

            patient.PatientId = serviceReq.TDL_PATIENT_CODE;
            if (LisRocheCFG.MESSAGE_FORMAT_PATIENT_INFO_IS_HAVING_BHYT_NUMBER)
            {
                patient.HeinCardNumber = serviceReq.TDL_HEIN_CARD_NUMBER;
            }
            patient.DateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.TDL_PATIENT_DOB).Value;
            patient.Gender = serviceReq.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ?
                RocheAstmGender.FEMALE : RocheAstmGender.MALE;
            return patient;
        }

        public static RocheHl7PatientData MakeHl7PatientData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> inserts)
        {
            RocheHl7PatientData patient = new RocheHl7PatientData();
            patient.Address = serviceReq.TDL_PATIENT_ADDRESS;
            patient.PatientName = serviceReq.TDL_PATIENT_NAME;
            patient.PatientId = serviceReq.TDL_PATIENT_CODE;
            patient.DateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.TDL_PATIENT_DOB).Value;
            patient.Gender = serviceReq.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE ?
                RocheHl7Gender.FEMALE : RocheHl7Gender.MALE;
            patient.IsOutPatient = serviceReq.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM || serviceReq.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU;
            patient.PhoneNumber = !string.IsNullOrWhiteSpace(serviceReq.TDL_PATIENT_PHONE) ? serviceReq.TDL_PATIENT_PHONE : serviceReq.TDL_PATIENT_MOBILE;
            patient.TreatmentCode = serviceReq.TDL_TREATMENT_CODE;
            return patient;
        }

        /// <summary>
        /// Phan tich dia chi de ra cac thong tin dia chi va khu vuc hanh chinh (tinh/huyen/xa)
        /// Du lieu nay ko can dung tuyet doi --> thuat toan duoi day la chap nhan duoc
        /// </summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="address">dia chi</param>
        /// <param name="administrativeDivision">khu vuc hanh chinh</param>
        private static void SplitAddressElement(string input, ref string address, ref string administrativeDivision)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    if (input.Contains(','))
                    {
                        int index = input.IndexOf(',');
                        address = CommonUtil.NVL(input.Substring(0, index));
                        if (index < input.Length)
                        {
                            administrativeDivision = CommonUtil.NVL(input.Substring(index + 1));
                        }
                    }
                    else
                    {
                        address = input;
                        administrativeDivision = "";
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
