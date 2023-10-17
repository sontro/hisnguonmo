using AutoMapper;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBloodType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBlood
{
    partial class HisBloodGet : BusinessBase
    {
        private const string BLOOD_TYPE_PREFIX = "T_";
        private const string BLOOD_PREFIX = "B_";

        internal List<HisBloodInStockSDO> GetInStockBloodWithTypeTree(HisBloodStockViewFilter filter)
        {
            try
            {
                List<HisBloodInStockSDO> inStockBloods = this.GetInStockBlood(filter);
                List<HIS_BLOOD_TYPE> bloodTypes = new HisBloodTypeGet().Get(new HisBloodTypeFilterQuery());

                List<HisBloodInStockSDO> list = this.GetBloodWithTypeTree(inStockBloods, bloodTypes);

                return list;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private List<HisBloodInStockSDO> GetBloodWithTypeTree(List<HisBloodInStockSDO> inStockBloods, List<HIS_BLOOD_TYPE> bloodTypes)
        {
            try
            {
                List<HisBloodInStockSDO> result = null;
                if (inStockBloods != null && inStockBloods.Count > 0)
                {
                    result = new List<HisBloodInStockSDO>();
                    Mapper.CreateMap<HisBloodInStockSDO, HisBloodInStockSDO>();

                    foreach (HisBloodInStockSDO sdo in inStockBloods)
                    {
                        sdo.ParentNodeId = BLOOD_TYPE_PREFIX + sdo.BloodTypeId;
                        sdo.NodeId = BLOOD_PREFIX + sdo.Id;
                        sdo.isTypeNode = false;
                        result.Add(sdo);

                        //neu node nay chua duoc add vao danh sach
                        if (!result.Exists(o => o.NodeId == sdo.ParentNodeId))
                        {
                            HIS_BLOOD_TYPE type = bloodTypes.FirstOrDefault(o => o.ID == sdo.BloodTypeId);
                            HisBloodInStockSDO t = new HisBloodInStockSDO();
                            t.BloodTypeCode = type.BLOOD_TYPE_CODE;
                            t.BloodTypeId = type.ID;
                            t.BloodTypeName = type.BLOOD_TYPE_NAME;

                            t.Amount = inStockBloods.Where(o => o.BloodTypeId == type.ID).Sum(o => o.Amount ?? 0);
                            t.NodeId = BLOOD_TYPE_PREFIX + type.ID;
                            t.ParentNodeId = type.PARENT_ID.HasValue ? BLOOD_TYPE_PREFIX + type.PARENT_ID : null;
                            t.isTypeNode = true;
                            t.MediStockId = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong HIS_MATERIAL_TYPE ko co MEDI_STOCK_ID)
                            result.Add(t);
                            this.BloodTypeTraversalToBuildTree(type, bloodTypes, result);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private void BloodTypeTraversalToBuildTree(HIS_BLOOD_TYPE child, List<HIS_BLOOD_TYPE> bloodTypes, List<HisBloodInStockSDO> builtTrees)
        {
            if (builtTrees == null)
            {
                builtTrees = new List<HisBloodInStockSDO>();
            }

            //Tim medicine_type la cha cua "child"
            if (child.PARENT_ID.HasValue)
            {
                string nodeId = BLOOD_TYPE_PREFIX + child.PARENT_ID.Value;
                HisBloodInStockSDO sdo = builtTrees.Where(t => t.NodeId == nodeId).FirstOrDefault();
                HIS_BLOOD_TYPE parent = bloodTypes.FirstOrDefault(o => o.ID == child.PARENT_ID);

                //Neu chua co thi tao moi
                //Neu da co trong danh sach "builtTrees" thi cap nhat lai so luong
                if (sdo == null)
                {
                    if (parent != null)
                    {
                        sdo = new HisBloodInStockSDO();

                        sdo.BloodTypeCode = parent.BLOOD_TYPE_CODE;
                        sdo.BloodTypeId = parent.ID;
                        sdo.BloodTypeName = parent.BLOOD_TYPE_NAME;

                        sdo.NodeId = nodeId;
                        sdo.ParentNodeId = parent.PARENT_ID.HasValue ? BLOOD_TYPE_PREFIX + parent.PARENT_ID : null;
                        sdo.isTypeNode = true;
                        sdo.MediStockId = null;//set null tranh bi set sai du lieu khi dung mapper (vi trong HIS_MATERIAL_TYPE ko co MEDI_STOCK_ID);
                        sdo.Amount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.Amount ?? 0);

                        builtTrees.Add(sdo);
                        this.BloodTypeTraversalToBuildTree(parent, bloodTypes, builtTrees);
                    }
                }
                else
                {
                    sdo.Amount = builtTrees.Where(o => o.ParentNodeId == sdo.NodeId).Sum(o => o.Amount ?? 0);
                    this.BloodTypeTraversalToBuildTree(parent, bloodTypes, builtTrees);
                }
            }
        }

        private List<HisBloodInStockSDO> GetInStockBlood(HisBloodStockViewFilter filter)
        {
            List<HisBloodInStockSDO> result = null;
            HisBloodViewFilterQuery bloodViewFilter = new HisBloodViewFilterQuery();
            bloodViewFilter.ORDER_FIELD = "BLOOD_CODE";
            bloodViewFilter.ORDER_DIRECTION = "ASC";
            bloodViewFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
            bloodViewFilter.IS_ACTIVE = filter.IS_ACTIVE;
            bloodViewFilter.BLOOD_TYPE_ID = filter.BLOOD_TYPE_ID;
            bloodViewFilter.IDs = filter.IDs;
            bloodViewFilter.EXPIRED_DATE_FROM = filter.EXPIRED_DATE_FROM;
            bloodViewFilter.EXPIRED_DATE_TO = filter.EXPIRED_DATE_TO;

            List<V_HIS_BLOOD> vBloods = new HisBloodGet(param).GetView(bloodViewFilter);
            if (IsNotNullOrEmpty(vBloods))
            {
                result = new List<HisBloodInStockSDO>();

                foreach (V_HIS_BLOOD blood in vBloods)
                {
                    HisBloodInStockSDO sdo = new HisBloodInStockSDO();
                    sdo.Id = blood.ID;
                    sdo.BloodTypeId = blood.BLOOD_TYPE_ID;
                    sdo.BloodTypeCode = blood.BLOOD_TYPE_CODE;
                    sdo.BloodTypeName = blood.BLOOD_TYPE_NAME;
                    sdo.MediStockId = blood.MEDI_STOCK_ID;
                    sdo.ParentId = blood.PARENT_ID;
                    sdo.ServiceId = blood.SERVICE_ID;
                    sdo.Volume = blood.VOLUME;
                    sdo.Amount = 1;
                    sdo.BloodCode = blood.BLOOD_CODE;
                    sdo.Volume = blood.VOLUME;
                    sdo.ExpiredDate = blood.EXPIRED_DATE;
                    sdo.PackageNumber = blood.PACKAGE_NUMBER;
                    sdo.SupplierName = blood.SUPPLIER_NAME;
                    sdo.BloodAboCode = blood.BLOOD_ABO_CODE;
                    sdo.BloodRhCode = blood.BLOOD_RH_CODE;
                    result.Add(sdo);
                }

                //Thuc hien phan trang lai theo du lieu param tu client (do du lieu ko duoc phan trang duoi tang DAO)
                int start = param.Start.HasValue ? param.Start.Value : 0;
                int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                param.Count = result.Count;
                return result.Skip(start).Take(limit).ToList();
            }
            return result;
        }
    }
}
