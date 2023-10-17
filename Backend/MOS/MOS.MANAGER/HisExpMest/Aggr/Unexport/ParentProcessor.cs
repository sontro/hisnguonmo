using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Unexport
{
    class ParentProcessor: BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;

        internal ParentProcessor()
            : base()
        {
            this.Init();
        }

        internal ParentProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        public bool Run(HIS_EXP_MEST parent)
        {
            try
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(parent);

                //Su dung truong nay de huy thuc xuat chứ ko update trực tiếp trường trạng thái để
                //chạy trigger nhằm tránh trường hợp 2 người cùng thực hiện hủy thực xuất đồng thời trên 1 phiếu
                parent.IS_HTX = MOS.UTILITY.Constant.IS_TRUE;

                if (!this.hisExpMestUpdate.Update(parent, beforeUpdate))
                {
                    throw new Exception("Rollback du lieu");
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
