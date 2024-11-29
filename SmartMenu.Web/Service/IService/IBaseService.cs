using SmartMenu.Web.Models;

namespace SmartMenu.Web.Service.IService
{
    /// <summary>
    /// Defines a base service for sending HTTP requests.
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="requestDto">The request data transfer object.</param>
        /// <param name="withBearer">Indicates whether to include a bearer token in the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data transfer object.</returns>
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
