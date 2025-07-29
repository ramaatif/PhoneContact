namespace PhoneContact
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Phone Contact Manager ---");

            bool KeepRunning = true;
            while (true)
            {
                Console.WriteLine("\n Main Menu: ");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View Contacts");
                Console.WriteLine("3. Update Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Exit");
                Console.Write("Enter Your Choise :");
                string UserChoise = Console.ReadLine();
                int choise;
                if (!int.TryParse(UserChoise, out choise))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }
                try
                {
                    using (var context = new ContactDBContext())
                    {
                        var contact = new ContactService(context);

                        switch (choise)
                        {
                            case 1:
                                contact.AddContact();
                                break;
                            case 2:
                                contact.ViewAllContacts();
                                break;

                            case 3:
                                contact.UpdateContact();
                                break;

                            case 4:
                                contact.DeleteContact();
                                break;

                            case 5:
                                KeepRunning = false;
                                Console.WriteLine("Exiting the application...Goodbyee");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                                break;

                        }



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ An error occurred: {ex.InnerException}");
                }


                Console.ReadKey();

            }


        }
    }
}
