using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTextLib
{
    partial class HisTextLibUpdate : EntityBase
    {
        public HisTextLibUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEXT_LIB>();
        }

        private BridgeDAO<HIS_TEXT_LIB> bridgeDAO;

        public bool Update(HIS_TEXT_LIB data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TEXT_LIB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
