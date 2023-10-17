using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisInteractiveGrade
{
    partial class HisInteractiveGradeCheck : EntityBase
    {
        public HisInteractiveGradeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INTERACTIVE_GRADE>();
        }

        private BridgeDAO<HIS_INTERACTIVE_GRADE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
