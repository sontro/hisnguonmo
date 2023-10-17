using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod
{
    partial class HisMediStockPeriodCreate : BusinessBase
    {
        private Dictionary<long, List<V_HIS_EXP_MEST_MATERIAL>> DicAppliedExpMestMaterials = new Dictionary<long, List<V_HIS_EXP_MEST_MATERIAL>>();
        private Dictionary<long, List<V_HIS_IMP_MEST_MATERIAL>> DicAppliedImpMestMaterials = new Dictionary<long, List<V_HIS_IMP_MEST_MATERIAL>>();

        private HisMestPeriodMatyCreateSql hisMestPeriodMatyCreate;
        private HisMestPeriodMateCreateSql hisMestPeriodMateCreate;

        private void PrepareDataMaterial(HIS_MEDI_STOCK_PERIOD data)
        {
            this.hisMestPeriodMateCreate = new HisMestPeriodMateCreateSql(param);
            this.hisMestPeriodMatyCreate = new HisMestPeriodMatyCreateSql(param);

            var appliedExpMestMaterials = this.GetAppliedExpMestMaterials(data);
            if (IsNotNullOrEmpty(appliedExpMestMaterials))
            {
                foreach (var item in appliedExpMestMaterials)
                {
                    if (!DicAppliedExpMestMaterials.ContainsKey(item.MATERIAL_ID ?? 0))
                        DicAppliedExpMestMaterials[item.MATERIAL_ID ?? 0] = new List<V_HIS_EXP_MEST_MATERIAL>();
                    DicAppliedExpMestMaterials[item.MATERIAL_ID ?? 0].Add(item);
                }
            }

            var appliedImpMestMaterials = this.GetAppliedImpMestMaterials(data);
            if (IsNotNullOrEmpty(appliedImpMestMaterials))
            {
                foreach (var item in appliedImpMestMaterials)
                {
                    if (!DicAppliedImpMestMaterials.ContainsKey(item.MATERIAL_ID))
                        DicAppliedImpMestMaterials[item.MATERIAL_ID] = new List<V_HIS_IMP_MEST_MATERIAL>();
                    DicAppliedImpMestMaterials[item.MATERIAL_ID].Add(item);
                }
            }
        }

        /// <summary>
        /// lay danh sach chi tiet nhap da thuc nhap
        /// co thoi gian thuc nhap nho hon thoi gian chot ky
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<V_HIS_IMP_MEST_MATERIAL> GetAppliedImpMestMaterials(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_IMP_MEST_MATERIAL WHERE IS_DELETE = 0 ";
            query += " AND MEDI_STOCK_PERIOD_ID IS NULL AND MEDI_STOCK_ID = :param1 AND IMP_TIME < :param2 AND IMP_MEST_STT_ID = :param3";

            return DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST_MATERIAL>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
        }

        /// <summary>
        /// lay danh sach chi tiet xuat da thuc xuat het
        /// co thoi gian thuc xuat nho hon thoi gian chot ky
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <returns></returns>
        private List<V_HIS_EXP_MEST_MATERIAL> GetAppliedExpMestMaterials(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_EXP_MEST_MATERIAL WHERE MEDI_STOCK_PERIOD_ID IS NULL AND TDL_MEDI_STOCK_ID = :param1 ";
            query += " AND IS_DELETE = 0 AND IS_EXPORT = 1 AND EXP_TIME <= :param2 AND EXP_MEST_STT_ID = :param3";

            return DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MATERIAL>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE);
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_MATY
        /// </summary>
        private void ProcessMestPeriodMaty()
        {
            if (!IsNotNullOrEmpty(Config.HisMaterialTypeCFG.DATA)) return;

            //Danh sach chot ky truoc
            List<HIS_MEST_PERIOD_MATY> previousMestPeriodMeties = null;
            List<HIS_MEST_PERIOD_MATY> hisMestPeriodMatys = new List<HIS_MEST_PERIOD_MATY>();
            if (this.previousHisMediStockPeriod != null)
            {
                previousMestPeriodMeties = this.GetPreviousMestPeriodMaty();
            }

            foreach (HIS_MATERIAL_TYPE materialType in Config.HisMaterialTypeCFG.DATA)
            {
                //So luong dau ky bang so luong cuoi ky cua ky truoc, neu ky truoc chua co thi = 0
                HIS_MEST_PERIOD_MATY previous = null;
                if (previousMestPeriodMeties != null)
                {
                    previous = previousMestPeriodMeties.SingleOrDefault(o => o.MATERIAL_TYPE_ID == materialType.ID);
                }

                HIS_MEST_PERIOD_MATY dto = new HIS_MEST_PERIOD_MATY();
                dto.MATERIAL_TYPE_ID = materialType.ID;
                dto.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                dto.BEGIN_AMOUNT = previous != null && previous.VIR_END_AMOUNT.HasValue ? previous.VIR_END_AMOUNT.Value : 0;
                dto.IN_AMOUNT = DicAppliedImpMestMaterials.SelectMany(s => s.Value).Where(o => o.MATERIAL_TYPE_ID == materialType.ID).Sum(o => o.AMOUNT);
                dto.OUT_AMOUNT = DicAppliedExpMestMaterials.SelectMany(s => s.Value).Where(o => o.TDL_MATERIAL_TYPE_ID == materialType.ID).Sum(o => o.AMOUNT);
                dto.INVENTORY_AMOUNT = dto.BEGIN_AMOUNT + dto.IN_AMOUNT - dto.OUT_AMOUNT;

                hisMestPeriodMatys.Add(dto);
            }

            if (IsNotNullOrEmpty(hisMestPeriodMatys) && !this.hisMestPeriodMatyCreate.Run(hisMestPeriodMatys))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_MATE
        /// </summary>
        private void ProcessMestPeriodMate()
        {
            //Danh sach chot ky truoc
            List<HIS_MEST_PERIOD_MATE> previousMestPeriodMates = null;
            if (this.previousHisMediStockPeriod != null)
            {
                previousMestPeriodMates = this.GetPreviousMestPeriodMate();
            }

            List<long> materialIds = new List<long>();
            if (IsNotNullOrEmpty(previousMestPeriodMates))
            {
                materialIds.AddRange(previousMestPeriodMates.Select(s => s.MATERIAL_ID).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedExpMestMaterials))
            {
                materialIds.AddRange(DicAppliedExpMestMaterials.Select(s => s.Key).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedImpMestMaterials))
            {
                materialIds.AddRange(DicAppliedImpMestMaterials.Select(s => s.Key).ToList());
            }

            materialIds = materialIds.Distinct().ToList();

            if (IsNotNullOrEmpty(materialIds))
            {
                List<HIS_MEST_PERIOD_MATE> hisMestPeriodMates = new List<HIS_MEST_PERIOD_MATE>();
                foreach (long mateId in materialIds)
                {
                    if (mateId == 0) continue;

                    long? materialTypeid = null;

                    List<HIS_MEST_PERIOD_MATE> listPrevious = null;
                    if (previousMestPeriodMates != null)
                    {
                        listPrevious = previousMestPeriodMates.Where(o => o.MATERIAL_ID == mateId && o.VIR_END_AMOUNT != 0).ToList();
                    }

                    //tong so luon xuat cua lo
                    decimal outAmount = 0;
                    if (DicAppliedExpMestMaterials.ContainsKey(mateId)) outAmount = DicAppliedExpMestMaterials[mateId].Sum(o => o.AMOUNT);

                    if (DicAppliedImpMestMaterials.ContainsKey(mateId))
                    {
                        var listImpMestMaterial = DicAppliedImpMestMaterials[mateId];
                        materialTypeid = listImpMestMaterial.First().MATERIAL_TYPE_ID;

                        List<long> reqRoomIds = listImpMestMaterial.Select(s => s.REQ_ROOM_ID ?? 0).Distinct().ToList();
                        var listMediStock = Config.HisMediStockCFG.DATA.Where(o => reqRoomIds.Contains(o.ROOM_ID)).ToList();
                        if (IsNotNullOrEmpty(listMediStock))
                        {
                            foreach (var stock in listMediStock)
                            {
                                var impMestMaterialInStock = listImpMestMaterial.Where(o => o.REQ_ROOM_ID == stock.ROOM_ID).ToList();

                                HIS_MEST_PERIOD_MATE previous = null;
                                if (listPrevious != null)
                                {
                                    previous = listPrevious.SingleOrDefault(o => o.MEDI_STOCK_ID == stock.ID);
                                }

                                HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(previous, stock.ID, mateId, materialTypeid, impMestMaterialInStock.Sum(s => s.AMOUNT), ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMates.Add(dto);
                            }

                            var impMestMaterialNotInStock = listImpMestMaterial.Where(o => !listMediStock.Select(s => s.ROOM_ID).Contains(o.REQ_ROOM_ID ?? 0)).ToList();
                            if (IsNotNullOrEmpty(impMestMaterialNotInStock))//nhap khong thuoc kho se cho vao kho dang chot
                            {
                                HIS_MEST_PERIOD_MATE dto = hisMestPeriodMates.FirstOrDefault(o => o.MATERIAL_ID == mateId && o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                                if (dto != null)
                                {
                                    decimal inAmount = impMestMaterialNotInStock.Sum(s => s.AMOUNT);
                                    if (outAmount > inAmount)
                                    {
                                        dto.IN_AMOUNT += inAmount;
                                        dto.OUT_AMOUNT += inAmount;
                                        outAmount -= inAmount;
                                    }
                                    else
                                    {
                                        dto.IN_AMOUNT += inAmount;
                                        dto.OUT_AMOUNT += outAmount;
                                        outAmount -= outAmount;
                                    }

                                    dto.AMOUNT = (dto.BEGIN_AMOUNT ?? 0) + (dto.IN_AMOUNT ?? 0) - (dto.OUT_AMOUNT ?? 0);
                                }
                                else
                                {
                                    HIS_MEST_PERIOD_MATE previous = null;
                                    if (listPrevious != null)
                                    {
                                        previous = listPrevious.FirstOrDefault(o => o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                                    }

                                    dto = ProcessDataMestPeriodMate(previous, previous != null ? previous.MEDI_STOCK_ID : recentHisMediStockPeriod.MEDI_STOCK_ID, mateId, materialTypeid, impMestMaterialNotInStock.Sum(s => s.AMOUNT), ref outAmount);
                                    if (dto != null)
                                        hisMestPeriodMates.Add(dto);
                                }
                            }
                        }
                        else
                        {
                            HIS_MEST_PERIOD_MATE previous = null;
                            if (previousMestPeriodMates != null)
                            {
                                previous = previousMestPeriodMates.FirstOrDefault(o => o.MATERIAL_ID == mateId && o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                            }

                            HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(previous, previous != null ? previous.MEDI_STOCK_ID : recentHisMediStockPeriod.MEDI_STOCK_ID, mateId, materialTypeid, listImpMestMaterial.Sum(s => s.AMOUNT), ref outAmount);
                            if (dto != null)
                                hisMestPeriodMates.Add(dto);
                        }

                        if (IsNotNullOrEmpty(listPrevious))//co Previous ma khong co kho nhap van tao moi
                        {
                            foreach (var item in listPrevious)
                            {
                                if (hisMestPeriodMates.Exists(e => e.MEDI_STOCK_ID == item.MEDI_STOCK_ID && e.MATERIAL_ID == item.MATERIAL_ID)) continue;

                                HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(item, item.MEDI_STOCK_ID, item.MATERIAL_ID, item.TDL_MATERIAL_TYPE_ID, 0, ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMates.Add(dto);
                            }
                        }
                    }
                    else if (outAmount > 0)//co xuat ma khong co nhap
                    {
                        materialTypeid = DicAppliedExpMestMaterials[mateId].First().MATERIAL_TYPE_ID;
                        if (IsNotNullOrEmpty(listPrevious))
                        {
                            foreach (var item in listPrevious)
                            {
                                HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(item, item.MEDI_STOCK_ID, item.MATERIAL_ID, item.TDL_MATERIAL_TYPE_ID, 0, ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMates.Add(dto);
                            }
                        }
                        else
                        {
                            HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(null, recentHisMediStockPeriod.MEDI_STOCK_ID, mateId, materialTypeid, 0, ref outAmount);
                            if (dto != null)
                                hisMestPeriodMates.Add(dto);
                        }
                    }
                    else if (IsNotNullOrEmpty(listPrevious)) //co ky truoc ma khong co xuat nhap
                    {
                        foreach (var item in listPrevious)
                        {
                            HIS_MEST_PERIOD_MATE dto = ProcessDataMestPeriodMate(item, item.MEDI_STOCK_ID, item.MATERIAL_ID, item.TDL_MATERIAL_TYPE_ID, 0, ref outAmount);
                            if (dto != null)
                                hisMestPeriodMates.Add(dto);
                        }
                    }

                    //chia deu so luong xuat tranh th am
                    if (outAmount > 0)
                    {
                        var listDto = hisMestPeriodMates.Where(o => o.MATERIAL_ID == mateId && o.AMOUNT > 0).ToList();
                        foreach (var item in listDto)
                        {
                            if (outAmount == 0) break;

                            if (outAmount - item.AMOUNT > 0)
                            {
                                item.OUT_AMOUNT += item.AMOUNT;
                                outAmount -= item.AMOUNT;
                                item.AMOUNT -= item.AMOUNT;
                            }
                            else
                            {
                                item.OUT_AMOUNT += outAmount;
                                item.AMOUNT -= outAmount;
                                outAmount = 0;
                            }
                        }
                    }

                    //neu con so luong xuat thi cong don vao dong dau tien
                    //loi du lieu se am chot ky
                    if (outAmount > 0)
                    {
                        var dto = hisMestPeriodMates.First(o => o.MATERIAL_ID == mateId);
                        if (dto != null)
                        {
                            dto.OUT_AMOUNT += outAmount;
                            dto.AMOUNT -= outAmount;
                        }
                    }
                }

                if (IsNotNullOrEmpty(hisMestPeriodMates) && !this.hisMestPeriodMateCreate.Run(hisMestPeriodMates))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private HIS_MEST_PERIOD_MATE ProcessDataMestPeriodMate(HIS_MEST_PERIOD_MATE previous, long? mediStockId, long materialId, long? materialTypeId, decimal inAmount, ref  decimal outAmount)
        {
            HIS_MEST_PERIOD_MATE result = new HIS_MEST_PERIOD_MATE();
            try
            {
                result.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                result.MEDI_STOCK_ID = mediStockId;
                result.MATERIAL_ID = materialId;
                result.TDL_MATERIAL_TYPE_ID = materialTypeId;
                result.BEGIN_AMOUNT = previous != null && previous.VIR_END_AMOUNT.HasValue ? previous.VIR_END_AMOUNT.Value : 0;
                result.IN_AMOUNT = inAmount;

                //neu so luong xuat lon hơn so luong nhap tu kho thi se gan so luong xuat bang so luong nhap
                if (mediStockId.HasValue && outAmount > 0 && outAmount > inAmount)
                {
                    result.OUT_AMOUNT = inAmount;
                    outAmount -= inAmount;
                }
                else
                {
                    result.OUT_AMOUNT = outAmount > 0 ? outAmount : 0;
                    outAmount = 0;
                }

                result.AMOUNT = (result.BEGIN_AMOUNT ?? 0) + (result.IN_AMOUNT ?? 0) - (result.OUT_AMOUNT ?? 0);
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Lay danh sach HisMestPeriodMate tuong ung voi ky kiem ke truoc
        /// </summary>
        /// <returns></returns>
        private List<HIS_MEST_PERIOD_MATE> GetPreviousMestPeriodMate()
        {
            HisMestPeriodMateFilterQuery filter = new HisMestPeriodMateFilterQuery();
            filter.MEDI_STOCK_PERIOD_ID = this.previousHisMediStockPeriod.ID;
            return new HisMestPeriodMateGet().Get(filter);
        }

        /// <summary>
        /// Lay danh sach HisMestPeriodMaty tuong ung voi ky kiem ke truoc
        /// </summary>
        /// <returns></returns>
        private List<HIS_MEST_PERIOD_MATY> GetPreviousMestPeriodMaty()
        {
            HisMestPeriodMatyFilterQuery filter = new HisMestPeriodMatyFilterQuery();
            filter.MEDI_STOCK_PERIOD_ID = this.previousHisMediStockPeriod.ID;
            return new HisMestPeriodMatyGet().Get(filter);
        }
    }
}
