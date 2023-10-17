using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientProgram
{
    partial class HisPatientProgramCreate : EntityBase
    {
        public HisPatientProgramCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_PROGRAM>();
        }

        private BridgeDAO<HIS_PATIENT_PROGRAM> bridgeDAO;

        public bool Create(HIS_PATIENT_PROGRAM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PATIENT_PROGRAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
