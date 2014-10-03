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

        public delegate void NewDataEvent(String data);
        public static event NewDataEvent newData;

        String[] data;
        int size, occupiedPositions;
        int head, tail;

        public MultiCellBuffer(int size) {
            data = new String[size];
            this.size = size;
            head = tail = occupiedPositions = 0;
        }

        public void put(string newData) {
            lock (this) {
                while (occupiedPositions == size) {
                    Monitor.Wait(this);
                }

                data[tail] = newData;
                tail = (tail + 1) % size;
                occupiedPositions++;

                Monitor.Pulse(this);
            }
        }

        public void run() {
            while (true) {
                lock (this) {
                    while (occupiedPositions == 0) {
                        Monitor.Wait(this);
                    }

                    String order = data[head];
                    head = (head + 1) % size;
                    occupiedPositions--;

                    if (newData != null) {
                        newData(order);
                    }

                    Monitor.Pulse(this);
                }
            }
        }

        public void subscribeToGet(NewDataEvent subscriber) {
            newData += subscriber;
        }
    }
}
