using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaTranslate
{
    partial class SdaTranslateUpdate : EntityBase
    {
        public SdaTranslateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TRANSLATE>();
        }

        private BridgeDAO<SDA_TRANSLATE> bridgeDAO;

        public bool Update(SDA_TRANSLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_TRANSLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
