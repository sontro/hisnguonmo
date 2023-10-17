using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateTemp
{
    partial class HisDebateTempCreate : EntityBase
    {
        public HisDebateTempCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TEMP>();
        }

        private BridgeDAO<HIS_DEBATE_TEMP> bridgeDAO;

        public bool Create(HIS_DEBATE_TEMP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_TEMP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
