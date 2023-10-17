using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaHideControl
{
    partial class SdaHideControlCheck : EntityBase
    {
        public SdaHideControlCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_HIDE_CONTROL>();
        }

        private BridgeDAO<SDA_HIDE_CONTROL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
