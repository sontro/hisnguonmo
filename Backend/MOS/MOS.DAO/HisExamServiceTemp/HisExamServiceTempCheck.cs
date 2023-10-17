using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExamServiceTemp
{
    partial class HisExamServiceTempCheck : EntityBase
    {
        public HisExamServiceTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERVICE_TEMP>();
        }

        private BridgeDAO<HIS_EXAM_SERVICE_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
