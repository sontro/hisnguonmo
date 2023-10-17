using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaGroupType
{
    partial class SdaGroupTypeCheck : EntityBase
    {
        public SdaGroupTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP_TYPE>();
        }

        private BridgeDAO<SDA_GROUP_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
