using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaDistrict
{
    partial class SdaDistrictUpdate : EntityBase
    {
        public SdaDistrictUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT>();
        }

        private BridgeDAO<SDA_DISTRICT> bridgeDAO;

        public bool Update(SDA_DISTRICT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_DISTRICT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
