using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAdrMedicineType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO.Update
{
    class AdrMedicineTypeProcessor : BusinessBase
    {
        private HisAdrMedicineTypeCreate hisAdrMedicineTypeCreate;
        private HisAdrMedicineTypeUpdate hisAdrMedicineTypeUpdate;

        internal AdrMedicineTypeProcessor()
            : base()
        {
            this.Init();
        }

        internal AdrMedicineTypeProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisAdrMedicineTypeCreate = new HisAdrMedicineTypeCreate(param);
            this.hisAdrMedicineTypeUpdate = new HisAdrMedicineTypeUpdate(param);
        }

        internal bool Run(HisAdrSDO data, HIS_ADR adr,List<HIS_ADR_MEDICINE_TYPE> olds, ref List<HIS_ADR_MEDICINE_TYPE> adrMedicineTypes, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (data != null && IsNotNullOrEmpty(data.AdrMedicineTypes))
                {
                    data.AdrMedicineTypes.ForEach(o => o.ADR_ID = adr.ID);
                    List<HIS_ADR_MEDICINE_TYPE> updates = new List<HIS_ADR_MEDICINE_TYPE>();
                    List<HIS_ADR_MEDICINE_TYPE> inserts = new List<HIS_ADR_MEDICINE_TYPE>();
                    List<HIS_ADR_MEDICINE_TYPE> deletes = new List<HIS_ADR_MEDICINE_TYPE>();
                    List<HIS_ADR_MEDICINE_TYPE> befores = new List<HIS_ADR_MEDICINE_TYPE>();

                    Mapper.CreateMap<HIS_ADR_MEDICINE_TYPE, HIS_ADR_MEDICINE_TYPE>();

                    foreach (HIS_ADR_MEDICINE_TYPE adrMety in data.AdrMedicineTypes)
                    {
                        if (adrMety.ID > 0)
                        {
                            HIS_ADR_MEDICINE_TYPE exists = olds.FirstOrDefault(o => o.ID == adrMety.ID);
                            if (exists == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("Khong tim thay HIS_ADR_MEDICINE_TYPE tuong ung voi ID: " + adrMety.ID);
                            }
                            befores.Add(Mapper.Map<HIS_ADR_MEDICINE_TYPE>(exists));
                            updates.Add(adrMety);
                        }
                        else
                        {
                            inserts.Add(adrMety);
                        }
                    }

                    deletes = olds != null ? olds.Where(s => !updates.Exists(e => e.ID == s.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(inserts))
                    {
                        if (!this.hisAdrMedicineTypeCreate.CreateList(inserts))
                        {
                            throw new Exception("Tao AdrMedicineType that bai. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(updates))
                    {
                        if (!this.hisAdrMedicineTypeUpdate.UpdateList(updates, befores))
                        {
                            throw new Exception("Cap nhat AdrMedicineType that bai. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(deletes))
                    {
                        if (sqls == null) sqls = new List<string>();
                        string sqlDelete = DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_ADR_MEDICINE_TYPE WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sqlDelete);
                    }

                    adrMedicineTypes = new List<HIS_ADR_MEDICINE_TYPE>();
                    if (IsNotNullOrEmpty(updates))
                    {
                        adrMedicineTypes.AddRange(updates);
                    }
                    if (IsNotNullOrEmpty(inserts))
                    {
                        adrMedicineTypes.AddRange(inserts);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisAdrMedicineTypeUpdate.RollbackData();
                this.hisAdrMedicineTypeCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
