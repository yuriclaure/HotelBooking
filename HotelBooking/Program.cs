using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBooking {
    class Program {
        
        /* List of Travel Agencies and Hotel Suppliers*/
        /* Buffer, etc */
        
        static void Main(string[] args) {


            /* Create threads and run */
            HotelSupplier h0 = new HotelSupplier(3, 2);

            Thread hotel0 = new Thread(new ThreadStart(h0.run));

            hotel0.Start();
            /* Join them */
            hotel0.Join();

            /* Finish */

        }
    }
}
