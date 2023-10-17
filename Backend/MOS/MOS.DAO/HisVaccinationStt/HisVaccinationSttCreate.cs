using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationStt
{
    partial class HisVaccinationSttCreate : EntityBase
    {
        public HisVaccinationSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_STT>();
        }

        private BridgeDAO<HIS_VACCINATION_STT> bridgeDAO;

        public bool Create(HIS_VACCINATION_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
