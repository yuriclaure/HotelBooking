﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Order from a travel agency to
 * a hotel supplier
 */ 
namespace HotelBooking {
    class Order {
        private static int idCounter = 0;

        private int id;
        private int senderID;
        private int receiverID;
        private int cardNumber;
        private int amount;

        public Order(int senderID, int receiverID, int cardNumber, int amount) {
            id = idCounter++;
            this.senderID = senderID;
            this.receiverID = receiverID;
            this.cardNumber = cardNumber;
            this.amount = amount;
        }

        public Order(int orderID, int senderID, int receiverID, int cardNumber, int amount) {
            id = orderID;
            this.senderID = senderID;
            this.receiverID = receiverID;
            this.cardNumber = cardNumber;
            this.amount = amount;
        }

        public int getOrderID() {
            return id;
        }

        public int getSenderID() {
            return senderID;
        }

        public int getReceiverID() {
            return receiverID;
        }

        public int getCardNumber() {
            return cardNumber;
        }

        public int getAmount() {
            return amount;
        }

    }
}
