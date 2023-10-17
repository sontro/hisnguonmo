using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaTrouble
{
    partial class SdaTroubleCheck : EntityBase
    {
        public SdaTroubleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TROUBLE>();
        }

        private BridgeDAO<SDA_TROUBLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
