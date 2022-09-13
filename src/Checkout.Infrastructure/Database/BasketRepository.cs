using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Core.Models;
using Checkout.Core.Repository;

namespace Checkout.Infrastructure.Database
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        readonly IBasketArticleRepository _basketArticleRepository;
        public BasketRepository(ShopContext c, IBasketArticleRepository basketArticleRepository) : base(c)
        {
            _basketArticleRepository = basketArticleRepository;
        }
        public IEnumerable<Basket> GetAll()
        {
            return GetAllBase();
        }

        public Basket Get(int id)
        {
            var b = GetBase(id);
            if (b != null)
                b.Articles = _basketArticleRepository.GetByBasketId(id);
            return b;
        }

        public Basket Create(Basket entity)
        {
            CreateBase(entity);
            SaveBase();

            return entity;
        }
        public Basket Update(Basket entity)
        {
            UpdateBase(entity);
            SaveBase();

            return entity;
        }

        public bool Delete(int id)
        {
            DeleteBase(new Basket { Id = id });
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