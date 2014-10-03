using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This class will encode and decode an Order
 * from and to a string, we can simply use JSON
 * in this case (search for libraries that codify
 * in JSON in c#)
 */ 
namespace HotelBooking {
    class OrderSerializer {

        public static String encode(Order order) 
        {
            return order.ToString();
            //return order.getSenderID() + " " + order.getReceiverID() + " " + order.getCardNumber() + " " + order.getAmount();
        }

        public static Order decode(String order) 
        {
            return Convert.ChangeType(order, typeof(Order));
            //Order theOrder = new Order(1,2,3,4);
            //return theOrder;
        }
    }
}
