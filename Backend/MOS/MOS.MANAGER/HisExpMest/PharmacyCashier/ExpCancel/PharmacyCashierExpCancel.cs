using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Delete;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Unapprove;
using MOS.MANAGER.HisExpMest.Common.Unexport;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.ExpCancel
{
    class PharmacyCashierExpCancel: BusinessBase
    {
        HisExpMestUnexport hisExpMestUnexport;
        HisExpMestUnapprove hisExpMestUnapprove;
        HisExpMestTruncate hisExpMestTruncate;

        internal PharmacyCashierExpCancel()
            : base()
        {
            this.Init();
        }

        internal PharmacyCashierExpCancel(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUnexport = new HisExpMestUnexport(param);
            this.hisExpMestUnapprove = new HisExpMestUnapprove(param);
            this.hisExpMestTruncate = new HisExpMestTruncate(param);
        }

        internal bool Run(PharmacyCashierExpCancelSDO sdo)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                bool valid = true;
                PharmacyCashierExpCancelCheck checker = new PharmacyCashierExpCancelCheck(param);
                valid = valid && checker.IsValidExpMest(sdo, ref expMest);

                if (valid)
                {
                    HisExpMestSDO expSdo = new HisExpMestSDO();
                    expSdo.ExpMestId = expMest.ID;
                    expSdo.ReqRoomId = sdo.WorkingRoomId;

                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        this.hisExpMestUnexport.Run(expSdo, ref expMest);
                    }
                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                    {
                        this.hisExpMestUnapprove.Run(expSdo, ref  expMest);
                    }
                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                    {
                        result = this.hisExpMestTruncate.Truncate(expSdo, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
