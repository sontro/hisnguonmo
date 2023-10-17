using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMediStockMaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MOS.MANAGER.HisMaterial
{
    partial class HisMaterialGet : GetBase
    {
        private const string MATERIAL_PREFIX = "M";
        private const string MATERIAL_TYPE_PREFIX = "T";

        /// <summary>
        /// Lay danh sach material voi thong tin ton, kha dung ket hop voi tree material_type
        /// Va sap xep theo so luong ton kho cua thuoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMaterialInStockSDO> GetInStockMaterialWithTypeTreeOrderByAmount(HisMaterialStockViewFilter filter)
        {
            try
            {
                List<HisMaterialInStockSDO> inStockMaterials = this.GetInStockMaterial(filter);
                List<V_HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetView(new HisMaterialTypeViewFilterQuery());
                List<HisMaterialInStockSDO> list = this.GetInStockMaterialWithTypeTree(inStockMaterials, materialTypes, "");

                List<HisMaterialInStockSDO> sortedList = null;
                if (list != null)
                {
                    List<HisMaterialInStockSDO> rootList = list.Where(o => string.IsNullOrEmpty(o.ParentNodeId)).ToList();
                    if (rootList != null && rootList.Count > 0)
                    {
                        sortedList = new List<HisMaterialInStockSDO>();
                        this.InStockMaterialTraversalToSortByAmount(rootList, list, sortedList);
                    }
                }
                return sortedList;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Lay danh sach material voi thong tin ton, kha dung ket hop voi tree material_type
        /// Va sap xep theo han su dung cua thuoc
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<List<HisMaterialInStockSDO>> GetInStockMaterialWithTypeTreeOrderByExpiredDate(HisMaterialStockViewFilter filter)
        {
            try
            {
                List<List<HisMaterialInStockSDO>> result = null;
                List<HisMaterialInStockSDO> inStockMaterials = this.GetInStockMaterial(filter);
                List<V_HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetView(new HisMaterialTypeViewFilterQuery());

                if (IsNotNullOrEmpty(inStockMaterials) && IsNotNullOrEmpty(materialTypes))
                {
                    result = new List<List<HisMaterialInStockSDO>>();
                    //Sap xep cac danh sach co HSD sap het len dau va thuoc ko co HSD de cuoi cung
                    var groups = inStockMaterials.GroupBy(o => o.EXPIRED_DATE).OrderByDescending(o => o.Key.HasValue).ThenBy(o => o.Key);
                    foreach (var group in groups)
                    {
                        var expiredDate = group.Key;
                        string key = expiredDate.HasValue ? expiredDate.ToString() : "null";
                        List<HisMaterialInStockSDO> subListWithTree = this.GetInStockMaterialWithTypeTree(group.ToList<HisMaterialInStockSDO>(), materialTypes, key);
                        subListWithTree.ForEach(em => em.EXPIRED_DATE = expiredDate);

                        List<HisMaterialInStockSDO> rootList = subListWithTree.Where(o => string.IsNullOrEmpty(o.ParentNodeId)).ToList();
                        List<HisMaterialInStockSDO> sortedList = null;
                        if (rootList != null && rootList.Count > 0)
                        {
                            sortedList = new List<HisMaterialInStockSDO>();
                            this.InStockMaterialTraversalToSortByExpiredDate(rootList, subListWithTree, sortedList);
                        }
                        result.Add(sortedList);
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

        /// <summary>
        /// Lay danh sach cac medincine voi du lieu ton va kha dung
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMaterialInStockSDO> GetInStockMaterial(HisMaterialStockViewFilter filter)
        {
            try
            {
                List<HisMaterialInStockSDO> result = new List<HisMaterialInStockSDO>();

                //Lay ra danh sach toan bo material_bean
                HisMaterialBeanViewFilterQuery materialBeanFilter = new HisMaterialBeanViewFilterQuery();
                materialBeanFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                materialBeanFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                materialBeanFilter.MATERIAL_ID = filter.ID;
                materialBeanFilter.MATERIAL_IDs = filter.IDs;
                materialBeanFilter.KEY_WORD = filter.KEY_WORD;
                materialBeanFilter.ORDER_FIELD = filter.ORDER_FIELD;
                materialBeanFilter.ORDER_DIRECTION = filter.ORDER_DIRECTION;
                //materialBeanFilter.MATERIAL_IS_ACTIVE = filter.IS_ACTIVE;
                materialBeanFilter.MATERIAL_TYPE_IS_ACTIVE = filter.MATERIAL_TYPE_IS_ACTIVE;
                materialBeanFilter.MATERIAL_TYPE_ID = filter.MATERIAL_TYPE_ID;
                materialBeanFilter.IN_STOCK = MOS.Filter.HisMaterialBeanViewFilter.InStockEnum.YES;
                materialBeanFilter.MATERIAL_IS_ACTIVE = filter.MATERIAL_IS_ACTIVE;

                List<V_HIS_MATERIAL_BEAN> vMaterialBeans = new HisMaterialBeanGet().GetView(materialBeanFilter);

                List<HIS_MEDI_STOCK_MATY> mediStockMatys = null;

                //co lay thong tin gia ban khong
                bool includeExpPrice = (filter.INCLUDE_EXP_PRICE && filter.EXP_PATIENT_TYPE_ID.HasValue);
                List<HIS_MATERIAL_PATY> materialPatys = this.GetMaterialPaty(vMaterialBeans, filter);

                //Chi lay cac thong tin cau hinh "thuoc-kho" trong truong hop xem theo tung kho
                bool showByOneMedistock = filter.MEDI_STOCK_ID.HasValue || (filter.MEDI_STOCK_IDs != null && filter.MEDI_STOCK_IDs.Count == 1);
                if (showByOneMedistock)
                {
                    HisMediStockMatyFilterQuery stockMatyFilter = new HisMediStockMatyFilterQuery();
                    stockMatyFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                    stockMatyFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                    stockMatyFilter.MATERIAL_TYPE_ID = filter.MATERIAL_TYPE_ID;
                    mediStockMatys = new HisMediStockMatyGet().Get(stockMatyFilter);
                }


                //neu co' yeu cau lay danh sach bao gom ca cac material_type ko co thuoc trong kho
                if (filter.INCLUDE_EMPTY)
                {
                    List<long> mediStockIds = new List<long>() { filter.MEDI_STOCK_ID ?? 0 };
                    if (filter.GROUP_BY_MEDI_STOCK || (filter.MEDI_STOCK_IDs != null && filter.MEDI_STOCK_IDs.Count == 1))
                    {
                        mediStockIds = filter.MEDI_STOCK_IDs;
                    }

                    List<V_HIS_MATERIAL_TYPE> materialTypes = DAOWorker.SqlDAO.GetSql<V_HIS_MATERIAL_TYPE>(string.Format("SELECT * FROM V_HIS_MATERIAL_TYPE MATY WHERE EXISTS (SELECT 1 FROM V_HIS_IMP_MEST_MATERIAL IMMA WHERE IMMA.MATERIAL_TYPE_ID = MATY.ID AND IMMA.MEDI_STOCK_ID IN ({0}))", string.Join(", ", mediStockIds)));

                    foreach (long mediStockId in mediStockIds)
                    {
                        foreach (V_HIS_MATERIAL_TYPE materialType in materialTypes)
                        {
                            //neu material_type co is_leaf = 1 va ko co trong danh sach bean thi tuc la khong co thuoc trong kho
                            if (materialType.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE &&
                                (vMaterialBeans == null || !vMaterialBeans.Exists(o => o.MATERIAL_TYPE_ID == materialType.ID)))
                            {
                                HisMaterialInStockSDO sdo = new HisMaterialInStockSDO();
                                //lay id cua material_type nhung nhan voi -1 de tranh trung voi ID cua material
                                sdo.ID = -1 * materialType.ID;
                                sdo.MATERIAL_TYPE_IS_ACTIVE = materialType.IS_ACTIVE;
                                sdo.IS_LEAF = materialType.IS_LEAF;
                                sdo.MATERIAL_TYPE_ID = materialType.ID;
                                sdo.MATERIAL_TYPE_CODE = materialType.MATERIAL_TYPE_CODE;
                                sdo.MATERIAL_TYPE_NAME = materialType.MATERIAL_TYPE_NAME;
                                sdo.MEDI_STOCK_ID = mediStockId;
                                sdo.PARENT_ID = materialType.PARENT_ID;
                                sdo.SERVICE_ID = materialType.SERVICE_ID;
                                sdo.SERVICE_UNIT_CODE = materialType.SERVICE_UNIT_CODE;
                                sdo.SERVICE_UNIT_NAME = materialType.SERVICE_UNIT_NAME;
                                sdo.ALERT_EXPIRED_DATE = materialType.ALERT_EXPIRED_DATE;
                                sdo.NATIONAL_NAME = materialType.NATIONAL_NAME;
                                sdo.MANUFACTURER_NAME = materialType.MANUFACTURER_NAME;
                                sdo.CONCENTRA = materialType.CONCENTRA;
                                sdo.LOCKING_REASON = materialType.LOCKING_REASON;
                                sdo.TotalAmount = 0;
                                sdo.AvailableAmount = 0;

                                if (showByOneMedistock)
                                {
                                    HIS_MEDI_STOCK_MATY stockMaty = mediStockMatys != null ? mediStockMatys
                                        .FirstOrDefault(o => o.MEDI_STOCK_ID == mediStockId && o.MATERIAL_TYPE_ID == materialType.ID) : null;

                                    sdo.BaseAmount = stockMaty != null ? stockMaty.ALERT_MAX_IN_STOCK : null;
                                    //neu co cau hinh theo kho thi lay theo kho, ko thi lay theo loai thuoc
                                    sdo.ALERT_MIN_IN_STOCK = stockMaty != null && stockMaty.ALERT_MIN_IN_STOCK.HasValue ? stockMaty.ALERT_MIN_IN_STOCK : materialType.ALERT_MIN_IN_STOCK;
                                }

                                result.Add(sdo);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(vMaterialBeans))
                {
                    Dictionary<string, HisMaterialInStockSDO> dic = new Dictionary<string, HisMaterialInStockSDO>();
                    foreach (V_HIS_MATERIAL_BEAN bean in vMaterialBeans)
                    {
                        string key = filter.GROUP_BY_MEDI_STOCK ? string.Format("{0}-{1}", bean.MATERIAL_ID, bean.MEDI_STOCK_ID) : bean.MATERIAL_ID.ToString();

                        if (dic.ContainsKey(key))
                        {
                            HisMaterialInStockSDO sdo = dic[key];
                            sdo.TotalAmount += bean.AMOUNT;
                            sdo.AvailableAmount += bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                && bean.MATERIAL_IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;
                            if (!string.IsNullOrWhiteSpace(bean.SERIAL_NUMBER))
                            {
                                if (!string.IsNullOrWhiteSpace(sdo.SERIAL_NUMBER))
                                {
                                    sdo.SERIAL_NUMBER += (";" + bean.SERIAL_NUMBER);
                                }
                                else
                                {
                                    sdo.SERIAL_NUMBER += bean.SERIAL_NUMBER;
                                }
                            }

                            if (String.IsNullOrWhiteSpace(sdo.LOCKING_REASON) && !String.IsNullOrWhiteSpace(bean.LOCKING_REASON))
                            {
                                sdo.LOCKING_REASON = bean.LOCKING_REASON;
                            }
                        }
                        else
                        {
                            HisMaterialInStockSDO sdo = new HisMaterialInStockSDO();
                            sdo.ID = bean.MATERIAL_ID;
                            sdo.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;//material_bean thi is_leaf luon la true
                            sdo.MATERIAL_TYPE_ID = bean.MATERIAL_TYPE_ID;
                            sdo.MATERIAL_TYPE_CODE = bean.MATERIAL_TYPE_CODE;
                            sdo.MATERIAL_TYPE_NAME = bean.MATERIAL_TYPE_NAME;
                            //MEDI_STOCK_ID set theo filter chu ko lay trong bean,
                            //tranh truong hop nguoi dung tim kiem o nhieu kho
                            sdo.MEDI_STOCK_ID = filter.GROUP_BY_MEDI_STOCK ? bean.MEDI_STOCK_ID : filter.MEDI_STOCK_ID;
                            sdo.MATERIAL_TYPE_IS_ACTIVE = bean.MATERIAL_TYPE_IS_ACTIVE;
                            sdo.IS_ACTIVE = bean.MATERIAL_IS_ACTIVE;
                            sdo.PARENT_ID = bean.PARENT_ID;
                            sdo.SERVICE_ID = bean.SERVICE_ID;
                            sdo.SUPPLIER_ID = bean.SUPPLIER_ID;
                            sdo.PACKAGE_NUMBER = bean.PACKAGE_NUMBER;
                            sdo.IMP_TIME = bean.IMP_TIME;
                            sdo.IMP_PRICE = bean.IMP_PRICE;
                            sdo.IMP_VAT_RATIO = bean.IMP_VAT_RATIO;
                            sdo.BID_NUMBER = bean.TDL_BID_NUMBER;
                            sdo.EXPIRED_DATE = bean.EXPIRED_DATE;
                            sdo.SERVICE_UNIT_CODE = bean.SERVICE_UNIT_CODE;
                            sdo.SERVICE_UNIT_NAME = bean.SERVICE_UNIT_NAME;
                            sdo.SUPPLIER_CODE = bean.SUPPLIER_CODE;
                            sdo.SUPPLIER_NAME = bean.SUPPLIER_NAME;
                            sdo.ALERT_EXPIRED_DATE = bean.ALERT_EXPIRED_DATE;
                            sdo.NATIONAL_NAME = bean.NATIONAL_NAME;
                            sdo.NUM_ORDER = bean.NUM_ORDER;
                            sdo.MANUFACTURER_NAME = bean.MANUFACTURER_NAME;
                            sdo.CONCENTRA = bean.CONCENTRA;
                            sdo.REGISTER_NUMBER = bean.MATERIAL_REGISTER_NUMBER;
                            sdo.TotalAmount = bean.AMOUNT;
                            sdo.SERIAL_NUMBER = bean.SERIAL_NUMBER;
                            sdo.LOCKING_REASON = bean.LOCKING_REASON;
                            sdo.AvailableAmount = bean.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE ? bean.AMOUNT : 0;

                            if (showByOneMedistock)
                            {
                                HIS_MEDI_STOCK_MATY stockMaty = mediStockMatys != null ? mediStockMatys
                                    .FirstOrDefault(o => o.MEDI_STOCK_ID == bean.MEDI_STOCK_ID && o.MATERIAL_TYPE_ID == bean.MATERIAL_TYPE_ID) : null;

                                sdo.BaseAmount = stockMaty != null ? stockMaty.ALERT_MAX_IN_STOCK : null;
                                //neu co cau hinh theo kho thi lay theo kho, ko thi lay theo loai thuoc
                                sdo.ALERT_MIN_IN_STOCK = stockMaty != null && stockMaty.ALERT_MIN_IN_STOCK.HasValue ? stockMaty.ALERT_MIN_IN_STOCK : bean.ALERT_MIN_IN_STOCK;
                            }

                            if (includeExpPrice)
                            {
                                if (bean.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && bean.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == Constant.IS_TRUE)
                                {
                                    sdo.ExpPrice = bean.TDL_MATERIAL_IMP_PRICE;
                                    sdo.ExpVatRatio = bean.TDL_MATERIAL_IMP_VAT_RATIO;
                                }
                                else
                                {
                                    HIS_MATERIAL_PATY paty = materialPatys != null ? materialPatys.FirstOrDefault(o => o.MATERIAL_ID == bean.MATERIAL_ID) : null;
                                    if (paty != null)
                                    {
                                        sdo.ExpPrice = paty.EXP_PRICE;
                                        sdo.ExpVatRatio = paty.EXP_VAT_RATIO;
                                    }
                                }
                            }

                            dic.Add(key, sdo);
                        }
                    }

                    result.AddRange(dic.Values.ToList());
                }
                //sort va paging lai du lieu
                int start = param.Start.HasValue ? param.Start.Value : 0;
                int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                param.Count = result.Count;
                result = result.AsQueryable().OrderByProperty(filter.ORDER_FIELD, filter.ORDER_DIRECTION).Skip(start).Take(limit).ToList();

                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        /// <summary>
        /// Xu ly de lay du lieu ton kho o 2 kho va hien thi tuong ung o 2 truong khac nhau
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal List<HisMaterialIn2StockSDO> GetIn2StockMaterial(HisMaterial2StockFilter filter)
        {
            List<HisMaterialIn2StockSDO> result = null;
            HisMaterialStockViewFilter firstFilter = new HisMaterialStockViewFilter();
            if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
            {
                firstFilter.MEDI_STOCK_IDs = filter.FIRST_MEDI_STOCK_IDs;
                firstFilter.GROUP_BY_MEDI_STOCK = true;
            }
            else
            {
                firstFilter.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
            }
            firstFilter.IS_ACTIVE = filter.IS_ACTIVE;
            firstFilter.IS_LEAF = filter.IS_LEAF;
            firstFilter.MATERIAL_TYPE_IS_ACTIVE = filter.MATERIAL_TYPE_IS_ACTIVE;
            firstFilter.ORDER_DIRECTION = filter.ORDER_DIRECTION;
            firstFilter.ORDER_FIELD = filter.ORDER_FIELD;

            List<HisMaterialInStockSDO> firstStock = this.GetInStockMaterial(firstFilter);

            HisMaterialStockViewFilter secondFilter = new HisMaterialStockViewFilter();
            secondFilter.MEDI_STOCK_ID = filter.SECOND_MEDI_STOCK_ID;
            secondFilter.IS_ACTIVE = filter.IS_ACTIVE;
            secondFilter.IS_LEAF = filter.IS_LEAF;
            secondFilter.MATERIAL_TYPE_IS_ACTIVE = filter.MATERIAL_TYPE_IS_ACTIVE;
            secondFilter.ORDER_DIRECTION = filter.ORDER_DIRECTION;
            secondFilter.ORDER_FIELD = filter.ORDER_FIELD;

            List<HisMaterialInStockSDO> secondStock = this.GetInStockMaterial(secondFilter);

            if (IsNotNullOrEmpty(firstStock) || IsNotNullOrEmpty(secondStock))
            {
                result = new List<HisMaterialIn2StockSDO>();
                Mapper.CreateMap<HisMaterialInStockSDO, HisMaterialIn2StockSDO>();

                //Duyet d/s kho 1 va tim cac du lieu o kho 2 tuong ung
                if (IsNotNullOrEmpty(firstStock))
                {
                    foreach (HisMaterialInStockSDO first in firstStock)
                    {
                        HisMaterialInStockSDO second = IsNotNullOrEmpty(secondStock) ? secondStock.Where(o => o.ID == first.ID).FirstOrDefault() : null;
                        HisMaterialIn2StockSDO s = Mapper.Map<HisMaterialIn2StockSDO>(first);
                        if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
                        {
                            s.MEDI_STOCK_ID = first.MEDI_STOCK_ID;
                        }
                        else
                        {
                            s.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
                        }
                        s.AvailableAmount = first.AvailableAmount;
                        s.TotalAmount = first.TotalAmount;

                        s.SECOND_MEDI_STOCK_ID = filter.SECOND_MEDI_STOCK_ID;
                        s.SecondAvailableAmount = second != null ? second.AvailableAmount : 0;
                        s.SecondTotalAmount = second != null ? second.TotalAmount : 0;

                        result.Add(s);
                    }
                }

                //Lay cac d/s co trong kho 2 va khong co trong kho 1
                List<HisMaterialInStockSDO> tmp = IsNotNullOrEmpty(secondStock) ? secondStock.Where(o => firstStock == null || !firstStock.Exists(t => t.ID == o.ID)).ToList() : null;
                if (IsNotNullOrEmpty(tmp))
                {
                    foreach (HisMaterialInStockSDO second in tmp)
                    {
                        if (IsNotNullOrEmpty(filter.FIRST_MEDI_STOCK_IDs))
                        {
                            foreach (var stock in filter.FIRST_MEDI_STOCK_IDs)
                            {
                                HisMaterialIn2StockSDO s = Mapper.Map<HisMaterialIn2StockSDO>(second);
                                s.MEDI_STOCK_ID = stock;
                                s.AvailableAmount = 0;
                                s.TotalAmount = 0;
                                s.SECOND_MEDI_STOCK_ID = second.MEDI_STOCK_ID;
                                s.SecondAvailableAmount = second.AvailableAmount;
                                s.SecondTotalAmount = second.TotalAmount;

                                result.Add(s);
                            }
                        }
                        else
                        {
                            HisMaterialIn2StockSDO s = Mapper.Map<HisMaterialIn2StockSDO>(second);
                            s.MEDI_STOCK_ID = filter.FIRST_MEDI_STOCK_ID;
                            s.AvailableAmount = 0;
                            s.TotalAmount = 0;
                            s.SECOND_MEDI_STOCK_ID = second.MEDI_STOCK_ID;
                            s.SecondAvailableAmount = second.AvailableAmount;
                            s.SecondTotalAmount = second.TotalAmount;

                            result.Add(s);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lay danh sach material voi thong tin ton, kha dung ket hop voi cay material_type
        /// De build tree, su dung khai niem "nodeId". Trong do:
        /// - "nodeId" cua cac node la material_type = nodeIdPrefix + T + material_type_id
        /// - "nodeId" cua cac node la material = nodeIdPrefix + M + material_id
        /// (nodeIdPrefix: duoc su dung trong truong hop tra ve nhieu list tree (vd: theo han su dung, tuong ung voi moi han su dung la 1 tree, khi do, cac phan tu trong cac tree can nodeIdPrefix de dam bao tinh duy nhat)
        /// </summary>
        /// <param name="inStockMaterials"></param>
        /// <param name="nodeIdPrefix"></param>
        /// <returns></returns>
        private List<HisMaterialInStockSDO> GetInStockMaterialWithTypeTree(List<HisMaterialInStockSDO> inStockMaterials, List<V_HIS_MATERIAL_TYPE> materialTypes, string nodeIdPrefix)
        {
            try
            {
                List<HisMaterialInStockSDO> result = null;
                if (IsNotNullOrEmpty(inStockMaterials) && IsNotNullOrEmpty(materialTypes))
                {
                    result = new List<HisMaterialInStockSDO>();
                    Mapper.CreateMap<V_HIS_MATERIAL_TYPE, HisMaterialInStockSDO>();

                    foreach (HisMaterialInStockSDO sdo in inStockMaterials)
                    {
                        sdo.ParentNodeId = nodeIdPrefix + MATERIAL_TYPE_PREFIX + sdo.MATERIAL_TYPE_ID;
                        sdo.NodeId = nodeIdPrefix + MATERIAL_PREFIX + sdo.ID;
                        sdo.isTypeNode = false;
                        result.Add(sdo);

                        //neu node nay chua duoc add vao danh sach
                        if (!result.Select(o => o.NodeId).ToList().Contains(nodeIdPrefix + MATERIAL_TYPE_PREFIX + sdo.MATERIAL_TYPE_ID))
                        {
                            V_HIS_MATERIAL_TYPE type = materialTypes.Where(o => o.ID == sdo.MATERIAL_TYPE_ID).SingleOrDefault();
                            HisMaterialInStockSDO t = Mapper.Map<HisMaterialInStockSDO>(type);
                            t.TotalAmount = inStockMaterials.Where(o => o.MATERIAL_TYPE_ID == type.ID).Sum(o => o.TotalAmount);
                            t.AvailableAmount = inStockMaterials.Where(o => o.MATERIAL_TYPE_ID == type.ID).Sum(o => o.AvailableAmount);
                            t.NodeId = nodeIdPrefix + MATERIAL_TYPE_PREFIX + type.ID;
                            t.ParentNodeId = type.PARENT_ID.HasValue ? nodeIdPrefix + MATERIAL_TYPE_PREFIX + type.PARENT_ID : null;
                            t.isTypeNode = true;
                            t.BaseAmount = sdo.BaseAmount;
                            t.RealBaseAmount = sdo.RealBaseAmount;
                            t.ALERT_MIN_IN_STOCK = sdo.ALERT_MIN_IN_STOCK;
                            t.MEDI_STOCK_ID = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong V_HIS_MEDICINE_TYPE ko co MEDI_STOCK_ID)
                            result.Add(t);
                            this.MaterialTypeTraversalToBuildTree(type, materialTypes, result, nodeIdPrefix);
                        }

                        // Chi hien thi baseAmount o cac dong cha (Dong lo khong hien thi)
                        sdo.BaseAmount = null;
                        sdo.RealBaseAmount = null;
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

        /// <summary>
        /// Duyet danh sach de thuc hien build tree (tao du lieu co quan he "cha-con")
        /// Duyet tu node "la'" (leaf) dan len cac node phia tren
        /// </summary>
        /// <param name="child"></param>
        /// <param name="allMaterialTypes"></param>
        /// <param name="builtTrees"></param>
        private void MaterialTypeTraversalToBuildTree(V_HIS_MATERIAL_TYPE child, List<V_HIS_MATERIAL_TYPE> allMaterialTypes, List<HisMaterialInStockSDO> builtTrees, string nodeIdPrefix)
        {
            if (builtTrees == null)
            {
                builtTrees = new List<HisMaterialInStockSDO>();
            }

            //Tim medicine_type la cha cua "child"
            if (child.PARENT_ID.HasValue)
            {
                string nodeId = nodeIdPrefix + MATERIAL_TYPE_PREFIX + child.PARENT_ID.Value;
                HisMaterialInStockSDO sdo = builtTrees.Where(t => t.NodeId == nodeId).FirstOrDefault();
                V_HIS_MATERIAL_TYPE parent = allMaterialTypes.Where(o => o.ID == child.PARENT_ID).SingleOrDefault();

                //Neu chua co thi tao moi
                //Neu da co trong danh sach "builtTrees" thi cap nhat lai so luong
                if (sdo == null)
                {
                    if (parent != null)
                    {
                        Mapper.CreateMap<V_HIS_MATERIAL_TYPE, HisMaterialInStockSDO>();
                        sdo = Mapper.Map<HisMaterialInStockSDO>(parent);
                        sdo.NodeId = nodeId;
                        sdo.ParentNodeId = parent.PARENT_ID.HasValue ? nodeIdPrefix + MATERIAL_TYPE_PREFIX + parent.PARENT_ID : null;
                        sdo.isTypeNode = true;
                        sdo.MEDI_STOCK_ID = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong HIS_MATERIAL_TYPE ko co MEDI_STOCK_ID)
                        builtTrees.Add(sdo);
                        sdo.TotalAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.TotalAmount);
                        sdo.AvailableAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.AvailableAmount);
                        this.MaterialTypeTraversalToBuildTree(parent, allMaterialTypes, builtTrees, nodeIdPrefix);
                    }
                }
                else
                {
                    sdo.TotalAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.TotalAmount);
                    sdo.AvailableAmount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.AvailableAmount);
                    this.MaterialTypeTraversalToBuildTree(parent, allMaterialTypes, builtTrees, nodeIdPrefix);
                }
            }
        }

        /// <summary>
        /// Sap xep theo cac thu tu uu tien sau:
        /// - Danh sach co so luong duoi muc canh bao nhung co amount > 0
        /// - Danh sach co so luong tren muc canh bao
        /// - Danh sach co so luong lon hon 0 nhung ko co muc canh bao
        /// - Danh sach co so luong bang 0
        /// - Sap xep theo NUM_ORDER
        /// </summary>
        /// <param name="listToSort"></param>
        /// <param name="all"></param>
        /// <param name="sortedList"></param>
        private void InStockMaterialTraversalToSortByAmount(List<HisMaterialInStockSDO> listToSort, List<HisMaterialInStockSDO> all, List<HisMaterialInStockSDO> sortedList)
        {
            if (sortedList == null)
            {
                sortedList = new List<HisMaterialInStockSDO>();
            }

            if (listToSort != null && listToSort.Count > 0)
            {
                //Danh sach co so luong duoi muc canh bao nhung co amount > 0
                List<HisMaterialInStockSDO> lessThanWarning = listToSort
                    .Where(o => o.ALERT_MIN_IN_STOCK.HasValue && o.TotalAmount.HasValue
                        && o.TotalAmount <= o.ALERT_MIN_IN_STOCK && o.TotalAmount > 0)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                //Danh sach co so luong tren muc canh bao
                List<HisMaterialInStockSDO> greaterThanWarning = listToSort
                    .Where(o => o.ALERT_MIN_IN_STOCK.HasValue
                        && o.TotalAmount.HasValue && o.TotalAmount > o.ALERT_MIN_IN_STOCK)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                //Danh sach co so luong lon hon 0 nhung ko co muc canh bao
                List<HisMaterialInStockSDO> greaterThan0AndNoWarning = listToSort
                    .Where(o => !o.ALERT_MIN_IN_STOCK.HasValue
                        && o.TotalAmount.HasValue && o.TotalAmount > 0)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();
                //Danh sach co so luong bang 0
                List<HisMaterialInStockSDO> equal0 = listToSort
                    .Where(o => !o.TotalAmount.HasValue || o.TotalAmount == 0)
                    .OrderBy(o => o.ALERT_MIN_IN_STOCK)
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                sortedList.AddRange(lessThanWarning);
                sortedList.AddRange(greaterThanWarning);
                sortedList.AddRange(greaterThan0AndNoWarning);
                sortedList.AddRange(equal0);

                foreach (HisMaterialInStockSDO tmp in listToSort)
                {
                    List<HisMaterialInStockSDO> newListToSort = all.Where(o => o.ParentNodeId == tmp.NodeId).ToList();
                    InStockMaterialTraversalToSortByAmount(newListToSort, all, sortedList);
                }
            }
        }

        /// <summary>
        /// Sap xep theo cac thu tu uu tien sau:
        /// - Thuoc co han su dung nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
        /// - Thuoc co han su dung chua nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
        /// - Thuoc ko co thong tin han su dung
        /// - Sap xep theo NUM_ORDER
        /// </summary>
        /// <param name="listToSort"></param>
        /// <param name="all"></param>
        /// <param name="sortedList"></param>
        private void InStockMaterialTraversalToSortByExpiredDate(List<HisMaterialInStockSDO> listToSort, List<HisMaterialInStockSDO> all, List<HisMaterialInStockSDO> sortedList)
        {
            if (sortedList == null)
            {
                sortedList = new List<HisMaterialInStockSDO>();
            }

            if (listToSort != null && listToSort.Count > 0)
            {
                DateTime now = DateTime.Now;
                //Thuoc co han su dung nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
                List<HisMaterialInStockSDO> inWarningRange = listToSort
                    .Where(o => o.ALERT_EXPIRED_DATE.HasValue
                        && o.EXPIRED_DATE.HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value) <= now)
                    .OrderBy(o => Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value))
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                //Thuoc co han su dung chua nam trong khoang canh bao so voi ngay hien tai (tu nho den lon)
                List<HisMaterialInStockSDO> outWarningRange = listToSort
                    .Where(o => o.ALERT_EXPIRED_DATE.HasValue
                        && o.EXPIRED_DATE.HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue
                        && Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value) > now)
                    .OrderBy(o => Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).Value.AddDays(-1 * o.ALERT_EXPIRED_DATE.Value))
                    .ThenByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                //Thuoc ko co thong tin canh bao han su dung
                List<HisMaterialInStockSDO> noAlert = listToSort
                    .Where(o => !o.ALERT_EXPIRED_DATE.HasValue
                        || !o.EXPIRED_DATE.HasValue
                        || !Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime((long)o.EXPIRED_DATE.Value).HasValue)
                    .OrderByDescending(o => o.NUM_ORDER)
                    .ThenBy(o => o.MATERIAL_TYPE_NAME)
                    .ToList();

                sortedList.AddRange(inWarningRange);
                sortedList.AddRange(outWarningRange);
                sortedList.AddRange(noAlert);

                foreach (HisMaterialInStockSDO tmp in listToSort)
                {
                    List<HisMaterialInStockSDO> newListToSort = all.Where(o => o.ParentNodeId == tmp.NodeId).ToList();
                    InStockMaterialTraversalToSortByExpiredDate(newListToSort, all, sortedList);
                }
            }
        }

        private List<HIS_MATERIAL_PATY> GetMaterialPaty(List<V_HIS_MATERIAL_BEAN> vMaterialBeans, HisMaterialStockViewFilter filter)
        {
            List<HIS_MATERIAL_PATY> rs = null;
            if (IsNotNullOrEmpty(vMaterialBeans) && filter.INCLUDE_EXP_PRICE && filter.EXP_PATIENT_TYPE_ID.HasValue)
            {
                List<long> materialIds = vMaterialBeans.Where(o => !o.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || o.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MATERIAL_ID).Distinct().ToList();
                if (IsNotNullOrEmpty(materialIds))
                {
                    HisMaterialPatyFilterQuery matePatyFilter = new HisMaterialPatyFilterQuery();
                    matePatyFilter.MATERIAL_IDs = materialIds;
                    matePatyFilter.PATIENT_TYPE_ID = filter.EXP_PATIENT_TYPE_ID.Value;
                    matePatyFilter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    rs = new HisMaterialPatyGet().Get(matePatyFilter);
                }
            }

            return rs;
        }
    }
}
