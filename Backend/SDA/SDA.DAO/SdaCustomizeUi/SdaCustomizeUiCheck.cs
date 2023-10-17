using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaCustomizeUi
{
    partial class SdaCustomizeUiCheck : EntityBase
    {
        public SdaCustomizeUiCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_UI>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_UI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
