using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaSql
{
    partial class SdaSqlCheck : EntityBase
    {
        public SdaSqlCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL>();
        }

        private BridgeDAO<SDA_SQL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
