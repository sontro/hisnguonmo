using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebate
{
    partial class HisDebateCreate : EntityBase
    {
        public HisDebateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE>();
        }

        private BridgeDAO<HIS_DEBATE> bridgeDAO;

        public bool Create(HIS_DEBATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
