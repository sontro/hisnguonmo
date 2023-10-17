using Inventec.Common.Repository;
using SDA.DTO.Base;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class EvSarReportDTO : SDA_EV_SAR_REPORT, IDTO<EvSarReportDTO, SDA_EV_SAR_REPORT>
    {
        public EvSarReportDTO()
        {
            IS_ACTIVE = ((IS_ACTIVE == null) ? (short)1 : IS_ACTIVE);
            IS_DELETE = ((IS_DELETE == null) ? (short)0 : IS_DELETE);
        }
        
        public void ProcessNullActiveDelete(EvSarReportDTO data)
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

        public EvSarReportDTO CreateDTO(SDA_EV_SAR_REPORT raw)
        {
            try
            {
                if (raw != null)
                {
                    EvSarReportDTO dto = new EvSarReportDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SAR_REPORT>();
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

        public SDA_EV_SAR_REPORT CreateRaw(EvSarReportDTO data)
        {
            try
            {
                if (data != null)
                {
                    SDA_EV_SAR_REPORT raw = new SDA_EV_SAR_REPORT();
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SAR_REPORT>();
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

        public void UpdateDTO(SDA_EV_SAR_REPORT raw, EvSarReportDTO data)
        {
            try
            {
                #region validate param
                if (raw == null) throw new ArgumentNullException("raw");
                if (data == null) throw new ArgumentNullException("data");
                #endregion

                PropertyInfo[] pi = Properties.Get<SDA_EV_SAR_REPORT>();
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

        public void UpdateRaw(EvSarReportDTO data, SDA_EV_SAR_REPORT raw)
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
                    PropertyInfo[] pi = Properties.Get<SDA_EV_SAR_REPORT>();
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
        
        private void SetNotChangeField(EvSarReportDTO data, SDA_EV_SAR_REPORT raw)
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
