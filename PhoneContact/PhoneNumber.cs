namespace PhoneContact
{
    public class PhoneNumber
    {
        #region Properties
        public int Id { get; set; } 
        public string Number { get; set; }
        public string Type { get; set; } // e.g., Mobile, Home, Work
        public int ContactId { get; set; } // العلاقه بين phone number , contact
        public Contact Contact { get; set; } //عشان من رقم التيليفون اقدر احصل على contact وبياناته
        #endregion

        #region constructor
        public PhoneNumber(string number, string type)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Phone number cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                Type = "Mobile"; 
            }
            else
            {
                Type = type;
            }
            Number = number;
        }
    }
        #endregion
    }

