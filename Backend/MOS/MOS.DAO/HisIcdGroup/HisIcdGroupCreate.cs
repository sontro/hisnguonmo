using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdGroup
{
    partial class HisIcdGroupCreate : EntityBase
    {
        public HisIcdGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_GROUP>();
        }

        private BridgeDAO<HIS_ICD_GROUP> bridgeDAO;

        public bool Create(HIS_ICD_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ICD_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
