using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaMetadata
{
    partial class SdaMetadataCreate : EntityBase
    {
        public SdaMetadataCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_METADATA>();
        }

        private BridgeDAO<SDA_METADATA> bridgeDAO;

        public bool Create(SDA_METADATA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_METADATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
