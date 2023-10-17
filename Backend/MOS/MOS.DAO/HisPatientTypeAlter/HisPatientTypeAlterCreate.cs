using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterCreate : EntityBase
    {
        public HisPatientTypeAlterCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALTER>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALTER> bridgeDAO;

        public bool Create(HIS_PATIENT_TYPE_ALTER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_TYPE_ALTER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
