using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaEventLog
{
    partial class SdaEventLogCreate : EntityBase
    {
        public SdaEventLogCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_EVENT_LOG>();
        }

        private BridgeDAO<SDA_EVENT_LOG> bridgeDAO;

        public bool Create(SDA_EVENT_LOG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_EVENT_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
