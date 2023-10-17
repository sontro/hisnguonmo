using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHealthExamRank
{
    partial class HisHealthExamRankCreate : EntityBase
    {
        public HisHealthExamRankCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEALTH_EXAM_RANK>();
        }

        private BridgeDAO<HIS_HEALTH_EXAM_RANK> bridgeDAO;

        public bool Create(HIS_HEALTH_EXAM_RANK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HEALTH_EXAM_RANK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
