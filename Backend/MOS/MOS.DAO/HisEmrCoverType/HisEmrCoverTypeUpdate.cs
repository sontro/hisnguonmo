using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrCoverType
{
    partial class HisEmrCoverTypeUpdate : EntityBase
    {
        public HisEmrCoverTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_TYPE>();
        }

        private BridgeDAO<HIS_EMR_COVER_TYPE> bridgeDAO;

        public bool Update(HIS_EMR_COVER_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EMR_COVER_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
