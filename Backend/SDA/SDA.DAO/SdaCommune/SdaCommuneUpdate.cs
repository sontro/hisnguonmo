using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCommune
{
    partial class SdaCommuneUpdate : EntityBase
    {
        public SdaCommuneUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE>();
        }

        private BridgeDAO<SDA_COMMUNE> bridgeDAO;

        public bool Update(SDA_COMMUNE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_COMMUNE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
