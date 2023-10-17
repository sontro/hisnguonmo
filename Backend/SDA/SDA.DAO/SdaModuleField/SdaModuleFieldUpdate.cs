using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaModuleField
{
    partial class SdaModuleFieldUpdate : EntityBase
    {
        public SdaModuleFieldUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_MODULE_FIELD>();
        }

        private BridgeDAO<SDA_MODULE_FIELD> bridgeDAO;

        public bool Update(SDA_MODULE_FIELD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_MODULE_FIELD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
