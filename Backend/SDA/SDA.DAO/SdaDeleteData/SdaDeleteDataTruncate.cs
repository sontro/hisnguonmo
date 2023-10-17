using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaDeleteData
{
    partial class SdaDeleteDataTruncate : EntityBase
    {
        public SdaDeleteDataTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DELETE_DATA>();
        }

        private BridgeDAO<SDA_DELETE_DATA> bridgeDAO;

        public bool Truncate(SDA_DELETE_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_DELETE_DATA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
