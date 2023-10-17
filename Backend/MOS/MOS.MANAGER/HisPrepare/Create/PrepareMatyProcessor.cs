using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Create
{
    class PrepareMatyProcessor : BusinessBase
    {
        private HisPrepareMatyCreate hisPrepareMatyCreate;

        internal PrepareMatyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMatyCreate = new HisPrepareMatyCreate(param);
        }

        internal bool Run(HIS_PREPARE hisPrepare, List<HIS_PREPARE_MATY> prepareMatys, ref List<HIS_PREPARE_MATY> materials)
        {
            bool result = false;
            try
            {
                if (hisPrepare != null && IsNotNullOrEmpty(prepareMatys))
                {
                    prepareMatys.ForEach(o =>
                    {
                        o.PREPARE_ID = hisPrepare.ID;
                        o.APPROVAL_AMOUNT = null;
                        o.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                    });

                    if (!this.hisPrepareMatyCreate.CreateList(prepareMatys))
                    {
                        throw new Exception("hisPrepareMatyCreate. Ket thuc nghiep vu");
                    }

                    materials = prepareMatys;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareMatyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
