using System;
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
        private int senderID;
        private int receiverID;
        private int cardNumber;
        private int amount;

        public Order(int senderID, int receiverID, int cardNumber, int amount) {
            this.senderID = senderID;
            this.receiverID = receiverID;
            this.cardNumber = cardNumber;
            this.amount = amount;
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
