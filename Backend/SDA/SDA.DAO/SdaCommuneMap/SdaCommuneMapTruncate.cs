using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCommuneMap
{
    partial class SdaCommuneMapTruncate : EntityBase
    {
        public SdaCommuneMapTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE_MAP>();
        }

        private BridgeDAO<SDA_COMMUNE_MAP> bridgeDAO;

        public bool Truncate(SDA_COMMUNE_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_COMMUNE_MAP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
