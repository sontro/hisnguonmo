using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaGroupType
{
    partial class SdaGroupTypeCreate : EntityBase
    {
        public SdaGroupTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP_TYPE>();
        }

        private BridgeDAO<SDA_GROUP_TYPE> bridgeDAO;

        public bool Create(SDA_GROUP_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_GROUP_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
