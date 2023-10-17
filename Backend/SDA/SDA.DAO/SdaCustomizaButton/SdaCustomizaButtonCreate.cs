using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizeButton
{
    partial class SdaCustomizeButtonCreate : EntityBase
    {
        public SdaCustomizeButtonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_BUTTON>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_BUTTON> bridgeDAO;

        public bool Create(SDA_CUSTOMIZE_BUTTON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_CUSTOMIZE_BUTTON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
