using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfig
{
    partial class SdaConfigCreate : EntityBase
    {
        public SdaConfigCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG>();
        }

        private BridgeDAO<SDA_CONFIG> bridgeDAO;

        public bool Create(SDA_CONFIG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
