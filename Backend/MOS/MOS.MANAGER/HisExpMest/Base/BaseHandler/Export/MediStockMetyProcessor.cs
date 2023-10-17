using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStockMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class MediStockMetyProcessor : BusinessBase
    {
        private HisMediStockMetyDecreaseMaxAmount decrease = null;
        private HisMediStockMetyIncreaseMaxAmount increase = null;
        private HisMediStockMetyCreate hisMediStockMetyCreate = null;

        internal MediStockMetyProcessor(CommonParam param)
            : base(param)
        {
            this.decrease = new HisMediStockMetyDecreaseMaxAmount(param);
            this.increase = new HisMediStockMetyIncreaseMaxAmount(param);
            this.hisMediStockMetyCreate = new HisMediStockMetyCreate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    List<HIS_MEDI_STOCK_METY> stockMetys = null;
                    if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION)
                    {
                        Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();
                        List<HIS_MEDI_STOCK_METY> createds = new List<HIS_MEDI_STOCK_METY>();
                        stockMetys = new HisMediStockMetyGet().GetByMediStockId(expMest.IMP_MEDI_STOCK_ID.Value);
                        var Groups = expMestMedicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();

                        List<long> stockMetyIds = new List<long>();

                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MEDICINE> list = group.ToList();
                            HIS_MEDI_STOCK_METY mety = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == group.Key) : null;
                            if (mety == null)
                            {
                                mety = new HIS_MEDI_STOCK_METY();
                                mety.MEDICINE_TYPE_ID = group.Key ?? 0;
                                mety.MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                                mety.ALERT_MAX_IN_STOCK = list.Sum(s => s.AMOUNT);
                                createds.Add(mety);
                            }
                            else
                            {
                                dicIncrease[mety.ID] = list.Sum(s => s.AMOUNT);
                                if (mety.EXP_MEDI_STOCK_ID != expMest.MEDI_STOCK_ID)
                                    stockMetyIds.Add(mety.ID);
                            }
                        }

                        if (dicIncrease.Count > 0)
                        {
                            if (!this.increase.Run(dicIncrease))
                            {
                                throw new Exception("increase. Ket thuc nghiep vu");
                            }
                        }

                        if (IsNotNullOrEmpty(createds) && !this.hisMediStockMetyCreate.CreateList(createds))
                        {
                            throw new Exception("hisMediStockMetyCreate. Ket thuc nghiep vu");
                        }

                        if (IsNotNullOrEmpty(createds))
                        {
                            stockMetyIds.AddRange(createds.Select(s => s.ID).ToList());
                        }

                        if (IsNotNullOrEmpty(stockMetyIds))
                        {
                            string tempSql = String.Format("UPDATE HIS_MEDI_STOCK_METY SET EXP_MEDI_STOCK_ID = {0} WHERE %IN_CLAUSE% ", expMest.MEDI_STOCK_ID);
                            sqls.Add(DAOWorker.SqlDAO.AddInClause(stockMetyIds, tempSql, "ID"));
                        }
                    }
                    else if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION)
                    {
                        stockMetys = new HisMediStockMetyGet().GetByMediStockId(expMest.MEDI_STOCK_ID);

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
            this.hisMediStockMetyCreate.RollbackData();
            this.decrease.Rollback();
            this.increase.Rollback();
        }
    }
}
