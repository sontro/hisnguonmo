using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MOS.KskSignData
{
    public class KskDataProcess
    {
        public KskSyncSDO MakeData(V_HIS_KSK_DRIVER raw, X509Certificate2 certificate)
        {
            KskSyncSDO ado = new KskSyncSDO();
            ado.BACSYKETLUAN = raw.CONCLUDER_USERNAME;
            ado.BENHVIEN = raw.BRANCH_NAME;
            ado.DIACHITHUONGTRU = SubString(raw.TDL_PATIENT_ADDRESS, 255);
            ado.GIOITINHVAL = (raw.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE) ? "1" : "0";
            ado.HANGBANGLAI = raw.LICENSE_CLASS;
            ado.HOTEN = raw.TDL_PATIENT_NAME;
            ado.IDBENHVIEN = raw.TDL_MEDI_ORG_CODE;
            ado.KETLUAN = raw.CONCLUSION;
            ado.LYDO = SubString(raw.REASON_BAD_HEATHLY, 255);

            ado.MATINH_THUONGTRU = long.Parse(raw.PROVINCE_CODE).ToString();
            ado.MAHUYEN_THUONGTRU = long.Parse(raw.DISTRICT_CODE).ToString();
            ado.MAXA_THUONGTRU = long.Parse(raw.COMMUNE_CODE).ToString();
            ado.NGAYKETLUAN = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.CONCLUSION_TIME);
            ado.NGAYSINH = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.TDL_PATIENT_DOB);
            if (!String.IsNullOrWhiteSpace(raw.CCCD_NUMBER) && !String.IsNullOrWhiteSpace(raw.CCCD_PLACE) && raw.CCCD_DATE.HasValue)
            {
                ado.SOCMND_PASSPORT = raw.CCCD_NUMBER;
                ado.NOICAP = SubString(raw.CCCD_PLACE, 30);
                ado.NGAYTHANGNAMCAPCMND = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.CCCD_DATE.Value);
            }
            else if (!String.IsNullOrWhiteSpace(raw.CMND_NUMBER) && !String.IsNullOrWhiteSpace(raw.CMND_PLACE) && raw.CMND_DATE.HasValue)
            {
                ado.SOCMND_PASSPORT = raw.CMND_NUMBER;
                ado.NOICAP = SubString(raw.CMND_PLACE, 30);
                ado.NGAYTHANGNAMCAPCMND = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.CMND_DATE.Value);
            }
            else if (!String.IsNullOrWhiteSpace(raw.PASSPORT_NUMBER) && !String.IsNullOrWhiteSpace(raw.PASSPORT_PLACE) && raw.PASSPORT_DATE.HasValue)
            {
                ado.SOCMND_PASSPORT = raw.PASSPORT_NUMBER;
                ado.NOICAP = SubString(raw.PASSPORT_PLACE, 30);
                ado.NGAYTHANGNAMCAPCMND = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.PASSPORT_DATE.Value);
            }
            ado.SO = raw.KSK_DRIVER_CODE;
            if (raw.APPOINTMENT_TIME.HasValue)
            {
                ado.NGAYKHAMLAI = Inventec.Common.DateTime.Convert.TimeNumberToDateString(raw.APPOINTMENT_TIME.Value);
            }

            if (raw.SYNC_RESULT_TYPE == IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__SYNC_SUCCESSFUL || raw.SYNC_RESULT_TYPE == IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__EDIT_INFO)
            {
                ado.STATE = "EDIT";
            }
            else
            {
                ado.STATE = "ADD";
            }
            ado.TINHTRANGBENH = SubString(raw.SICK_CONDITION, 255);
            ado.NONGDOCON = raw.CONCENTRATION.HasValue ? raw.CONCENTRATION.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) : "";
            if (raw.CONCENTRATION.HasValue)
            {
                if (raw.CONCENTRATION_TYPE == 1)
                {
                    ado.DVINONGDOCON = "1";
                }
                else
                {
                    ado.DVINONGDOCON = "0";
                }
            }
            if (raw.DRUG_TYPE.HasValue)
            {
                ado.MATUY = (raw.DRUG_TYPE.Value == 2) ? "1" : "0";
            }

            KskSignDataProcess.ProcessSignData(ado, certificate);

            return ado;
        }

        public static string SubString(string input, int maxLength)
        {
            string rs = input;
            if (!String.IsNullOrWhiteSpace(input) && Encoding.UTF8.GetByteCount(input) > maxLength)
            {
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (Encoding.UTF8.GetByteCount(input.Substring(0, i + 1)) <= maxLength)
                    {
                        rs = String.Format("{0}", input.Substring(0, i + 1));
                        break;
                    }
                }
            }
            return rs;
        }
    }
}
