﻿using Nongwe.Web.Models;

namespace Nongwe.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
