using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking {
    class HotelSupplier {

        public delegate void PriceUpdate(int newPrice);
        public static event PriceUpdate priceCut;

        float roomPrice;
        /* some structure to save the occupied rooms */


        /**
         * PricingModel, generates a new price for the room
         * based on some pricing model (random is fine)
         */ 
        public float getNewPrice() {
            return 0.0f;
        }

        /**
         * Gets called by an event on MultiCellBuffer
         * and it has to decode the string (from plain string
         * to Order) and decide what to do with it 
         * (check if the order is for you, and if it is
         * process it).
        public void newOrder(String encodedOrder) {

        }

        /**
         * This is the thread method 
         * Performs price updates and triggers events.
         */
        public void run() {

        }

    }
}
