using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaCommune
{
    partial class SdaCommuneCreate : EntityBase
    {
        public SdaCommuneCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE>();
        }

        private BridgeDAO<SDA_COMMUNE> bridgeDAO;

        public bool Create(SDA_COMMUNE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_COMMUNE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
