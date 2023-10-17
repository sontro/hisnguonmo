using MOS.LibraryHein.Bhyt.HeinRatio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class BhytRatioTypeMapConfig
    {
        //cau hinh de anh xa giua nhom dich vu BHYT voi nhom dich vu khi tinh ti le BHYT
        private static Dictionary<long, string> HEIN_SERVICE_TYPE__RATIO_TYPE = new Dictionary<long, string>()
        {
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CDHA, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__DVKTC, HeinRatioTypeCode.HIGH },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NGT, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NT, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_L, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_BN, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__KH, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__MAU, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__PTTT, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_UT, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_NDM, HeinRatioTypeCode.OTHER },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TDM, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TL, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TDCN, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VC, HeinRatioTypeCode.TRANSPORT },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_NDM, HeinRatioTypeCode.OTHER },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TDM, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TT, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TL, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__XN, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CPM, HeinRatioTypeCode.NORMAL },
            { IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TT, HeinRatioTypeCode.NORMAL }
        };

        public static string GetRatioType(long? heinServiceTypeId)
        {
            if (heinServiceTypeId.HasValue && HEIN_SERVICE_TYPE__RATIO_TYPE != null && HEIN_SERVICE_TYPE__RATIO_TYPE.ContainsKey(heinServiceTypeId.Value))
            {
                return HEIN_SERVICE_TYPE__RATIO_TYPE[heinServiceTypeId.Value];
            }
            return HeinRatioTypeCode.OTHER;//neu ko duoc gan thi mac dinh tra ve loai la dich vu "khac" ==> ko duoc BHYT chi tra
        }
    }
}
