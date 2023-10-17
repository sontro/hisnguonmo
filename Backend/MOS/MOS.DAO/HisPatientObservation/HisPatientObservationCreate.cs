using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationCreate : EntityBase
    {
        public HisPatientObservationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_OBSERVATION>();
        }

        private BridgeDAO<HIS_PATIENT_OBSERVATION> bridgeDAO;

        public bool Create(HIS_PATIENT_OBSERVATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
