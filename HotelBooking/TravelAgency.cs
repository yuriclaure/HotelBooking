using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking {
    class TravelAgency {

        private static int idCounter = 0;
        private static Random rnd = new Random();

        private int id;
        private double lowestPrice;
        private int idOfHotelSupplierWithLowestPrice;
        private int numOfIterations;
        private MultiCellBuffer buffer;
        private LinkedList<int> unconfirmedOrders;

        public TravelAgency(int numOfIterations, MultiCellBuffer buffer) {
            id = idCounter++;
            lowestPrice = 40.0;
            idOfHotelSupplierWithLowestPrice = 0;
            this.numOfIterations = numOfIterations;
            this.buffer = buffer;
            unconfirmedOrders = new LinkedList<int>();
        }


        /* method called by the priceCut event from Hotel Supplier */
        public void priceUpdateCallback(int idOfHotelSupplier, double newPrice) {
            if (newPrice < lowestPrice) {
                lowestPrice = newPrice;
                idOfHotelSupplierWithLowestPrice = idOfHotelSupplier;
            }
        }

        /* callback when the order gets confirmed by the hotel supplier */
        public void orderConfirmationCallback(String encodedOrder) {
            Order order = OrderSerializer.decode(encodedOrder);
            Console.WriteLine("OrderID: " + order.getOrderID() + " unconfirmedOrders: " + unconfirmedOrders);
            if (order.getSenderID() == id) {
                if (unconfirmedOrders.Contains(order.getOrderID())) {
                    unconfirmedOrders.Remove(order.getOrderID());
                    Console.Write("Order [" + order + "] confirmed.");
                }
            }
        }

        /**
         * Thread method
         * it will generate orders and put them
         * on the Multi cell buffer 
         */ 
        public void run() {
            for (int i = 0; i < numOfIterations; i++) {
                Order newOrder = new Order(id, idOfHotelSupplierWithLowestPrice, 20, getNumOfRooms());

                unconfirmedOrders.AddLast(newOrder.getOrderID());

                buffer.put(OrderSerializer.encode(newOrder));
            }
        }

        private int getNumOfRooms() {
            return rnd.Next(7) + 1;
        }
    }
}
