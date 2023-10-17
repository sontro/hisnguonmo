using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSarReportDTO : SDA_V_SAR_REPORT
    {
        public VSarReportDTO()
        {
        }

        public VSarReportDTO CreateDTO(SDA_V_SAR_REPORT raw)
        {
            try
            {
                if (raw != null)
                {
                    VSarReportDTO dto = new VSarReportDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SAR_REPORT>();
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
