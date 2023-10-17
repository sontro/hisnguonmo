using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaSql
{
    partial class SdaSqlCreate : EntityBase
    {
        public SdaSqlCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL>();
        }

        private BridgeDAO<SDA_SQL> bridgeDAO;

        public bool Create(SDA_SQL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_SQL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
