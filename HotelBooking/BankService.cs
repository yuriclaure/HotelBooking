using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/**
 * This class will be responsible for validation of credit cards
 */
namespace HotelBooking {
    class BankService {
        private static LinkedList<String> cards = new LinkedList<String>();
        private static Random random = new Random();
        private static EncryptionService.ServiceClient encryptionService = new EncryptionService.ServiceClient();

        public static String generateNewCreditCard() {
            String newCard = random.Next(6778, 9765).ToString();
            Monitor.Enter(cards);
            cards.AddLast(newCard);
            Monitor.Exit(cards);
            return encryptionService.Encrypt(newCard);
        }

        public static bool isCardValid(String card) {
            Monitor.Enter(cards);
            if (cards.Contains(encryptionService.Decrypt(card))) {
                Monitor.Exit(cards);
                return true;
            }
            Monitor.Exit(cards);
            return false;
        }
    }
}
