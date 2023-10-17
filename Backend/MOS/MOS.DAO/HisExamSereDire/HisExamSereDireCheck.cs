using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireCheck : EntityBase
    {
        public HisExamSereDireCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERE_DIRE>();
        }

        private BridgeDAO<HIS_EXAM_SERE_DIRE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
