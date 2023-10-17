using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaCustomizeButton
{
    partial class SdaCustomizeButtonCheck : EntityBase
    {
        public SdaCustomizeButtonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_BUTTON>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_BUTTON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
