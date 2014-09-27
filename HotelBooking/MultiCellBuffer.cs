using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * This class will be responsible to transfer 
 * orders from the travel agencies to the hotel
 * suppliers, this will be implemented using a 
 * limited buffer with k positions (k = 3),
 * and receive orders from travel agencies, 
 * and save them in one of the positions (if there are
 * positions available), and trigger the event
 * so all the hotel suppliers can receive the order.
 */ 
namespace HotelBooking {
    class MultiCellBuffer {
    }
}
