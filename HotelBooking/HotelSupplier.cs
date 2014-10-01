using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking {
    class HotelSupplier {

        public delegate void PriceUpdate(double newPrice);
        public static event PriceUpdate priceCut;

        double roomPrice;
        int numOfRooms;
        int numOfOccupiedRooms;
        int numOfIterations;

        public HotelSupplier(int numOfRooms, int numOfIterations) {
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
            
            /* we need to define order before implementing this */

            /* Check if order is for you */
            /* Check availability */
            /* Confirm order with bank service */
            /* Send confirmation back to the travel agency */
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

    }
}
