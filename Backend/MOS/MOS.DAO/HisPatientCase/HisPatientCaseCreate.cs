using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientCase
{
    partial class HisPatientCaseCreate : EntityBase
    {
        public HisPatientCaseCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CASE>();
        }

        private BridgeDAO<HIS_PATIENT_CASE> bridgeDAO;

        public bool Create(HIS_PATIENT_CASE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_CASE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
