using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Models.ViewModels
{
    public class OrderListViewModel
    {
        public IList<OrderDetailsViewModel> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}