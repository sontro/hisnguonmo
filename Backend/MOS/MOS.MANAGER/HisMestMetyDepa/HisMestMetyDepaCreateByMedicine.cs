using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMestMetyDepa
{
    class HisMestMetyDepaCreateByMedicine : BusinessBase
    {

        private HisMestMetyDepaCreate hisMestMetyDepaCreate = null;
        private HisMestMetyDepaUpdate hisMestMetyDepaUpdate = null;
        private HisMestMetyDepaTruncate hisMestMetyDepaTruncate = null;

        internal HisMestMetyDepaCreateByMedicine()
            : base()
        {
            this.Init();
        }

        internal HisMestMetyDepaCreateByMedicine(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMestMetyDepaCreate = new HisMestMetyDepaCreate(param);
            this.hisMestMetyDepaUpdate = new HisMestMetyDepaUpdate(param);
            this.hisMestMetyDepaTruncate = new HisMestMetyDepaTruncate(param);
        }

        internal bool Run(HisMestMetyDepaByMedicineSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    List<HIS_MEST_METY_DEPA> inserts = new List<HIS_MEST_METY_DEPA>();
                    List<HIS_MEST_METY_DEPA> deletes = new List<HIS_MEST_METY_DEPA>();
                    List<HIS_MEST_METY_DEPA> updates = new List<HIS_MEST_METY_DEPA>();
                    List<HIS_MEST_METY_DEPA> beforeUpdates = new List<HIS_MEST_METY_DEPA>();

                    Mapper.CreateMap<HIS_MEST_METY_DEPA, HIS_MEST_METY_DEPA>();

                    HisMestMetyDepaFilterQuery filter = new HisMestMetyDepaFilterQuery();
                    filter.MEDICINE_TYPE_ID = data.MedicineTypeId;
                    filter.HAS_MEDI_STOCK_ID = false;

                    List<HIS_MEST_METY_DEPA> olds = new HisMestMetyDepaGet().Get(filter);

                    if (IsNotNullOrEmpty(data.DepartmentIds))
                    {
                        foreach (long departmentId in data.DepartmentIds)
                        {
                            HIS_MEST_METY_DEPA old = olds != null ? olds.FirstOrDefault(o => o.DEPARTMENT_ID == departmentId) : null;
                            if (old == null)
                            {
                                HIS_MEST_METY_DEPA d = new HIS_MEST_METY_DEPA();
                                d.DEPARTMENT_ID = departmentId;
                                d.MEDICINE_TYPE_ID = data.MedicineTypeId;
                                inserts.Add(d);
                            }
                            else
                            {
                                if (old.IS_JUST_PRESCRIPTION == Constant.IS_TRUE)
                                {
                                    HIS_MEST_METY_DEPA before = Mapper.Map<HIS_MEST_METY_DEPA>(old);
                                    old.IS_JUST_PRESCRIPTION = null;
                                    updates.Add(old);
                                    beforeUpdates.Add(before);
                                }
                            }
                        }
                    }

                    deletes = olds != null ? olds.Where(o => data.DepartmentIds == null || !data.DepartmentIds.Contains(o.DEPARTMENT_ID)).ToList() : null;


                    if (IsNotNullOrEmpty(inserts) && !this.hisMestMetyDepaCreate.CreateList(inserts, true))
                    {
                        throw new Exception("hisMestMetyDepaCreate. Ket thuc nghiep vu");
                    }
                    if (IsNotNullOrEmpty(updates) && !this.hisMestMetyDepaUpdate.UpdateList(updates, beforeUpdates, true))
                    {
                        throw new Exception("hisMestMetyDepaUpdate. Ket thuc nghiep vu");
                    }
                    if (IsNotNullOrEmpty(deletes) && !this.hisMestMetyDepaTruncate.TruncateList(deletes))
                    {
                        throw new Exception("hisMestMetyDepaTruncate. Ket thuc nghiep vu");
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void Rollback()
        {
            this.hisMestMetyDepaCreate.RollbackData();
        }
    }
}
