using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaModuleField
{
    partial class SdaModuleFieldCreate : EntityBase
    {
        public SdaModuleFieldCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_MODULE_FIELD>();
        }

        private BridgeDAO<SDA_MODULE_FIELD> bridgeDAO;

        public bool Create(SDA_MODULE_FIELD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_MODULE_FIELD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
