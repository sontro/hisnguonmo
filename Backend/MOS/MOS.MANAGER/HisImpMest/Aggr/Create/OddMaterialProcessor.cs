using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr
{
    class OddMaterialProcessor : BusinessBase
    {
        private List<V_HIS_IMP_MEST_MATERIAL> hisImpMestMaterials;
        private List<HisImpMestCreate> hisImpMestCreates;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisImpMestMaterialUpdate hisImpMestMaterialUpdate;

        private Dictionary<long, List<HIS_IMP_MEST_MATERIAL>> dicCreate = new Dictionary<long, List<HIS_IMP_MEST_MATERIAL>>();

        internal OddMaterialProcessor()
            : base()
        {
            this.Init();
        }

        internal OddMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreates = new List<HisImpMestCreate>();
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisImpMestMaterialUpdate = new HisImpMestMaterialUpdate(param);
        }

        internal bool Process(HisImpMestAggrSDO data, List<HIS_IMP_MEST> childImpMests, ref List<HIS_IMP_MEST_MATERIAL> allImpMaterials, ref List<HIS_IMP_MEST_MATERIAL> deleteImpMaterials)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(data.OddMaterialTypes))
                {
                    return true;
                }

                this.CheckValidData(data, ref allImpMaterials);

                this.ProcessOddImpMestMaterial(data, childImpMests, ref deleteImpMaterials);

                this.ProcessNewImpMest(data, childImpMests);
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

        private void CheckValidData(HisImpMestAggrSDO data, ref List<HIS_IMP_MEST_MATERIAL> allImpMaterials)
        {
            if (!data.OddMediStockId.HasValue)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong truyen len Medi_Stock_id cua kho le");
            }
            List<long> medicineTypeIdForOdds = data
                    .OddMaterialTypes.Select(o => o.MaterialTypeId).Distinct().ToList();
            if (medicineTypeIdForOdds.Count != data.OddMaterialTypes.Count)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Ton tai 2 OddImpMestMaterial co MaterialTypeId giong nhau");
            }

            HisImpMestMaterialViewFilterQuery filterQuery = new HisImpMestMaterialViewFilterQuery();
            filterQuery.IMP_MEST_IDs = data.ImpMestIds;
            this.hisImpMestMaterials = new HisImpMestMaterialGet().GetView(filterQuery);
            if (!IsNotNullOrEmpty(this.hisImpMestMaterials))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong lay duoc V_HIS_IMP_MEST_MATERIAL theo impMestIds");
            }
            this.hisImpMestMaterials = this.hisImpMestMaterials.OrderBy(o => o.MATERIAL_ID).ThenByDescending(t => t.AMOUNT).ToList();
            Mapper.CreateMap<V_HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();
            allImpMaterials = Mapper.Map<List<HIS_IMP_MEST_MATERIAL>>(this.hisImpMestMaterials);
        }

        private void ProcessOddImpMestMaterial(HisImpMestAggrSDO data, List<HIS_IMP_MEST> childImpMests, ref List<HIS_IMP_MEST_MATERIAL> deleteImpMaterials)
        {
            List<HIS_IMP_MEST_MATERIAL> listUpdate = new List<HIS_IMP_MEST_MATERIAL>();
            List<HIS_IMP_MEST_MATERIAL> listDelete = new List<HIS_IMP_MEST_MATERIAL>();
            Mapper.CreateMap<V_HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();
            var Groups = this.hisImpMestMaterials.GroupBy(o => o.MATERIAL_TYPE_ID).ToList();
            foreach (var oddMediSdo in data.OddMaterialTypes)
            {
                var group = Groups.FirstOrDefault(o => o.Key == oddMediSdo.MaterialTypeId);
                if (group == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong co V_HIS_IMP_MEST_MATERIAL tuong uong voi MaterialTypeId :" + oddMediSdo.MaterialTypeId);
                }
                List<V_HIS_IMP_MEST_MATERIAL> listByMaterialType = group.ToList();

                //Lay cac chi tiet co so luong le va order theo so luong le giam gian 
                List<ImpMestMaterialData> materialOdds = this.ProcessOrderOddAmount(listByMaterialType);

                decimal totalAmount = listByMaterialType.Sum(s => s.AMOUNT);
                if (totalAmount < oddMediSdo.Amount)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So luong vat tu le khong duoc lon hon so luong theo loai vat tu do: Sl le: " + oddMediSdo.Amount + "; Tong vat tu nhap: " + totalAmount);
                }

                decimal totalNewAmount = totalAmount - oddMediSdo.Amount;

                decimal oddAmount = oddMediSdo.Amount;

                foreach (var oddData in materialOdds)
                {
                    if (oddAmount <= 0)
                        break;
                    if (oddData.OddAmount > oddAmount)
                    {
                        HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMaterial.AMOUNT = oddAmount;
                        impMaterial.MATERIAL_ID = oddData.ImpMestMaterial.MATERIAL_ID;
                        impMaterial.TH_EXP_MEST_MATERIAL_ID = oddData.ImpMestMaterial.TH_EXP_MEST_MATERIAL_ID;

                        var impMest = childImpMests.FirstOrDefault(o => o.ID == oddData.ImpMestMaterial.IMP_MEST_ID);
                        if (!this.dicCreate.ContainsKey(impMest.MOBA_EXP_MEST_ID.Value))
                            this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value] = new List<HIS_IMP_MEST_MATERIAL>();
                        this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value].Add(impMaterial);
                        HIS_IMP_MEST_MATERIAL update = Mapper.Map<HIS_IMP_MEST_MATERIAL>(oddData.ImpMestMaterial);
                        update.AMOUNT = update.AMOUNT - oddAmount;
                        oddAmount = 0;
                        listUpdate.Add(update);
                        break;
                    }
                    else
                    {
                        HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                        impMaterial.AMOUNT = oddData.OddAmount;
                        impMaterial.MATERIAL_ID = oddData.ImpMestMaterial.MATERIAL_ID;
                        impMaterial.TH_EXP_MEST_MATERIAL_ID = oddData.ImpMestMaterial.TH_EXP_MEST_MATERIAL_ID;
                        oddAmount = oddAmount - oddData.OddAmount;
                        var impMest = childImpMests.FirstOrDefault(o => o.ID == oddData.ImpMestMaterial.IMP_MEST_ID);
                        if (!this.dicCreate.ContainsKey(impMest.MOBA_EXP_MEST_ID.Value))
                            this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value] = new List<HIS_IMP_MEST_MATERIAL>();
                        this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value].Add(impMaterial);
                        HIS_IMP_MEST_MATERIAL update = Mapper.Map<HIS_IMP_MEST_MATERIAL>(oddData.ImpMestMaterial);
                        update.AMOUNT = update.AMOUNT - oddData.OddAmount;
                        if (update.AMOUNT > 0)
                        {
                            listUpdate.Add(update);
                        }
                        else
                        {
                            listDelete.Add(update);
                        }
                    }
                }

                //Neu sau khi xu cac impMestMedicine le ma van con so luong le thi xu ly lay ngau nhien
                if (oddAmount > 0)
                {
                    listByMaterialType = listByMaterialType.OrderByDescending(o => o.AMOUNT).ToList();
                    foreach (var item in listByMaterialType)
                    {
                        if (oddAmount <= 0) break;
                        if (listDelete != null && listDelete.Any(a => a.ID == item.ID)) continue;
                        var hasUpdate = listUpdate != null ? listUpdate.FirstOrDefault(o => o.ID == item.ID) : null;
                        if (hasUpdate != null)
                        {
                            var impMest = childImpMests.FirstOrDefault(o => o.ID == item.IMP_MEST_ID);
                            var list = this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value];
                            var newImp = list.FirstOrDefault(o => o.TH_EXP_MEST_MATERIAL_ID == item.TH_EXP_MEST_MATERIAL_ID && o.MATERIAL_ID == item.MATERIAL_ID);
                            if (hasUpdate.AMOUNT > oddAmount)
                            {
                                newImp.AMOUNT = newImp.AMOUNT + oddAmount;
                                hasUpdate.AMOUNT = hasUpdate.AMOUNT - oddAmount;
                                oddAmount = 0;
                                break;
                            }
                            else
                            {
                                newImp.AMOUNT = newImp.AMOUNT + hasUpdate.AMOUNT;
                                oddAmount = oddAmount - hasUpdate.AMOUNT;
                                hasUpdate.AMOUNT = 0;
                                listDelete.Add(hasUpdate);
                                listUpdate.Remove(hasUpdate);
                            }
                        }
                        else
                        {
                            if (item.AMOUNT > oddAmount)
                            {
                                HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                                impMaterial.AMOUNT = oddAmount;
                                impMaterial.MATERIAL_ID = item.MATERIAL_ID;
                                impMaterial.TH_EXP_MEST_MATERIAL_ID = item.TH_EXP_MEST_MATERIAL_ID;

                                var impMest = childImpMests.FirstOrDefault(o => o.ID == item.IMP_MEST_ID);
                                if (!this.dicCreate.ContainsKey(impMest.MOBA_EXP_MEST_ID.Value))
                                    this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value] = new List<HIS_IMP_MEST_MATERIAL>();
                                this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value].Add(impMaterial);
                                HIS_IMP_MEST_MATERIAL update = Mapper.Map<HIS_IMP_MEST_MATERIAL>(item);
                                update.AMOUNT = update.AMOUNT - oddAmount;
                                listUpdate.Add(update);
                                oddAmount = 0;
                                break;
                            }
                            else
                            {
                                HIS_IMP_MEST_MATERIAL impMaterial = new HIS_IMP_MEST_MATERIAL();
                                impMaterial.AMOUNT = item.AMOUNT;
                                impMaterial.MATERIAL_ID = item.MATERIAL_ID;
                                impMaterial.TH_EXP_MEST_MATERIAL_ID = item.TH_EXP_MEST_MATERIAL_ID;
                                oddAmount = oddAmount - item.AMOUNT;
                                var impMest = childImpMests.FirstOrDefault(o => o.ID == item.IMP_MEST_ID);
                                if (!this.dicCreate.ContainsKey(impMest.MOBA_EXP_MEST_ID.Value))
                                    this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value] = new List<HIS_IMP_MEST_MATERIAL>();
                                this.dicCreate[impMest.MOBA_EXP_MEST_ID.Value].Add(impMaterial);
                                HIS_IMP_MEST_MATERIAL update = Mapper.Map<HIS_IMP_MEST_MATERIAL>(item);
                                update.AMOUNT = 0;
                                listDelete.Add(update);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(listUpdate))
                {
                    if (!this.hisImpMestMaterialUpdate.UpdateList(listUpdate))
                    {
                        throw new Exception("Update HIS_IMP_MEST_MATERIAL that bai");
                    }
                }
                deleteImpMaterials = listDelete;
            }
        }

        private void ProcessNewImpMest(HisImpMestAggrSDO data, List<HIS_IMP_MEST> childImpMests)
        {
            if (this.dicCreate.Count > 0)
            {
                List<HIS_IMP_MEST_MATERIAL> listCreate = new List<HIS_IMP_MEST_MATERIAL>();
                foreach (var dic in dicCreate)
                {
                    HIS_IMP_MEST impMestNew = new HIS_IMP_MEST();
                    impMestNew.MEDI_STOCK_ID = data.OddMediStockId.Value;
                    impMestNew.REQ_ROOM_ID = data.RequestRoomId;
                    impMestNew.MOBA_EXP_MEST_ID = dic.Key;
                    impMestNew.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                    impMestNew.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL;
                    var oldImpMest = childImpMests.FirstOrDefault(o => o.MOBA_EXP_MEST_ID == dic.Key);
                    HisImpMestUtil.SetTdl(impMestNew, oldImpMest);
                    HisImpMestCreate create = new HisImpMestCreate(param);
                    if (!create.Create(impMestNew))
                    {
                        throw new Exception("Tao ImpMest thu hoi le that bai");
                    }
                    this.hisImpMestCreates.Add(create);
                    dic.Value.ForEach(o => o.IMP_MEST_ID = impMestNew.ID);
                    listCreate.AddRange(dic.Value);
                }

                if (!this.hisImpMestMaterialCreate.CreateList(listCreate))
                {
                    throw new Exception("Tao HIS_IMP_MEST_MATERIAL thu hoi le that bai");
                }
            }
        }

        private List<ImpMestMaterialData> ProcessOrderOddAmount(List<V_HIS_IMP_MEST_MATERIAL> listMaterial)
        {
            List<ImpMestMaterialData> rs = new List<ImpMestMaterialData>();
            if (IsNotNullOrEmpty(listMaterial))
            {
                foreach (var item in listMaterial)
                {
                    decimal amount = item.AMOUNT;
                    decimal oddAmount = amount - Math.Floor(amount);
                    ImpMestMaterialData d = new ImpMestMaterialData();
                    d.ImpMestMaterial = item;
                    d.OddAmount = oddAmount;
                    if (oddAmount > 0)
                    {
                        rs.Add(d);
                    }
                }
                rs = rs.OrderByDescending(o => o.OddAmount).ToList();
            }
            return rs;
        }

        internal void RollbackData()
        {
            this.hisImpMestMaterialCreate.RollbackData();
            this.hisImpMestMaterialUpdate.RollbackData();
            if (this.hisImpMestCreates.Count > 0)
            {
                foreach (var create in this.hisImpMestCreates)
                {
                    create.RollbackData();
                }
            }
        }
    }

    public class ImpMestMaterialData
    {
        public V_HIS_IMP_MEST_MATERIAL ImpMestMaterial { get; set; }
        public decimal OddAmount { get; set; }
    }
}
