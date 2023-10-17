using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFilmSize
{
    partial class HisFilmSizeCheck : EntityBase
    {
        public HisFilmSizeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILM_SIZE>();
        }

        private BridgeDAO<HIS_FILM_SIZE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
