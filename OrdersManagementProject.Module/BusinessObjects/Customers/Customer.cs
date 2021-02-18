using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Domain.Base;

namespace Domain.Customers
{
    public class Customer : PersonBase
    {
        public Customer(Session session) : base(session)
        {
        }
    }
}
