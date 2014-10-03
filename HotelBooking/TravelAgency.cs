using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelBooking {
    class TravelAgency {

        private static int idCounter = 0;
        private static Random rnd = new Random();

        private Mutex lowestPriceHotelSupplierMutex = new Mutex();

        private int id;
        private double lowestPrice;
        private int idOfHotelSupplierWithLowestPrice;
        private MultiCellBuffer buffer;
        private LinkedList<int> unconfirmedOrders;

        public TravelAgency(MultiCellBuffer buffer) {
            id = idCounter++;
            lowestPrice = 40.0;
            this.buffer = buffer;
            unconfirmedOrders = new LinkedList<int>();
        }


        /* method called by the priceCut event from Hotel Supplier */
        public void priceUpdateCallback(int idOfHotelSupplier, double newPrice) {
            if (newPrice < lowestPrice) {
                Monitor.Enter(this);
                Console.WriteLine("[TravelAgency (" + id + ")] Price cut! New price: " + newPrice + " - HotelSupplierID: " + idOfHotelSupplier);
                lowestPrice = newPrice;
                idOfHotelSupplierWithLowestPrice = idOfHotelSupplier;
                Monitor.Exit(this);
            }
        }

        /* callback when the order gets confirmed by the hotel supplier */
        public void orderConfirmationCallback(String encodedOrder) {
            Order order = OrderSerializer.decode(encodedOrder);
            if (order.getSenderID() == id) {
                if (unconfirmedOrders.Contains(order.getOrderID())) {
                    unconfirmedOrders.Remove(order.getOrderID());
                    Console.WriteLine("[TravelAgency (" + id + ")] Order #" + order.getOrderID() + " confirmed.");
                }
            }
        }

        /**
         * Thread method
         * it will generate orders and put them
         * on the Multi cell buffer 
         */
        public void run() {
            while (true) {
                Monitor.Enter(this);
                Order newOrder = new Order(id, idOfHotelSupplierWithLowestPrice, 20, getNumOfRooms());
                Console.WriteLine("[TravelAgency (" + id + ")] New order: " + newOrder.ToString());

                unconfirmedOrders.AddLast(newOrder.getOrderID());

                buffer.put(OrderSerializer.encode(newOrder));
                Monitor.Exit(this);
            }
        }

        private int getNumOfRooms() {
            return rnd.Next(7) + 1;
        }
    }
}
