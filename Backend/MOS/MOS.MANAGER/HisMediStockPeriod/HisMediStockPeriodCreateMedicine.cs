using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.MANAGER.HisMestPeriodMety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod
{
    partial class HisMediStockPeriodCreate : BusinessBase
    {
        private Dictionary<long, List<V_HIS_EXP_MEST_MEDICINE>> DicAppliedExpMestMedicines = new Dictionary<long, List<V_HIS_EXP_MEST_MEDICINE>>();
        private Dictionary<long, List<V_HIS_IMP_MEST_MEDICINE>> DicAppliedImpMestMedicines = new Dictionary<long, List<V_HIS_IMP_MEST_MEDICINE>>();

        private HisMestPeriodMetyCreateSql hisMestPeriodMetyCreate;
        private HisMestPeriodMediCreateSql hisMestPeriodMediCreate;

        private void PrepareDataMedicine(HIS_MEDI_STOCK_PERIOD data)
        {
            this.hisMestPeriodMetyCreate = new HisMestPeriodMetyCreateSql(param);
            this.hisMestPeriodMediCreate = new HisMestPeriodMediCreateSql(param);

            var appliedExpMestMedicines = this.GetAppliedExpMestMedicines(data);
            var appliedImpMestMedicines = this.GetAppliedImpMestMedicines(data);

            if (IsNotNullOrEmpty(appliedExpMestMedicines))
            {
                foreach (var item in appliedExpMestMedicines)
                {
                    if (!DicAppliedExpMestMedicines.ContainsKey(item.MEDICINE_ID ?? 0))
                        DicAppliedExpMestMedicines[item.MEDICINE_ID ?? 0] = new List<V_HIS_EXP_MEST_MEDICINE>();
                    DicAppliedExpMestMedicines[item.MEDICINE_ID ?? 0].Add(item);
                }
            }

            if (IsNotNullOrEmpty(appliedImpMestMedicines))
            {
                foreach (var item in appliedImpMestMedicines)
                {
                    if (!DicAppliedImpMestMedicines.ContainsKey(item.MEDICINE_ID))
                        DicAppliedImpMestMedicines[item.MEDICINE_ID] = new List<V_HIS_IMP_MEST_MEDICINE>();
                    DicAppliedImpMestMedicines[item.MEDICINE_ID].Add(item);
                }
            }
        }

        /// <summary>
        /// lay danh sach chi tiet nhap da thuc nhap
        /// co thoi gian thuc nhap nho hon thoi gian chot ky
        /// </summary>
        /// <returns></returns>
        private List<V_HIS_IMP_MEST_MEDICINE> GetAppliedImpMestMedicines(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_IMP_MEST_MEDICINE WHERE IS_DELETE = 0 ";
            query += " AND MEDI_STOCK_PERIOD_ID IS NULL AND MEDI_STOCK_ID = :param1 AND IMP_TIME < :param2 AND IMP_MEST_STT_ID = :param3";
            return DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST_MEDICINE>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT);
        }

        /// <summary>
        /// lay danh sach chi tiet xuat da thuc xuat het
        /// co thoi gian thuc xuat nho hon thoi gian chot ky
        /// </summary>
        /// <returns></returns>
        private List<V_HIS_EXP_MEST_MEDICINE> GetAppliedExpMestMedicines(HIS_MEDI_STOCK_PERIOD data)
        {
            string query = "SELECT * FROM V_HIS_EXP_MEST_MEDICINE WHERE MEDI_STOCK_PERIOD_ID IS NULL AND TDL_MEDI_STOCK_ID = :param1 ";
            query += " AND IS_DELETE = 0 AND IS_EXPORT = 1 AND EXP_TIME <= :param2 AND EXP_MEST_STT_ID = :param3";
            return DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MEDICINE>(query, data.MEDI_STOCK_ID, data.TO_TIME, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE);
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_METY
        /// </summary>
        private void ProcessMestPeriodMety()
        {
            if (!IsNotNullOrEmpty(Config.HisMedicineTypeCFG.DATA)) return;

            //Danh sach chot ky truoc
            List<HIS_MEST_PERIOD_METY> previousMestPeriodMeties = null;
            List<HIS_MEST_PERIOD_METY> hisMestPeriodMetys = new List<HIS_MEST_PERIOD_METY>();
            if (this.previousHisMediStockPeriod != null)
            {
                previousMestPeriodMeties = this.GetPreviousMestPeriodMety();
            }

            foreach (HIS_MEDICINE_TYPE medicineType in Config.HisMedicineTypeCFG.DATA)
            {
                HIS_MEST_PERIOD_METY previous = null;
                if (previousMestPeriodMeties != null)
                {
                    previous = previousMestPeriodMeties.SingleOrDefault(o => o.MEDICINE_TYPE_ID == medicineType.ID);
                }

                HIS_MEST_PERIOD_METY dto = new HIS_MEST_PERIOD_METY();
                dto.MEDICINE_TYPE_ID = medicineType.ID;
                dto.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                dto.BEGIN_AMOUNT = previous != null && previous.VIR_END_AMOUNT.HasValue ? previous.VIR_END_AMOUNT.Value : 0;
                dto.IN_AMOUNT = DicAppliedImpMestMedicines.SelectMany(s => s.Value).Where(o => o.MEDICINE_TYPE_ID == medicineType.ID).Sum(o => o.AMOUNT);
                dto.OUT_AMOUNT = DicAppliedExpMestMedicines.SelectMany(s => s.Value).Where(o => o.MEDICINE_TYPE_ID == medicineType.ID).Sum(o => o.AMOUNT);
                dto.INVENTORY_AMOUNT = dto.BEGIN_AMOUNT + dto.IN_AMOUNT - dto.OUT_AMOUNT;

                hisMestPeriodMetys.Add(dto);
            }

            if ((IsNotNullOrEmpty(hisMestPeriodMetys) && !this.hisMestPeriodMetyCreate.Run(hisMestPeriodMetys)))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        /// <summary>
        /// Tao du lieu HIS_MEST_PERIOD_MEDI
        /// </summary>
        private void ProcessMestPeriodMedi()
        {
            //Danh sach chot ky truoc
            List<HIS_MEST_PERIOD_MEDI> previousMestPeriodMedis = null;
            if (this.previousHisMediStockPeriod != null)
            {
                previousMestPeriodMedis = this.GetPreviousMestPeriodMedi();
            }

            List<long> MedicineIds = new List<long>();
            if (IsNotNullOrEmpty(previousMestPeriodMedis))
            {
                MedicineIds.AddRange(previousMestPeriodMedis.Select(s => s.MEDICINE_ID).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedExpMestMedicines))
            {
                MedicineIds.AddRange(DicAppliedExpMestMedicines.Select(s => s.Key).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedImpMestMedicines))
            {
                MedicineIds.AddRange(DicAppliedImpMestMedicines.Select(s => s.Key).ToList());
            }

            MedicineIds = MedicineIds.Distinct().ToList();

            if (IsNotNullOrEmpty(MedicineIds))
            {
                List<HIS_MEST_PERIOD_MEDI> hisMestPeriodMedis = new List<HIS_MEST_PERIOD_MEDI>();
                foreach (long mediId in MedicineIds)
                {
                    if (mediId == 0) continue;

                    long? MedicineTypeid = null;

                    List<HIS_MEST_PERIOD_MEDI> listPrevious = null;
                    if (previousMestPeriodMedis != null)
                    {
                        listPrevious = previousMestPeriodMedis.Where(o => o.MEDICINE_ID == mediId && o.VIR_END_AMOUNT != 0).ToList();
                    }

                    //tong so luon xuat cua lo
                    decimal outAmount = 0;
                    if (DicAppliedExpMestMedicines.ContainsKey(mediId)) outAmount = DicAppliedExpMestMedicines[mediId].Sum(o => o.AMOUNT);

                    if (DicAppliedImpMestMedicines.ContainsKey(mediId))
                    {
                        var listImpMestMedicine = DicAppliedImpMestMedicines[mediId];
                        MedicineTypeid = listImpMestMedicine.First().MEDICINE_TYPE_ID;

                        List<long> reqRoomIds = listImpMestMedicine.Select(s => s.REQ_ROOM_ID ?? 0).Distinct().ToList();
                        var listMediStock = Config.HisMediStockCFG.DATA.Where(o => reqRoomIds.Contains(o.ROOM_ID)).ToList();
                        if (IsNotNullOrEmpty(listMediStock))
                        {
                            foreach (var stock in listMediStock)
                            {
                                var impMestMedicineInStock = listImpMestMedicine.Where(o => o.REQ_ROOM_ID == stock.ROOM_ID).ToList();

                                HIS_MEST_PERIOD_MEDI previous = null;
                                if (listPrevious != null)
                                {
                                    previous = listPrevious.FirstOrDefault(o => o.MEDI_STOCK_ID == stock.ID);
                                }

                                HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(previous, stock.ID, mediId, MedicineTypeid, impMestMedicineInStock.Sum(s => s.AMOUNT), ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMedis.Add(dto);
                            }

                            var impMestMedicineNotInStock = listImpMestMedicine.Where(o => !listMediStock.Select(s => s.ROOM_ID).Contains(o.REQ_ROOM_ID ?? 0)).ToList();
                            if (IsNotNullOrEmpty(impMestMedicineNotInStock))//nhap khong thuoc kho se cho vao kho dang chot
                            {
                                HIS_MEST_PERIOD_MEDI dto = hisMestPeriodMedis.FirstOrDefault(o => o.MEDICINE_ID == mediId && o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                                if (dto != null)
                                {
                                    decimal inAmount = impMestMedicineNotInStock.Sum(s => s.AMOUNT);
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
                                    HIS_MEST_PERIOD_MEDI previous = null;
                                    if (listPrevious != null)
                                    {
                                        previous = listPrevious.FirstOrDefault(o => o.MEDICINE_ID == mediId && o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                                    }

                                    dto = ProcessDataMestPeriodMedi(previous, previous != null ? previous.MEDI_STOCK_ID : recentHisMediStockPeriod.MEDI_STOCK_ID, mediId, MedicineTypeid, impMestMedicineNotInStock.Sum(s => s.AMOUNT), ref outAmount);
                                    if (dto != null)
                                        hisMestPeriodMedis.Add(dto);
                                }
                            }
                        }
                        else//khong co kho se cho vao kho dang chot
                        {
                            HIS_MEST_PERIOD_MEDI previous = null;
                            if (listPrevious != null)
                            {
                                previous = listPrevious.FirstOrDefault(o => o.MEDICINE_ID == mediId && o.MEDI_STOCK_ID == recentHisMediStockPeriod.MEDI_STOCK_ID);
                            }

                            HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(previous, previous != null ? previous.MEDI_STOCK_ID : recentHisMediStockPeriod.MEDI_STOCK_ID, mediId, MedicineTypeid, listImpMestMedicine.Sum(s => s.AMOUNT), ref outAmount);
                            if (dto != null)
                                hisMestPeriodMedis.Add(dto);
                        }

                        if (IsNotNullOrEmpty(listPrevious))//co Previous ma khong co kho nhap van tao moi
                        {
                            foreach (var item in listPrevious)
                            {
                                if (hisMestPeriodMedis.Exists(e => e.MEDI_STOCK_ID == item.MEDI_STOCK_ID && e.MEDICINE_ID == item.MEDICINE_ID)) continue;

                                HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(item, item.MEDI_STOCK_ID, item.MEDICINE_ID, item.TDL_MEDICINE_TYPE_ID, 0, ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMedis.Add(dto);
                            }
                        }
                    }
                    else if (outAmount > 0)//co xuat ma khong co nhap
                    {
                        MedicineTypeid = DicAppliedExpMestMedicines[mediId].First().MEDICINE_TYPE_ID;
                        if (IsNotNullOrEmpty(listPrevious))
                        {
                            foreach (var item in listPrevious)
                            {
                                HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(item, item.MEDI_STOCK_ID, item.MEDICINE_ID, item.TDL_MEDICINE_TYPE_ID, 0, ref outAmount);
                                if (dto != null)
                                    hisMestPeriodMedis.Add(dto);
                            }
                        }
                        else
                        {
                            HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(null, recentHisMediStockPeriod.MEDI_STOCK_ID, mediId, MedicineTypeid, 0, ref outAmount);
                            if (dto != null)
                                hisMestPeriodMedis.Add(dto);
                        }
                    }
                    else if (IsNotNullOrEmpty(listPrevious))//co ky truoc ma khong co xuat nhap
                    {
                        foreach (var item in listPrevious)
                        {
                            HIS_MEST_PERIOD_MEDI dto = ProcessDataMestPeriodMedi(item, item.MEDI_STOCK_ID, item.MEDICINE_ID, item.TDL_MEDICINE_TYPE_ID, 0, ref outAmount);
                            if (dto != null)
                                hisMestPeriodMedis.Add(dto);
                        }
                    }

                    //chia deu so luong xuat tranh th am
                    if (outAmount > 0)
                    {
                        var listDto = hisMestPeriodMedis.Where(o => o.MEDICINE_ID == mediId && o.AMOUNT > 0).ToList();
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
                        var dto = hisMestPeriodMedis.First(o => o.MEDICINE_ID == mediId);
                        if (dto != null)
                        {
                            dto.OUT_AMOUNT += outAmount;
                            dto.AMOUNT -= outAmount;
                        }
                    }
                }

                if (IsNotNullOrEmpty(hisMestPeriodMedis) && !this.hisMestPeriodMediCreate.Run(hisMestPeriodMedis))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private HIS_MEST_PERIOD_MEDI ProcessDataMestPeriodMedi(HIS_MEST_PERIOD_MEDI previous, long? mediStockId, long materialId, long? materialTypeId, decimal inAmount, ref  decimal outAmount)
        {
            HIS_MEST_PERIOD_MEDI result = new HIS_MEST_PERIOD_MEDI();
            try
            {
                result.MEDI_STOCK_PERIOD_ID = this.recentHisMediStockPeriod.ID;
                result.MEDI_STOCK_ID = mediStockId;
                result.MEDICINE_ID = materialId;
                result.TDL_MEDICINE_TYPE_ID = materialTypeId;
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
        /// Lay danh sach HisMestPeriodMedi tuong ung voi ky kiem ke truoc
        /// </summary>
        /// <returns></returns>
        private List<HIS_MEST_PERIOD_MEDI> GetPreviousMestPeriodMedi()
        {
            HisMestPeriodMediFilterQuery filter = new HisMestPeriodMediFilterQuery();
            filter.MEDI_STOCK_PERIOD_ID = this.previousHisMediStockPeriod.ID;
            return new HisMestPeriodMediGet().Get(filter);
        }

        /// <summary>
        /// Lay danh sach HisMestPeriodMety tuong ung voi ky kiem ke truoc
        /// </summary>
        /// <returns></returns>
        private List<HIS_MEST_PERIOD_METY> GetPreviousMestPeriodMety()
        {
            HisMestPeriodMetyFilterQuery filter = new HisMestPeriodMetyFilterQuery();
            filter.MEDI_STOCK_PERIOD_ID = this.previousHisMediStockPeriod.ID;
            return new HisMestPeriodMetyGet().Get(filter);
        }
    }
}
