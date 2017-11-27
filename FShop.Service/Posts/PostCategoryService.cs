using FShop.Data.Infrastructure;
using FShop.Data.Repositories.Posts;
using FShop.Entities.Models;
using System.Collections.Generic;

namespace FShop.Service.Posts
{
    public interface IPostCategoryService
    {
        void Add(PostCategory postCategoryCategory);

        void Update(PostCategory postCategoryCategory);

        void Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAllByParentId(int parentId);

        PostCategory GetById(int id);

        void Save();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(PostCategory postCategoryCategory)
        {
            _postCategoryRepository.Add(postCategoryCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Save();
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }
    }
}