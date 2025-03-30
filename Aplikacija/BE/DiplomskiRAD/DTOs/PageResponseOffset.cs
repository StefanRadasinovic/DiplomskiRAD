namespace DiplomskiRAD.DTOs
{
    public class PageResponseOffset<T>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public List<T> Data { get; init; }

        public PageResponseOffset(List<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        }
    }
}
