using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaLanguage
{
    partial class SdaLanguageCreate : EntityBase
    {
        public SdaLanguageCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LANGUAGE>();
        }

        private BridgeDAO<SDA_LANGUAGE> bridgeDAO;

        public bool Create(SDA_LANGUAGE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_LANGUAGE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
