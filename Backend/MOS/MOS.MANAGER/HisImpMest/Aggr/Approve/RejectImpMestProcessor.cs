using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr.Approve
{
    class RejectMedicine
    {
        public long MOBA_EXP_MEST_ID { get; set; }
        public HIS_IMP_MEST_MEDICINE Medicine { get; set; }
    }

    class RejectMaterial
    {
        public long MOBA_EXP_MEST_ID { get; set; }
        public HIS_IMP_MEST_MATERIAL Material { get; set; }
    }

    /// <summary>
    /// Xu ly de tao phieu nhap luu thuoc/vat tu bi tu choi duyet
    /// </summary>
    public class RejectImpMestProcessor : BusinessBase
    {
        private HisImpMestCreate hisImpMestCreate;
        private HisImpMestMedicineCreate hisImpMestMedicineCreate;
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;
        private HisImpMestMedicineUpdate hisImpMestMedicineUpdate;
        private HisImpMestMaterialUpdate hisImpMestMaterialUpdate;

        internal RejectImpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal RejectImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
            this.hisImpMestMaterialUpdate = new HisImpMestMaterialUpdate(param);
            this.hisImpMestMedicineCreate = new HisImpMestMedicineCreate(param);
            this.hisImpMestMedicineUpdate = new HisImpMestMedicineUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST aggrImpMest, WorkPlaceSDO workPlace, long? rejectedMediStockId, List<HIS_IMP_MEST> childs, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, List<MobaMedicineSDO> rejectedMedicineList, List<MobaMaterialSDO> rejectedMaterialList, ref List<HIS_IMP_MEST> newImpMests)
        {
            bool result = false;
            try
            {
                //Luu cac du lieu de tao cac phieu tra moi tuong ung voi so luong bi tu choi
                List<RejectMedicine> newMedicines = new List<RejectMedicine>();
                List<RejectMaterial> newMaterials = new List<RejectMaterial>();

                //Luu cac du lieu cua phieu cu can update so luong con lai sau khi bi tu choi 1 phan
                List<HIS_IMP_MEST_MEDICINE> oldUpdateMedicines = new List<HIS_IMP_MEST_MEDICINE>();
                List<HIS_IMP_MEST_MATERIAL> oldUpdateMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                List<HIS_IMP_MEST_MEDICINE> oldBeforeUpdateMedicines = new List<HIS_IMP_MEST_MEDICINE>();
                List<HIS_IMP_MEST_MATERIAL> oldBeforeUpdateMaterials = new List<HIS_IMP_MEST_MATERIAL>();

                this.PrepareMaterial(childs, rejectedMaterialList, impMestMaterials, ref oldUpdateMaterials, ref oldBeforeUpdateMaterials, ref newMaterials);
                this.PrepareMedicine(childs, rejectedMedicineList, impMestMedicines, ref oldUpdateMedicines, ref oldBeforeUpdateMedicines, ref newMedicines);

                //Tao ra cac phieu tra moi
                newImpMests = this.CreateImpMest(aggrImpMest, childs, workPlace, newMedicines, newMaterials, rejectedMediStockId);

                //Tao ra cac chi tiet cua phieu nhap moi
                List<HIS_IMP_MEST_MEDICINE> newImpMestMedicines = this.CreateImpMestMedicine(newMedicines, newImpMests, ref oldUpdateMedicines, ref oldBeforeUpdateMedicines);
                List<HIS_IMP_MEST_MATERIAL> newImpMestMaterials = this.CreateImpMestMaterial(newMaterials, newImpMests, ref oldUpdateMaterials, ref oldBeforeUpdateMaterials);
                
                //Cap nhat chi tiet cua cac phieu nhap cu
                List<HIS_IMP_MEST_MEDICINE> updateImpMestMedicines = this.UpdateImpMestMedicine(oldUpdateMedicines, oldBeforeUpdateMedicines);
                List<HIS_IMP_MEST_MATERIAL> updateImpMestMaterials = this.UpdateImpMestMaterial(oldUpdateMaterials, oldBeforeUpdateMaterials);
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Xu ly de tao du lieu his_imp_mest
        /// </summary>
        /// <param name="newMedicines"></param>
        /// <param name="newMaterials"></param>
        /// <param name="rejectedMediStockId"></param>
        /// <returns></returns>
        private List<HIS_IMP_MEST> CreateImpMest(HIS_IMP_MEST aggrImpMest, List<HIS_IMP_MEST> childs, WorkPlaceSDO workPlace, List<RejectMedicine> newMedicines, List<RejectMaterial> newMaterials, long? rejectedMediStockId)
        {
            if ((IsNotNullOrEmpty(newMaterials) || IsNotNullOrEmpty(newMedicines)) && rejectedMediStockId.HasValue)
            {
                List<HIS_IMP_MEST> toInsertImpMests = new List<HIS_IMP_MEST>();

                //Duyet de tao ra cac phieu thu hoi chuyen ve kho luu thuoc/vat tu bi tu choi duyet
                //Tuong ung voi 1 phieu xuat khac nhau thi tao 1 phieu nhap tuong ung
                if (IsNotNullOrEmpty(newMedicines))
                {
                    foreach (RejectMedicine m in newMedicines)
                    {
                        if (!toInsertImpMests.Exists(t => t.MOBA_EXP_MEST_ID == m.MOBA_EXP_MEST_ID))
                        {
                            //Lay phieu nhap cu de copy thong tin
                            HIS_IMP_MEST old = childs != null ? childs.Where(o => o.MOBA_EXP_MEST_ID.HasValue && o.MOBA_EXP_MEST_ID == m.MOBA_EXP_MEST_ID).OrderByDescending(o => o.ID).FirstOrDefault() : null;
                            if (old != null)
                            {
                                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                                impMest.MEDI_STOCK_ID = rejectedMediStockId.Value;
                                impMest.MOBA_EXP_MEST_ID = m.MOBA_EXP_MEST_ID;
                                impMest.REQ_ROOM_ID = workPlace.RoomId;
                                impMest.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                                impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                                impMest.IMP_MEST_TYPE_ID = old.IMP_MEST_TYPE_ID;
                                impMest.APPROVAL_IMP_MEST_ID = aggrImpMest.ID;
                                HisImpMestUtil.SetTdl(impMest, old);

                                toInsertImpMests.Add(impMest);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(newMaterials))
                {
                    foreach (RejectMaterial m in newMaterials)
                    {
                        if (!toInsertImpMests.Exists(t => t.MOBA_EXP_MEST_ID == m.MOBA_EXP_MEST_ID))
                        {
                            //Lay phieu nhap cu de copy thong tin
                            HIS_IMP_MEST old = childs != null ? childs.Where(o => o.MOBA_EXP_MEST_ID.HasValue && o.MOBA_EXP_MEST_ID == m.MOBA_EXP_MEST_ID).OrderByDescending(o => o.ID).FirstOrDefault() : null;
                            if (old != null)
                            {
                                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                                impMest.MEDI_STOCK_ID = rejectedMediStockId.Value;
                                impMest.MOBA_EXP_MEST_ID = m.MOBA_EXP_MEST_ID;
                                impMest.REQ_ROOM_ID = workPlace.RoomId;
                                impMest.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                                impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                                impMest.IMP_MEST_TYPE_ID = old.IMP_MEST_TYPE_ID;
                                impMest.APPROVAL_IMP_MEST_ID = aggrImpMest.ID;

                                HisImpMestUtil.SetTdl(impMest, old);

                                toInsertImpMests.Add(impMest);
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(toInsertImpMests) && !this.hisImpMestCreate.CreateList(toInsertImpMests))
                {
                    throw new Exception("Insert du lieu HIS_IMP_MEST that bai");
                }
                return toInsertImpMests;
            }
            return null;
        }

        private List<HIS_IMP_MEST_MEDICINE> CreateImpMestMedicine(List<RejectMedicine> newMedicines, List<HIS_IMP_MEST> impMests, ref List<HIS_IMP_MEST_MEDICINE> toUpdateImpMestIds, ref List<HIS_IMP_MEST_MEDICINE> beforeUpdateImpMestIds)
        {
            if (IsNotNullOrEmpty(newMedicines))
            {
                List<HIS_IMP_MEST_MEDICINE> toInsertImpMestMedicines = new List<HIS_IMP_MEST_MEDICINE>();
                Mapper.CreateMap<HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();

                foreach(HIS_IMP_MEST imp in impMests)
                {
                    List<HIS_IMP_MEST_MEDICINE> impMestMedicines = newMedicines
                        .Where(o => imp.MOBA_EXP_MEST_ID.HasValue
                            && o.MOBA_EXP_MEST_ID == imp.MOBA_EXP_MEST_ID.Value
                            && o.Medicine.ID <= 0) //chi lay ra cac ban ghi can insert
                        .Select(o => o.Medicine).ToList();

                    List<HIS_IMP_MEST_MEDICINE> toUpdates = newMedicines
                        .Where(o => imp.MOBA_EXP_MEST_ID.HasValue
                            && o.MOBA_EXP_MEST_ID == imp.MOBA_EXP_MEST_ID.Value
                            && o.Medicine.ID > 0) //chi lay ra cac ban ghi can update
                        .Select(o => o.Medicine).ToList();

                    if (IsNotNullOrEmpty(impMestMedicines))
                    {
                        impMestMedicines.ForEach(o => o.IMP_MEST_ID = imp.ID);
                        toInsertImpMestMedicines.AddRange(impMestMedicines);
                    }

                    if (IsNotNullOrEmpty(toUpdates))
                    {
                        List<HIS_IMP_MEST_MEDICINE> beforeUpdates = Mapper.Map<List<HIS_IMP_MEST_MEDICINE>>(toUpdates);
                        toUpdates.ForEach(o => o.IMP_MEST_ID = imp.ID);

                        toUpdateImpMestIds.AddRange(toUpdates);
                        beforeUpdateImpMestIds.AddRange(beforeUpdates);
                    }
                }


                if (IsNotNullOrEmpty(toInsertImpMestMedicines) && !this.hisImpMestMedicineCreate.CreateList(toInsertImpMestMedicines))
                {
                    throw new Exception("Insert du lieu HIS_IMP_MEST_MEDICINE that bai");
                }
                return toInsertImpMestMedicines;
            }
            return null;
        }

        private List<HIS_IMP_MEST_MATERIAL> CreateImpMestMaterial(List<RejectMaterial> newMaterials, List<HIS_IMP_MEST> impMests, ref List<HIS_IMP_MEST_MATERIAL> toUpdateImpMestIds, ref List<HIS_IMP_MEST_MATERIAL> beforeUpdateImpMestIds)
        {
            if (IsNotNullOrEmpty(newMaterials))
            {
                List<HIS_IMP_MEST_MATERIAL> toInsertImpMestMaterials = new List<HIS_IMP_MEST_MATERIAL>();
                Mapper.CreateMap<HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();

                foreach (HIS_IMP_MEST imp in impMests)
                {
                    List<HIS_IMP_MEST_MATERIAL> impMestMaterials = newMaterials
                        .Where(o => imp.MOBA_EXP_MEST_ID.HasValue 
                            && o.MOBA_EXP_MEST_ID == imp.MOBA_EXP_MEST_ID.Value
                            && o.Material.ID <= 0) //chi lay ra cac ban ghi can insert
                        .Select(o => o.Material).ToList();

                    List<HIS_IMP_MEST_MATERIAL> toUpdates = newMaterials
                        .Where(o => imp.MOBA_EXP_MEST_ID.HasValue
                            && o.MOBA_EXP_MEST_ID == imp.MOBA_EXP_MEST_ID.Value
                            && o.Material.ID > 0) //chi lay ra cac ban ghi can update
                        .Select(o => o.Material).ToList();

                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        impMestMaterials.ForEach(o => o.IMP_MEST_ID = imp.ID);
                        toInsertImpMestMaterials.AddRange(impMestMaterials);
                    }

                    if (IsNotNullOrEmpty(toUpdates))
                    {
                        List<HIS_IMP_MEST_MATERIAL> beforeUpdates = Mapper.Map<List<HIS_IMP_MEST_MATERIAL>>(toUpdates);
                        toUpdates.ForEach(o => o.IMP_MEST_ID = imp.ID);

                        toUpdateImpMestIds.AddRange(toUpdates);
                        beforeUpdateImpMestIds.AddRange(beforeUpdates);
                    }
                }


                if (IsNotNullOrEmpty(toInsertImpMestMaterials) && !this.hisImpMestMaterialCreate.CreateList(toInsertImpMestMaterials))
                {
                    throw new Exception("Insert du lieu HIS_IMP_MEST_MATERIAL that bai");
                }
                return toInsertImpMestMaterials;
            }
            return null;
        }

        private List<HIS_IMP_MEST_MEDICINE> UpdateImpMestMedicine(List<HIS_IMP_MEST_MEDICINE> oldUpdateMedicines, List<HIS_IMP_MEST_MEDICINE> oldBeforeUpdateMedicines)
        {
            if (IsNotNullOrEmpty(oldUpdateMedicines))
            {
                if (IsNotNullOrEmpty(oldUpdateMedicines) && !this.hisImpMestMedicineUpdate.UpdateList(oldUpdateMedicines, oldBeforeUpdateMedicines))
                {
                    throw new Exception("Update du lieu HIS_IMP_MEST_MEDICINE that bai");
                }
                return oldUpdateMedicines;
            }
            return null;
        }

        private List<HIS_IMP_MEST_MATERIAL> UpdateImpMestMaterial(List<HIS_IMP_MEST_MATERIAL> oldUpdateMaterials, List<HIS_IMP_MEST_MATERIAL> oldBeforeUpdateMaterials)
        {
            if (IsNotNullOrEmpty(oldUpdateMaterials))
            {
                if (IsNotNullOrEmpty(oldUpdateMaterials) && !this.hisImpMestMaterialUpdate.UpdateList(oldUpdateMaterials, oldBeforeUpdateMaterials))
                {
                    throw new Exception("Update du lieu HIS_IMP_MEST_MATERIAL that bai");
                }
                return oldUpdateMaterials;
            }
            return null;
        }

        private void PrepareMaterial(List<HIS_IMP_MEST> childs, List<MobaMaterialSDO> rejectedMaterialList, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<HIS_IMP_MEST_MATERIAL> oldUpdateMaterials, ref List<HIS_IMP_MEST_MATERIAL> oldBeforeUpdateMaterials, ref List<RejectMaterial> newMaterials)
        {
            if (IsNotNullOrEmpty(rejectedMaterialList) && IsNotNullOrEmpty(impMestMaterials))
            {
                //sap xep truoc khi duyet
                impMestMaterials = impMestMaterials.OrderByDescending(o => o.AMOUNT).OrderBy(o => o.IMP_MEST_ID).ToList();

                Mapper.CreateMap<HIS_IMP_MEST_MATERIAL, HIS_IMP_MEST_MATERIAL>();

                foreach (MobaMaterialSDO rejected in rejectedMaterialList)
                {
                    decimal remain = rejected.Amount;
                    List<HIS_IMP_MEST_MATERIAL> toUses = impMestMaterials.Where(o => o.MATERIAL_ID == rejected.MaterialId).ToList();

                    int index = 0;

                    while (remain > 0)
                    {
                        HIS_IMP_MEST impMest = childs.Where(o => o.ID == toUses[index].IMP_MEST_ID && o.MOBA_EXP_MEST_ID.HasValue).FirstOrDefault();

                        if (impMest != null)
                        {
                            RejectMaterial newMaterial = new RejectMaterial();
                            newMaterial.MOBA_EXP_MEST_ID = impMest.MOBA_EXP_MEST_ID.Value;

                            //Tao ban ghi moi de gan voi phieu nhap moi
                            HIS_IMP_MEST_MATERIAL impMestMaterial = new HIS_IMP_MEST_MATERIAL();
                            impMestMaterial.MATERIAL_ID = toUses[index].MATERIAL_ID;
                            impMestMaterial.TH_EXP_MEST_MATERIAL_ID = toUses[index].TH_EXP_MEST_MATERIAL_ID;
                            impMestMaterial.SERIAL_NUMBER = toUses[index].SERIAL_NUMBER;
                            impMestMaterial.REMAIN_REUSE_COUNT = toUses[index].REMAIN_REUSE_COUNT;
                            impMestMaterial.NOTE = rejected.Note;

                            if (remain >= toUses[index].AMOUNT)
                            {
                                impMestMaterial.AMOUNT = toUses[index].AMOUNT;
                            }
                            else
                            {
                                impMestMaterial.AMOUNT = remain;
                            }


                            newMaterial.Material = impMestMaterial;

                            //Dong thoi cap nhat de giam so luong cua phieu tra hien tai
                            HIS_IMP_MEST_MATERIAL beforeUpdate = Mapper.Map<HIS_IMP_MEST_MATERIAL>(toUses[index]);

                            toUses[index].AMOUNT = toUses[index].AMOUNT - impMestMaterial.AMOUNT;
                            toUses[index].NOTE = rejected.Note;

                            oldUpdateMaterials.Add(toUses[index]);
                            newMaterials.Add(newMaterial);
                            oldBeforeUpdateMaterials.Add(beforeUpdate);

                            remain = remain - newMaterial.Material.AMOUNT;
                        }
                        index++;
                    }
                }
            }
        }

        private void PrepareMedicine(List<HIS_IMP_MEST> childs, List<MobaMedicineSDO> rejectedMedicineList, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<HIS_IMP_MEST_MEDICINE> oldUpdateMedicines, ref List<HIS_IMP_MEST_MEDICINE> oldBeforeUpdateMedicines, ref List<RejectMedicine> newMedicines)
        {
            if (IsNotNullOrEmpty(rejectedMedicineList) && IsNotNullOrEmpty(impMestMedicines))
            {
                Mapper.CreateMap<HIS_IMP_MEST_MEDICINE, HIS_IMP_MEST_MEDICINE>();

                //sap xep truoc khi duyet
                impMestMedicines = impMestMedicines.OrderByDescending(o => o.AMOUNT).OrderBy(o => o.IMP_MEST_ID).ToList();

                foreach (MobaMedicineSDO rejected in rejectedMedicineList)
                {
                    decimal remain = rejected.Amount;
                    List<HIS_IMP_MEST_MEDICINE> toUses = impMestMedicines.Where(o => o.MEDICINE_ID == rejected.MedicineId).ToList();

                    int index = 0;

                    while (remain > 0)
                    {
                        HIS_IMP_MEST impMest = childs.Where(o => o.ID == toUses[index].IMP_MEST_ID && o.MOBA_EXP_MEST_ID.HasValue).FirstOrDefault();
                        if (impMest != null)
                        {
                            RejectMedicine newMedicine = new RejectMedicine();
                            newMedicine.MOBA_EXP_MEST_ID = impMest.MOBA_EXP_MEST_ID.Value;

                            //Tao ban ghi moi de gan voi phieu nhap moi
                            HIS_IMP_MEST_MEDICINE impMestMedicine = new HIS_IMP_MEST_MEDICINE();
                            impMestMedicine.MEDICINE_ID = toUses[index].MEDICINE_ID;
                            impMestMedicine.TH_EXP_MEST_MEDICINE_ID = toUses[index].TH_EXP_MEST_MEDICINE_ID;
                            impMestMedicine.NOTE = rejected.Note;

                            if (remain >= toUses[index].AMOUNT)
                            {
                                impMestMedicine.AMOUNT = toUses[index].AMOUNT;
                            }
                            else
                            {
                                impMestMedicine.AMOUNT = remain;
                            }


                            newMedicine.Medicine = impMestMedicine;

                            //Dong thoi cap nhat de giam so luong cua phieu tra hien tai
                            HIS_IMP_MEST_MEDICINE beforeUpdate = Mapper.Map<HIS_IMP_MEST_MEDICINE>(toUses[index]);
                            toUses[index].AMOUNT = toUses[index].AMOUNT - impMestMedicine.AMOUNT;
                            toUses[index].NOTE = rejected.Note;

                            oldUpdateMedicines.Add(toUses[index]);
                            oldBeforeUpdateMedicines.Add(beforeUpdate);
                            newMedicines.Add(newMedicine);

                            remain = remain - newMedicine.Medicine.AMOUNT;
                            
                        }
                        
                        index++;
                    }
                }
            }
        }

        public void Rollback()
        {
            try
            {
                this.hisImpMestMaterialCreate.RollbackData();
                this.hisImpMestMedicineCreate.RollbackData();
                this.hisImpMestCreate.RollbackData();
                this.hisImpMestMaterialUpdate.RollbackData();
                this.hisImpMestMedicineUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
