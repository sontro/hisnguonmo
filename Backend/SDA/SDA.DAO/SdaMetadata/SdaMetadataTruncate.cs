using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaMetadata
{
    partial class SdaMetadataTruncate : EntityBase
    {
        public SdaMetadataTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_METADATA>();
        }

        private BridgeDAO<SDA_METADATA> bridgeDAO;

        public bool Truncate(SDA_METADATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_METADATA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
