using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccineType
{
    partial class HisVaccineTypeCreate : EntityBase
    {
        public HisVaccineTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINE_TYPE>();
        }

        private BridgeDAO<HIS_VACCINE_TYPE> bridgeDAO;

        public bool Create(HIS_VACCINE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
