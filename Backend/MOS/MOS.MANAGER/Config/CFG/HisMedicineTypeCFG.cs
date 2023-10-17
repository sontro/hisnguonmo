using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.Config
{
    class HisMedicineTypeCFG
    {
        private static List<HIS_MEDICINE_TYPE> data;
        public static List<HIS_MEDICINE_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMedicineTypeGet().Get(new HisMedicineTypeFilterQuery());
                }
                return data;
            }
        }

        private static List<long> gayNghienIds;
        public static List<long> GAY_NGHIEN_IDs
        {
            get
            {
                if (gayNghienIds == null)
                {
                    gayNghienIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__GN)
                        .Select(o => o.ID).ToList();
                }
                return gayNghienIds;
            }
        }

        private static List<long> starIds;
        public static List<long> STAR_IDs
        {
            get
            {
                if (starIds == null)
                {
                    starIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.IS_STAR_MARK == Constant.IS_TRUE)
                        .Select(o => o.ID).ToList();
                }
                return starIds;
            }
        }

        private static List<long> huongThanIds;
        public static List<long> HUONG_THAN_IDs
        {
            get
            {
                if (huongThanIds == null)
                {
                    huongThanIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__HT)
                        .Select(o => o.ID).ToList();
                }
                return huongThanIds;
            }
        }

        private static List<long> thuocDocIds;
        public static List<long> THUOC_DOC_IDs
        {
            get
            {
                if (thuocDocIds == null)
                {
                    thuocDocIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__DOC)
                        .Select(o => o.ID).ToList();
                }
                return thuocDocIds;
            }
        }

        private static List<long> thucPhamChucNangIds;
        public static List<long> THUC_PHAM_CHUC_NANG_IDs
        {
            get
            {
                if (thucPhamChucNangIds == null)
                {
                    thucPhamChucNangIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.IS_FUNCTIONAL_FOOD == Constant.IS_TRUE)
                        .Select(o => o.ID).ToList();
                }
                return thucPhamChucNangIds;
            }
        }

        public static void Reload()
        {
            var tmp = new HisMedicineTypeGet().Get(new HisMedicineTypeFilterQuery());
            data = tmp;

            gayNghienIds = HisMedicineTypeCFG.DATA
                .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__GN)
                .Select(o => o.ID).ToList();

            huongThanIds = HisMedicineTypeCFG.DATA
                .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__HT)
                .Select(o => o.ID).ToList();

            thucPhamChucNangIds = HisMedicineTypeCFG.DATA
                .Where(o => o.IS_FUNCTIONAL_FOOD == Constant.IS_TRUE)
                .Select(o => o.ID).ToList();

            starIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.IS_STAR_MARK == Constant.IS_TRUE)
                        .Select(o => o.ID).ToList();

            thuocDocIds = HisMedicineTypeCFG.DATA
                        .Where(o => o.MEDICINE_GROUP_ID == IMSys.DbConfig.HIS_RS.HIS_MEDICINE_GROUP.ID__DOC)
                        .Select(o => o.ID).ToList();
        }
    }
}
