using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common
{
    class HisExpMestConstant
    {
        //Cac loai xuat cho phep duyet (cac loai xuat khac tao lenh truc tiep --> ko cho phep duyet)
        internal static List<long> HAS_REQ_EXP_MEST_TYPE_IDs = new List<long> {
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__HPKP,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM
        };

        //cac loai xuat tao lenh luc yeu cau khong can check chi tiet
        internal static List<long> NOT_HAS_REQ_EXP_MEST_TYPE_IDs = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__KHAC,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__TNCC,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VITAMINA,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VACCIN,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__TEST
        };


        internal static List<long> ALLOW_UPDATE_DETAIL_STT_IDs = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
        };

        internal static List<long> CHMS_EXP_MEST_TYPE_IDs = new List<long> {
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL
        };
    }
}
