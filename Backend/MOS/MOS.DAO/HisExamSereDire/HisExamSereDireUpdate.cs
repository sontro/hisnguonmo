using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireUpdate : EntityBase
    {
        public HisExamSereDireUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERE_DIRE>();
        }

        private BridgeDAO<HIS_EXAM_SERE_DIRE> bridgeDAO;

        public bool Update(HIS_EXAM_SERE_DIRE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
