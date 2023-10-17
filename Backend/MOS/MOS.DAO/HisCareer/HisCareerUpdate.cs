using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareer
{
    partial class HisCareerUpdate : EntityBase
    {
        public HisCareerUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CAREER>();
        }

        private BridgeDAO<HIS_CAREER> bridgeDAO;

        public bool Update(HIS_CAREER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CAREER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
