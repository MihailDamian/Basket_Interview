using Checkout.Core.Models;
using Checkout.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests
{
    public class BasketArticleRepositoryFake : IBasketArticleRepository
    {
        private readonly List<BasketArticle> articles;
        public BasketArticleRepositoryFake()
        {
            articles = new List<BasketArticle>()
            {
                new BasketArticle
                {
                   Id=1,
                   Item="i1",
                   Price=10
                }
            };
        }

        public IEnumerable<BasketArticle> GetAll()
        {
            return articles;
        }

        public BasketArticle Get(int id)
        {
            return articles.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public BasketArticle Create(BasketArticle entity)
        {
            entity.Id = new Random().Next(20,999);
            articles.Add(entity);
            return entity;
        }

        public BasketArticle Update(BasketArticle entity)
        {
            int index = articles.FindIndex(a => a.Id == entity.Id);
            if(index >=0)
            {
                articles[index] = entity;
            }
            return entity;
        }

        public bool Delete(int id)
        {
            var existing = articles.FirstOrDefault(a => a.Id == id);
            if (existing != null)
            {
                articles.Remove(existing);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<BasketArticle> GetByBasketId(int basketId)
        {
            return articles.Where(a => a.BasketId == basketId);
        }
    }
}
