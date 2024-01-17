namespace SP_Entities.Responses
{
    public class PagedResponse<T> : Response
    {
        public int Page { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
