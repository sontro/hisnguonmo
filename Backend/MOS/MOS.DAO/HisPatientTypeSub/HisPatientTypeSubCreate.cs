using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeSub
{
    partial class HisPatientTypeSubCreate : EntityBase
    {
        public HisPatientTypeSubCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_SUB>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_SUB> bridgeDAO;

        public bool Create(HIS_PATIENT_TYPE_SUB data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_TYPE_SUB> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
