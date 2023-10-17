using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdService
{
    partial class HisIcdServiceCreate : EntityBase
    {
        public HisIcdServiceCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_SERVICE>();
        }

        private BridgeDAO<HIS_ICD_SERVICE> bridgeDAO;

        public bool Create(HIS_ICD_SERVICE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ICD_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
