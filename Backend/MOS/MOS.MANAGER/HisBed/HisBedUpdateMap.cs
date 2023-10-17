using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBed
{
    class HisBedUpdateMap : BusinessBase
    {
        private HisBedUpdate hisBedUpdate;

        internal HisBedUpdateMap()
            : base()
        {
            this.hisBedUpdate = new HisBedUpdate(param);
        }

        internal HisBedUpdateMap(CommonParam param)
            : base(param)
        {
            this.hisBedUpdate = new HisBedUpdate(param);
        }

        internal bool Run(List<HIS_BED> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedCheck checker = new HisBedCheck(param);
                List<HIS_BED> listRaw = new List<HIS_BED>();
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_BED, HIS_BED>();
                    List<HIS_BED> listBefore = Mapper.Map<List<HIS_BED>>(listRaw);
                    foreach (HIS_BED bed in listData)
                    {
                        HIS_BED old = listRaw.FirstOrDefault(o => o.ID == bed.ID);
                        old.X = bed.X;
                        old.Y = bed.Y;
                    }

                    if (!this.hisBedUpdate.UpdateList(listRaw, listBefore, true))
                    {
                        throw new Exception("hisBedUpdate. ket thuc nghiep vu");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
