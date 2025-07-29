namespace PhoneContact
{
    public class Contact
    {
        #region properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }


        #endregion

        #region Constructor
        public Contact(string name, string phoneNumber, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Contact name cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber), "Phone number cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            CreatedDate = DateTime.Now;
        }
        #endregion


    }
}
