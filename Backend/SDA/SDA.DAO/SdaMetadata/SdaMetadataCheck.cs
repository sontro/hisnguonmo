using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaMetadata
{
    partial class SdaMetadataCheck : EntityBase
    {
        public SdaMetadataCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_METADATA>();
        }

        private BridgeDAO<SDA_METADATA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
