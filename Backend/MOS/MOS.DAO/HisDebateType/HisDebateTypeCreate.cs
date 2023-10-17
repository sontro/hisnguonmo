using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateType
{
    partial class HisDebateTypeCreate : EntityBase
    {
        public HisDebateTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TYPE>();
        }

        private BridgeDAO<HIS_DEBATE_TYPE> bridgeDAO;

        public bool Create(HIS_DEBATE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
