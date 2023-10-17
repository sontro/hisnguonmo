using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStockMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class MediStockMetyProcessor : BusinessBase
    {
        private HisMediStockMetyDecreaseMaxAmount decrease = null;
        private HisMediStockMetyIncreaseMaxAmount increase = null;

        internal MediStockMetyProcessor(CommonParam param)
            : base(param)
        {
            this.decrease = new HisMediStockMetyDecreaseMaxAmount(param);
            this.increase = new HisMediStockMetyIncreaseMaxAmount(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    List<HIS_MEDI_STOCK_METY> stockMetys = null;
                    if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION)
                    {
                        Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();

                        stockMetys = new HisMediStockMetyGet().GetByMediStockId(expMest.MEDI_STOCK_ID);
                        var Groups = expMestMedicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MEDICINE> list = group.ToList();
                            HIS_MEDI_STOCK_METY mety = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == group.Key) : null;
                            if (mety != null)
                            {
                                dicIncrease[mety.ID] = list.Sum(s => s.AMOUNT);
                            }
                            else
                            {
                                LogSystem.Warn("Khong tim duoc HIS_MEDI_STOCK_METY tuong ung voi MedicineTypeId: " + group.Key);
                            }
                        }

                        if (dicIncrease.Count>0)
                        {
                            if (!this.increase.Run(dicIncrease))
                            {
                                throw new Exception("increase. Ket thuc nghiep vu");
                            }
                        }

                    }
                    else if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION)
                    {
                        stockMetys = new HisMediStockMetyGet().GetByMediStockId(expMest.IMP_MEDI_STOCK_ID.Value);

                        Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();

                        var Groups = expMestMedicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MEDICINE> list = group.ToList();
                            HIS_MEDI_STOCK_METY mety = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == group.Key) : null;
                            if (mety == null)
                            {
                                throw new Exception("Khong tim thay HIS_MEDI_STOCK_METY tuong ung voi MEDIICNE_TYPE_ID: " + group.Key);
                            }
                            else
                            {
                                dicDecrease[mety.ID] = list.Sum(s => s.AMOUNT);
                            }
                        }

                        if (dicDecrease.Count > 0)
                        {
                            if (!this.decrease.Run(dicDecrease))
                            {
                                throw new Exception("decrease. Ket thuc nghiep vu");
                            }
                        }
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

        internal void Rollback()
        {
            this.decrease.Rollback();
            this.increase.Rollback();
        }
    }
}
