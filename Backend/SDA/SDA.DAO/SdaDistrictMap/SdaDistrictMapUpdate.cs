using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaDistrictMap
{
    partial class SdaDistrictMapUpdate : EntityBase
    {
        public SdaDistrictMapUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT_MAP>();
        }

        private BridgeDAO<SDA_DISTRICT_MAP> bridgeDAO;

        public bool Update(SDA_DISTRICT_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_DISTRICT_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
