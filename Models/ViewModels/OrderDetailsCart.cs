using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Models.ViewModels
{
    public class OrderDetailsCart
    {
        public List<ShoppingCart> listCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}