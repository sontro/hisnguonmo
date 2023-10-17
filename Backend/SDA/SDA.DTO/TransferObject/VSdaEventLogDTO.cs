using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaEventLogDTO : SDA_V_SDA_EVENT_LOG
    {
        public VSdaEventLogDTO()
        {
        }

        public VSdaEventLogDTO CreateDTO(SDA_V_SDA_EVENT_LOG raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaEventLogDTO dto = new VSdaEventLogDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_EVENT_LOG>();
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
    }
}
