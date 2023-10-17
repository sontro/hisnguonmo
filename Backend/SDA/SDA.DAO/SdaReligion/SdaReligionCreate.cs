using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaReligion
{
    partial class SdaReligionCreate : EntityBase
    {
        public SdaReligionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_RELIGION>();
        }

        private BridgeDAO<SDA_RELIGION> bridgeDAO;

        public bool Create(SDA_RELIGION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_RELIGION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
