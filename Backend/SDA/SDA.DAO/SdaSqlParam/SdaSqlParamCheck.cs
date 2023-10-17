using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamCheck : EntityBase
    {
        public SdaSqlParamCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL_PARAM>();
        }

        private BridgeDAO<SDA_SQL_PARAM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
