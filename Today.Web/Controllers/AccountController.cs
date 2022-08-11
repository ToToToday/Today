using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private string _returnUrl; //不確定

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        //AccountController右鍵產生【建構函式】
        //產生建構函式 就要去註冊DI


        //註冊
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp([FromForm] SignUpVM requestParam)
        {
            //1.內建的 模型檢核 機制(檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            //if (!ModelState.IsValid)
            //{
            //    return View(requestParam);//體貼地將資料填回去
            //}

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
                //ModelState.AddModelError(string.Empty, outputDto.Message);
                //return View(requestParam); //體貼地將資料填回去

                TempData["SignUpError"] = outputDto.Message;
                TempData["SignUpEmail"] = requestParam.Email;
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


        [HttpGet("/Account/LoginOption/{scheme}")]
        public IActionResult LoginOption([FromRoute] string scheme)
        {
            //A. 我家的驗證
            if (scheme == CookieAuthenticationDefaults.AuthenticationScheme)
                return Challenge();

            //B. 其他三方驗證
            //設定驗證選項
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleResponse)),
            };
            return Challenge(properties, scheme);
        }


        public async Task<IActionResult> HandleResponse() // 挑戰完的 重導到此
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = User.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value,
            });

            //用JSON格式觀察測試結果
            //return Json(claims);
            //網頁畫面出現後，到開發工具中會發現已有cookie，表示已經完成了cookie驗證

            //claims到手了，就可以先登出，待會重新調整claim，重發後再登入
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var identifierClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            var inputDto = new Login3rdPartyInputDto
            {
                Provider = identifierClaim.Issuer,
                NameIdentifier = identifierClaim.Value,
            };

            //同名多載
            var outputDto = await _service.LoginAccountAsync(inputDto);

            //成敗分支 (必定成功就免了)
            //if (!outputDto.IsSuccess){ ... }

            //重新導向至前一頁面
            return LocalRedirect(_returnUrl ?? "/");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm] LoginVM requestParam) //收資料，資料庫寫入
        {
            //1. 內建的 模型檢核 機制   (檢核欄位)  //後端檢核：顧及正確   //前端檢核：顧及消費者體驗
            //if (!ModelState.IsValid)
            //{
            //    return Content("輸入不合規");
            //    //return View(requestParam);//體貼地將資料填回去
            //}

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
                //ModelState.AddModelError(string.Empty, outputDto.Message);
                //return Content("輸入不合規");
                //return View(requestParam); //體貼地將資料填回去

                TempData["LoginError"] = outputDto.Message;
                TempData["LoginEmail"] = requestParam.Email;
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
            #region 取到
            //int.Parse(User.Identity.Name);
            //int.Parse(User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.Name ).Value );
            //int.Parse(User.Claims.FirstOrDefault(c=> c.Type == "MemberID" ).Value);

            #endregion 

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
