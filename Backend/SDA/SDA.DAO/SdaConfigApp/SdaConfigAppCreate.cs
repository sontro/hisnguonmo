using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfigApp
{
    partial class SdaConfigAppCreate : EntityBase
    {
        public SdaConfigAppCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP>();
        }

        private BridgeDAO<SDA_CONFIG_APP> bridgeDAO;

        public bool Create(SDA_CONFIG_APP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_CONFIG_APP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
