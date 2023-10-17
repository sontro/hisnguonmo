using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatient
{
    partial class HisPatientCreate : EntityBase
    {
        public HisPatientCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT>();
        }

        private BridgeDAO<HIS_PATIENT> bridgeDAO;

        public bool Create(HIS_PATIENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
