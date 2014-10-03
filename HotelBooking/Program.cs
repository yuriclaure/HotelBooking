using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBooking {
    class Program {

        private static MultiCellBuffer orderBuffer = new MultiCellBuffer(3);
        private static MultiCellBuffer confirmationBuffer = new MultiCellBuffer(5);

        private static LinkedList<TravelAgency> travelAgencies = new LinkedList<TravelAgency>();
        private static LinkedList<HotelSupplier> hotelSuppliers = new LinkedList<HotelSupplier>();

        private static LinkedList<Thread> travelAgenciesThreads = new LinkedList<Thread>();
        private static LinkedList<Thread> hotelSuppliersThreads = new LinkedList<Thread>();

        private const int NUM_OF_TRAVEL_AGENCIES = 5;
        private const int NUM_OF_HOTEL_SUPPLIERS = 3;
        
        static void Main(string[] args) {

            Thread orderBufferThread = new Thread(orderBuffer.run);
            Thread confirmationBufferThread = new Thread(confirmationBuffer.run);

            for (int i = 0; i < NUM_OF_HOTEL_SUPPLIERS; i++) {
                HotelSupplier hotelSupplier = new HotelSupplier(10, confirmationBuffer);
                hotelSuppliers.AddLast(hotelSupplier);
                Thread hotelSupplierThread = new Thread(hotelSupplier.run);
                hotelSupplierThread.Name = "HotelSupplier [" + i + "]";
                hotelSuppliersThreads.AddLast(new Thread(hotelSupplier.run));
                orderBuffer.subscribeToGet(new MultiCellBuffer.NewDataEvent(hotelSupplier.newOrder));
            }

            for (int i = 0; i < NUM_OF_TRAVEL_AGENCIES; i++) {
                TravelAgency travelAgency = new TravelAgency(orderBuffer);
                travelAgencies.AddLast(travelAgency);
                Thread travelAgencyThread = new Thread(travelAgency.run);
                travelAgencyThread.Name = "TravelAgency [" + i + "]";
                travelAgenciesThreads.AddLast(travelAgencyThread);
                for (LinkedListNode<HotelSupplier> it = hotelSuppliers.First; it != null; it = it.Next) {
                    it.Value.subscribeToPriceCut(new HotelSupplier.PriceUpdate(travelAgency.priceUpdateCallback));
                }
                confirmationBuffer.subscribeToGet(new MultiCellBuffer.NewDataEvent(travelAgency.orderConfirmationCallback));
            }            

            orderBufferThread.Start();
            confirmationBufferThread.Start();

            for (LinkedListNode<Thread> it = hotelSuppliersThreads.First; it != null; it = it.Next) {
                it.Value.Start();
            }

            for (LinkedListNode<Thread> it = travelAgenciesThreads.First; it != null; it = it.Next) {
                it.Value.Start();
            }

            for (LinkedListNode<Thread> it = hotelSuppliersThreads.First; it != null; it = it.Next) {
                it.Value.Join();
            }

            for (LinkedListNode<Thread> it = travelAgenciesThreads.First; it != null; it = it.Next) {
                it.Value.Abort();
            }

            orderBufferThread.Abort();
            confirmationBufferThread.Abort();

            Console.ReadKey();

        }
    }
}
