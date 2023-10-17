using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdCm
{
    partial class HisIcdCmCreate : EntityBase
    {
        public HisIcdCmCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ICD_CM>();
        }

        private BridgeDAO<HIS_ICD_CM> bridgeDAO;

        public bool Create(HIS_ICD_CM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ICD_CM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
