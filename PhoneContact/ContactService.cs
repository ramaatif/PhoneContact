using Microsoft.EntityFrameworkCore;

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

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ Name cannot be empty.");
                return;
            }


            try
            {
                var contact = new Contact(name, email);
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                Console.Write("Enter Phone Number : ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Enter phone number type (e.g., Mobile/Home/Work): ");
                string type = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {

                    var phone = new PhoneNumber(phoneNumber, type);
                    phone.ContactId = contact.Id; // Associate phone number with the contact
                    _context.PhoneNumbers.Add(phone);
                    _context.SaveChanges();

                    Console.WriteLine("\n✅ Phone number added successfully.");
                }

                Console.WriteLine($"✅ Contact '{contact.Name}' added successfully with ID: {contact.Id}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
        #endregion
        #region View All Contacts Method
        public void ViewAllContacts()
        {
            Console.WriteLine("\n--- All Contacts ---");

            var contacts = _context.Contacts
                .Include(c => c.PhoneNumbers)// Include phone numbers for each contact
                .OrderBy(c => c.CreatedDate)
                .ToList();
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\nContacts:");

            foreach (var contact in contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name},Email: {contact.Email}, Created On: {contact.CreatedDate}");
                if (contact.PhoneNumbers != null && contact.PhoneNumbers.Any())
                {
                    foreach (var phone in contact.PhoneNumbers)
                    {
                        Console.WriteLine($" - {phone.Type} : {phone.Number}");
                    }
                }
                else
                {
                    Console.WriteLine($"Contact: {contact.Name} has no phone numbers.");
                }
            }

            

        }
        #endregion
        #region Add Phone Number Method
        public void AddPhoneNumber()
        {
            Console.WriteLine("Enter Contact Id to add phone number");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int ContactId))
            {
                Console.WriteLine("Invalid Id,please enter valid id");
                return;

            }
            var contact = _context.Contacts.Find(ContactId);
            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine($"Adding to: {contact.Name}");

            Console.Write("Enter new phone number: ");
            string newNumber = Console.ReadLine();
            Console.Write("Enter phone type (Mobile/Home/Work): ");
            string type = Console.ReadLine();

            try
            {
                var phone = new PhoneNumber(newNumber, type);
                phone.ContactId = contact.Id;
                _context.PhoneNumbers.Add(phone);
                _context.SaveChanges();
                Console.WriteLine("✅ Phone number added successfully.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }


        #endregion
        #region Update Contact Method
        public void UpdateContact()
        {
            Console.Write("Enter Contact Id to update:");
            string input = Console.ReadLine();
            if (!int.TryParse(input,out int ContactId))
            {
                Console.WriteLine("Invalid id ,please enter valid id");
                return;
            }
            var contact = _context.Contacts.Find(ContactId);
            if (contact == null)
            {
                Console.WriteLine("❌ Contact not found.");
                return;
            }

            Console.WriteLine("\n Current data");
            Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name},  Email: {contact.Email}, Created On: {contact.CreatedDate}");
            Console.WriteLine("\nCurrent Phone Numbers:");
            foreach (var phone in contact.PhoneNumbers)
            {
                Console.WriteLine($"  - {phone.Number} ({phone.Type})");
            }
            Console.WriteLine("\nNew data");

            Console.WriteLine("Enter new name");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                contact.Name = newName;

            Console.WriteLine("Enter new email");
            string newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newEmail))
                contact.Email = newEmail;

            _context.SaveChanges();
            Console.WriteLine("Contact updated successfully.");


        }
        #endregion
        #region search
        public void Search()
        {
            Console.WriteLine("Choose to search with" +
                "\n[1] name \n[2] phone number ");
            int choise = Convert.ToInt32(Console.ReadLine());

            if (choise == 1)
            {
                Console.WriteLine("Enter name to search");
                string input = Console.ReadLine();
                var contact = _context.Contacts.Include(c => c.PhoneNumbers).Single(contact => contact.Name == input);

                Console.WriteLine($" Contact details : Name {contact.Name}, Email {contact.Email},CreatedDate{contact.CreatedDate}");

                foreach (var phone in contact.PhoneNumbers)
                {
                    Console.WriteLine($"Phone Number: {phone.Number}, Type: {phone.Type}");
                }


            }
            else if (choise == 2)
            {
                Console.WriteLine("Enter contact  phone number ");
                string EntertedPhoneNumber = Console.ReadLine();
                long number;
                if (string.IsNullOrEmpty(EntertedPhoneNumber) || !long.TryParse(EntertedPhoneNumber, out number))
                {
                    Console.WriteLine("❌ Phone number cannot be empty or Contains Chars or wrong format.");
                    return;
                }

                var ContactfromPhoneNumberTable = _context.PhoneNumbers.FirstOrDefault(p => p.Number == EntertedPhoneNumber);

                if (ContactfromPhoneNumberTable == null)
                {
                    Console.WriteLine("❌ Contact not found.");
                    return;
                }
                var ContactFromContactTable = _context.Contacts.Include(c => c.PhoneNumbers).FirstOrDefault(c => c.Id == ContactfromPhoneNumberTable.ContactId);
                if (ContactFromContactTable == null)
                { Console.WriteLine("❌ Contact not found."); return; }

                string contactDetails = $"Contact details : Name {ContactFromContactTable.Name}, Email {ContactFromContactTable.Email}, CreatedDate {ContactFromContactTable.CreatedDate}";
                foreach (var phone in ContactFromContactTable.PhoneNumbers)
                {
                    contactDetails += $"\nPhone Number: {phone.Number}, Type: {phone.Type}";
                }

                //  
                Console.WriteLine(contactDetails);
                //  var contact = _context.Contacts.Include(c =>c.PhoneNumbers).Single(contact => contact.PhoneNumbers == input)
            }
            else { Console.WriteLine("Your Entered Choise wrong , try again"); }

        }

        #endregion
        #region Delete Contact Method
        public void DeleteContact()
        {
            Console.WriteLine("Enter contact id to delete :");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid contact id.");
                return;
            }


            var contact = _context.Contacts
                .Include(c => c.PhoneNumbers)// Include phone numbers for the contact
                .FirstOrDefault(c => c.Id == id);// Find the contact by ID

            if ((contact == null))
            {
                Console.WriteLine("Contact not found.");
                return;
            }
            Console.WriteLine($"Are you sure you want to delete {contact.Name}? Yes or No");
            string confirmation = Console.ReadLine();
            if (confirmation == "yes" || confirmation == "Yes")
            {
                _context.PhoneNumbers.RemoveRange(contact.PhoneNumbers);// remove all phone numbers first
                _context.Contacts.Remove(contact);// then remove the contact
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
