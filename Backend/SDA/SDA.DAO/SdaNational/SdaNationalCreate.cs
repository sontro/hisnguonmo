using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaNational
{
    partial class SdaNationalCreate : EntityBase
    {
        public SdaNationalCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NATIONAL>();
        }

        private BridgeDAO<SDA_NATIONAL> bridgeDAO;

        public bool Create(SDA_NATIONAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_NATIONAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
