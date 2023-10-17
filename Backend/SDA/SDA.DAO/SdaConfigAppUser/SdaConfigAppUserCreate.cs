using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserCreate : EntityBase
    {
        public SdaConfigAppUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP_USER>();
        }

        private BridgeDAO<SDA_CONFIG_APP_USER> bridgeDAO;

        public bool Create(SDA_CONFIG_APP_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_CONFIG_APP_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
