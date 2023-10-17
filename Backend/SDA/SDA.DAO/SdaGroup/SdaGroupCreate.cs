using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaGroup
{
    partial class SdaGroupCreate : EntityBase
    {
        public SdaGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP>();
        }

        private BridgeDAO<SDA_GROUP> bridgeDAO;

        public bool Create(SDA_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
