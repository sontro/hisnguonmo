using Inventec.Common.Logging;
using MOS.DAO.HisExpMestStt;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPackage;
using MOS.UTILITY;

namespace MOS.MANAGER.Config
{
    class HisPackageCFG
    {
        /// <summary>
        /// Cua loai chinh sach "3 ngay 7 ngay"
        /// </summary>
        private const string PACKAGE_CODE__3DAY7DAY = "MOS.HIS_PACKAGE.PACKAGE_CODE.3DAY7DAY";
        /// <summary>
        /// Goi đẻ
        /// </summary>
        private const string PACKAGE_CODE__DE = "MOS.HIS_PACKAGE.PACKAGE_CODE.DE";
        /// <summary>
        /// Goi phau thuat tham my
        /// </summary>
        private const string PACKAGE_CODE__PTTM = "MOS.HIS_PACKAGE.PACKAGE_CODE.PTTM";

        private static List<HIS_PACKAGE> data;
        public static List<HIS_PACKAGE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisPackageGet().Get(new HisPackageFilterQuery());
                }
                return data;
            }
        }

        private static List<long> notFixedServicePackageIds;
        public static List<long> NOT_FIXED_SERVICE_PACKAGE_IDS
        {
            get
            {
                if (notFixedServicePackageIds == null)
                {
                    notFixedServicePackageIds = DATA != null ? DATA.Where(o => o.IS_NOT_FIXED_SERVICE == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
                }
                return notFixedServicePackageIds;
            }
        }

        private static long? packageId3day7day;
        public static long? PACKAGE_ID__3DAY7DAY
        {
            get
            {
                if (!packageId3day7day.HasValue)
                {
                    packageId3day7day = GetId(PACKAGE_CODE__3DAY7DAY);
                }
                return packageId3day7day;
            }
        }

        private static long? packageIdPttm;
        public static long? PACKAGE_ID__PTTM
        {
            get
            {
                if (!packageIdPttm.HasValue)
                {
                    packageIdPttm = GetId(PACKAGE_CODE__PTTM);
                }
                return packageIdPttm;
            }
        }

        private static long? packageIdDe;
        public static long? PACKAGE_ID__DE
        {
            get
            {
                if (!packageIdDe.HasValue)
                {
                    packageIdDe = GetId(PACKAGE_CODE__DE);
                }
                return packageIdDe;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                string value = ConfigUtil.GetStrConfig(code);
                if (value != null)
                {
                    var data = DATA.Where(o => o.PACKAGE_CODE == value).FirstOrDefault();
                    if (data == null) throw new ArgumentNullException(code);
                    result = data.ID;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }

        public static void Reload()
        {
            var tmp = new HisPackageGet().Get(new HisPackageFilterQuery());
            data = tmp;
            var id3day7day = GetId(PACKAGE_CODE__3DAY7DAY);
            packageId3day7day = id3day7day;

            notFixedServicePackageIds = DATA != null ? DATA.Where(o => o.IS_NOT_FIXED_SERVICE == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
        }
    }
}
