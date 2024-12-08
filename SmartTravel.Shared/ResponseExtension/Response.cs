namespace SmartTravel.Shared.ResponseExtension
{
    public record Response(
        ResponseResultEnum responseResult = ResponseResultEnum.None, 
        string message = "", object data = null, 
        IEnumerable<object> collection = null
        );
}
