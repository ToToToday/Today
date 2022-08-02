using Microsoft.AspNetCore.Mvc;
using Today.Web.DTOModels.AccountDTO;
using Today.Web.Services.AccountService;
using Today.Web.ViewModels.Account;

namespace Today.Web.Controllers
{
    public class AccountController : Controller
    {
        //【型別】底下 才有方法成員
        // readonly 找我的 **IAccountService介面**
        // _service欄位
        private readonly IAccountService _service; // 宣告欄位

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        //AccountController右鍵產生【建構函式】
        //產生建構函式 就要去註冊DI


        //註冊
        [HttpPost]
        public IActionResult SignUp([FromForm] SignUpVM requestParam)
        {
            //1. 內建的 模型檢核 機制   (檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            if (!ModelState.IsValid)
            {
                //return View(requestParam);//體貼地將資料填回去
                //return
            }

            //2. 輸出 = service 方法(輸入)
            //參數型別 Mapping(映射) 成input型別
            //(這兩個型別 會有大量重複，也許可考慮用繼承)
            //把requestParam參數 變成 CreateAccountInputDTO
            var inputDto = new CreateAccountInputDTO
            {
                Email = requestParam.Email,
                Password = requestParam.Password,
            };

            //呼叫service介面裡面的方法
            var outputDto = _service.CreateAccount(inputDto);

            //檢查outputDto是否有成功
            //成敗分支(有成功就做分支判斷)
            if (!outputDto.IsSuccess)
            {
                //手動增加模型的Error 錯誤訊息
                ModelState.AddModelError(string.Empty, outputDto.Message);
                return View(requestParam); //體貼地將資料填回去
            }

            //最後 回傳View到首頁
            return Redirect("/");
        }


        //登入
        [HttpGet]
        public IActionResult Login() //顯示
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm] LoginVM requestParam) //收資料，資料庫寫入
        {
            //1. 內建的 模型檢核 機制   (檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            if (!ModelState.IsValid)
            {
                return Content("輸入不合規");
                //return View(requestParam);//體貼地將資料填回去
            }

            //2. 輸出 = service 方法(輸入)
            //參數型別 Mapping(映射) 成input型別
            //(這兩個型別 會有大量重複，也許可考慮用繼承)
            var inputDto = new LoginAccountInputDTO
            {
                Email = requestParam.Email,
                Password = requestParam.Password,
            };

            var outputDto = _service.LoginAccount(inputDto);

            //成敗分支(有成功就做分支判斷)
            if (!outputDto.IsSuccess)
            {
                //手動增加模型的Error 錯誤訊息
                ModelState.AddModelError(string.Empty, outputDto.Message);
                return Content("輸入不合規");
                //return View(requestParam); //體貼地將資料填回去
            }

            //最後
            //第一種 : 回傳View  (要Mapping成 目的地的VM)
            //第二種 : 重導向到 網址 (> 路由 > action)


            //重新導向(不需要VM) 至 來源【網址】，null的話就首頁
            //看是不是null，是null的話就改用後面那個
            return Redirect("/");
        } 


        //登出
        public IActionResult Logout()
        {
            //基本上就是把cookie刪除
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _service.LogoutAccount();

            //重新導向到首頁
            return Redirect("/");
            //return RedirectToAction("Index", "Home");
                                     //Action   Controller

        }
    }
}
