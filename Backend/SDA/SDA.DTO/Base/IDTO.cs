
namespace SDA.DTO.Base
{
    public interface IDTO<DTO, RAW>
    {
        DTO CreateDTO(RAW raw);
        RAW CreateRaw(DTO data);
        void UpdateDTO(RAW raw, DTO data);
        void UpdateRaw(DTO data, RAW raw);
        void ProcessNullActiveDelete(DTO data);
    }
}
