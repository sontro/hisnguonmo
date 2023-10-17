using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathWithin
{
    partial class HisDeathWithinCreate : EntityBase
    {
        public HisDeathWithinCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_WITHIN>();
        }

        private BridgeDAO<HIS_DEATH_WITHIN> bridgeDAO;

        public bool Create(HIS_DEATH_WITHIN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEATH_WITHIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
