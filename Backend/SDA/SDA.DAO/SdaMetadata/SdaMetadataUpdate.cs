using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaMetadata
{
    partial class SdaMetadataUpdate : EntityBase
    {
        public SdaMetadataUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_METADATA>();
        }

        private BridgeDAO<SDA_METADATA> bridgeDAO;

        public bool Update(SDA_METADATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_METADATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
