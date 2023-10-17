using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    class RequestPrice
    {
        public long Id { get; set; }
        public decimal TotalPatientPrice { get; set; }
    }

    class HisServiceReqUtil
    {
        //Luu du thua du lieu
        public static void SetTdl(HIS_SERVICE_REQ serviceReq, HIS_TREATMENT treatment)
        {
            if (serviceReq != null && treatment != null)
            {
                serviceReq.TDL_PATIENT_ADDRESS = treatment.TDL_PATIENT_ADDRESS;
                serviceReq.TDL_PATIENT_CAREER_NAME = treatment.TDL_PATIENT_CAREER_NAME;
                serviceReq.TDL_PATIENT_ID = treatment.PATIENT_ID;
                serviceReq.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                serviceReq.TDL_PATIENT_DISTRICT_CODE = treatment.TDL_PATIENT_DISTRICT_CODE;
                serviceReq.TDL_PATIENT_DOB = treatment.TDL_PATIENT_DOB;
                serviceReq.TDL_PATIENT_FIRST_NAME = treatment.TDL_PATIENT_FIRST_NAME;
                serviceReq.TDL_PATIENT_GENDER_ID = treatment.TDL_PATIENT_GENDER_ID;
                serviceReq.TDL_PATIENT_GENDER_NAME = treatment.TDL_PATIENT_GENDER_NAME;
                serviceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                serviceReq.TDL_PATIENT_LAST_NAME = treatment.TDL_PATIENT_LAST_NAME;
                serviceReq.TDL_PATIENT_MILITARY_RANK_NAME = treatment.TDL_PATIENT_MILITARY_RANK_NAME;
                serviceReq.TDL_PATIENT_NATIONAL_NAME = treatment.TDL_PATIENT_NATIONAL_NAME;
                serviceReq.TDL_PATIENT_PROVINCE_CODE = treatment.TDL_PATIENT_PROVINCE_CODE;
                serviceReq.TDL_PATIENT_WORK_PLACE = treatment.TDL_PATIENT_WORK_PLACE;
                serviceReq.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                serviceReq.TDL_HEIN_CARD_NUMBER = treatment.TDL_HEIN_CARD_NUMBER;
                serviceReq.TDL_TREATMENT_TYPE_ID = treatment.TDL_TREATMENT_TYPE_ID;
                serviceReq.TDL_HEIN_MEDI_ORG_CODE = treatment.TDL_HEIN_MEDI_ORG_CODE;
                serviceReq.TDL_HEIN_MEDI_ORG_NAME = treatment.TDL_HEIN_MEDI_ORG_NAME;
                serviceReq.TDL_PATIENT_WORK_PLACE_NAME = treatment.TDL_PATIENT_WORK_PLACE_NAME;
                serviceReq.TDL_PATIENT_NAME = treatment.TDL_PATIENT_NAME;
                serviceReq.TDL_PATIENT_UNSIGNED_NAME = !string.IsNullOrWhiteSpace(serviceReq.TDL_PATIENT_NAME) ? Inventec.Common.String.Convert.UnSignVNese2(serviceReq.TDL_PATIENT_NAME) : null;
                serviceReq.TDL_PATIENT_AVATAR_URL = treatment.TDL_PATIENT_AVATAR_URL;
                serviceReq.TDL_PATIENT_COMMUNE_CODE = treatment.TDL_PATIENT_COMMUNE_CODE;
                serviceReq.TDL_PATIENT_TYPE_ID = treatment.TDL_PATIENT_TYPE_ID;
                serviceReq.TDL_PATIENT_MOBILE = treatment.TDL_PATIENT_MOBILE;
                serviceReq.TDL_PATIENT_PHONE = treatment.TDL_PATIENT_PHONE;
                serviceReq.TDL_PATIENT_CLASSIFY_ID = treatment.TDL_PATIENT_CLASSIFY_ID;
                serviceReq.TDL_PATIENT_CMND_NUMBER = treatment.TDL_PATIENT_CMND_NUMBER;
                serviceReq.TDL_PATIENT_CMND_DATE = treatment.TDL_PATIENT_CMND_DATE;
                serviceReq.TDL_PATIENT_CMND_PLACE = treatment.TDL_PATIENT_CMND_PLACE;
                serviceReq.TDL_PATIENT_CCCD_NUMBER = treatment.TDL_PATIENT_CCCD_NUMBER;
                serviceReq.TDL_PATIENT_CCCD_DATE = treatment.TDL_PATIENT_CCCD_DATE;
                serviceReq.TDL_PATIENT_CCCD_PLACE = treatment.TDL_PATIENT_CCCD_PLACE;
                serviceReq.TDL_PATIENT_PASSPORT_DATE = treatment.TDL_PATIENT_PASSPORT_DATE;
                serviceReq.TDL_PATIENT_PASSPORT_NUMBER = treatment.TDL_PATIENT_PASSPORT_NUMBER;
                serviceReq.TDL_PATIENT_PASSPORT_PLACE = treatment.TDL_PATIENT_PASSPORT_PLACE;

                serviceReq.TDL_PATIENT_PROVINCE_NAME = treatment.TDL_PATIENT_PROVINCE_NAME;
                serviceReq.TDL_PATIENT_DISTRICT_NAME = treatment.TDL_PATIENT_DISTRICT_NAME;
                serviceReq.TDL_PATIENT_COMMUNE_NAME = treatment.TDL_PATIENT_COMMUNE_NAME;
                serviceReq.TDL_PATIENT_NATIONAL_CODE = treatment.TDL_PATIENT_NATIONAL_CODE;
                serviceReq.TDL_PATIENT_POSITION_ID = treatment.TDL_PATIENT_POSITION_ID;
            }
        }

        public static void SetTdl(HIS_SERVICE_REQ serviceReq, V_HIS_RATION_SCHEDULE rationSchedule)
        {
            if (serviceReq != null && rationSchedule != null)
            {
                serviceReq.TDL_PATIENT_ID = rationSchedule.PATIENT_ID ?? 0;
                serviceReq.TDL_PATIENT_ADDRESS = rationSchedule.TDL_PATIENT_ADDRESS;
                serviceReq.TDL_PATIENT_AVATAR_URL = rationSchedule.TDL_PATIENT_AVATAR_URL;
                serviceReq.TDL_PATIENT_CAREER_NAME = rationSchedule.TDL_PATIENT_CAREER_NAME;
                serviceReq.TDL_PATIENT_CCCD_DATE = rationSchedule.TDL_PATIENT_CCCD_DATE;
                serviceReq.TDL_PATIENT_CCCD_NUMBER = rationSchedule.TDL_PATIENT_CCCD_NUMBER;
                serviceReq.TDL_PATIENT_CCCD_PLACE = rationSchedule.TDL_PATIENT_CCCD_PLACE;
                serviceReq.TDL_PATIENT_CLASSIFY_ID = rationSchedule.TDL_PATIENT_CLASSIFY_ID;
                serviceReq.TDL_PATIENT_CMND_DATE = rationSchedule.TDL_PATIENT_CMND_DATE;
                serviceReq.TDL_PATIENT_CMND_NUMBER = rationSchedule.TDL_PATIENT_CMND_NUMBER;
                serviceReq.TDL_PATIENT_CMND_PLACE = rationSchedule.TDL_PATIENT_CMND_PLACE;
                serviceReq.TDL_PATIENT_CODE = rationSchedule.TDL_PATIENT_CODE;
                serviceReq.TDL_PATIENT_COMMUNE_CODE = rationSchedule.TDL_PATIENT_COMMUNE_CODE;
                serviceReq.TDL_PATIENT_COMMUNE_NAME = rationSchedule.TDL_PATIENT_COMMUNE_NAME;
                serviceReq.TDL_PATIENT_DISTRICT_CODE = rationSchedule.TDL_PATIENT_DISTRICT_CODE;
                serviceReq.TDL_PATIENT_DISTRICT_NAME = rationSchedule.TDL_PATIENT_DISTRICT_NAME;
                serviceReq.TDL_PATIENT_DOB = rationSchedule.TDL_PATIENT_DOB ?? 0;
                serviceReq.TDL_PATIENT_FIRST_NAME = rationSchedule.TDL_PATIENT_FIRST_NAME;
                serviceReq.TDL_PATIENT_GENDER_ID = rationSchedule.TDL_PATIENT_GENDER_ID;
                serviceReq.TDL_PATIENT_GENDER_NAME = rationSchedule.TDL_PATIENT_GENDER_NAME;
                serviceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = rationSchedule.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                serviceReq.TDL_PATIENT_LAST_NAME = rationSchedule.TDL_PATIENT_LAST_NAME;
                serviceReq.TDL_PATIENT_MILITARY_RANK_NAME = rationSchedule.TDL_PATIENT_MILITARY_RANK_NAME;
                serviceReq.TDL_PATIENT_MOBILE = rationSchedule.TDL_PATIENT_MOBILE;
                serviceReq.TDL_PATIENT_NAME = rationSchedule.TDL_PATIENT_NAME;
                serviceReq.TDL_PATIENT_NATIONAL_CODE = rationSchedule.TDL_PATIENT_NATIONAL_CODE;
                serviceReq.TDL_PATIENT_NATIONAL_NAME = rationSchedule.TDL_PATIENT_NATIONAL_NAME;
                serviceReq.TDL_PATIENT_PASSPORT_DATE = rationSchedule.TDL_PATIENT_PASSPORT_DATE;
                serviceReq.TDL_PATIENT_PASSPORT_NUMBER = rationSchedule.TDL_PATIENT_PASSPORT_NUMBER;
                serviceReq.TDL_PATIENT_PASSPORT_PLACE = rationSchedule.TDL_PATIENT_PASSPORT_PLACE;
                serviceReq.TDL_PATIENT_PHONE = rationSchedule.TDL_PATIENT_PHONE;
                serviceReq.TDL_PATIENT_POSITION_ID = rationSchedule.TDL_PATIENT_POSITION_ID;
                serviceReq.TDL_PATIENT_PROVINCE_CODE = rationSchedule.TDL_PATIENT_PROVINCE_CODE;
                serviceReq.TDL_PATIENT_PROVINCE_NAME = rationSchedule.TDL_PATIENT_PROVINCE_NAME;
                serviceReq.TDL_PATIENT_TYPE_ID = rationSchedule.TDL_PATIENT_TYPE_ID;
                serviceReq.TDL_PATIENT_UNSIGNED_NAME = rationSchedule.TDL_PATIENT_UNSIGNED_NAME;
                serviceReq.TDL_PATIENT_WORK_PLACE = rationSchedule.TDL_PATIENT_WORK_PLACE;
                serviceReq.TDL_PATIENT_WORK_PLACE_NAME = rationSchedule.TDL_PATIENT_WORK_PLACE_NAME;
            }
        }

        public static void SetReturnAddress(HIS_SERVICE_REQ serviceReq)
        {
            if (serviceReq != null)
            {
                List<HIS_SUBCLINICAL_RS_ADD> lstAddress = null;
                if (HisSubclinicalRsAddCFG.DATA != null)
                {
                    lstAddress = HisSubclinicalRsAddCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && (o.RESULT_DESK_ID.HasValue || o.RESULT_ROOM_ID.HasValue) && (o.REQUEST_ROOM_ID == serviceReq.REQUEST_ROOM_ID || o.EXECUTE_ROOM_ID == serviceReq.EXECUTE_ROOM_ID) && CheckDayAndTime(serviceReq, o.INSTR_DAY_FROM, o.INSTR_DAY_TO, o.INSTR_TIME_FROM, o.INSTR_TIME_TO)).ToList();
                }

                HIS_SUBCLINICAL_RS_ADD add = lstAddress != null ? lstAddress.OrderByDescending(o => (o.MODIFY_TIME ?? 0)).FirstOrDefault() : null;

                if (add != null)
                {
                    if (add.RESULT_ROOM_ID.HasValue) serviceReq.RESULT_ROOM_ID = add.RESULT_ROOM_ID;
                    else serviceReq.RESULT_DESK_ID = add.RESULT_DESK_ID;
                }
                else
                {
                    serviceReq.RESULT_DESK_ID = null;
                    serviceReq.RESULT_ROOM_ID = null;
                }
            }
        }

        public static void SetCashierRoom(HIS_SERVICE_REQ serviceReq, bool isOnlyType)
        {
            if (serviceReq != null)
            {
                List<HIS_CASHIER_ADD_CONFIG> lstAddress = null;
                if (HisCashierAddConfigCFG.DATA != null)
                {
                    lstAddress = HisCashierAddConfigCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && (o.REQUEST_ROOM_ID.HasValue || o.EXECUTE_ROOM_ID.HasValue) && (o.REQUEST_ROOM_ID == serviceReq.REQUEST_ROOM_ID || o.EXECUTE_ROOM_ID == serviceReq.EXECUTE_ROOM_ID) && CheckDayAndTime(serviceReq, o.INSTR_DAY_FROM, o.INSTR_DAY_TO, o.INSTR_TIME_FROM, o.INSTR_TIME_TO)).ToList();
                }
                HIS_CASHIER_ADD_CONFIG add = null;
                if (isOnlyType)
                {
                    add = lstAddress != null ? lstAddress.OrderBy(o => o.IS_NOT_PRIORITY != Constant.IS_TRUE).OrderByDescending(o => (o.MODIFY_TIME ?? 0)).FirstOrDefault() : null;
                }
                else
                {
                    add = lstAddress != null ? lstAddress.OrderBy(o => o.IS_NOT_PRIORITY == Constant.IS_TRUE).OrderByDescending(o => (o.MODIFY_TIME ?? 0)).FirstOrDefault() : null;
                }

                if (add != null)
                {
                    serviceReq.CASHIER_ROOM_ID = add.CASHIER_ROOM_ID;
                }
                else
                {
                    serviceReq.CASHIER_ROOM_ID = null;
                }
            }
        }

        private static bool CheckDayAndTime(HIS_SERVICE_REQ serviceReq, long? fromDay, long? toDay, string fromTime, string toTime)
        {
            try
            {
                DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME).Value;
                long day = (long)dt.DayOfWeek;
                long time = Convert.ToInt64(serviceReq.INTRUCTION_TIME.ToString().Substring(8, 4))
                    ;
                if (fromDay.HasValue && fromDay > (day + 1)) return false;
                if (toDay.HasValue && toDay < (day + 1)) return false;

                long tFrom = 0;
                if (!String.IsNullOrWhiteSpace(fromTime) && long.TryParse(fromTime, out tFrom))
                {
                    if (tFrom > time) return false;
                }

                long tTo = 0;
                if (!String.IsNullOrWhiteSpace(toTime) && long.TryParse(toTime, out tTo))
                {
                    if (tTo < time) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        public static void SetReturnInDiffDay(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs)
        {
            if (serviceReq != null && sereServs != null && sereServs.Count > 0)
            {
                List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => o.PARENT_ID.HasValue && sereServs.Any(a => a.SERVICE_ID == o.ID)).ToList();
                List<HIS_SERVICE_RERE_TIME> lstExist = null;
                if (HisServiceRereTimeCFG.DATA != null && services != null)
                {
                    lstExist = HisServiceRereTimeCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && services.Any(a => a.PARENT_ID == o.SERVICE_ID) && o.IS_DIFFERENT_DAY == Constant.IS_TRUE && CheckDayAndTime(serviceReq, o.INSTR_DAY_FROM, o.INSTR_DAY_TO, o.INSTR_TIME_FROM, o.INSTR_TIME_TO)).ToList();
                }

                if (lstExist != null && lstExist.Count > 0)
                {
                    serviceReq.IS_RESULT_IN_DIFF_DAY = Constant.IS_TRUE;
                }
                else
                {
                    serviceReq.IS_RESULT_IN_DIFF_DAY = null;
                }
            }
        }

        /// <summary>
        /// Lay ra danh sach co tong gia lon nhat va nho hon gioi han cho truoc.
        /// Thuat toan: Duyet tat ca cac danh sach co the tao ra de lay ra d/s co gia tri lon nhat va nho hon limit. Cu the:
        /// - Tao ra chuoi nhi phan co dang 100010000... Moi ki tu cua chuoi nhi phan se dai dien cho 1 phan tu trong list danh sach. Neu ki tu = 1 thi la duoc chon
        /// - Dung chuoi nhi phan de dai dien cho 1 danh sach
        /// - Thuc hien sinh ra tat ca chuoi nhi phan co the va duyet lay ra chuoi nhi phan tuong ung voi danh sach co tong gia tri lon nhat va nho hon limit
        /// </summary>
        /// <param name="items"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<RequestPrice> SelectMaxList(List<RequestPrice> items, decimal limit)
        {
            //Lay ra tong so cac case can duyet
            //(tong so case = 2^n voi n = so phan tu)
            int size = (int)Math.Pow(2, items.Count);

            List<RequestPrice> maxSelected = new List<RequestPrice>();
            decimal maxTotal = 0;

            for (int t = 0; t < size; t++)
            {
                //Tao ra chuoi nhi phan
                string s = Convert.ToString(t, 2).PadLeft(items.Count, '0');

                decimal totalPrice = 0;

                List<RequestPrice> selected = new List<RequestPrice>();

                //Duyet danh sach tuong ung voi chuoi nhi phan de lay ra cac phan tu duoc chon
                //Dong thoi tinh tong tien cua danh sach duoc chon
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.ElementAt(i) == '1')
                    {
                        totalPrice += items[i].TotalPatientPrice;
                        selected.Add(items[i]);
                    }
                }

                //Neu tong tien lon hon tong tien cua d/s duoc chon truoc do va nho hon "limit" thi cap nhat lai d/s duoc chon
                //Lam tron vi khi thanh toan chi gui sang ngan hang so nguyen
                if (Math.Round(totalPrice, 0) <= limit && totalPrice > maxTotal)
                {
                    maxSelected = selected;
                    maxTotal = totalPrice;

                    //Neu lay duoc d/s co tong tien = limit --> da lay duoc d/s toi uu nhat --> ket thuc xu ly
                    if (Math.Round(totalPrice, 0) == limit)
                    {
                        break;
                    }
                }
            }
            return maxSelected;
        }

        /// <summary>
        /// Neu dich vu duoc cau hinh "tach dich vu" thi tu dong tach thanh nhieu dong neu ke so luong > 1
        /// </summary>
        /// <param name="materials"></param>
        /// <returns></returns>
        internal static List<HIS_SERE_SERV> MakeSingleService(HIS_SERE_SERV sereServ)
        {
            List<HIS_SERE_SERV> list = new List<HIS_SERE_SERV>();

            try
            {
                if (sereServ.AMOUNT > 1)
                {
                    V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sereServ.SERVICE_ID).FirstOrDefault();

                    if (service != null && service.IS_SPLIT_SERVICE == Constant.IS_TRUE)
                    {
                        decimal remain = sereServ.AMOUNT;

                        Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                        while (remain > 0)
                        {
                            HIS_SERE_SERV s = Mapper.Map<HIS_SERE_SERV>(sereServ);
                            s.AMOUNT = remain > 1 ? 1 : remain;

                            list.Add(s);
                            remain = remain - s.AMOUNT;
                        }
                    }
                    else
                    {
                        list.Add(sereServ);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return list;
        }
    }
}
