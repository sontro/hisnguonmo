using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaDistrict
{
    partial class SdaDistrictCreate : EntityBase
    {
        public SdaDistrictCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DISTRICT>();
        }

        private BridgeDAO<SDA_DISTRICT> bridgeDAO;

        public bool Create(SDA_DISTRICT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_DISTRICT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
