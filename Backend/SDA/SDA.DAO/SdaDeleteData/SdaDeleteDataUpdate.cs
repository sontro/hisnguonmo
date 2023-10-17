using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaDeleteData
{
    partial class SdaDeleteDataUpdate : EntityBase
    {
        public SdaDeleteDataUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DELETE_DATA>();
        }

        private BridgeDAO<SDA_DELETE_DATA> bridgeDAO;

        public bool Update(SDA_DELETE_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_DELETE_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
