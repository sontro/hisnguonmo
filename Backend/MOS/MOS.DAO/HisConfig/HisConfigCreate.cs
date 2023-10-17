using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisConfig
{
    partial class HisConfigCreate : EntityBase
    {
        public HisConfigCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG>();
        }

        private BridgeDAO<HIS_CONFIG> bridgeDAO;

        public bool Create(HIS_CONFIG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
