using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.UTILITY
{
    public class Constant
    {
        //So chu so sau phan thap phan
        public const int DECIMAL_PRECISION = 4;
        public const string APPLICATION_CODE = "MOS";
        public const string CLIENT_APPLICATION_CODE = "HIS";
        public const string THE_VIET_APPLICATION_CODE = "TVA";
        public const short IS_TRUE = (short)1;
        public const short IS_FALSE = (short)0;
        public const string PATIENT_IMG_BHYT = "BHYT";
        public const string PATIENT_IMG_AVATAR = "AVATAR";
        public const string PATIENT_IMG_CMND_BEFORE = "CMND_BEFORE";
        public const string PATIENT_IMG_CMND_AFTER = "CMND_AFTER";
        public const string DB_NULL_STR = "null";
        /// <summary>
        /// Sai so khi thuc hien thanh toan (do viec lam tron so)
        /// </summary>
        public const decimal PRICE_DIFFERENCE = 0.0001m;

        public const string APP_CODE__SANCY = "SANCY";
        public const string HIS_CODE__PACS = "HIS_INVENTEC";

        public const decimal DECIMAL_PRECISION_AMOUNT = 1000000m;

        public const int DB__MAX_LENGTH = 4000;
    }
}
