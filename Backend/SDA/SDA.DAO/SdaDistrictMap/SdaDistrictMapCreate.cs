using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaDistrictMap
{
    partial class SdaDistrictMapCreate : EntityBase
    {
        public SdaDistrictMapCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT_MAP>();
        }

        private BridgeDAO<SDA_DISTRICT_MAP> bridgeDAO;

        public bool Create(SDA_DISTRICT_MAP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_DISTRICT_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
