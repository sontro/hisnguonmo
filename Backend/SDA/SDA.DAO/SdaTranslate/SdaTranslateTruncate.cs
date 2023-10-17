using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaTranslate
{
    partial class SdaTranslateTruncate : EntityBase
    {
        public SdaTranslateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TRANSLATE>();
        }

        private BridgeDAO<SDA_TRANSLATE> bridgeDAO;

        public bool Truncate(SDA_TRANSLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_TRANSLATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
