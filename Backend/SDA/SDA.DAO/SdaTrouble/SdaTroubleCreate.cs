using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaTrouble
{
    partial class SdaTroubleCreate : EntityBase
    {
        public SdaTroubleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TROUBLE>();
        }

        private BridgeDAO<SDA_TROUBLE> bridgeDAO;

        public bool Create(SDA_TROUBLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_TROUBLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
