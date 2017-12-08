using AutoMapper;
using FShop.Data;
using FShop.Entities.Models;
using FShop.Service.Errors;
using FShop.Service.Posts;
using FShop.Web.Models;
using FShop.WebApi.Infrastructure.Core;
using FShop.WebApi.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Linq;
namespace FShop.WebApi.Areas.Admin.Controllers
{
    [RoutePrefix("api/PostCategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            using (var db = new FShopDbContext())
            {
                var query = db.Menus.Select(x => new TestModel
                {
                    Id = x.ID,
                    Name = x.Name,
                    //GroupId = x.GroupID == 0 ? null : x.GroupID
                    GroupName = x.MenuGroup.Name == string.Empty ? "Null" : x.MenuGroup.Name,
                });
                return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    response = request.CreateResponse(HttpStatusCode.OK, query);
                    return response;
                });
            }
        }
        private bool CheckExits(string Name)
        {
            using(var db = new FShopDbContext())
            {
                var check = db.Menus.Where(x => x.Name == Name);
                foreach(var item in check)
                {
                    if (item.Name == Name)
                        return true;
                }
                return false;
            }
        }
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        var listCategory = _postCategoryService.GetAll();
        //        var listCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

        //        response = request.CreateResponse(HttpStatusCode.OK, listCategoryVm);

        //        return response;
        //    });
        //}

        //[Route("Add")]
        //[HttpPost]
        //public HttpResponseMessage Add(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            PostCategory newPostCategory = new PostCategory();
        //            newPostCategory.UpdatePostCategory(postCategoryVm);
        //            var category = _postCategoryService.Add(newPostCategory);
        //            _postCategoryService.Save();

        //            response = request.CreateResponse(HttpStatusCode.Created, category);   
        //        }
        //        return response;
        //    });
        //}

    }
    public class TestModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int? Max { get; set; }
        public TestModel()
        {
            Max = 0;
        }
    }
}