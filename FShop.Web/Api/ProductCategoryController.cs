using AutoMapper;
using FShop.Entities.Models;
using FShop.Service.Errors;
using FShop.Service.Products;
using FShop.Web.Infrastructure.Core;
using FShop.Web.Models;
using FShop.WebApi.Infrastructure.Core;
using FShop.WebApi.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using LibResponse;
using System.Data.Entity.Validation;

namespace FShop.Web.Api
{
    [RoutePrefix("api/ProductCategory")]
    [EnableCors("*", "*", "*")]
    [AllowAnonymous]
    public class ProductCategoryController : ApiControllerBase
    {
        #region Initialize
        private IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) :
            base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        #endregion

        [Route("GetAllParents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var Res = new Res()
                {
                    Status = true,
                    Data = responseData
                };

                var response = request.CreateResponse(HttpStatusCode.OK, Res);
                return response;
            });
        }
        [Route("GetById/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);

                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                var Res = new Res()
                {
                    Status = true,
                    Data = responseData
                };

                var response = request.CreateResponse(HttpStatusCode.OK, Res);

                return response;
            });
        }

        [Route("GetAllPaging")]
        [HttpGet]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var Res = new Res()
                {
                    Status = true,
                    Data = paginationSet
                };
                var response = request.CreateResponse(HttpStatusCode.OK, Res);
                return response;
            });
        }

        /*===Add===*/
        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    var Error = "";
                    if (string.IsNullOrEmpty(productCategoryVm.Name))
                    {
                        Error = "Tên Không được trống" + productCategoryVm.Name;
                    }
                    else if (string.IsNullOrEmpty(productCategoryVm.Alias))
                    {
                        Error = "Alias Không được trống" + productCategoryVm.Alias;
                    }
                    var Res = new Res
                    {
                        Status = false,
                        Message = Error
                    };
                    response = request.CreateResponse(HttpStatusCode.BadRequest, Res);
                }
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);
                    newProductCategory.CreatedDate = DateTime.Now;
                    newProductCategory.UpdatedDate = DateTime.Now;
                    if (productCategoryVm.Status)
                    {
                        newProductCategory.Status = true;
                    }
                    else
                    {
                        newProductCategory.Status = false;
                    }

                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.Save();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    var Res = new Res
                    {
                        Status = true,
                        Message = "Thêm mới thành công !!!",
                        Data = responseData
                    };
                    response = request.CreateResponse(HttpStatusCode.Created, Res);
                }
                return response;
            });
        }

        /*===Update===*/
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    var Error = "";
                    if (string.IsNullOrEmpty(productCategoryVm.Name))
                    {
                        Error = "Tên danh mục không được trống" + productCategoryVm.Name;
                    }
                    else if (string.IsNullOrEmpty(productCategoryVm.Alias))
                    {
                        Error = "Tiêu đề Seo không được trống" + productCategoryVm.Alias;
                    }
                    var Res = new Res
                    {
                        Status = false,
                        Message = Error
                    };
                    response = request.CreateResponse(HttpStatusCode.BadRequest, Res);
                }
                else
                {
                    var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    var defaultDatime = dbProductCategory.CreatedDate;
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    dbProductCategory.CreatedDate = defaultDatime;
                    dbProductCategory.UpdatedDate = DateTime.Now;

                    _productCategoryService.Update(dbProductCategory);
                    try
                    {
                        _productCategoryService.Save();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }


                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    var Res = new Res
                    {
                        Status = true,
                        Message = "Cập nhập thành công !!!",
                        Data = responseData
                    };
                    response = request.CreateResponse(HttpStatusCode.Created, Res);
                }

                return response;
            });
        }

        /*===Delete===*/
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    //var Error = "Không thể xóa được";

                    //var Res = new Res
                    //{
                    //    Status = false,
                    //    Message = Error
                    //};
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var idProductCategory = _productCategoryService.GetById(productCategoryVm.ID);

                    idProductCategory.UpdateProductCategory(productCategoryVm);

                    _productCategoryService.Delete(idProductCategory);

                    _productCategoryService.Save();


                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(idProductCategory);
                    var Res = new Res
                    {
                        Status = true,
                        Message = "Xóa thành công !!!",
                        Data = responseData
                    };
                    response = request.CreateResponse(HttpStatusCode.Created, Res);
                }

                return response;
            });
        }

        /*===Xóa theo pt put or delete===*/
        [Route("DeleteByDelete/{id}")]
        [HttpPost]
        public HttpResponseMessage DeleteByDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    var Error = "Vui lòng nhập đủ dữ liệu để xóa";
                    var Res = new Res
                    {
                        Status = false,
                        Message = Error
                    };
                    response = request.CreateResponse(HttpStatusCode.BadRequest, Res);
                }
                else
                {
                    var productCategoryVm = new ProductCategoryViewModel();
                    var IdDele = _productCategoryService.GetById(productCategoryVm.ID);
                    // IdDele.UpdateProductCategory(productCategoryVm);
                    _productCategoryService.DeleteByDelete(id);
                    _productCategoryService.Save();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(IdDele);
                    var Res = new Res
                    {
                        Status = true,
                        Message = "Xóa thành công !!!",
                        Data = responseData
                    };
                    response = request.CreateResponse(HttpStatusCode.Created, Res);
                }
                return response;
            });
        }
    }
}