using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMediStockMaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeGet : GetBase
    {

        private const string ID = "ID";
        private const string MATY_CODE = "MATERIAL_TYPE_CODE";
        private const string MATY_NAME = "MATERIAL_TYPE_NAME";
        private const string CONCENTRA = "CONCENTRA";
        private const string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        private const string SERVICE_UNIT_NAME = "SERVICE_UNIT_NAME";
        private const string PARENT_TYPE_NAME = "PARENT_TYPE_NAME";

        private static string AMOUNT_FORMAT = "AMOUNT_{0}";
        internal List<HisMaterialTypeInStockSDO> GetInStockMaterialType(HisMaterialTypeStockViewFilter filter)
        {
            try
            {
                List<HisMaterialTypeInStockSDO> result = null;
                HisMaterialBeanViewFilterQuery materialBeanFilter = new HisMaterialBeanViewFilterQuery();
                materialBeanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                materialBeanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                materialBeanFilter.MATERIAL_TYPE_ID = filter.ID;
                materialBeanFilter.MATERIAL_TYPE_IDs = filter.IDs;
                materialBeanFilter.MATERIAL_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                materialBeanFilter.ORDER_FIELD = "NUM_ORDER";
                materialBeanFilter.ORDER_DIRECTION = "DESC";
                materialBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;
                materialBeanFilter.MATERIAL_TYPE_IDs = filter.MATERIAL_TYPE_IDs;
                materialBeanFilter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL = filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL;

                List<V_HIS_MATERIAL_BEAN> vHisMaterialBeans = new HisMaterialBeanGet().GetView(materialBeanFilter);
                if (IsNotNullOrEmpty(vHisMaterialBeans))
                {
                    result = new List<HisMaterialTypeInStockSDO>();
                    Dictionary<string, HisMaterialTypeInStockSDO> dic = new Dictionary<string, HisMaterialTypeInStockSDO>();

                    foreach (V_HIS_MATERIAL_BEAN bean in vHisMaterialBeans)
                    {
                        string key = string.Format("{0}|{1}", bean.MATERIAL_TYPE_ID, bean.MEDI_STOCK_ID);
                        if (dic.ContainsKey(key))
                        {
                            HisMaterialTypeInStockSDO sdo = dic[key];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                        }
                        else
                        {
                            HisMaterialTypeInStockSDO sdo = new HisMaterialTypeInStockSDO();
                            sdo.Id = bean.MATERIAL_TYPE_ID;
                            sdo.IsActive = bean.MATERIAL_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//material_bean thi is_leaf luon la true
                            sdo.MaterialTypeCode = bean.MATERIAL_TYPE_CODE;
                            sdo.MaterialTypeName = bean.MATERIAL_TYPE_NAME;
                            sdo.MediStockId = bean.MEDI_STOCK_ID;
                            sdo.ParentId = bean.PARENT_ID;
                            sdo.ServiceId = bean.SERVICE_ID;
                            sdo.ManufacturerName = bean.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = bean.MATERIAL_TYPE_CODE;
                            sdo.NationalName = bean.NATIONAL_NAME;
                            sdo.ServiceUnitCode = bean.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = bean.SERVICE_UNIT_NAME;
                            sdo.NumOrder = bean.NUM_ORDER;
                            sdo.MediStockCode = bean.MEDI_STOCK_CODE;
                            sdo.MediStockName = bean.MEDI_STOCK_NAME;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.IsGoodsRestrict = bean.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = bean.IS_BUSINESS;
                            sdo.Concentra = bean.CONCENTRA;
                            sdo.LastExpPrice = bean.LAST_EXP_PRICE;
                            sdo.LastExpVatRatio = bean.LAST_EXP_VAT_RATIO;
                            sdo.LastExpiredDate = bean.LAST_EXPIRED_DATE;
                            sdo.MaterialTypeMapId = bean.MATERIAL_TYPE_MAP_ID;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            dic.Add(key, sdo);
                        }
                    }

                    //sort lai du lieu
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

        internal List<HisMaterialTypeView1SDO> GetPriceLists(HisMaterialTypeView1SDOFilter filter)
        {
            List<HisMaterialTypeView1SDO> result = null;
            try
            {
                HisMaterialTypeView1FilterQuery mediFilter = new HisMaterialTypeView1FilterQuery();
                mediFilter.IS_BUSINESS = filter.IS_BUSINESS;
                mediFilter.IS_ACTIVE = Constant.IS_TRUE;
                if (IsNotNullOrEmpty(filter.ColumnParams))
                {
                    mediFilter.ColumnParams = filter.ColumnParams;
                }

                List<HisMaterialTypeView1DTO> dtos = new HisMaterialTypeGet().GetView1Dynamic(mediFilter);
                if (IsNotNullOrEmpty(dtos) && filter.PARENT_ID.HasValue)
                {
                    dtos = this.GetChilds(filter.PARENT_ID.Value, dtos);
                }
                if (IsNotNullOrEmpty(dtos))
                {
                    result = new List<HisMaterialTypeView1SDO>();
                    Mapper.CreateMap<HisMaterialTypeView1DTO, HisMaterialTypeView1SDO>();
                    List<long> medicineIds = new List<long>();
                    foreach (var item in dtos)
                    {
                        if (item.IS_LEAF != Constant.IS_TRUE) continue;
                        if (!String.IsNullOrWhiteSpace(item.MATERIAL_INFO))
                        {
                            HisMaterialTypeView1SDO sdo = Mapper.Map<HisMaterialTypeView1SDO>(item);
                            this.ProcessMaterialInfo(sdo, item.MATERIAL_INFO);
                            if (sdo.MATERIAL_ID.HasValue)
                            {
                                if (sdo.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE)
                                {
                                    sdo.EXP_PRICE = sdo.MATERIAL_IMP_PRICE ?? 0;
                                    sdo.EXP_VAT_RATIO = sdo.MATERIAL_IMP_VAT_RATIO ?? 0;
                                    sdo.PRICE = sdo.EXP_PRICE.Value * (1 + sdo.EXP_VAT_RATIO.Value);
                                }
                                else
                                {
                                    medicineIds.Add(sdo.MATERIAL_ID.Value);
                                }
                            }
                            result.Add(sdo);
                        }
                    }

                    if (IsNotNullOrEmpty(medicineIds))
                    {
                        HisMaterialPatyFilterQuery patyFilter = new HisMaterialPatyFilterQuery();
                        patyFilter.MATERIAL_IDs = medicineIds;
                        patyFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        List<HIS_MATERIAL_PATY> medicinePatys = new HisMaterialPatyGet().Get(patyFilter);
                        if (IsNotNullOrEmpty(medicinePatys))
                        {
                            medicinePatys = medicinePatys.OrderByDescending(o => o.ID).ToList();
                            foreach (var sdo in result)
                            {
                                if (!sdo.MATERIAL_ID.HasValue) continue;
                                var paty = medicinePatys.FirstOrDefault(o => o.MATERIAL_ID == sdo.MATERIAL_ID.Value);
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

        private void ProcessMaterialInfo(HisMaterialTypeView1SDO sdo, string medicineInfo)
        {
            if (!String.IsNullOrWhiteSpace(medicineInfo))
            {
                List<string> listInfo = medicineInfo.Split('|').ToList();
                if (listInfo == null || listInfo.Count != 5)
                {
                    LogSystem.Warn("MaterialInfo Invalid: " + medicineInfo);
                    return;
                }
                long medicineId = 0;
                if (long.TryParse(listInfo[0], out medicineId))
                {
                    sdo.MATERIAL_ID = medicineId;
                }
                long impTime = 0;
                if (long.TryParse(listInfo[1], out impTime))
                {
                    sdo.IMP_TIME = impTime;
                }

                decimal impPrice = 0;
                if (decimal.TryParse(listInfo[2], out impPrice))
                {
                    sdo.MATERIAL_IMP_PRICE = impPrice;
                }

                decimal impVat = 0;
                if (decimal.TryParse(listInfo[3], out impVat))
                {
                    sdo.MATERIAL_IMP_VAT_RATIO = impVat;
                }

                short isSaleEqualImpPrice = 0;
                if (short.TryParse(listInfo[4], out isSaleEqualImpPrice))
                {
                    sdo.IS_SALE_EQUAL_IMP_PRICE = isSaleEqualImpPrice;
                }
            }
        }

        private List<HisMaterialTypeView1DTO> GetChilds(long parentId, List<HisMaterialTypeView1DTO> datas)
        {
            List<HisMaterialTypeView1DTO> result = new List<HisMaterialTypeView1DTO>();
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

        internal List<HisMaterialTypeInStockSDO> GetInStockMaterialTypeWithImpStock(HisMatyStockWithImpStockViewFilter filter)
        {
            List<HisMaterialTypeInStockSDO> result = null;
            try
            {
                if (!filter.MEDI_STOCK_ID.HasValue || !filter.IMP_MEDI_STOCK_ID.HasValue
                    || !filter.EXP_DATE_FROM.HasValue || !filter.EXP_DATE_TO.HasValue)
                {
                    throw new Exception("filter.MEDI_STOCK_ID or filter.IMP_MEDI_STOCK_ID or filter.EXP_DATE_FROM or filter.EXP_DATE_TO is null");
                }

                HisExpMestMaterialFilterQuery expMaterialFilter = new HisExpMestMaterialFilterQuery();
                expMaterialFilter.EXP_DATE_FROM = filter.EXP_DATE_FROM;
                expMaterialFilter.EXP_DATE_TO = filter.EXP_DATE_TO;
                expMaterialFilter.TDL_MEDI_STOCK_ID = filter.IMP_MEDI_STOCK_ID;
                expMaterialFilter.IS_EXPORT = true;
                expMaterialFilter.ColumnParams = new List<string>();
                expMaterialFilter.ColumnParams.Add("AMOUNT");
                expMaterialFilter.ColumnParams.Add("TDL_MATERIAL_TYPE_ID");

                List<HisExpMestMaterialDTO> expMestMaterialDTOs = new HisExpMestMaterialGet().GetDynamic(expMaterialFilter);

                if (IsNotNullOrEmpty(expMestMaterialDTOs))
                {
                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == filter.MEDI_STOCK_ID.Value);

                    HisMaterialBeanViewFilterQuery medicineBeanFilter = new HisMaterialBeanViewFilterQuery();
                    medicineBeanFilter.MEDI_STOCK_IDs = new List<long>() { filter.MEDI_STOCK_ID.Value, filter.IMP_MEDI_STOCK_ID.Value };
                    medicineBeanFilter.ORDER_FIELD = "NUM_ORDER";
                    medicineBeanFilter.ORDER_DIRECTION = "DESC";
                    medicineBeanFilter.MATERIAL_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                    medicineBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;
                    medicineBeanFilter.MATERIAL_TYPE_IDs = expMestMaterialDTOs.Select(s => s.TDL_MATERIAL_TYPE_ID ?? 0).Distinct().ToList();

                    List<V_HIS_MATERIAL_BEAN> vMaterialBeans = new HisMaterialBeanGet().GetView(medicineBeanFilter);

                    result = new List<HisMaterialTypeInStockSDO>();
                    var Groups = expMestMaterialDTOs.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        var beanExpStocks = vMaterialBeans != null ? vMaterialBeans.Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key && o.MEDI_STOCK_ID == filter.MEDI_STOCK_ID).ToList() : null;
                        var beanImpStocks = vMaterialBeans != null ? vMaterialBeans.Where(o => o.TDL_MATERIAL_TYPE_ID == group.Key && o.MEDI_STOCK_ID == filter.IMP_MEDI_STOCK_ID).ToList() : null;
                        HisMaterialTypeInStockSDO sdo = new HisMaterialTypeInStockSDO();
                        var beanFirst = IsNotNullOrEmpty(beanExpStocks) ? beanExpStocks.FirstOrDefault() : (IsNotNullOrEmpty(beanImpStocks) ? beanImpStocks.FirstOrDefault() : null);
                        if (beanFirst != null)
                        {
                            sdo.Id = beanFirst.MATERIAL_TYPE_ID;
                            sdo.IsActive = beanFirst.MATERIAL_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MaterialTypeCode = beanFirst.MATERIAL_TYPE_CODE;
                            sdo.MaterialTypeName = beanFirst.MATERIAL_TYPE_NAME;
                            sdo.MediStockId = stock.ID;
                            sdo.ParentId = beanFirst.PARENT_ID;
                            sdo.ServiceId = beanFirst.SERVICE_ID;
                            sdo.ManufacturerName = beanFirst.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = beanFirst.MATERIAL_TYPE_CODE;
                            sdo.NationalName = beanFirst.NATIONAL_NAME;
                            sdo.ServiceUnitCode = beanFirst.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = beanFirst.SERVICE_UNIT_NAME;
                            sdo.Concentra = beanFirst.CONCENTRA;
                            sdo.MediStockCode = stock.MEDI_STOCK_CODE;
                            sdo.MediStockName = stock.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = stock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = beanFirst.IS_BUSINESS;
                            sdo.MaterialTypeMapId = beanFirst.MATERIAL_TYPE_MAP_ID;
                        }
                        else
                        {
                            HIS_MATERIAL_TYPE medicineType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                            if (medicineType == null)
                            {
                                LogSystem.Warn("MaterialTypeId Invalid. Khong lay duoc Loai thuoc tu ram: " + group.Key);
                                continue;
                            }
                            HIS_SERVICE_UNIT serviceUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == medicineType.TDL_SERVICE_UNIT_ID);
                            sdo.Id = medicineType.ID;
                            sdo.IsActive = medicineType.IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//medicine_bean thi is_leaf luon la true
                            sdo.MaterialTypeCode = medicineType.MATERIAL_TYPE_CODE;
                            sdo.MaterialTypeName = medicineType.MATERIAL_TYPE_NAME;
                            sdo.MediStockId = stock.ID;
                            sdo.ParentId = medicineType.PARENT_ID;
                            sdo.ServiceId = medicineType.SERVICE_ID;
                            sdo.ManufacturerCode = medicineType.MATERIAL_TYPE_CODE;
                            sdo.NationalName = medicineType.NATIONAL_NAME;
                            sdo.ServiceUnitCode = serviceUnit.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = serviceUnit.SERVICE_UNIT_NAME;
                            sdo.Concentra = medicineType.CONCENTRA;
                            sdo.MediStockCode = stock.MEDI_STOCK_CODE;
                            sdo.MediStockName = stock.MEDI_STOCK_NAME;
                            sdo.IsGoodsRestrict = stock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = medicineType.IS_BUSINESS;
                            sdo.MaterialTypeMapId = medicineType.MATERIAL_TYPE_MAP_ID;
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
                                && o.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Sum(s => s.AMOUNT);
                        }
                        if (IsNotNullOrEmpty(beanImpStocks))
                        {
                            sdo.ImpStockTotalAmount = beanImpStocks.Sum(s => s.AMOUNT);
                            sdo.ImpStockAvailableAmount = beanImpStocks.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && o.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Sum(s => s.AMOUNT);
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

        internal List<HisMaterialTypeInStockSDO> GetInStockMaterialTypeWithBaseInfo(HisMatyStockWithBaseInfoViewFilter filter)
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

                List<HisMaterialTypeInStockSDO> result = null;
                HisMaterialBeanViewFilterQuery materialBeanFilter = new HisMaterialBeanViewFilterQuery();
                materialBeanFilter.MEDI_STOCK_ID = filter.EXP_MEDI_STOCK_ID.Value;
                materialBeanFilter.MATERIAL_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                materialBeanFilter.ORDER_FIELD = "NUM_ORDER";
                materialBeanFilter.ORDER_DIRECTION = "DESC";
                materialBeanFilter.IS_GOODS_RESTRICT = filter.IS_GOODS_RESTRICT;

                List<V_HIS_MATERIAL_BEAN> vHisMaterialBeans = new HisMaterialBeanGet().GetView(materialBeanFilter);

                HisMediStockMatyViewFilterQuery stockMatyFilter = new HisMediStockMatyViewFilterQuery();
                stockMatyFilter.MEDI_STOCK_ID = filter.CABINET_MEDI_STOCK_ID.Value;
                List<V_HIS_MEDI_STOCK_MATY> stockMatys = new HisMediStockMatyGet().GetView(stockMatyFilter);

                Dictionary<long, HisMaterialTypeInStockSDO> dic = new Dictionary<long, HisMaterialTypeInStockSDO>();

                if (IsNotNullOrEmpty(vHisMaterialBeans))
                {
                    foreach (V_HIS_MATERIAL_BEAN bean in vHisMaterialBeans)
                    {
                        if (dic.ContainsKey(bean.MATERIAL_TYPE_ID))
                        {
                            HisMaterialTypeInStockSDO sdo = dic[bean.MATERIAL_TYPE_ID];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                        }
                        else
                        {
                            HisMaterialTypeInStockSDO sdo = new HisMaterialTypeInStockSDO();
                            sdo.Id = bean.MATERIAL_TYPE_ID;
                            sdo.IsActive = bean.MATERIAL_TYPE_IS_ACTIVE;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;//material_bean thi is_leaf luon la true
                            sdo.MaterialTypeCode = bean.MATERIAL_TYPE_CODE;
                            sdo.MaterialTypeName = bean.MATERIAL_TYPE_NAME;
                            sdo.MediStockId = bean.MEDI_STOCK_ID;
                            sdo.ParentId = bean.PARENT_ID;
                            sdo.ServiceId = bean.SERVICE_ID;
                            sdo.ManufacturerName = bean.MANUFACTURER_NAME;
                            sdo.ManufacturerCode = bean.MATERIAL_TYPE_CODE;
                            sdo.NationalName = bean.NATIONAL_NAME;
                            sdo.ServiceUnitCode = bean.SERVICE_UNIT_CODE;
                            sdo.ServiceUnitName = bean.SERVICE_UNIT_NAME;
                            sdo.NumOrder = bean.NUM_ORDER;
                            sdo.MediStockCode = bean.MEDI_STOCK_CODE;
                            sdo.MediStockName = bean.MEDI_STOCK_NAME;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.IsGoodsRestrict = bean.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = bean.IS_BUSINESS;
                            sdo.Concentra = bean.CONCENTRA;
                            sdo.MaterialTypeMapId = bean.MATERIAL_TYPE_MAP_ID;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            dic.Add(bean.MATERIAL_TYPE_ID, sdo);
                        }
                    }
                }

                if (IsNotNullOrEmpty(stockMatys))
                {
                    HisMaterialTypeViewFilterQuery typeFilter = new HisMaterialTypeViewFilterQuery();
                    typeFilter.IDs = stockMatys.Select(s => s.MATERIAL_TYPE_ID).ToList();
                    List<V_HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetView(typeFilter);
                    foreach (var sMaty in stockMatys)
                    {
                        if (dic.ContainsKey(sMaty.MATERIAL_TYPE_ID))
                        {
                            HisMaterialTypeInStockSDO sdo = dic[sMaty.MATERIAL_TYPE_ID];
                            sdo.BaseAmount = (sMaty.ALERT_MAX_IN_STOCK ?? 0);
                        }
                        else
                        {
                            HisMaterialTypeInStockSDO sdo = new HisMaterialTypeInStockSDO();
                            V_HIS_MATERIAL_TYPE materialType = materialTypes.FirstOrDefault(o => o.ID == sMaty.MATERIAL_TYPE_ID);
                            sdo.Id = sMaty.MATERIAL_TYPE_ID;
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;
                            sdo.MaterialTypeCode = sMaty.MATERIAL_TYPE_CODE;
                            sdo.MaterialTypeName = sMaty.MATERIAL_TYPE_NAME;
                            sdo.IsGoodsRestrict = mediStock.IS_GOODS_RESTRICT;
                            sdo.IsBusiness = mediStock.IS_BUSINESS;
                            sdo.BaseAmount = (sMaty.ALERT_MAX_IN_STOCK ?? 0);
                            sdo.AvailableAmount = 0;
                            sdo.TotalAmount = 0;
                            if (materialType != null)
                            {
                                sdo.IsActive = materialType.IS_ACTIVE;
                                sdo.MaterialTypeCode = materialType.MATERIAL_TYPE_CODE;
                                sdo.MaterialTypeName = materialType.MATERIAL_TYPE_NAME;
                                sdo.ParentId = materialType.PARENT_ID;
                                sdo.ServiceId = materialType.SERVICE_ID;
                                sdo.ManufacturerName = materialType.MANUFACTURER_NAME;
                                sdo.ManufacturerCode = materialType.MANUFACTURER_CODE;
                                sdo.NationalName = materialType.NATIONAL_NAME;
                                sdo.ServiceUnitCode = materialType.SERVICE_UNIT_CODE;
                                sdo.ServiceUnitName = materialType.SERVICE_UNIT_NAME;
                                sdo.Concentra = materialType.CONCENTRA;
                                sdo.MaterialTypeMapId = materialType.MATERIAL_TYPE_MAP_ID;
                            }
                            dic.Add(sMaty.MATERIAL_TYPE_ID, sdo);
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

        internal MaterialTypeInHospitalSDO GetInHospitalMaterialType(HisMaterialTypeHospitalViewFilter filter)
        {
            MaterialTypeInHospitalSDO result = new MaterialTypeInHospitalSDO();
            try
            {
                HisMaterialBeanLViewFilterQuery beanFilter = new HisMaterialBeanLViewFilterQuery();
                beanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                beanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                beanFilter.MATERIAL_TYPE_IDs = filter.MATERIAL_TYPE_IDs;
                beanFilter.MATERIAL_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                beanFilter.IS_BUSINESS = filter.IS_BUSINESS;
                Dictionary<long, ExpandoObject> datas = new Dictionary<long, ExpandoObject>();
                List<string> mediStockNames = new List<string>();
                List<string> mediStockCodes = new List<string>();


                List<L_HIS_MATERIAL_BEAN> medicineBeans = new HisMaterialBeanGet().GetLView(beanFilter);
                if (IsNotNullOrEmpty(medicineBeans))
                {
                    List<long> medicineTypeIds = medicineBeans.Select(s => s.TDL_MATERIAL_TYPE_ID).Distinct().ToList();

                    Dictionary<long, decimal> dicTotalAmount = new Dictionary<long, decimal>();
                    var GroupsByStock = medicineBeans.GroupBy(g => g.MEDI_STOCK_ID ?? 0).ToList();
                    foreach (var group in GroupsByStock)
                    {
                        List<L_HIS_MATERIAL_BEAN> listByStock = group.ToList();
                        L_HIS_MATERIAL_BEAN first = listByStock.FirstOrDefault();
                        string fieldName = String.Format(AMOUNT_FORMAT, group.Key);
                        mediStockNames.Add(first.MEDI_STOCK_NAME);
                        mediStockCodes.Add(fieldName);

                        Dictionary<long, List<L_HIS_MATERIAL_BEAN>> Groups = listByStock.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToDictionary(d => d.Key, d => d.ToList());
                        foreach (var matyId in medicineTypeIds)
                        {
                            HIS_MATERIAL_TYPE parent = null;
                            HIS_MATERIAL_TYPE currMaterialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == matyId);
                            if (currMaterialType != null && currMaterialType.PARENT_ID.HasValue)
                            {
                                parent = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == currMaterialType.PARENT_ID.Value);
                            }

                            decimal amount = 0;

                            ExpandoObject dt = new ExpandoObject();
                            if (datas.ContainsKey(matyId))
                                dt = datas[matyId];
                            else datas[matyId] = dt;

                            if (Groups.ContainsKey(matyId))
                            {
                                List<L_HIS_MATERIAL_BEAN> list = Groups[matyId];
                                L_HIS_MATERIAL_BEAN f = list.FirstOrDefault();
                                amount = list.Sum(s => s.AMOUNT);

                                CommonUtil.AddProperty(dt, ID, f.TDL_MATERIAL_TYPE_ID);
                                CommonUtil.AddProperty(dt, MATY_CODE, f.MATERIAL_TYPE_CODE);
                                CommonUtil.AddProperty(dt, MATY_NAME, f.MATERIAL_TYPE_NAME);
                                CommonUtil.AddProperty(dt, CONCENTRA, f.CONCENTRA);
                                CommonUtil.AddProperty(dt, SERVICE_UNIT_NAME, f.SERVICE_UNIT_NAME);
                                CommonUtil.AddProperty(dt, PARENT_TYPE_NAME, parent != null ? parent.MATERIAL_TYPE_NAME : "");
                            }

                            if (!dicTotalAmount.ContainsKey(matyId)) dicTotalAmount[matyId] = 0;
                            dicTotalAmount[matyId] += amount;

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
                result.MaterialTypeDatas = datas.Values.ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

    }
}
