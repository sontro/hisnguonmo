using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInteractiveGrade
{
    partial class HisInteractiveGradeUpdate : EntityBase
    {
        public HisInteractiveGradeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INTERACTIVE_GRADE>();
        }

        private BridgeDAO<HIS_INTERACTIVE_GRADE> bridgeDAO;

        public bool Update(HIS_INTERACTIVE_GRADE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
