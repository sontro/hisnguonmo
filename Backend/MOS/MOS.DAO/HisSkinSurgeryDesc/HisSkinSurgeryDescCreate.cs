using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescCreate : EntityBase
    {
        public HisSkinSurgeryDescCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SKIN_SURGERY_DESC>();
        }

        private BridgeDAO<HIS_SKIN_SURGERY_DESC> bridgeDAO;

        public bool Create(HIS_SKIN_SURGERY_DESC data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
