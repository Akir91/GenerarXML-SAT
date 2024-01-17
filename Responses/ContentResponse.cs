namespace SP_Entities.Responses
{
    public class ContentResponse<T> : Response
    {
        public T? Data { get; set; } = default(T?);
    }
}
