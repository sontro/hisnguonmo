using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineType
{
    partial class HisMedicineTypeGet : GetBase
    {
        private const string ID = "ID";
        private const string METY_CODE = "MEDICINE_TYPE_CODE";
        private const string METY_NAME = "MEDICINE_TYPE_NAME";
        private const string CONCENTRA = "CONCENTRA";
        private const string ACTIVE_CODE = "ACTIVE_INGR_BHYT_CODE";
        private const string ACTIVE_NAME = "ACTIVE_INGR_BHYT_NAME";
        private const string REGISTER = "REGISTER_NUMBER";
        private const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        private const string SERVICE_UNIT_NAME = "SERVICE_UNIT_NAME";
        private const string PARENT_TYPE_NAME = "PARENT_TYPE_NAME";

        private static string AMOUNT_FORMAT = "AMOUNT_{0}";

        internal List<HisMedicineTypeInStockSDO> GetInStockMedicineType(HisMedicineTypeStockViewFilter filter)
        {
            try
            {
                List<HisMedicineTypeInStockSDO> result = null;
                HisMedicineBeanViewFilterQuery medicineBeanFilter = new HisMedicineBeanViewFilterQuery();
                medicineBeanFilter.MEDICINE_TYPE_ID = filter.ID;
                medicineBeanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                medicineBeanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                medicineBeanFilter.MEDICINE_TYPE_IDs = filter.IDs;
                medicineBeanFilter.ORDER_FIELD = "NUM_ORDER";
                medicineBeanFilter.ORDER_DIRECTION = "DESC";
                medicineBeanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                medicineBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;
                medicineBeanFilter.MEDICINE_TYPE_IDs = filter.MEDICINE_TYPE_IDs;
                medicineBeanFilter.EXPIRED_DATE_NULl__OR__GREATER_THAN__OR__EQUAL = filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL;

                List<V_HIS_MEDICINE_BEAN> vMedicineBeans = new HisMedicineBeanGet().GetView(medicineBeanFilter);
                if (IsNotNullOrEmpty(vMedicineBeans))
                {
                    result = new List<HisMedicineTypeInStockSDO>();
                    Dictionary<string, HisMedicineTypeInStockSDO> dic = new Dictionary<string, HisMedicineTypeInStockSDO>();

                    foreach (V_HIS_MEDICINE_BEAN bean in vMedicineBeans)
                    {
                        string key = string.Format("{0}|{1}", bean.MEDICINE_TYPE_ID, bean.MEDI_STOCK_ID);

                        if (dic.ContainsKey(key))
                        {
                            HisMedicineTypeInStockSDO sdo = dic[key];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                        }
                        else
                        {
                            HisMedicineTypeInStockSDO sdo = new HisMedicineTypeInStockSDO();
                            sdo.Id = bean.MEDICINE_TYPE_ID;
                            sdo.IsActive = bean.MEDICINE_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MedicineTypeCode = bean.MEDICINE_TYPE_CODE;
                            sdo.MedicineTypeName = bean.MEDICINE_TYPE_NAME;
                            sdo.MediStockId = bean.MEDI_STOCK_ID;
                            sdo.ParentId = bean.PARENT_ID;
                            sdo.ServiceId = bean.SERVICE_ID;
                            sdo.ManufacturerName = bean.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = bean.MEDICINE_TYPE_CODE;
                            sdo.NationalName = bean.NATIONAL_NAME;
                            sdo.ServiceUnitCode = bean.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = bean.SERVICE_UNIT_NAME;
                            sdo.Concentra = bean.CONCENTRA;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.RegisterNumber = bean.REGISTER_NUMBER;
                            sdo.ActiveIngrBhytCode = bean.ACTIVE_INGR_BHYT_CODE;
                            sdo.ActiveIngrBhytName = bean.ACTIVE_INGR_BHYT_NAME;
                            sdo.MediStockCode = bean.MEDI_STOCK_CODE;
                            sdo.MediStockName = bean.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = bean.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = bean.IS_BUSINESS;
                            sdo.LastExpPrice = bean.LAST_EXP_PRICE;
                            sdo.LastExpVatRatio = bean.LAST_EXP_VAT_RATIO;
                            sdo.LastExpiredDate = bean.LAST_EXPIRED_DATE;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            dic.Add(key, sdo);

                        }
                    }

                    ////sort lai du lieu
                    //result = result.AsQueryable().OrderByProperty(filter.ORDER_FIELD, filter.ORDER_DIRECTION).ToList();
                    if (dic.Values != null)
                    {
                        result = dic.Values.ToList();
                        if (filter.IS_AVAILABLE.HasValue && filter.IS_AVAILABLE.Value)
                        {
                            result = result.Where(o => o.AvailableAmount > 0).ToList();
                        }
                        else if (filter.IS_AVAILABLE.HasValue && !filter.IS_AVAILABLE.Value)
                        {
                            result = result.Where(o => o.AvailableAmount <= 0).ToList();
                        }
                    }
                    //Thuc hien phan trang lai theo du lieu param tu client (do du lieu ko duoc phan trang duoi tang DAO)
                    int start = param.Start.HasValue ? param.Start.Value : 0;
                    int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                    param.Count = result.Count;
                    return result.Skip(start).Take(limit).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisMedicineTypeView1SDO> GetPriceLists(HisMedicineTypeView1SDOFilter filter)
        {
            List<HisMedicineTypeView1SDO> result = null;
            try
            {
                HisMedicineTypeView1FilterQuery mediFilter = new HisMedicineTypeView1FilterQuery();
                mediFilter.IS_BUSINESS = filter.IS_BUSINESS;
                mediFilter.IS_ACTIVE = Constant.IS_TRUE;
                if (IsNotNullOrEmpty(filter.ColumnParams))
                {
                    mediFilter.ColumnParams = filter.ColumnParams;
                }

                List<HisMedicineTypeView1DTO> dtos = new HisMedicineTypeGet().GetView1Dynamic(mediFilter);
                if (IsNotNullOrEmpty(dtos) && filter.PARENT_ID.HasValue)
                {
                    dtos = this.GetChilds(filter.PARENT_ID.Value, dtos);
                }
                if (IsNotNullOrEmpty(dtos))
                {
                    result = new List<HisMedicineTypeView1SDO>();
                    Mapper.CreateMap<HisMedicineTypeView1DTO, HisMedicineTypeView1SDO>();
                    List<long> medicineIds = new List<long>();
                    foreach (var item in dtos)
                    {
                        if (item.IS_LEAF != Constant.IS_TRUE) continue;
                        if (!String.IsNullOrWhiteSpace(item.MEDICINE_INFO))
                        {
                            HisMedicineTypeView1SDO sdo = Mapper.Map<HisMedicineTypeView1SDO>(item);
                            this.ProcessMedicineInfo(sdo, item.MEDICINE_INFO);
                            if (sdo.MEDICINE_ID.HasValue)
                            {
                                if (sdo.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
                                {
                                    sdo.EXP_PRICE = sdo.MEDICINE_IMP_PRICE ?? 0;
                                    sdo.EXP_VAT_RATIO = sdo.MEDICINE_IMP_VAT_RATIO ?? 0;
                                    sdo.PRICE = sdo.EXP_PRICE.Value * (1 + sdo.EXP_VAT_RATIO.Value);
                                }
                                else
                                {
                                    medicineIds.Add(sdo.MEDICINE_ID.Value);
                                }
                            }
                            result.Add(sdo);
                        }
                    }

                    if (IsNotNullOrEmpty(medicineIds))
                    {
                        HisMedicinePatyFilterQuery patyFilter = new HisMedicinePatyFilterQuery();
                        patyFilter.MEDICINE_IDs = medicineIds;
                        patyFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        List<HIS_MEDICINE_PATY> medicinePatys = new HisMedicinePatyGet().Get(patyFilter);
                        if (IsNotNullOrEmpty(medicinePatys))
                        {
                            medicinePatys = medicinePatys.OrderByDescending(o => o.ID).ToList();
                            foreach (var sdo in result)
                            {
                                if (!sdo.MEDICINE_ID.HasValue) continue;
                                var paty = medicinePatys.FirstOrDefault(o => o.MEDICINE_ID == sdo.MEDICINE_ID.Value);
                                if (paty != null)
                                {
                                    sdo.EXP_PRICE = paty.EXP_PRICE;
                                    sdo.EXP_VAT_RATIO = paty.EXP_VAT_RATIO;
                                    sdo.PRICE = sdo.EXP_PRICE.Value * (1 + sdo.EXP_VAT_RATIO.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        private void ProcessMedicineInfo(HisMedicineTypeView1SDO sdo, string medicineInfo)
        {
            if (!String.IsNullOrWhiteSpace(medicineInfo))
            {
                List<string> listInfo = medicineInfo.Split('|').ToList();
                if (listInfo == null || listInfo.Count != 5)
                {
                    LogSystem.Warn("MedicineInfo Invalid: " + medicineInfo);
                    return;
                }
                long medicineId = 0;
                if (long.TryParse(listInfo[0], out medicineId))
                {
                    sdo.MEDICINE_ID = medicineId;
                }
                long impTime = 0;
                if (long.TryParse(listInfo[1], out impTime))
                {
                    sdo.IMP_TIME = impTime;
                }

                decimal impPrice = 0;
                if (decimal.TryParse(listInfo[2], out impPrice))
                {
                    sdo.MEDICINE_IMP_PRICE = impPrice;
                }

                decimal impVat = 0;
                if (decimal.TryParse(listInfo[3], out impVat))
                {
                    sdo.MEDICINE_IMP_VAT_RATIO = impVat;
                }

                short isSaleEqualImpPrice = 0;
                if (short.TryParse(listInfo[4], out isSaleEqualImpPrice))
                {
                    sdo.IS_SALE_EQUAL_IMP_PRICE = isSaleEqualImpPrice;
                }
            }
        }

        private List<HisMedicineTypeView1DTO> GetChilds(long parentId, List<HisMedicineTypeView1DTO> datas)
        {
            List<HisMedicineTypeView1DTO> result = new List<HisMedicineTypeView1DTO>();
            if (IsNotNullOrEmpty(datas))
            {

                var childs = datas.Where(o => o.PARENT_ID == parentId).ToList();
                var childIsLeafs = childs != null ? childs.Where(o => o.IS_LEAF == Constant.IS_TRUE).ToList() : null;
                var childNotLeafs = childs != null ? childs.Where(o => o.IS_LEAF != Constant.IS_TRUE).ToList() : null;
                if (IsNotNullOrEmpty(childIsLeafs))
                {
                    result.AddRange(childIsLeafs);
                }
                if (IsNotNullOrEmpty(childNotLeafs))
                {
                    foreach (var c in childNotLeafs)
                    {
                        var rs = this.GetChilds(c.ID, datas);
                        if (IsNotNullOrEmpty(rs))
                        {
                            result.AddRange(rs);
                        }
                    }
                }
            }
            return result;
        }

        internal List<HisMedicineTypeInStockSDO> GetInStockMedicineTypeWithImpStock(HisMetyStockWithImpStockViewFilter filter)
        {
            List<HisMedicineTypeInStockSDO> result = null;
            try
            {
                if (!filter.MEDI_STOCK_ID.HasValue || !filter.IMP_MEDI_STOCK_ID.HasValue
                    || !filter.EXP_DATE_FROM.HasValue || !filter.EXP_DATE_TO.HasValue)
                {
                    throw new Exception("filter.MEDI_STOCK_ID or filter.IMP_MEDI_STOCK_ID or filter.EXP_DATE_FROM or filter.EXP_DATE_TO is null");
                }

                HisExpMestMedicineFilterQuery expMedicineFilter = new HisExpMestMedicineFilterQuery();
                expMedicineFilter.EXP_DATE_FROM = filter.EXP_DATE_FROM;
                expMedicineFilter.EXP_DATE_TO = filter.EXP_DATE_TO;
                expMedicineFilter.TDL_MEDI_STOCK_ID = filter.IMP_MEDI_STOCK_ID;
                expMedicineFilter.IS_EXPORT = true;
                expMedicineFilter.ColumnParams = new List<string>();
                expMedicineFilter.ColumnParams.Add("AMOUNT");
                expMedicineFilter.ColumnParams.Add("TDL_MEDICINE_TYPE_ID");

                List<HisExpMestMedicineDTO> expMestMedicineDTOs = new HisExpMestMedicineGet().GetDynamic(expMedicineFilter);

                if (IsNotNullOrEmpty(expMestMedicineDTOs))
                {
                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == filter.MEDI_STOCK_ID.Value);

                    HisMedicineBeanViewFilterQuery medicineBeanFilter = new HisMedicineBeanViewFilterQuery();
                    medicineBeanFilter.MEDI_STOCK_IDs = new List<long>() { filter.MEDI_STOCK_ID.Value, filter.IMP_MEDI_STOCK_ID.Value };
                    medicineBeanFilter.ORDER_FIELD = "NUM_ORDER";
                    medicineBeanFilter.ORDER_DIRECTION = "DESC";
                    medicineBeanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                    medicineBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;
                    medicineBeanFilter.MEDICINE_TYPE_IDs = expMestMedicineDTOs.Select(s => s.TDL_MEDICINE_TYPE_ID ?? 0).Distinct().ToList();

                    List<V_HIS_MEDICINE_BEAN> vMedicineBeans = new HisMedicineBeanGet().GetView(medicineBeanFilter);

                    result = new List<HisMedicineTypeInStockSDO>();
                    var Groups = expMestMedicineDTOs.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        var beanExpStocks = vMedicineBeans != null ? vMedicineBeans.Where(o => o.TDL_MEDICINE_TYPE_ID == group.Key && o.MEDI_STOCK_ID == filter.MEDI_STOCK_ID).ToList() : null;
                        var beanImpStocks = vMedicineBeans != null ? vMedicineBeans.Where(o => o.TDL_MEDICINE_TYPE_ID == group.Key && o.MEDI_STOCK_ID == filter.IMP_MEDI_STOCK_ID).ToList() : null;
                        HisMedicineTypeInStockSDO sdo = new HisMedicineTypeInStockSDO();
                        var beanFirst = IsNotNullOrEmpty(beanExpStocks) ? beanExpStocks.FirstOrDefault() : (IsNotNullOrEmpty(beanImpStocks) ? beanImpStocks.FirstOrDefault() : null);
                        if (beanFirst != null)
                        {
                            sdo.Id = beanFirst.MEDICINE_TYPE_ID;
                            sdo.IsActive = beanFirst.MEDICINE_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MedicineTypeCode = beanFirst.MEDICINE_TYPE_CODE;
                            sdo.MedicineTypeName = beanFirst.MEDICINE_TYPE_NAME;
                            sdo.MediStockId = stock.ID;
                            sdo.ParentId = beanFirst.PARENT_ID;
                            sdo.ServiceId = beanFirst.SERVICE_ID;
                            sdo.ManufacturerName = beanFirst.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = beanFirst.MEDICINE_TYPE_CODE;
                            sdo.NationalName = beanFirst.NATIONAL_NAME;
                            sdo.ServiceUnitCode = beanFirst.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = beanFirst.SERVICE_UNIT_NAME;
                            sdo.Concentra = beanFirst.CONCENTRA;
                            sdo.RegisterNumber = beanFirst.REGISTER_NUMBER;
                            sdo.ActiveIngrBhytCode = beanFirst.ACTIVE_INGR_BHYT_CODE;
                            sdo.ActiveIngrBhytName = beanFirst.ACTIVE_INGR_BHYT_NAME;
                            sdo.MediStockCode = stock.MEDI_STOCK_CODE;
                            sdo.MediStockName = stock.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = stock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = beanFirst.IS_BUSINESS;
                        }
                        else
                        {
                            HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                            if (medicineType == null)
                            {
                                LogSystem.Warn("MedicineTypeId Invalid. Khong lay duoc Loai thuoc tu ram: " + group.Key);
                                continue;
                            }
                            HIS_SERVICE_UNIT serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == medicineType.TDL_SERVICE_UNIT_ID);
                            sdo.Id = medicineType.ID;
                            sdo.IsActive = medicineType.IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MedicineTypeCode = medicineType.MEDICINE_TYPE_CODE;
                            sdo.MedicineTypeName = medicineType.MEDICINE_TYPE_NAME;
                            sdo.MediStockId = stock.ID;
                            sdo.ParentId = medicineType.PARENT_ID;
                            sdo.ServiceId = medicineType.SERVICE_ID;
                            sdo.ManufacturerCode = medicineType.MEDICINE_TYPE_CODE;
                            sdo.NationalName = medicineType.NATIONAL_NAME;
                            sdo.ServiceUnitCode = serviceUnit.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = serviceUnit.SERVICE_UNIT_NAME;
                            sdo.Concentra = medicineType.CONCENTRA;
                            sdo.RegisterNumber = medicineType.REGISTER_NUMBER;
                            sdo.ActiveIngrBhytCode = medicineType.ACTIVE_INGR_BHYT_CODE;
                            sdo.ActiveIngrBhytName = medicineType.ACTIVE_INGR_BHYT_NAME;
                            sdo.MediStockCode = stock.MEDI_STOCK_CODE;
                            sdo.MediStockName = stock.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = stock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = medicineType.IS_BUSINESS;
                        }

                        sdo.ExportedTotalAmount = group.Sum(s => s.AMOUNT);
                        sdo.TotalAmount = 0;
                        sdo.AvailableAmount = 0;
                        sdo.ImpStockTotalAmount = 0;
                        sdo.ImpStockAvailableAmount = 0;
                        if (IsNotNullOrEmpty(beanExpStocks))
                        {
                            sdo.TotalAmount = beanExpStocks.Sum(s => s.AMOUNT);
                            sdo.AvailableAmount = beanExpStocks.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && o.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Sum(s => s.AMOUNT);
                        }
                        if (IsNotNullOrEmpty(beanImpStocks))
                        {
                            sdo.ImpStockTotalAmount = beanImpStocks.Sum(s => s.AMOUNT);
                            sdo.ImpStockAvailableAmount = beanImpStocks.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && o.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Sum(s => s.AMOUNT);
                        }
                        result.Add(sdo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        internal List<HisMedicineTypeInStockSDO> GetInStockMedicineTypeWithBaseInfo(HisMetyStockWithBaseInfoViewFilter filter)
        {
            try
            {
                if (!filter.CABINET_MEDI_STOCK_ID.HasValue)
                    throw new Exception("CABINET_MEDI_STOCK_ID IS NULL");
                if (!filter.EXP_MEDI_STOCK_ID.HasValue)
                    throw new Exception("EXP_MEDI_STOCK_ID IS NULL");

                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == filter.CABINET_MEDI_STOCK_ID.Value);
                if (mediStock == null || mediStock.IS_CABINET != Constant.IS_TRUE)
                {
                    throw new Exception("Id tu truc khong chinh xac: " + filter.CABINET_MEDI_STOCK_ID.Value);
                }

                List<HisMedicineTypeInStockSDO> result = null;
                HisMedicineBeanViewFilterQuery medicineBeanFilter = new HisMedicineBeanViewFilterQuery();
                medicineBeanFilter.MEDI_STOCK_ID = filter.EXP_MEDI_STOCK_ID.Value;
                medicineBeanFilter.ORDER_FIELD = "NUM_ORDER";
                medicineBeanFilter.ORDER_DIRECTION = "DESC";
                medicineBeanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                medicineBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;

                List<V_HIS_MEDICINE_BEAN> vMedicineBeans = new HisMedicineBeanGet().GetView(medicineBeanFilter);

                HisMediStockMetyViewFilterQuery stockMetyFilter = new HisMediStockMetyViewFilterQuery();
                stockMetyFilter.MEDI_STOCK_ID = filter.CABINET_MEDI_STOCK_ID.Value;
                List<V_HIS_MEDI_STOCK_METY> stockMetys = new HisMediStockMetyGet().GetView(stockMetyFilter);


                Dictionary<long, HisMedicineTypeInStockSDO> dic = new Dictionary<long, HisMedicineTypeInStockSDO>();
                if (IsNotNullOrEmpty(vMedicineBeans))
                {
                    foreach (V_HIS_MEDICINE_BEAN bean in vMedicineBeans)
                    {
                        if (dic.ContainsKey(bean.MEDICINE_TYPE_ID))
                        {
                            HisMedicineTypeInStockSDO sdo = dic[bean.MEDICINE_TYPE_ID];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                        }
                        else
                        {
                            HisMedicineTypeInStockSDO sdo = new HisMedicineTypeInStockSDO();
                            sdo.Id = bean.MEDICINE_TYPE_ID;
                            sdo.IsActive = bean.MEDICINE_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MedicineTypeCode = bean.MEDICINE_TYPE_CODE;
                            sdo.MedicineTypeName = bean.MEDICINE_TYPE_NAME;
                            sdo.MediStockId = bean.MEDI_STOCK_ID;
                            sdo.ParentId = bean.PARENT_ID;
                            sdo.ServiceId = bean.SERVICE_ID;
                            sdo.ManufacturerName = bean.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = bean.MEDICINE_TYPE_CODE;
                            sdo.NationalName = bean.NATIONAL_NAME;
                            sdo.ServiceUnitCode = bean.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = bean.SERVICE_UNIT_NAME;
                            sdo.Concentra = bean.CONCENTRA;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.RegisterNumber = bean.REGISTER_NUMBER;
                            sdo.ActiveIngrBhytCode = bean.ACTIVE_INGR_BHYT_CODE;
                            sdo.ActiveIngrBhytName = bean.ACTIVE_INGR_BHYT_NAME;
                            sdo.MediStockCode = bean.MEDI_STOCK_CODE;
                            sdo.MediStockName = bean.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = bean.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = bean.IS_BUSINESS;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MEDICINE_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            dic.Add(bean.MEDICINE_TYPE_ID, sdo);
                        }
                    }

                }

                if (IsNotNullOrEmpty(stockMetys))
                {
                    HisMedicineTypeViewFilterQuery typeFilter = new HisMedicineTypeViewFilterQuery();
                    typeFilter.IDs = stockMetys.Select(s => s.MEDICINE_TYPE_ID).ToList();
                    List<V_HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetView(typeFilter);
                    foreach (var sMety in stockMetys)
                    {
                        if (dic.ContainsKey(sMety.MEDICINE_TYPE_ID))
                        {
                            HisMedicineTypeInStockSDO sdo = dic[sMety.MEDICINE_TYPE_ID];
                            sdo.BaseAmount = (sMety.ALERT_MAX_IN_STOCK ?? 0);
                        }
                        else
                        {
                            HisMedicineTypeInStockSDO sdo = new HisMedicineTypeInStockSDO();
                            V_HIS_MEDICINE_TYPE medicineType = medicineTypes.FirstOrDefault(o => o.ID == sMety.MEDICINE_TYPE_ID);
                            sdo.Id = sMety.MEDICINE_TYPE_ID;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;
                            sdo.MedicineTypeCode = sMety.MEDICINE_TYPE_CODE;
                            sdo.MedicineTypeName = sMety.MEDICINE_TYPE_NAME;
                            sdo.IsGoodsRestrict = mediStock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = mediStock.IS_BUSINESS;
                            sdo.BaseAmount = (sMety.ALERT_MAX_IN_STOCK ?? 0);
                            sdo.AvailableAmount = 0;
                            sdo.TotalAmount = 0;
                            if (medicineType != null)
                            {
                                sdo.IsActive = medicineType.IS_ACTIVE;
                                sdo.MedicineTypeCode = medicineType.MEDICINE_TYPE_CODE;
                                sdo.MedicineTypeName = medicineType.MEDICINE_TYPE_NAME;
                                sdo.ParentId = medicineType.PARENT_ID;
                                sdo.ServiceId = medicineType.SERVICE_ID;
                                sdo.ManufacturerName = medicineType.MANUFACTURER_NAME;
                                sdo.ManufacturerCode = medicineType.MANUFACTURER_CODE;
                                sdo.NationalName = medicineType.NATIONAL_NAME;
                                sdo.ServiceUnitCode = medicineType.SERVICE_UNIT_CODE;
                                sdo.ServiceUnitName = medicineType.SERVICE_UNIT_NAME;
                                sdo.Concentra = medicineType.CONCENTRA;
                                sdo.RegisterNumber = medicineType.REGISTER_NUMBER;
                                sdo.ActiveIngrBhytCode = medicineType.ACTIVE_INGR_BHYT_CODE;
                                sdo.ActiveIngrBhytName = medicineType.ACTIVE_INGR_BHYT_NAME;
                            }
                            dic.Add(sMety.MEDICINE_TYPE_ID, sdo);
                        }
                    }
                }
                if (dic.Values != null)
                {
                    result = dic.Values.ToList();

                    if (filter.IS_AVAILABLE.HasValue && filter.IS_AVAILABLE.Value)
                    {
                        result = result.Where(o => o.AvailableAmount > 0).ToList();
                    }
                    else if (filter.IS_AVAILABLE.HasValue && !filter.IS_AVAILABLE.Value)
                    {
                        result = result.Where(o => o.AvailableAmount <= 0).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal MedicineTypeInHospitalSDO GetInHospitalMedicineType(HisMedicineTypeHospitalViewFilter filter)
        {
            MedicineTypeInHospitalSDO result = new MedicineTypeInHospitalSDO();
            try
            {
                HisMedicineBeanLViewFilterQuery beanFilter = new HisMedicineBeanLViewFilterQuery();
                beanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                beanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                beanFilter.MEDICINE_TYPE_IDs = filter.MEDICINE_TYPE_IDs;
                beanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                beanFilter.IS_BUSINESS = filter.IS_BUSINESS;
                Dictionary<long, ExpandoObject> datas = new Dictionary<long, ExpandoObject>();
                List<string> mediStockNames = new List<string>();
                List<string> mediStockCodes = new List<string>();


                List<L_HIS_MEDICINE_BEAN> medicineBeans = new HisMedicineBeanGet().GetLView(beanFilter);
                if (IsNotNullOrEmpty(medicineBeans))
                {
                    List<long> medicineTypeIds = medicineBeans.Select(s => s.TDL_MEDICINE_TYPE_ID).Distinct().ToList();

                    Dictionary<long, decimal> dicTotalAmount = new Dictionary<long, decimal>();
                    var GroupsByStock = medicineBeans.GroupBy(g => g.MEDI_STOCK_ID ?? 0).ToList();
                    foreach (var group in GroupsByStock)
                    {
                        List<L_HIS_MEDICINE_BEAN> listByStock = group.ToList();
                        L_HIS_MEDICINE_BEAN first = listByStock.FirstOrDefault();
                        string fieldName = String.Format(AMOUNT_FORMAT, group.Key);
                        mediStockNames.Add(first.MEDI_STOCK_NAME);
                        mediStockCodes.Add(fieldName);

                        Dictionary<long, List<L_HIS_MEDICINE_BEAN>> Groups = listByStock.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToDictionary(d => d.Key, d => d.ToList());
                        foreach (var metyId in medicineTypeIds)
                        {
                            HIS_MEDICINE_TYPE parent = null;
                            HIS_MEDICINE_TYPE currMedicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == metyId);
                            if (currMedicineType != null && currMedicineType.PARENT_ID.HasValue)
                            {
                                parent = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == currMedicineType.PARENT_ID.Value);
                            }

                            decimal amount = 0;

                            ExpandoObject dt = new ExpandoObject();
                            if (datas.ContainsKey(metyId))
                                dt = datas[metyId];
                            else datas[metyId] = dt;

                            if (Groups.ContainsKey(metyId))
                            {
                                List<L_HIS_MEDICINE_BEAN> list = Groups[metyId];
                                L_HIS_MEDICINE_BEAN f = list.FirstOrDefault();
                                amount = list.Sum(s => s.AMOUNT);

                                CommonUtil.AddProperty(dt, ID, f.TDL_MEDICINE_TYPE_ID);
                                CommonUtil.AddProperty(dt, METY_CODE, f.MEDICINE_TYPE_CODE);
                                CommonUtil.AddProperty(dt, METY_NAME, f.MEDICINE_TYPE_NAME);
                                CommonUtil.AddProperty(dt, CONCENTRA, f.CONCENTRA);
                                CommonUtil.AddProperty(dt, ACTIVE_CODE, f.ACTIVE_INGR_BHYT_CODE);
                                CommonUtil.AddProperty(dt, ACTIVE_NAME, f.ACTIVE_INGR_BHYT_NAME);
                                CommonUtil.AddProperty(dt, REGISTER, f.REGISTER_NUMBER);
                                CommonUtil.AddProperty(dt, SERVICE_UNIT_NAME, f.SERVICE_UNIT_NAME);
                                CommonUtil.AddProperty(dt, PARENT_TYPE_NAME, parent != null ? parent.MEDICINE_TYPE_NAME : "");
                            }

                            if (!dicTotalAmount.ContainsKey(metyId)) dicTotalAmount[metyId] = 0;
                            dicTotalAmount[metyId] += amount;

                            CommonUtil.AddProperty(dt, fieldName, amount);
                        }
                    }

                    foreach (var dic in dicTotalAmount)
                    {
                        ExpandoObject dt = datas[dic.Key];
                        CommonUtil.AddProperty(dt, TOTAL_AMOUNT, dic.Value);
                    }
                }
                result.MediStockNames = mediStockNames;
                result.MediStockCodes = mediStockCodes;
                result.MedicineTypeDatas = datas.Values.ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        internal MedicineInHospitalSDO GetInHospitalMedicine(HisMedicineTypeHospitalViewFilter filter)
        {
            //MedicineInHospitalSDO result = new MedicineInHospitalSDO();
            //try
            //{
            //    HisMedicineBeanViewFilterQuery beanFilter = new HisMedicineBeanViewFilterQuery();
            //    beanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
            //    beanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
            //    beanFilter.MEDICINE_TYPE_IDs = filter.MEDICINE_TYPE_IDs;
            //    beanFilter.MEDICINE_TYPE_IS_ACTIVE = filter.IS_ACTIVE == Constant.IS_TRUE;
            //    //beanFilter.IS_BUSINESS = filter.IS_BUSINESS == Constant.IS_TRUE;

            //    Dictionary<long, ExpandoObject> datas = new Dictionary<long, ExpandoObject>();
            //    List<string> mediStockNames = new List<string>();
            //    List<string> mediStockCodes = new List<string>();

            //    List<L_HIS_MEDICINE_BEAN> medicineBeans = new HisMedicineBeanGet().GetLView(beanFilter);
            //    if (IsNotNullOrEmpty(medicineBeans))
            //    {
            //        List<long> medicineTypeIds = medicineBeans.Select(s => s.TDL_MEDICINE_TYPE_ID).Distinct().ToList();

            //        Dictionary<long, decimal> dicTotalAmount = new Dictionary<long, decimal>();
            //        var GroupsByStock = medicineBeans.GroupBy(g => g.MEDI_STOCK_ID ?? 0).ToList();
            //        foreach (var group in GroupsByStock)
            //        {
            //            List<L_HIS_MEDICINE_BEAN> listByStock = group.ToList();
            //            L_HIS_MEDICINE_BEAN first = listByStock.FirstOrDefault();
            //            string fieldName = String.Format(AMOUNT_FORMAT, group.Key);
            //            mediStockNames.Add(first.MEDI_STOCK_NAME);
            //            mediStockCodes.Add(fieldName);
            //            //mã thuốc, tên thuốc, mã hoạt chất, tên hoạt chất, hàm lượng, Số đăng ký , Tổng tồn, Số lượng tồn trong kho, Số lô, hạn sử dụng, giá nhập, VAT
            //            Dictionary<long, List<L_HIS_MEDICINE_BEAN>> Groups = listByStock.GroupBy(g => new 
            //            {
            //                g.TDL_MEDICINE_TYPE_ID,
            //                g.MEDICINE_TYPE_CODE,
            //                g.MEDICINE_TYPE_NAME,
            //                g.ACTIVE_INGR_BHYT_CODE,
            //                g.ACTIVE_INGR_BHYT_NAME,
            //                g.CONCENTRA,
            //                g.REGISTER_NUMBER,
            //                g.
            //            }).ToDictionary(d => d.Key, d => d.ToList());
            //            foreach (var metyId in medicineTypeIds)
            //            {
            //                decimal amount = 0;

            //                ExpandoObject dt = new ExpandoObject();
            //                if (datas.ContainsKey(metyId))
            //                    dt = datas[metyId];
            //                else datas[metyId] = dt;

            //                if (Groups.ContainsKey(metyId))
            //                {
            //                    List<L_HIS_MEDICINE_BEAN> list = Groups[metyId];
            //                    L_HIS_MEDICINE_BEAN f = list.FirstOrDefault();
            //                    amount = list.Sum(s => s.AMOUNT);

            //                    CommonUtil.AddProperty(dt, ID, f.TDL_MEDICINE_TYPE_ID);
            //                    CommonUtil.AddProperty(dt, METY_CODE, f.MEDICINE_TYPE_CODE);
            //                    CommonUtil.AddProperty(dt, METY_NAME, f.MEDICINE_TYPE_NAME);
            //                    CommonUtil.AddProperty(dt, CONCENTRA, f.CONCENTRA);
            //                    CommonUtil.AddProperty(dt, ACTIVE_CODE, f.ACTIVE_INGR_BHYT_CODE);
            //                    CommonUtil.AddProperty(dt, ACTIVE_NAME, f.ACTIVE_INGR_BHYT_NAME);
            //                    CommonUtil.AddProperty(dt, REGISTER, f.REGISTER_NUMBER);
            //                    CommonUtil.AddProperty(dt, SERVICE_UNIT_NAME, f.SERVICE_UNIT_NAME);
            //                }

            //                if (!dicTotalAmount.ContainsKey(metyId)) dicTotalAmount[metyId] = 0;
            //                dicTotalAmount[metyId] += amount;

            //                CommonUtil.AddProperty(dt, fieldName, amount);
            //            }
            //        }

            //        foreach (var dic in dicTotalAmount)
            //        {
            //            ExpandoObject dt = datas[dic.Key];
            //            CommonUtil.AddProperty(dt, TOTAL_AMOUNT, dic.Value);
            //        }
            //    }
            //    result.MediStockNames = mediStockNames;
            //    result.MediStockCodes = mediStockCodes;
            //    result.MedicineTypeDatas = datas.Values.ToList();
            //}
            //catch (Exception ex)
            //{
            //    Inventec.Common.Logging.LogSystem.Error(ex);
            //    result = null;
            //}
            //return result;
            return null;
        }
    }
}