using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDiseaseRelation
{
    partial class HisDiseaseRelationCheck : EntityBase
    {
        public HisDiseaseRelationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_RELATION>();
        }

        private BridgeDAO<HIS_DISEASE_RELATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
