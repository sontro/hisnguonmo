using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaNotify
{
    partial class SdaNotifyCreate : EntityBase
    {
        public SdaNotifyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NOTIFY>();
        }

        private BridgeDAO<SDA_NOTIFY> bridgeDAO;

        public bool Create(SDA_NOTIFY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_NOTIFY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
