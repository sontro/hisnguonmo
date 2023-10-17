using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaLanguage
{
    partial class SdaLanguageTruncate : EntityBase
    {
        public SdaLanguageTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_LANGUAGE>();
        }

        private BridgeDAO<SDA_LANGUAGE> bridgeDAO;

        public bool Truncate(SDA_LANGUAGE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_LANGUAGE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
