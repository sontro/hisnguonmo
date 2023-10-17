using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHospitalizeReason
{
    partial class HisHospitalizeReasonCreate : EntityBase
    {
        public HisHospitalizeReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HOSPITALIZE_REASON>();
        }

        private BridgeDAO<HIS_HOSPITALIZE_REASON> bridgeDAO;

        public bool Create(HIS_HOSPITALIZE_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
