using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoCreate : EntityBase
    {
        public HisSevereIllnessInfoCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SEVERE_ILLNESS_INFO>();
        }

        private BridgeDAO<HIS_SEVERE_ILLNESS_INFO> bridgeDAO;

        public bool Create(HIS_SEVERE_ILLNESS_INFO data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
