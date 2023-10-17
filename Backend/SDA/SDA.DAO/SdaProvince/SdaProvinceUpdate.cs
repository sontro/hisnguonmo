using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaProvince
{
    partial class SdaProvinceUpdate : EntityBase
    {
        public SdaProvinceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE>();
        }

        private BridgeDAO<SDA_PROVINCE> bridgeDAO;

        public bool Update(SDA_PROVINCE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_PROVINCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
