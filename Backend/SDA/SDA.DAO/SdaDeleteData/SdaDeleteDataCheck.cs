using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaDeleteData
{
    partial class SdaDeleteDataCheck : EntityBase
    {
        public SdaDeleteDataCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DELETE_DATA>();
        }

        private BridgeDAO<SDA_DELETE_DATA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
