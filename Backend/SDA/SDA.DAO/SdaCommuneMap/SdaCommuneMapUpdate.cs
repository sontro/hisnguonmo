using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCommuneMap
{
    partial class SdaCommuneMapUpdate : EntityBase
    {
        public SdaCommuneMapUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE_MAP>();
        }

        private BridgeDAO<SDA_COMMUNE_MAP> bridgeDAO;

        public bool Update(SDA_COMMUNE_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_COMMUNE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
