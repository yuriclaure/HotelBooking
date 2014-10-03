using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HotelBooking {
    class HotelSupplier {

        public delegate void PriceUpdate(int hotelSupplierID, double newPrice);
        public event PriceUpdate priceCut;

        private static int idCounter = 0;
        private static Random rnd = new Random();

        private int id;
        private double roomPrice;
        private int numOfIterations;
        private MultiCellBuffer confirmationBuffer;

        public HotelSupplier(int numOfIterations, MultiCellBuffer confirmationBuffer) {
            this.id = idCounter++;
            this.numOfIterations = numOfIterations;
            this.roomPrice = 30.0;
            this.confirmationBuffer = confirmationBuffer;
        }

        /**
         * PricingModel, generates a new price for the room
         * based on some pricing model (random is fine)
         */ 
        public double getNewPrice() {
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
                Console.WriteLine("[HotelSupplier (" + id + ")] Order #" + order.getOrderID() + " received.");
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
                    if (priceCut != null) {
                        priceCut(id, newPrice);
                    }
                }
                roomPrice = newPrice;
            }

        }

        public void processOrder(Order order) {
            // TODO: Check card number with Bank Service.
            double totalPrice = order.getAmount() * roomPrice;
            Console.WriteLine("[HotelSupplier (" + id + ")] Order #" + order.getOrderID() + " confirmation sent.");
            confirmationBuffer.put(OrderSerializer.encode(order));
        }

        public void subscribeToPriceCut(PriceUpdate subscriber) {
            priceCut += subscriber;
        }

    }
}
