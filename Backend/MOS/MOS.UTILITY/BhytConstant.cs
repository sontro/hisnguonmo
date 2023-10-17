using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.UTILITY
{
    public class BhytConstant
    {
        #region Fix gia tri ti le tinh gia doi voi dich vu giuong nam ghep
        public static Dictionary<long, decimal> MAP_SHARED_BED_PRICE_RATIO = new Dictionary<long, decimal>()
        {
            {1, 1m}, //ko nam ghep tinh 100%
            {2, 0.5m}, //ghep 2 tinh 50%
            {3, 0.3m}, //ghep 3 tro len tinh 30%
        };
        #endregion

        #region Fix gia tri ti le tinh gia doi voi dich vu kham trong cung 1 ngay do Bo Y Te quy dinh
        public static Dictionary<int, decimal> MAP_INDEX_TO_PRICE_RATIO_EXAM = new Dictionary<int, decimal>()
        {
            {0, 1}, //lan dau tien trong ngay tinh 100%
            {1, 0.3m},//lan thu 2 trong ngay tinh 30%
            {2, 0.3m},//lan thu 3 trong ngay tinh 30%
            {3, 0.3m},//lan thu 4 trong ngay tinh 30%
            {4, 0.1m},//lan thu 5 trong ngay tinh 10%, tu lan thu 6 tro di tinh 0%
        };
        #endregion

        #region Fix gia tri ti le tinh gia doi voi dich vu phau thuat, thu thuat kem theo goi dich vu phau thuat do BYT quy dinh
        /// <summary>
        /// Ti le cua dich vu thu thuat kem theo 1 dich vu phau thuat
        /// </summary>
        public const decimal ATTACH_MISU_RATIO = 0.8m;
        /// <summary>
        /// Ti le gia cua dich vu phau thuat kem theo dich vu phau thuat, nhung cung kip mo thuc hien (ekip)
        /// </summary>
        public const decimal ATTACH_SURG_RATIO = 0.5m;
        /// <summary>
        /// Ti le gia cua dich vu phau thuat kem theo dich vu phau thuat, nhung ko cung kip mo thuc hien (ekip)
        /// </summary>
        public const decimal ATTACH_SURG_OTHER_EKIP_RATIO = 0.8m;
        #endregion

        /// <summary>
        /// Thoi gian (tinh bang gio) tinh dieu tri cap cuu
        /// </summary>
        public const double CLINICAL_TIME_FOR_EMERGENCY = 4;

        /// <summary>
        /// Do dai cua the BHYT
        /// </summary>
        public const int HEIN_NUMBER_LENGTH = 15;

        /// <summary>
        /// Muc huong cua the bi ap dung cong thuc check dong du 5 nam ==> duoc tinh 39 TLTT
        /// </summary>
        public static List<decimal> DEFAULT_RATIO_FOR_HIGH_HEIN_SERVICE_5_YEAR = new List<decimal>() { 0.8m };

        /// <summary>
        /// Chuoi dinh dang de sinh so the BHYT
        /// </summary>
        public const string CHILD_HEIN_NUMBER_FORMAT = "TE1{0}KT{1}";
        /// <summary>
        /// Chuoi dinh dang de sinh ma DK KCB ban dau
        /// </summary>
        public const string CHILD_HEIN_ORG_CODE_FORMAT = "{0}000";
        /// <summary>
        /// Chuoi dinh dang de sinh ten DK KCB ban dau
        /// </summary>
        public const string CHILD_HEIN_ORG_NAME_FORMAT = "BHYT {0}";

        /// <summary>
        /// Chuoi dinh dang de sinh so the BHYT(Quan nhan co giay gioi thieu)
        /// </summary>
        public const string QN_HEIN_NUMBER_FORMAT = "QN597KT{0}";

        /// <summary>
        /// So chu so lam tron sau phan thap phan
        /// </summary>
        public const int DECIMAL_PRECISION = 4;
        /// <summary>
        /// So tuoi gioi han duoc quy dinh la tre em
        /// </summary>
        public const int CHILD_AGE = 6;
        /// <summary>
        /// Neu tre co so tuoi hon "child_age" nhung chua den tuoi di hoc thi hieu luc tinh tu thang nay
        /// </summary>
        public const int CHILD_MONTH_EXPIRY = 9;
        /// <summary>
        /// Neu tre co so tuoi hon "child_age" nhung chua den tuoi di hoc thi hieu luc tinh tu ngay nay cua thang o tren
        /// </summary>
        public const int CHILD_DAY_EXPIRY = 30;
    }
}
