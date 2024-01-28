namespace Excellerent.SharedModules.DTO
{
    public class PredicatedResponseDTO
    {
        public PredicatedResponseDTO()
        {

        }
        public PredicatedResponseDTO(dynamic data, int totalRecord, int pageIndex, int pageSize, int totalPage)
        {
            Data = data;
            TotalRecord = totalRecord;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = totalPage;
        }
        public dynamic Data { get; set; }
        public int TotalRecord { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }

    }
}
