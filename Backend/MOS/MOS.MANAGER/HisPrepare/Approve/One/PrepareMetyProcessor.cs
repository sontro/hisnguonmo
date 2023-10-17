using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve.One
{
    class PrepareMetyProcessor : BusinessBase
    {
        private HisPrepareMetyUpdate hisPrepareMetyUpdate;

        internal PrepareMetyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMetyUpdate = new HisPrepareMetyUpdate(param);
        }

        internal bool Run(List<HisPrepareMetySDO> matySDOs, List<HIS_PREPARE_METY> prepareMetys)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(matySDOs))
                {
                    Mapper.CreateMap<HIS_PREPARE_METY, HIS_PREPARE_METY>();
                    List<HIS_PREPARE_METY> listBefore = Mapper.Map<List<HIS_PREPARE_METY>>(prepareMetys);
                    foreach (HisPrepareMetySDO sdo in matySDOs)
                    {
                        HIS_PREPARE_METY exist = prepareMetys.FirstOrDefault(o => o.ID == sdo.PrepareMetyId);
                        if (exist == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("sdo.PrepareMetyId invalid: " + sdo.PrepareMetyId);
                        }
                        exist.APPROVAL_AMOUNT = sdo.ApproveAmount;
                    }

                    if (!this.hisPrepareMetyUpdate.UpdateList(prepareMetys, listBefore))
                    {
                        throw new Exception("hisPrepareMetyUpdate. Ket thuc nghiep vu");
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
