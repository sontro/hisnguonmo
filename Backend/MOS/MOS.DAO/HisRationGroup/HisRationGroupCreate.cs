using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRationGroup
{
    partial class HisRationGroupCreate : EntityBase
    {
        public HisRationGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_GROUP>();
        }

        private BridgeDAO<HIS_RATION_GROUP> bridgeDAO;

        public bool Create(HIS_RATION_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_RATION_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
