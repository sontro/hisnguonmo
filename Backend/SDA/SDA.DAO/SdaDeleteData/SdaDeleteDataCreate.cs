using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaDeleteData
{
    partial class SdaDeleteDataCreate : EntityBase
    {
        public SdaDeleteDataCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_DELETE_DATA>();
        }

        private BridgeDAO<SDA_DELETE_DATA> bridgeDAO;

        public bool Create(SDA_DELETE_DATA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_DELETE_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
