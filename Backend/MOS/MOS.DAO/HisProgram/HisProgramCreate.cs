using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisProgram
{
    partial class HisProgramCreate : EntityBase
    {
        public HisProgramCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROGRAM>();
        }

        private BridgeDAO<HIS_PROGRAM> bridgeDAO;

        public bool Create(HIS_PROGRAM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PROGRAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
