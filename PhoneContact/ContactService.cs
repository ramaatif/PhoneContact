namespace PhoneContact
{
    public class ContactService
    {
        #region Properties
        private readonly ContactDBContext _context;
        #endregion
        #region constructor
        public ContactService(ContactDBContext context)
        {
            _context = context;

        }
        #endregion
        #region Add Contact Method
        public void AddContact()
        {

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            var contact = new Contact(name, phoneNumber, email);
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            Console.WriteLine("Contact added successfully.");
        }
        #endregion
        #region View All Contacts Method
        public void ViewAllContacts()
        {
            var contacts = _context.Contacts.ToList();
            if (contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\nContacts:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Created On: {contact.CreatedDate}");
            }

        }
        #endregion
        #region Update Contact Method
        public void UpdateContact()
        {
            Console.Write("Enter contact name to update:");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid contact id.");
                return;
            }
            var contact = _context.Contacts.Where(c => c.Name == input).FirstOrDefault()
            ;
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }
            Console.WriteLine("\n Current data");
            Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Created On: {contact.CreatedDate}");
            Console.WriteLine("\nNew data");
            Console.WriteLine("Enter new name");
            string newName = Console.ReadLine();
            Console.WriteLine("Enter new phone number");
            string newPhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter new email");
            string newEmail = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(newName))
            contact.Name = newName ;
            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                contact.PhoneNumber = newPhoneNumber;
            if (!string.IsNullOrWhiteSpace(newEmail))
                contact.Email = newEmail;
            _context.SaveChanges();
            Console.WriteLine("Contact updated successfully.");

        }
        #endregion
        #region Delete Contact Method
        public void DeleteContact()
        {
            Console.WriteLine("Enter contact id to delete :");
            string input = Console.ReadLine();
            if (!int.TryParse(input,out int id))
            {
                Console.WriteLine("Invalid contact id.");
                return;
            }
            var contact = _context.Contacts.Find(id);
            if((contact==null))
            {
                Console.WriteLine("Contact not found.");
                return;
            }
            Console.WriteLine($"Are you sure you want to delete {contact.Name}? Yes or No");
            string confirmation = Console.ReadLine();
            if (confirmation == "yes"||confirmation=="Yes")
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Delete operation canceled.");
            }
        }
        #endregion
        
    }
}
