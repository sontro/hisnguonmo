using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.LibraryHein.Bhyt.HeinJoin5Year;
using MOS.LibraryHein.Bhyt.HeinLevel;
using MOS.LibraryHein.Bhyt.HeinLiveArea;
//using MOS.LibraryHein.Bhyt.HeinMediOrg;
using MOS.LibraryHein.Bhyt.HeinObject;
using MOS.LibraryHein.Bhyt.HeinPaid6Month;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.LibraryHein.Bhyt.HeinTreatmentType;
using MOS.LibraryHein.Bhyt.HeinUpToStandard;
using MOS.LibraryHein.Common;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Bhyt
{
    public class BhytPatientTypeData : HIS_PATIENT_TYPE_ALTER
    {
        public static bool IsChild(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int month = GetMonthsBetween(dateOfBirth, today);
            if (month < 12 * BhytConstant.CHILD_AGE)
            {
                return true;
            }
            if (month == 12 * BhytConstant.CHILD_AGE && dateOfBirth.AddMonths(month) >= today)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Kiem tra xem co thuoc truong hop tu sinh the BHYT khong. Neu co thi
        /// thuc hien sinh thong tin the va cap nhat lai thong tin "patient_type"
        /// Theo quy dinh cua BHYT: Tre em duoi do tuoi tre em va co giay chung
        /// sinh thi se duoc cap the BHYT co thoi han den het do tuoi tre em hoac
        /// hon do tuoi tre em nhung chua den tuoi di hoc (cau hinh la ngay 30/9)
        /// </summary>
        /// <param name="heinCardNumbers"></param>
        /// <param name="districtCode"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool UpdateInCaseOfHavingBirthCertificate(List<string> heinCardNumbers, DateTime dateOfBirth, string districtCode, string districtName, string provinceCode, string provinceName, string communeName, string address, string levelCode)
        {
            bool result = false;
            try
            {
                //Neu benh nhan duoi so tuoi tre em va co giay chung sinh
                if (IsChild(dateOfBirth) && HeinHasBirthCertificateCode.TRUE.Equals(this.HAS_BIRTH_CERTIFICATE))
                {
                    if (!HeinLevelStore.IsValidCode(levelCode))
                    {
                        throw new Exception("levelCode khong hop le. levelCode:" + levelCode);
                    }
                    string generateCardNumber = BhytPatientTypeData.GenerateChildHeinNumber(heinCardNumbers, provinceCode);
                    if (string.IsNullOrWhiteSpace(generateCardNumber))
                    {
                        throw new Exception("Loi khi sinh the BHYT");
                    }

                    DateTime configMaxExpiryTime = new DateTime(dateOfBirth.Year, BhytConstant.CHILD_MONTH_EXPIRY, BhytConstant.CHILD_DAY_EXPIRY, 0, 0, 0).AddYears(BhytConstant.CHILD_AGE);

                    //neu > do tuoi tre em nhung chua den ngay thang quy dinh thi duoc thiet lap den ngay thang quy dinh
                    DateTime expiryTime = dateOfBirth.AddYears(BhytConstant.CHILD_AGE);
                    expiryTime = expiryTime < configMaxExpiryTime ? configMaxExpiryTime : expiryTime;

                    this.HEIN_CARD_FROM_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateOfBirth).Value;
                    this.HEIN_CARD_TO_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(expiryTime).Value;
                    this.HEIN_CARD_NUMBER = generateCardNumber;

                    this.HEIN_MEDI_ORG_CODE = String.Format(BhytConstant.CHILD_HEIN_ORG_CODE_FORMAT, provinceCode);
                    this.HEIN_MEDI_ORG_NAME = String.Format(BhytConstant.CHILD_HEIN_ORG_NAME_FORMAT, provinceName);
                    this.LEVEL_CODE = levelCode;
                    this.RIGHT_ROUTE_CODE = HeinRightRouteCode.TRUE;
                    this.JOIN_5_YEAR = HeinJoin5YearCode.FALSE;
                    this.PAID_6_MONTH = HeinPaid6MonthCode.FALSE;

                    this.ADDRESS = string.Format("{0} - {1}", districtName, provinceName);
                    if (!string.IsNullOrWhiteSpace(communeName))
                    {
                        this.ADDRESS = string.Format("{0} - {1}", communeName, this.ADDRESS);
                    }
                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        this.ADDRESS = string.Format("{0} - {1}", address, this.ADDRESS);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Tu dong sinh the BHYT doi voi quan nhan co giay gioi thieu
        /// Thoi han the la tu 01/01 nam hien tai den 31/12 nam  hien tai
        /// </summary>
        /// <param name="heinCardNumbers"></param>
        /// <param name="districtCode"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool UpdateInCaseOfIsTempQnTrue(List<string> heinCardNumbers, string levelCode)
        {
            bool result = false;
            try
            {
                //Neu benh nhan duoi so tuoi tre em va co giay chung sinh
                if (this.IS_TEMP_QN.HasValue && this.IS_TEMP_QN.Value == 1)
                {
                    if (!HeinLevelStore.IsValidCode(levelCode))
                    {
                        throw new Exception("levelCode khong hop le. levelCode:" + levelCode);
                    }
                    string generateCardNumber = BhytPatientTypeData.GenerateQnHeinNumber(heinCardNumbers);
                    if (string.IsNullOrWhiteSpace(generateCardNumber))
                    {
                        throw new Exception("Loi khi sinh the BHYT");
                    }
                    DateTime fromDt = new DateTime(DateTime.Now.Year, 1, 1);
                    DateTime toDt = new DateTime(DateTime.Now.Year, 12, 31);
                    this.HEIN_CARD_FROM_TIME = Convert.ToInt64(fromDt.ToString("yyyyMMdd") + "000000");
                    this.HEIN_CARD_TO_TIME = Convert.ToInt64(toDt.ToString("yyyyMMdd") + "000000");
                    this.HEIN_CARD_NUMBER = generateCardNumber;

                    this.LEVEL_CODE = levelCode;
                    this.RIGHT_ROUTE_CODE = HeinRightRouteCode.TRUE;
                    this.JOIN_5_YEAR = HeinJoin5YearCode.FALSE;
                    this.PAID_6_MONTH = HeinPaid6MonthCode.FALSE;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool ValidateData(string currentHeinMediOrgCode, List<string> acceptedHeinMediOrgCodes)
        {
            bool valid = true;

            //Neu ma noi KCB ban dau co trong danh sach chap nhan thi se la dung tuyen
            if (this.LEVEL_CODE == HeinLevelCode.DISTRICT || this.LEVEL_CODE == HeinLevelCode.COMMUNE)
            {
                //bo validate nay vi co the BN vao o chi nhanh tuyen huyen nhung chuyen sang chi nhanh tuyen tinh
                /*
                if (!HeinRightRouteCode.TRUE.Equals(this.RIGHT_ROUTE_CODE))
                {
                    LogSystem.Error("Neu benh vien la tuyen xa hoac tuyen huyen thi phai luon la 'Dung Tuyen'");
                    valid = false;
                }*/
            }
            else if (currentHeinMediOrgCode.Equals(this.HEIN_MEDI_ORG_CODE) || (acceptedHeinMediOrgCodes != null && acceptedHeinMediOrgCodes.Contains(this.HEIN_MEDI_ORG_CODE)) || this.HAS_BIRTH_CERTIFICATE == HeinHasBirthCertificateCode.TRUE)
            {
                //tam thoi bo validate nay vi se co truong hop luc tao moi thi ko dung tuyen, nhung khi update thi la DT,
                //tuy vay, nguoi dung van can luu lai thong tin nhu cu
                /*if (!HeinRightRouteCode.TRUE.Equals(this.RIGHT_ROUTE_CODE))
                {
                    LogSystem.Error("Neu ma noi KCB ban dau nam trong Danh sach chap nhan cua benh vien hoac thuoc loai 'Chung sinh' thi phai la 'Dung Tuyen'");
                    valid = false;
                }*/
            }
            else if (this.RIGHT_ROUTE_TYPE_CODE != null && HeinRightRouteTypeStore.IsValidCode(this.RIGHT_ROUTE_TYPE_CODE))
            {
                if (!HeinRightRouteCode.TRUE.Equals(this.RIGHT_ROUTE_CODE))
                {
                    LogSystem.Error("Neu co du lieu 'Loai Dung tuyen' thi doi tuong phai luon la 'Dung Tuyen'");
                    valid = false;
                }
            }
            else if (!HeinRightRouteCode.FALSE.Equals(this.RIGHT_ROUTE_CODE))
            {
                //Bo validate nay vi cho phep chuyen BN giua cac khoa cua nhieu chi nhanh (cac chi nhanh nay co the co ma KCB khac nhau)
                //LogSystem.Error("Neu co du lieu ma KCB ko nam trong danh sach chap nhan dung tuyen, benh vien ko phai la tuyen huyen va ko thuoc loai cap cuu hoac gioi thieu thi doi tuong phai luon la 'Trai Tuyen'");
                //valid = false;
            }
            valid = valid && HeinRightRouteStore.IsValidCode(this.RIGHT_ROUTE_CODE)
                && (this.JOIN_5_YEAR == null || HeinJoin5YearStore.IsValidCode(this.JOIN_5_YEAR))
                && (this.PAID_6_MONTH == null || HeinPaid6MonthStore.IsValidCode(this.PAID_6_MONTH))
                //neu la the tre em do he thong tu sinh thi ma KCB la ma cua tinh/thanh pho ==> ko co trong danh sach duoc khai bao trong file xml
                //&& (this.HAS_BIRTH_CERTIFICATE == HeinHasBirthCertificateCode.TRUE || HeinMediOrgStore.IsValidCode(this.HEIN_MEDI_ORG_CODE)) cần chú ý
                && HeinLevelStore.IsValidCode(this.LEVEL_CODE)
                && (this.LIVE_AREA_CODE == null || HeinLiveAreaStore.IsValidCode(this.LIVE_AREA_CODE))
                && (this.RIGHT_ROUTE_TYPE_CODE == null || HeinRightRouteTypeStore.IsValidCode(this.RIGHT_ROUTE_TYPE_CODE));
            return valid;
        }

        public static string ToJsonString(HIS_PATIENT_TYPE_ALTER bhyt)
        {
            try
            {
                //luon set cac truong ko quan trong ve null 
                //(==> de khi cac du lieu nay bi thay doi cung ko anh huong den noi dung cua truong json)
                bhyt.APP_CREATOR = null;
                bhyt.APP_MODIFIER = null;
                bhyt.CREATE_TIME = null;
                bhyt.CREATOR = null;
                bhyt.GROUP_CODE = null;
                bhyt.MODIFIER = null;
                bhyt.MODIFY_TIME = null;
                return JsonConvert.SerializeObject(bhyt);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Sinh so the BHYT cho doi tuong tre em dua vao cac thong so truyen vao
        /// </summary>
        /// <param name="nextNumber">So thu tu tiep theo</param>
        /// <param name="provinceCode">Ma tinh</param>
        /// <param name="districtCode">Ma huyen</param>
        /// <returns>Chuoi so the BHYT</returns>
        private static string GenerateChildHeinNumber(List<string> heinCardNumbers, string provinceCode)
        {
            if (!String.IsNullOrWhiteSpace(provinceCode) && (provinceCode.Length == 2))
            {
                int nextNumber = 1;
                if (heinCardNumbers != null)
                {
                    //Lay so the BHYT la TE lon nhat va cong them 1
                    nextNumber = heinCardNumbers
                        .Where(o => HeinObjectBenefitStore.IsChild(o))
                        .Select(o => Int32.Parse(o.Substring(o.Length - 5)))
                        .OrderByDescending(o => o)
                        .FirstOrDefault() + 1;
                }
                if (nextNumber > 0)
                {
                    return String.Format(BhytConstant.CHILD_HEIN_NUMBER_FORMAT, provinceCode, nextNumber.ToString("D8"));
                }
            }
            return null;
        }

        /// <summary>
        /// Sinh so the BHYT cho doi tuong quan nhan co giay gioi thieu
        /// </summary>
        /// <param name="nextNumber">So thu tu tiep theo</param>
        /// <returns>Chuoi so the BHYT</returns>
        private static string GenerateQnHeinNumber(List<string> heinCardNumbers)
        {
            int nextNumber = 1;
            if (heinCardNumbers != null && heinCardNumbers.Count > 0)
            {
                //Lay so the BHYT la TE lon nhat va cong them 1
                nextNumber = heinCardNumbers
                    .Where(o => HeinObjectBenefitStore.IsQn(o))
                    .Select(o => Int32.Parse(o.Substring(o.Length - 5)))
                    .OrderByDescending(o => o)
                    .FirstOrDefault() + 1;
            }
            if (nextNumber > 0)
            {
                return String.Format(BhytConstant.QN_HEIN_NUMBER_FORMAT, nextNumber.ToString("D8"));
            }
            return null;
        }

        private static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }
    }
}
