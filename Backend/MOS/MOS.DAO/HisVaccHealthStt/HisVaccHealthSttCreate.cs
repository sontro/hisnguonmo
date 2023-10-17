using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccHealthStt
{
    partial class HisVaccHealthSttCreate : EntityBase
    {
        public HisVaccHealthSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACC_HEALTH_STT>();
        }

        private BridgeDAO<HIS_VACC_HEALTH_STT> bridgeDAO;

        public bool Create(HIS_VACC_HEALTH_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACC_HEALTH_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
