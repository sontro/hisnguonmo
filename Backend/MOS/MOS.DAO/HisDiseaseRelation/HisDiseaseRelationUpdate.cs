using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDiseaseRelation
{
    partial class HisDiseaseRelationUpdate : EntityBase
    {
        public HisDiseaseRelationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_RELATION>();
        }

        private BridgeDAO<HIS_DISEASE_RELATION> bridgeDAO;

        public bool Update(HIS_DISEASE_RELATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DISEASE_RELATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
