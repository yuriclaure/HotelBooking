using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This class will be responsible for validation of credit cards
 */
namespace HotelBooking {
    class BankService 
    {
        Stack<int> cards;
        
        public BankService()
        {
            cards = new Stack<int>();
        }
        public string applyForCreditCard()
        {
            Random random = new Random();
            int newCard = random.Next(6778, 9765);
            cards.Push(newCard);
            return "";
        }

        public string checkCreditCard()
        {
            string ans = "Not Valid";
            if(cards.Contains(0))
            {
                ans = "Valid";
            }
            return ans;
        }
    }
}
