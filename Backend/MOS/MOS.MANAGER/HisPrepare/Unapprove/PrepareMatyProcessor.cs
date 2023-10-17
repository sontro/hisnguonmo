using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepare.Check;
using MOS.MANAGER.HisPrepareMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Unapprove
{
    class PrepareMatyProcessor : BusinessBase
    {
        private HisPrepareMatyUpdate hisPrepareMatyUpdate;

        internal PrepareMatyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMatyUpdate = new HisPrepareMatyUpdate(param);
        }

        internal bool Run(HIS_PREPARE prepare, List<HIS_PREPARE_MATY> prepareMatys)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(prepareMatys))
                {
                    Mapper.CreateMap<HIS_PREPARE_MATY, HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> listBefore = Mapper.Map<List<HIS_PREPARE_MATY>>(prepareMatys);
                    prepareMatys.ForEach(o => o.APPROVAL_AMOUNT = null);
                    if (!this.hisPrepareMatyUpdate.UpdateList(prepareMatys,listBefore))
                    {
                        throw new Exception("hisPrepareMatyUpdate. Ket thuc nghiep vu");
                    }
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
                this.hisPrepareMatyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
