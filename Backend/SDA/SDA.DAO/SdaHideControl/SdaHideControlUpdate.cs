using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaHideControl
{
    partial class SdaHideControlUpdate : EntityBase
    {
        public SdaHideControlUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_HIDE_CONTROL>();
        }

        private BridgeDAO<SDA_HIDE_CONTROL> bridgeDAO;

        public bool Update(SDA_HIDE_CONTROL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_HIDE_CONTROL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
