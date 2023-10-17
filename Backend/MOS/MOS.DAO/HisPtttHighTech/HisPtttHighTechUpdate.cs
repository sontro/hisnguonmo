using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttHighTech
{
    partial class HisPtttHighTechUpdate : EntityBase
    {
        public HisPtttHighTechUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_HIGH_TECH>();
        }

        private BridgeDAO<HIS_PTTT_HIGH_TECH> bridgeDAO;

        public bool Update(HIS_PTTT_HIGH_TECH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_HIGH_TECH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
