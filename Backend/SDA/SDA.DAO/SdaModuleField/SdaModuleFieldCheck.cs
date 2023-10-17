using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaModuleField
{
    partial class SdaModuleFieldCheck : EntityBase
    {
        public SdaModuleFieldCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_MODULE_FIELD>();
        }

        private BridgeDAO<SDA_MODULE_FIELD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
