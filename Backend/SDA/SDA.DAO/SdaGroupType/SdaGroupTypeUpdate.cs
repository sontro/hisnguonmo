using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaGroupType
{
    partial class SdaGroupTypeUpdate : EntityBase
    {
        public SdaGroupTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP_TYPE>();
        }

        private BridgeDAO<SDA_GROUP_TYPE> bridgeDAO;

        public bool Update(SDA_GROUP_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_GROUP_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
