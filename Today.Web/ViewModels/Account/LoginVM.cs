﻿using System.ComponentModel.DataAnnotations;

namespace Today.Web.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "是要不要填蛤?")] //前端檢核
        [EmailAddress] //前端檢核
        public string Email { get; set; }

        [Required] //前端檢核
        [DataType(DataType.Password)] //前端檢核 讓密碼名稱看不見，變成*
        //[StringLength(3, ErrorMessage = "沒機會顯示")] //前端檢核 密碼最多只能輸入3碼
        public string Password { get; set; }
    }


    //public class A
    //{
    //    public LoginVM loginVM { get; set; }
    //    public SignUpVM signUpVM { get; set; }
    //}
}
