using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaReligion
{
    partial class SdaReligionCheck : EntityBase
    {
        public SdaReligionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_RELIGION>();
        }

        private BridgeDAO<SDA_RELIGION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
