using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaProvinceDTO : SDA_V_SDA_PROVINCE
    {
        public VSdaProvinceDTO()
        {
        }

        public VSdaProvinceDTO CreateDTO(SDA_V_SDA_PROVINCE raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaProvinceDTO dto = new VSdaProvinceDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_PROVINCE>();
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
