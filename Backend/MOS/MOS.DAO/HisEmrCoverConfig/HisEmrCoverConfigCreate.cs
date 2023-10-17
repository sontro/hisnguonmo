using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigCreate : EntityBase
    {
        public HisEmrCoverConfigCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_CONFIG>();
        }

        private BridgeDAO<HIS_EMR_COVER_CONFIG> bridgeDAO;

        public bool Create(HIS_EMR_COVER_CONFIG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
