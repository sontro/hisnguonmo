using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.Config
{
    class HisServiceCFG
    {
        /// <summary>
        /// ma cua dich vu duoc cau hinh de luu gia tien gan cho BN trong truong hop
        /// so dich vu hao phi vuot qua gioi han hao phi cho phep
        /// </summary>
        private const string SERVICE_CODE__OVER_EXPEND = "MOS.HIS_SERVICE.SERVICE_CODE.OVER_EXPEND";
        
        private static long serviceIdOverExpend;
        public static long SERVICE_ID__OVER_EXPEND
        {
            get
            {
                if (serviceIdOverExpend == 0)
                {
                    serviceIdOverExpend = GetId(SERVICE_CODE__OVER_EXPEND);
                }
                return serviceIdOverExpend;
            }
        }

        private static List<V_HIS_SERVICE> dataView;
        public static List<V_HIS_SERVICE> DATA_VIEW
        {
            get
            {
                if (dataView == null)
                {
                    dataView = new HisServiceGet().GetView(new HisServiceViewFilterQuery());
                }
                return dataView;
            }
        }

        private static List<V_HIS_SERVICE> hasPackageView;
        public static List<V_HIS_SERVICE> HAS_PACKAGE_DATA_VIEW
        {
            get
            {
                if (hasPackageView == null)
                {
                    hasPackageView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.PACKAGE_ID.HasValue).ToList() : null;
                }
                return hasPackageView;
            }
        }


        private static List<V_HIS_SERVICE> notRequiredComplete;
        public static List<V_HIS_SERVICE> IS_NOT_REQUIRED_COMPLETE_DATA_VIEW
        {
            get
            {
                if (notRequiredComplete == null)
                {
                    notRequiredComplete = DATA_VIEW != null ? DATA_VIEW.Where(o => o.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE).ToList() : null;
                }
                return notRequiredComplete;
            }
        }

        private static List<long> noHeinLimitForSpecialCard;
        public static List<long> NO_HEIN_LIMIT_FOR_SPECIAL_CARD
        {
            get
            {
                if (noHeinLimitForSpecialCard == null)
                {
                    noHeinLimitForSpecialCard = DATA_VIEW != null ? DATA_VIEW.Where(o => o.IS_NO_HEIN_LIMIT_FOR_SPECIAL == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
                }
                return noHeinLimitForSpecialCard;
            }
        }

        private static List<V_HIS_SERVICE> hasMinProcessTimeDataView;
        public static List<V_HIS_SERVICE> HAS_MIN_PROCESS_TIME_DATA_VIEW
        {
            get
            {
                if (hasMinProcessTimeDataView == null)
                {
                    hasMinProcessTimeDataView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.MIN_PROCESS_TIME.HasValue).ToList() : null;
                }
                return hasMinProcessTimeDataView;
            }
        }

        private static List<V_HIS_SERVICE> DoNotUseBhytDataView;
        public static List<V_HIS_SERVICE> DO_NOT_USE_BHYT_DATA_VIEW
        {
            get
            {
                if (DoNotUseBhytDataView == null)
                {
                    DoNotUseBhytDataView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.DO_NOT_USE_BHYT == Constant.IS_TRUE).ToList() : null;
                }
                return DoNotUseBhytDataView;
            }
        }

        private static long GetId(string code)
        {
            string value = ConfigUtil.GetStrConfig(code);
            if (!string.IsNullOrWhiteSpace(value))
            {
                V_HIS_SERVICE service = DATA_VIEW != null ? DATA_VIEW.Where(o => o.SERVICE_CODE == value).FirstOrDefault() : null;
                return service != null ? service.ID : -1;
            }
            return -1;
        }

        public static void Reload()
        {
            var data = new HisServiceGet().GetView(new HisServiceViewFilterQuery());
            dataView = data;
            noHeinLimitForSpecialCard = DATA_VIEW != null ? DATA_VIEW.Where(o => o.IS_NO_HEIN_LIMIT_FOR_SPECIAL == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
            var idOverExpend = GetId(SERVICE_CODE__OVER_EXPEND);
            serviceIdOverExpend = idOverExpend;
            hasPackageView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.PACKAGE_ID.HasValue).ToList() : null;
            notRequiredComplete = DATA_VIEW != null ? DATA_VIEW.Where(o => o.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE).ToList() : null;
            hasMinProcessTimeDataView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.MIN_PROCESS_TIME.HasValue).ToList() : null;
            DoNotUseBhytDataView = DATA_VIEW != null ? DATA_VIEW.Where(o => o.DO_NOT_USE_BHYT == Constant.IS_TRUE).ToList() : null;
        }
    }
}
