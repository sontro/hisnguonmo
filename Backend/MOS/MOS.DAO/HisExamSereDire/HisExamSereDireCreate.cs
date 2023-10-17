using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireCreate : EntityBase
    {
        public HisExamSereDireCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXAM_SERE_DIRE>();
        }

        private BridgeDAO<HIS_EXAM_SERE_DIRE> bridgeDAO;

        public bool Create(HIS_EXAM_SERE_DIRE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXAM_SERE_DIRE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
