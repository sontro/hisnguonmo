using Inventec.Common.Repository;
using SDA.DTO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class EvSdaGroupDTO : SDA_EV_SDA_GROUP, IDTO<EvSdaGroupDTO, SDA_EV_SDA_GROUP>
    {
        public EvSdaGroupDTO()
        {
            IS_ACTIVE = ((IS_ACTIVE == null) ? (short)1 : IS_ACTIVE);
            IS_DELETE = ((IS_DELETE == null) ? (short)0 : IS_DELETE);
        }
        
        public void ProcessNullActiveDelete(EvSdaGroupDTO data)
        {
            try
            {
                if (data != null)
                {
                    data.IS_ACTIVE = ((data.IS_ACTIVE == null) ? (short)1 : data.IS_ACTIVE);
                    data.IS_DELETE = ((data.IS_DELETE == null) ? (short)0 : data.IS_DELETE);
                }
            }
            catch (Exception)
            {
                
            }
        }

        public EvSdaGroupDTO CreateDTO(SDA_EV_SDA_GROUP raw)
        {
            try
            {
                if (raw != null)
                {
                    EvSdaGroupDTO dto = new EvSdaGroupDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SDA_GROUP>();
                    foreach (var item in pi)
                    {
                        item.SetValue(dto, (item.GetValue(raw)));
                    }
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SDA_EV_SDA_GROUP CreateRaw(EvSdaGroupDTO data)
        {
            try
            {
                if (data != null)
                {
                    SDA_EV_SDA_GROUP raw = new SDA_EV_SDA_GROUP();
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SDA_GROUP>();
                    foreach (var item in pi)
                    {
                        item.SetValue(raw, (item.GetValue(data)));
                    }
                    raw.APP_CREATOR = DTOConstant.APP_NAME;
                    raw.APP_MODIFIER = raw.APP_CREATOR;
                    raw.CREATOR = Inventec.Token.Manager.GetLoginname();
                    raw.MODIFIER = Inventec.Token.Manager.GetLoginname();
                    raw.GROUP_CODE = Inventec.Token.Manager.GetGroupCode();
                    return raw;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateDTO(SDA_EV_SDA_GROUP raw, EvSdaGroupDTO data)
        {
            try
            {
                #region validate param
                if (raw == null) throw new ArgumentNullException("raw");
                if (data == null) throw new ArgumentNullException("data");
                #endregion

                PropertyInfo[] pi = Properties.Get<SDA_EV_SDA_GROUP>();
                foreach (var item in pi)
                {
                    item.SetValue(data, (item.GetValue(raw)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateRaw(EvSdaGroupDTO data, SDA_EV_SDA_GROUP raw)
        {
            try
            {
                #region validate param
                if (raw == null) throw new ArgumentNullException("raw");
                if (data == null) throw new ArgumentNullException("data");
                #endregion
                
                if (data != null)
                {
                    SetNotChangeField(data, raw);
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SDA_GROUP>();
                    foreach (var item in pi)
                    {
                        item.SetValue(raw, (item.GetValue(data)));
                    }
                    raw.APP_MODIFIER = DTOConstant.APP_NAME;
                    raw.MODIFIER = Inventec.Token.Manager.GetLoginname();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void SetNotChangeField(EvSdaGroupDTO data, SDA_EV_SDA_GROUP raw)
        {
            try
            {
                #region validate param
                if (data == null) throw new ArgumentNullException("data");
                if (raw == null) throw new ArgumentNullException("raw");
                #endregion

                data.CREATOR = raw.CREATOR;
                data.APP_CREATOR = raw.APP_CREATOR;
                data.CREATE_TIME = raw.CREATE_TIME;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
