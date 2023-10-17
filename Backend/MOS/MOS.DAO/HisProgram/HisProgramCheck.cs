using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisProgram
{
    partial class HisProgramCheck : EntityBase
    {
        public HisProgramCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PROGRAM>();
        }

        private BridgeDAO<HIS_PROGRAM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
