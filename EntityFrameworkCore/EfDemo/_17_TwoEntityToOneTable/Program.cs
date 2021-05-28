using System;
using System.Linq;

namespace _17_TwoEntityToOneTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new AppDbContext())
            //{
            //    context.Articles.Add(new Article { Title = "Hello world", Author = "Albert", No = "SD00001", Price = 49 });
            //    context.SaveChanges();

            //    context.Novels.Add(new Novel { Title = "C# is the bast language" });

            //    context.SaveChanges();
            //}
            //using (var context = new AppDbContext())
            //{

            //    context.Articles.ToList().ForEach(x => Console.WriteLine(x));

            //    context.Novels.ToList().ForEach(x => Console.WriteLine(x));
            //}



            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //context.Add(
                //    new Order
                //    {
                //        Status = 1,
                //        DetailedOrder = new DetailedOrder
                //        {
                //            Status = 1,
                //            ShippingAddress = "221 B Baker St, London",
                //            BillingAddress = "11 Wall Street, New York"
                //        }
                //    });

                context.Orders.Add(new Order { Status = 1 });
                context.DetailedOrders.Add(new DetailedOrder { Status = 2 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext())
            {
                var pendingCount = context.Orders.Count(o => o.Status == 1);
                Console.WriteLine($"Current number of pending orders: {pendingCount}");
            }

            using (var context = new AppDbContext())
            {
                var order = context.DetailedOrders.First(o => o.Status == 1);
                Console.WriteLine($"First pending order will ship to: {order.ShippingAddress}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}
