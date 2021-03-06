﻿using System;
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
           
           return order.getOrderID() + " " + order.getSenderID() + " " + order.getReceiverID() + " " + order.getCardNumber() + " " + order.getAmount();
        }

        public static Order decode(String order) 
        {

            String[] orderSplitted = order.Split(' ');
            Order theOrder = new Order(Int32.Parse(orderSplitted[0]), Int32.Parse(orderSplitted[1]), Int32.Parse(orderSplitted[2]), orderSplitted[3], Int32.Parse(orderSplitted[4]));
            return theOrder;
        }
    }
}
