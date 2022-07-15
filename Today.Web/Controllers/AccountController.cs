using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Today.Web.Models.DTOModels;
using Today.Web.Models.DTOModels.Account;
using Today.Web.Services.AccountService;
using Today.Web.ViewModels.Account;

namespace Today.Web.Controllers
{
                    //路由
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

        [HttpGet]           //Action
        public IActionResult SignUp()
        {
            return View(); //new SignUpVM()
        }

        //接收post的Action
        //兩個方法名稱一樣，參數又一樣，就違反了多載的規則
        //要有方法多載的特性，參數名稱要不同
        [HttpPost]
        public IActionResult SignUp([FromForm]SignUpVM requestParam) //造一個型別來接收參數
        {
            //1. 內建的 模型檢核 機制   (檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            if (!ModelState.IsValid)
            {
                return View(requestParam);//體貼地將資料填回去
            }

            //2. 輸出 = service 方法(輸入)
            //參數型別 Mapping(映射) 成input型別
            //(這兩個型別 會有大量重複，也許可考慮用繼承)
            //把requestParam參數 變成 CreateAccountInputDTO
            var inputDto = new CreateAccountInputDTO
            {
                Email = requestParam.Email,
                Password = requestParam.Password,
                PasswordCheck = requestParam.PasswordCheck,
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

            //最後 要Mapping成 目的地的VM，回傳View
            var vm = new GoCheckEmailVM();
            return View("GoCheckEmail", vm);
        }

        /*
        //public IActionResult Verify([FromQuery]int VIP_NO) //(不需要)
        //{
        //    _service.VerifyAccount(VIP_NO);
        //    return View();
        //}
        */


        //以下登入                  //給他一個選擇性參數，因為他不一定會有，如果自己按【登入】按鈕時是null值，
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]LoginVM requestParam, string returnUrl = null)
        {
            //1. 內建的 模型檢核 機制   (檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            if (!ModelState.IsValid)
            {
                return View(requestParam);//體貼地將資料填回去
            }

            //2. 輸出 = service 方法(輸入)
            //參數型別 Mapping(映射) 成input型別
            //(這兩個型別 會有大量重複，也許可考慮用繼承)
            var inputDto = new LoginAccountInputDTO
            {
                Email = requestParam.EmailXXX,
                Password = requestParam.PasswordXXX,
            };

            var outputDto = _service.LoginAccount(inputDto);

            //成敗分支(有成功就做分支判斷)
            if (!outputDto.IsSuccess)
            {
                //手動增加模型的Error 錯誤訊息
                ModelState.AddModelError(string.Empty, outputDto.Message);
                return View(requestParam); //體貼地將資料填回去
            }

            //最後
            //第一種 : 回傳View  (要Mapping成 目的地的VM)
            //第二種 : 重導向到 網址 (> 路由 > action)


            //重新導向(不需要VM) 至 來源【網址】，null的話就首頁
                            //看是不是null，是null的話就改用後面那個
            return Redirect( returnUrl ?? "/");
        }


        //登出
        public IActionResult Logout()
        {
            //基本上就是把cookie刪除
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _service.LogoutAccount();
            return Redirect("/");
            //return RedirectToAction("Index","Home");
        }

    }
}
