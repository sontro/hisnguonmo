using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCause
{
    partial class HisDeathCauseCreate : EntityBase
    {
        public HisDeathCauseCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CAUSE>();
        }

        private BridgeDAO<HIS_DEATH_CAUSE> bridgeDAO;

        public bool Create(HIS_DEATH_CAUSE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEATH_CAUSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
