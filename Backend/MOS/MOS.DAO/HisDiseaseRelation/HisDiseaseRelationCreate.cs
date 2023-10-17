using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDiseaseRelation
{
    partial class HisDiseaseRelationCreate : EntityBase
    {
        public HisDiseaseRelationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISEASE_RELATION>();
        }

        private BridgeDAO<HIS_DISEASE_RELATION> bridgeDAO;

        public bool Create(HIS_DISEASE_RELATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DISEASE_RELATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
