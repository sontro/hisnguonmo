using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaCustomizeUi
{
    partial class SdaCustomizeUiCreate : EntityBase
    {
        public SdaCustomizeUiCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_UI>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_UI> bridgeDAO;

        public bool Create(SDA_CUSTOMIZE_UI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_CUSTOMIZE_UI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
