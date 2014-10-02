using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelBooking {
    class HotelSupplier {

        public delegate void PriceUpdate(double newPrice);
        public static event PriceUpdate priceCut;

        static int idCounter = 0;

        int id;
        double roomPrice;
        int numOfRooms;
        int numOfOccupiedRooms;
        int numOfIterations;

        public HotelSupplier(int numOfRooms, int numOfIterations) {
            this.id = idCounter++;
            this.numOfRooms = numOfRooms;
            this.numOfOccupiedRooms = 0;
            this.numOfIterations = numOfIterations;
            this.roomPrice = 30.0;
        }

        /**
         * PricingModel, generates a new price for the room
         * based on some pricing model (random is fine)
         */ 
        public double getNewPrice() {
            Random rnd = new Random();
            double newPrice = (rnd.NextDouble()*20.0) + 10.0;
            return newPrice;
        }

        /**
         * Gets called by an event on MultiCellBuffer
         * and it has to decode the string (from plain string
         * to Order) and decide what to do with it 
         * (check if the order is for you, and if it is
         * process it).
         */ 
        public void newOrder(String encodedOrder) {
            Order order = OrderSerializer.decode(encodedOrder);

            if (order.getReceiverID() == id) {
                Thread orderProcesser = new Thread(()=>this.processOrder(order));
                orderProcesser.Start();
            }
        }

        /**
         * This is the thread method 
         * Performs price updates and triggers events.
         */
        public void run() {
            for (int i = 0; i < numOfIterations; i++) {
                double newPrice = getNewPrice();
                if (newPrice < roomPrice) {
                    priceCut(newPrice);
                }
                roomPrice = newPrice;
            }

        }

        public void processOrder(Order order) {
            // Process order and send confirmation
        }

    }
}
