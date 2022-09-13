
using Checkout.Core.Models;
using System.Collections.Generic;

namespace Checkout.Core.Repository
{
    public interface IBasketArticleRepository : IDefaultRepository<BasketArticle>
    {
        IEnumerable<BasketArticle> GetByBasketId(int basketId);
    }
}
