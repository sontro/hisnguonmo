using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeAllow
{
    partial class HisPatientTypeAllowCreate : EntityBase
    {
        public HisPatientTypeAllowCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALLOW>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALLOW> bridgeDAO;

        public bool Create(HIS_PATIENT_TYPE_ALLOW data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
