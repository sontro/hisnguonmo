using Inventec.Core;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using SAR.MANAGER.Core.SarFormData.Get;
using SAR.MANAGER.Core.SarFormType.Get;
using SAR.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.MANAGER.Core.SarForm.Create
{
    class SarFormCreateOrUpdateBehaviorEv : BeanObjectBase, ISarFormCreate
    {
        SarFormCreateOrUpdateSDO entity;
        SAR_FORM_TYPE CurrentFormType;

        internal SarFormCreateOrUpdateBehaviorEv(CommonParam param, SarFormCreateOrUpdateSDO data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormCreate.Run()
        {
            bool result = false;
            try
            {
                result = IsNotNull(this.entity);
                result = result && Check();
                if (result)
                {
                    if (this.entity.FormId.HasValue)
                    {
                        UpdateDataProcessor(ref result);
                    }
                    else
                    {
                        CreateDataProcessor(ref result);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Khoa du lieu SAR_FORM_DATA cu truoc khi tao du lieu SAR_FORM_DATA moi 
        /// </summary>
        /// <param name="result"></param>
        private void UpdateDataProcessor(ref bool result)
        {
            try
            {
                SAR_FORM updateData = new SAR_FORM();
                bool valid = true;
                valid = valid && SarFormCheckVerifyId.Verify(param, this.entity.FormId ?? 0, ref updateData);
                if (valid)
                {
                    updateData.DESCRIPTION = this.entity.Description;
                    if (this.CurrentFormType != null && this.CurrentFormType.ID > 0)
                    {
                        updateData.FORM_TYPE_ID = this.CurrentFormType.ID;
                    }

                    if (UpdateIsDeleteFormData(updateData) && DAOWorker.SarFormDAO.Update(updateData))
                    {
                        foreach (var formData in this.entity.FormData)
                        {
                            formData.FORM_ID = updateData.ID;
                        }

                        if (!DAOWorker.SarFormDataDAO.CreateList(this.entity.FormData))
                        {
                            SAR.MANAGER.Base.BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__ThemMoiThatBai);
                            DAOWorker.SarFormDAO.Truncate(updateData);
                            throw new Exception("Them moi thong tin SarFormData that bai." + Inventec.Common.Logging.LogUtil.TraceData("data", this.entity.FormData));
                        }
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
        }

        private bool UpdateIsDeleteFormData(SAR_FORM updateData)
        {
            bool result = false;
            try
            {
                SarFormDataFilterQuery filterQuery = new SarFormDataFilterQuery();
                filterQuery.FORM_ID = updateData.ID;
                var formData = DAOWorker.SarFormDataDAO.Get(filterQuery.Query(), param);
                if (formData != null && formData.Count > 0)
                {
                    result = DAOWorker.SarFormDataDAO.DeleteList(formData);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void CreateDataProcessor(ref bool result)
        {
            try
            {
                SAR_FORM createData = new SAR_FORM();
                createData.DESCRIPTION = this.entity.Description;
                createData.FORM_TYPE_ID = this.CurrentFormType.ID;
                if (DAOWorker.SarFormDAO.Create(createData))
                {
                    foreach (var formData in this.entity.FormData)
                    {
                        formData.FORM_ID = createData.ID;
                    }

                    if (!DAOWorker.SarFormDataDAO.CreateList(this.entity.FormData))
                    {
                        SAR.MANAGER.Base.BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__ThemMoiThatBai);
                        DAOWorker.SarFormDAO.Truncate(createData);
                        throw new Exception("Them moi thong tin SarFormData that bai." + Inventec.Common.Logging.LogUtil.TraceData("data", this.entity.FormData));
                    }
                    this.entity.FormId = createData.ID;
                    result = true;
                }
                else
                {
                    SAR.MANAGER.Base.BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__ThemMoiThatBai);
                    result = false;
                    throw new Exception("Them moi thong tin SarFormData that bai." + Inventec.Common.Logging.LogUtil.TraceData("data", this.entity.FormData));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && IsNotNullOrEmpty(this.entity.FormData);
                if (this.entity.FormId.HasValue)
                {
                    result = result && SarFormCheckVerifyId.Verify(param, this.entity.FormId.Value);
                }
                result = result && CheckFormTypeCode(this.entity.FormTypeCode, ref CurrentFormType);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool CheckFormTypeCode(string FormTypeCode, ref SAR_FORM_TYPE CurrentFormType)
        {
            bool result = false;
            try
            {
                SarFormTypeFilterQuery filterQuery = new SarFormTypeFilterQuery();
                filterQuery.FORM_TYPE_CODE__EXACT = FormTypeCode;
                var formType = DAOWorker.SarFormTypeDAO.Get(filterQuery.Query(), param);
                if (formType != null && formType.Count == 1)
                {
                    CurrentFormType = formType.FirstOrDefault();
                    result = true;
                }
                else
                {
                    CurrentFormType = null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                CurrentFormType = null;
            }
            return result;
        }
    }
}
