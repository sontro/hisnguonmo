using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaEthnic
{
    partial class SdaEthnicCreate : EntityBase
    {
        public SdaEthnicCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_ETHNIC>();
        }

        private BridgeDAO<SDA_ETHNIC> bridgeDAO;

        public bool Create(SDA_ETHNIC data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_ETHNIC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
