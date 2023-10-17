using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientType
{
    partial class HisPatientTypeCreate : EntityBase
    {
        public HisPatientTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE> bridgeDAO;

        public bool Create(HIS_PATIENT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
