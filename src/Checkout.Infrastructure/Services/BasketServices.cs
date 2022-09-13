using Checkout.Core.Models;
using Checkout.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Services
{
    public class BasketServices
    {
        public Basket UpdateColumns(Basket b)
        {
            b.TotalNet = b.Articles.Sum(a => a.Price);
            b.TotalGross = b.TotalNet + (b.PaysVat ? (b.TotalNet * (1 / b.VAT)) : 0);
            return b;
        }
    }
}
