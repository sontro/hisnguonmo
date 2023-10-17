using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaProvinceMap
{
    partial class SdaProvinceMapUpdate : EntityBase
    {
        public SdaProvinceMapUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_PROVINCE_MAP>();
        }

        private BridgeDAO<SDA_PROVINCE_MAP> bridgeDAO;

        public bool Update(SDA_PROVINCE_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_PROVINCE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
