using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAllergenic
{
    partial class HisAllergenicCreate : EntityBase
    {
        public HisAllergenicCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALLERGENIC>();
        }

        private BridgeDAO<HIS_ALLERGENIC> bridgeDAO;

        public bool Create(HIS_ALLERGENIC data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ALLERGENIC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
