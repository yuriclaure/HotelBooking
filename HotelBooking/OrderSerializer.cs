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

        public static String encode(Order order) {
            return "encoded order";
        }

        public static Order decode(String order) {
            return new Order();
        }
    }
}
