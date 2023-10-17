using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaLanguage
{
    partial class SdaLanguageCheck : EntityBase
    {
        public SdaLanguageCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LANGUAGE>();
        }

        private BridgeDAO<SDA_LANGUAGE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
