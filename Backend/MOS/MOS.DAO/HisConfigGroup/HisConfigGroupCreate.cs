using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisConfigGroup
{
    partial class HisConfigGroupCreate : EntityBase
    {
        public HisConfigGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG_GROUP>();
        }

        private BridgeDAO<HIS_CONFIG_GROUP> bridgeDAO;

        public bool Create(HIS_CONFIG_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CONFIG_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
