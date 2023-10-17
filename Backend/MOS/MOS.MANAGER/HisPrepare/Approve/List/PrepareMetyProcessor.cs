using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve.List
{
    class PrepareMetyProcessor : BusinessBase
    {
        private HisPrepareMetyUpdate hisPrepareMetyUpdate;

        internal PrepareMetyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMetyUpdate = new HisPrepareMetyUpdate(param);
        }

        internal bool Run(List<HIS_PREPARE> listPrepare)
        {
            bool result = false;
            try
            {
                List<HIS_PREPARE_METY> matys = new HisPrepareMetyGet().GetByPrepareIds(listPrepare.Select(s => s.ID).ToList());
                if (IsNotNullOrEmpty(matys))
                {
                    Mapper.CreateMap<HIS_PREPARE_METY, HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> listBefore = Mapper.Map<List<HIS_PREPARE_METY>>(matys);
                    matys.ForEach(o => o.APPROVAL_AMOUNT = o.REQ_AMOUNT);
                    if (!this.hisPrepareMetyUpdate.UpdateList(matys, listBefore))
                    {
                        throw new Exception("hisPrepareMayUpdate. Ket thuc nghiep vu");
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
                this.hisPrepareMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
