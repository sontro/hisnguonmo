using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisIcd
{
    partial class HisIcdCreate : EntityBase
    {
        public HisIcdCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD>();
        }

        private BridgeDAO<HIS_ICD> bridgeDAO;

        public bool Create(HIS_ICD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ICD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
