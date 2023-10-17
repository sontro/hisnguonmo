using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSaroExro
{
    partial class HisSaroExroCheck : EntityBase
    {
        public HisSaroExroCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SARO_EXRO>();
        }

        private BridgeDAO<HIS_SARO_EXRO> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
