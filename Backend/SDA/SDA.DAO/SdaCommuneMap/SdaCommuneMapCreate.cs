using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaCommuneMap
{
    partial class SdaCommuneMapCreate : EntityBase
    {
        public SdaCommuneMapCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_COMMUNE_MAP>();
        }

        private BridgeDAO<SDA_COMMUNE_MAP> bridgeDAO;

        public bool Create(SDA_COMMUNE_MAP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_COMMUNE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
