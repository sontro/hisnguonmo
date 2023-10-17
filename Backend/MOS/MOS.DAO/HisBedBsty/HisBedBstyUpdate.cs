using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBedBsty
{
    partial class HisBedBstyUpdate : EntityBase
    {
        public HisBedBstyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED_BSTY>();
        }

        private BridgeDAO<HIS_BED_BSTY> bridgeDAO;

        public bool Update(HIS_BED_BSTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BED_BSTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
