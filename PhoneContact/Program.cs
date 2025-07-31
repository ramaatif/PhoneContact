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
                Console.WriteLine("4. Add Another PhoneNumber");
                Console.WriteLine("5. Delete Contact");
                Console.WriteLine("6. Search");
                Console.WriteLine("7. Exit");
                Console.Write("Enter Your Choise :");
                string UserChoise = Console.ReadLine();
                int choise;
                if (!int.TryParse(UserChoise, out choise))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
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
                                contact.AddPhoneNumber();
                                break;
                            case 5:
                                contact.DeleteContact();
                                break;
                            case 6:
                                contact.Search();
                                break;

                            case 7:
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

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();

            }


        }
    }
}
