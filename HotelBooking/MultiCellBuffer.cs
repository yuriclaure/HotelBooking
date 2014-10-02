using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


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

        public delegate void newOrderEvent(String order);
        public static event newOrderEvent newOrder;

        const int SIZE = 3;
        String[] orders = new String[SIZE];
        int head = 0, tail = 0, n = 0;

        public void put(string order) {
            lock (this) {
                while (n == SIZE) {
                    Monitor.Wait(this);
                }

                Console.WriteLine("writing thread entered");
                orders[tail] = order;
                tail = (tail + 1) % SIZE;
                n++;

                Console.WriteLine("writing thread " + Thread.CurrentThread.Name + " " + c + " " + n);
                Monitor.Pulse(this);
            }
        }

        public void run() {
            while (true) {
                lock (this) {
                    while (n == 0) {
                        Monitor.Wait(this);
                    }

                    String order = orders[head];
                    head = (head + 1) % SIZE;
                    n--;

                    if (newOrder != null) {
                        newOrder(order);
                    }

                    Monitor.Pulse(this);
                }
            }
        }

        public void subscribeGet(newOrderEvent subscriber) {
            newOrder += subscriber;
        }
    }
}
