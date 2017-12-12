using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FShop.WebApi.Infrastructure.Core
{
    public class AuthCustom : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            /*
                Nếu Request Header Authoriztion = rỗng thì sẽ chưa đăng nhập
            */
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    Status = false,
                    StatusCode = 401,
                    Message = HttpStatusCode.Unauthorized
                });
            }
            else
            {
                /*
                  Kiểm tra quyền
               */
                var AuToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodeAuToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuToken)) ;
                string[] usernamePasswordArray = decodeAuToken.Split(':');
                if (usernamePasswordArray != null)
                {
                    var UserName = usernamePasswordArray[0];
                    var Pass = usernamePasswordArray[1];
                    //Function kiểm tra đăng nhập
                    //Function này trả về true và false
                    if (CheckLogin(UserName, Pass))
                    {
                        // kt check quyền
                        // quyền có định dạng như sau Tên Controller - Action (Test-Insert)
                        //Lấy tên controller và action
                        var actionName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" +
                                            actionContext.ActionDescriptor.ActionName;
                        var listPermiss = FetchPermission().Where(x => x.Name.Contains(actionName));
                        if (listPermiss.Count() < 0 && listPermiss == null)
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
                            {
                                Status = false,
                                StatusCode = 401,
                                Message = "Bạn không có quyền [[ " + actionName + " ]]"
                            });
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
                        {
                            Status = false,
                            StatusCode = 401,
                            Message = "Tên đăng nhập không chính xác"
                        });
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new
                    {
                        Status = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = HttpStatusCode.BadRequest
                    });
                }
            }
            base.OnAuthorization(actionContext);
        }
        private bool CheckLogin(string UserName, string Pass)
        {
            if (UserName == "admin" && Pass == "admin")
                return true;
            return false;
        }
        private List<ListRolse> FetchPermission()
        {
            var list = new List<ListRolse>();
            return list;
        }
    }
    public class ListRolse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}