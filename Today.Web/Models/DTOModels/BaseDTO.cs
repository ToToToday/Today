﻿namespace Today.Web.Models.DTOModels
{
    public class BaseOutputDTO
    {
        public bool IsSuccess { get; set; } //true / false
        public string Message { get; set; } //null / 人看的錯誤訊息
    }
}
