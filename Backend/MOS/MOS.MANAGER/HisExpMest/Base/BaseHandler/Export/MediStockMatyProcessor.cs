using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStockMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Export
{
    class MediStockMatyProcessor : BusinessBase
    {
        private HisMediStockMatyDecreaseMaxAmount decrease = null;
        private HisMediStockMatyIncreaseMaxAmount increase = null;
        private HisMediStockMatyCreate hisMediStockMatyCreate = null;

        internal MediStockMatyProcessor(CommonParam param)
            : base(param)
        {
            this.decrease = new HisMediStockMatyDecreaseMaxAmount(param);
            this.increase = new HisMediStockMatyIncreaseMaxAmount(param);
            this.hisMediStockMatyCreate = new HisMediStockMatyCreate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(expMestMaterials))
                {
                    List<HIS_MEDI_STOCK_MATY> stockMatys = null;
                    if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__ADDITION)
                    {
                        Dictionary<long, decimal> dicIncrease = new Dictionary<long, decimal>();
                        List<HIS_MEDI_STOCK_MATY> createds = new List<HIS_MEDI_STOCK_MATY>();
                        stockMatys = new HisMediStockMatyGet().GetByMediStockId(expMest.IMP_MEDI_STOCK_ID.Value);
                        var Groups = expMestMaterials.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();

                        List<long> stockMatyIds = new List<long>();

                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MATERIAL> list = group.ToList();
                            HIS_MEDI_STOCK_MATY maty = stockMatys != null ? stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == group.Key) : null;
                            if (maty == null)
                            {
                                maty = new HIS_MEDI_STOCK_MATY();
                                maty.MATERIAL_TYPE_ID = group.Key ?? 0;
                                maty.MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                                maty.ALERT_MAX_IN_STOCK = list.Sum(s => s.AMOUNT);
                                createds.Add(maty);
                            }
                            else
                            {
                                dicIncrease[maty.ID] = list.Sum(s => s.AMOUNT);
                                if (maty.EXP_MEDI_STOCK_ID != expMest.MEDI_STOCK_ID)
                                    stockMatyIds.Add(maty.ID);
                            }
                        }

                        if (dicIncrease.Count > 0)
                        {
                            if (!this.increase.Run(dicIncrease))
                            {
                                throw new Exception("increase. Ket thuc nghiep vu");
                            }
                        }

                        if (IsNotNullOrEmpty(createds) && !this.hisMediStockMatyCreate.CreateList(createds))
                        {
                            throw new Exception("hisMediStockMatyCreate. Ket thuc nghiep vu");
                        }
                        if (IsNotNullOrEmpty(createds))
                        {
                            stockMatyIds.AddRange(createds.Select(s => s.ID).ToList());
                        }

                        if (IsNotNullOrEmpty(stockMatyIds))
                        {
                            string tempSql = String.Format("UPDATE HIS_MEDI_STOCK_MATY SET EXP_MEDI_STOCK_ID = {0} WHERE %IN_CLAUSE% ", expMest.MEDI_STOCK_ID);
                            sqls.Add(DAOWorker.SqlDAO.AddInClause(stockMatyIds, tempSql, "ID"));
                        }
                    }
                    else if (expMest.CHMS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.CHMS_TYPE__ID__REDUCTION)
                    {
                        stockMatys = new HisMediStockMatyGet().GetByMediStockId(expMest.MEDI_STOCK_ID);

                        Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();

                        var Groups = expMestMaterials.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();
                        foreach (var group in Groups)
                        {
                            List<HIS_EXP_MEST_MATERIAL> list = group.ToList();
                            HIS_MEDI_STOCK_MATY maty = stockMatys != null ? stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == group.Key) : null;
                            if (maty == null)
                            {
                                throw new Exception("Khong tim thay HIS_MEDI_STOCK_MATY tuong ung voi MATERIAL_TYPE_ID: " + group.Key);
                            }
                            else
                            {
                                dicDecrease[maty.ID] = list.Sum(s => s.AMOUNT);
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
            this.hisMediStockMatyCreate.RollbackData();
            this.decrease.Rollback();
            this.increase.Rollback();
        }
    }
}
