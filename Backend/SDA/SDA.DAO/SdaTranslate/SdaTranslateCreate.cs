using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaTranslate
{
    partial class SdaTranslateCreate : EntityBase
    {
        public SdaTranslateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TRANSLATE>();
        }

        private BridgeDAO<SDA_TRANSLATE> bridgeDAO;

        public bool Create(SDA_TRANSLATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_TRANSLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
