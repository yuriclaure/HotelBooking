using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking {
    class TravelAgency {

        static int idCounter = 0;

        int id;

        public TravelAgency() {
            id = idCounter++;
        }


        /* method called by the priceCut event from Hotel Supplier */
        public void priceUpdateCallback(int newPrice) {

        }

        /* callback when the order gets confirmed by the hotel supplier */
        public void orderConfirmationCallback(String order) {

        }

        /**
         * Thread method
         * it will generate orders and put them
         * on the Multi cell buffer 
         */ 
        public void run() {

        }
    }
}
