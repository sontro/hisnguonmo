using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Update
{
    class PrepareMatyProcessor : BusinessBase
    {
        private HisPrepareMatyCreate hisPrepareMatyCreate;
        private HisPrepareMatyUpdate hisPrepareMetyUpdate;

        internal PrepareMatyProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareMatyCreate = new HisPrepareMatyCreate(param);
            this.hisPrepareMetyUpdate = new HisPrepareMatyUpdate(param);
        }

        internal bool Run(HIS_PREPARE hisPrepare, List<HIS_PREPARE_MATY> prepareMatys, ref List<HIS_PREPARE_MATY> materials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_PREPARE_MATY> olds = new HisPrepareMatyGet().GetByPrepareId(hisPrepare.ID);
                if (IsNotNullOrEmpty(prepareMatys) || IsNotNullOrEmpty(olds))
                {
                    List<HIS_PREPARE_MATY> createList = new List<HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> updateList = new List<HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> beforeList = new List<HIS_PREPARE_MATY>();
                    List<HIS_PREPARE_MATY> deleteList = new List<HIS_PREPARE_MATY>();
                    if (IsNotNullOrEmpty(prepareMatys))
                    {
                        Mapper.CreateMap<HIS_PREPARE_MATY, HIS_PREPARE_MATY>();
                        foreach (HIS_PREPARE_MATY item in prepareMatys)
                        {
                            HIS_PREPARE_MATY old = olds != null ? olds.FirstOrDefault(o => o.MATERIAL_TYPE_ID == item.MATERIAL_TYPE_ID) : null;
                            if (old != null)
                            {
                                HIS_PREPARE_MATY before = Mapper.Map<HIS_PREPARE_MATY>(old);
                                old.REQ_AMOUNT = item.REQ_AMOUNT;
                                old.PREPARE_ID = hisPrepare.ID;
                                old.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                                updateList.Add(old);
                                beforeList.Add(before);
                            }
                            else
                            {
                                item.PREPARE_ID = hisPrepare.ID;
                                item.APPROVAL_AMOUNT = null;
                                item.TDL_TREATMENT_ID = hisPrepare.TREATMENT_ID;
                                createList.Add(item);
                            }
                        }
                    }

                    deleteList = olds != null ? olds.Where(o => !updateList.Exists(e => e.ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(updateList))
                    {
                        if (!this.hisPrepareMetyUpdate.UpdateList(updateList, beforeList))
                        {
                            throw new Exception("hisPrepareMetyUpdate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(createList))
                    {
                        if (!this.hisPrepareMatyCreate.CreateList(createList))
                        {
                            throw new Exception("hisPrepareMatyCreate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(deleteList))
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(deleteList.Select(s => s.ID).ToList(), "DELETE HIS_PREPARE_MATY WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }

                    updateList.AddRange(createList);
                    if (IsNotNullOrEmpty(updateList))
                    {
                        materials = updateList;
                    }
                }
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

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareMatyCreate.RollbackData();
                this.hisPrepareMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
