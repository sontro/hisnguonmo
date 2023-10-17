using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest
{
    class HisImpMestContanst
    {
        internal static List<long> TYPE_MUST_CREATE_PACKAGE_IDS = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DK,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KHAC,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KK,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC,
                IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HM
        };

        internal static List<long> TYPE_MOBA_IDS = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HPTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BTL
        };

        internal static List<long> TYPE_MOBA_PRES_IDS = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL
        };

        internal static List<long> TYPE_CHMS_IDS = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BCS,
            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL
        };
    }
}
