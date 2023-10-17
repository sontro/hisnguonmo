using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserUpdate : EntityBase
    {
        public SdaConfigAppUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP_USER>();
        }

        private BridgeDAO<SDA_CONFIG_APP_USER> bridgeDAO;

        public bool Update(SDA_CONFIG_APP_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_CONFIG_APP_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
