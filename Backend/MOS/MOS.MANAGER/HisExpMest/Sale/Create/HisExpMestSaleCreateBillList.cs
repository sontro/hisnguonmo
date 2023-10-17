using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class HisExpMestSaleCreateBillList : BusinessBase
    {
        private List<HisExpMestSaleCreateListSdo> processors;

        internal HisExpMestSaleCreateBillList()
            : base()
        {
            this.Init();
        }

        internal HisExpMestSaleCreateBillList(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.processors = new List<HisExpMestSaleCreateListSdo>();
        }

        internal bool Run(List<HisExpMestSaleListSDO> data, ref List<HisExpMestSaleListResultSDO> resultData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    resultData = new List<HisExpMestSaleListResultSDO>();

                    foreach (HisExpMestSaleListSDO sdo in data)
                    {
                        HisExpMestSaleListResultSDO rs = null;
                        HisExpMestSaleCreateListSdo processor = new HisExpMestSaleCreateListSdo(param);
                        if (!processor.Run(sdo, ref rs))
                        {
                            throw new Exception("Tao phieu xuat ban that bai.");
                        }
                        else
                        {
                            resultData.Add(rs);
                        }
                        processors.Add(processor);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.processors))
                {
                    foreach (HisExpMestSaleCreateListSdo p in this.processors)
                    {
                        p.RollBack();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }

}
