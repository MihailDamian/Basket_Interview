using System;
using System.Collections.Generic;
using Checkout.Core.Models;
using Checkout.Core.Repository;

namespace Checkout.Infrastructure.Database
{
    public class BasketArticleRepository : BaseRepository<BasketArticle>, IBasketArticleRepository
    {
        public BasketArticleRepository(ShopContext c) : base(c)
        {
        }
        public IEnumerable<BasketArticle> GetAll()
        {
            return GetAllBase();
        }

        public IEnumerable<BasketArticle> GetByBasketId(int basketId)
        {
            return GetByConditionBase((a => a.BasketId == basketId));
        }
        
        public BasketArticle Get(int id)
        {
            return GetBase(id);
        }

        public BasketArticle Create(BasketArticle entity)
        {
            CreateBase(entity);
            SaveBase();

            return entity;
        }
        public BasketArticle Update(BasketArticle entity)
        {
            UpdateBase(entity);
            SaveBase();

            return entity;
        }

        public bool Delete(int id)
        {
            DeleteBase(new BasketArticle { Id = id });
            try
            {
                SaveBase();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException dbException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }
    }
}