using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBodyPart
{
    partial class HisBodyPartUpdate : EntityBase
    {
        public HisBodyPartUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BODY_PART>();
        }

        private BridgeDAO<HIS_BODY_PART> bridgeDAO;

        public bool Update(HIS_BODY_PART data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BODY_PART> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
