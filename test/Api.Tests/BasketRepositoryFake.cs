using Checkout.Core.Models;
using Checkout.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Tests
{
    public class BasketRepositoryFake : IBasketRepository
    {
        private readonly List<Basket> baskets;
        private readonly IBasketArticleRepository _basketArticleRepository;
        public BasketRepositoryFake(IBasketArticleRepository basketArticleRepository)
        {
            baskets = new List<Basket>()
            {
                new Basket
                {
                   Id = 1,
                   Customer="customer1",
                   PaysVat=true,
                   TotalGross=10,
                   TotalNet=10
                }
            };
            _basketArticleRepository = basketArticleRepository;
        }

        public IEnumerable<Basket> GetAll()
        {
            return baskets;
        }

        public Basket Get(int id)
        {
            var b = baskets.Where(a => a.Id == id).FirstOrDefault();
            if (b != null)
                b.Articles = _basketArticleRepository.GetByBasketId(id);
            return b;
        }

        public Basket Create(Basket entity)
        {
            entity.Id = new Random().Next(20, 999);
            baskets.Add(entity);
            return entity;
        }

        public Basket Update(Basket entity)
        {
            int index = baskets.FindIndex(a => a.Id == entity.Id);
            if (index >= 0)
            {
                baskets[index] = entity;
            }
            return entity;
        }

        public bool Delete(int id)
        {
            var existing = baskets.FirstOrDefault(a => a.Id == id);
            if (existing != null)
            {
                baskets.Remove(existing);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
