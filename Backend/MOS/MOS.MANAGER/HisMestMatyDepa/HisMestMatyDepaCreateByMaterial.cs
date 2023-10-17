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

namespace MOS.MANAGER.HisMestMatyDepa
{
    class HisMestMatyDepaCreateByMaterial : BusinessBase
    {

        private HisMestMatyDepaCreate hisMestMatyDepaCreate = null;
        private HisMestMatyDepaTruncate hisMestMatyDepaTruncate = null;
        private HisMestMatyDepaUpdate hisMestMatyDepaUpdate = null;

        internal HisMestMatyDepaCreateByMaterial()
            : base()
        {
            this.Init();
        }

        internal HisMestMatyDepaCreateByMaterial(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMestMatyDepaCreate = new HisMestMatyDepaCreate(param);
            this.hisMestMatyDepaTruncate = new HisMestMatyDepaTruncate(param);
            this.hisMestMatyDepaUpdate = new HisMestMatyDepaUpdate(param);
        }

        internal bool Run(HisMestMatyDepaByMaterialSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    List<HIS_MEST_MATY_DEPA> inserts = new List<HIS_MEST_MATY_DEPA>();
                    List<HIS_MEST_MATY_DEPA> deletes = new List<HIS_MEST_MATY_DEPA>();
                    List<HIS_MEST_MATY_DEPA> updates = new List<HIS_MEST_MATY_DEPA>();
                    List<HIS_MEST_MATY_DEPA> beforeUpdates = new List<HIS_MEST_MATY_DEPA>();

                    Mapper.CreateMap<HIS_MEST_MATY_DEPA, HIS_MEST_MATY_DEPA>();

                    HisMestMatyDepaFilterQuery filter = new HisMestMatyDepaFilterQuery();
                    filter.MATERIAL_TYPE_ID = data.MaterialTypeId;
                    filter.HAS_MEDI_STOCK_ID = false;
                    
                    List<HIS_MEST_MATY_DEPA> olds = new HisMestMatyDepaGet().Get(filter);

                    if (IsNotNullOrEmpty(data.DepartmentIds))
                    {
                        foreach (long departmentId in data.DepartmentIds)
                        {
                            HIS_MEST_MATY_DEPA old = olds != null ? olds.FirstOrDefault(o => o.DEPARTMENT_ID == departmentId) : null;
                            if (old == null)
                            {
                                HIS_MEST_MATY_DEPA d = new HIS_MEST_MATY_DEPA();
                                d.DEPARTMENT_ID = departmentId;
                                d.MATERIAL_TYPE_ID = data.MaterialTypeId;
                                inserts.Add(d);
                            }
                            else
                            {
                                if (old.IS_JUST_PRESCRIPTION == Constant.IS_TRUE)
                                {
                                    HIS_MEST_MATY_DEPA before = Mapper.Map<HIS_MEST_MATY_DEPA>(old);
                                    old.IS_JUST_PRESCRIPTION = null;
                                    updates.Add(old);
                                    beforeUpdates.Add(before);
                                }
                            }
                        }
                    }

                    deletes = olds != null ? olds.Where(o => data.DepartmentIds == null || !data.DepartmentIds.Contains(o.DEPARTMENT_ID)).ToList() : null;


                    if (IsNotNullOrEmpty(inserts) && !this.hisMestMatyDepaCreate.CreateList(inserts, true))
                    {
                        throw new Exception("hisMestMatyDepaCreate. Ket thuc nghiep vu");
                    }
                    if (IsNotNullOrEmpty(updates) && !this.hisMestMatyDepaUpdate.UpdateList(updates, beforeUpdates, true))
                    {
                        throw new Exception("hisMestMatyDepaUpdate. Ket thuc nghiep vu");
                    }
                    if (IsNotNullOrEmpty(deletes) && !this.hisMestMatyDepaTruncate.TruncateList(deletes))
                    {
                        throw new Exception("hisMestMatyDepaTruncate. Ket thuc nghiep vu");
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
            this.hisMestMatyDepaCreate.RollbackData();
        }
    }
}
