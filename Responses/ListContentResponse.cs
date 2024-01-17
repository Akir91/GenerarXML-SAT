namespace SP_Entities.Responses
{
    public class ListContentResponse<T> : Response
    {
        public List<T>? Data { get; set; } = new();
    }
}
