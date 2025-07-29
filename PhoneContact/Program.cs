using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PhoneContact.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneContact
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Phone Contact Manager ---");
# region//Initialize Database
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PhoneContact;Integrated Security=True");
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
                if (int.TryParse(UserChoise, out choise))
                {
                    try
                    {
                        using (var context = new ContactContext(optionsBuilder.Options))
                        {
                            switch (choise)
                            {
                                case 1:
                                    AddContact(context);
                                    break;
                                case 2:
                                    ViewAllContacts(context);
                                    break;

                                case 3:
                                    UpdateContact(context);
                                    break;

                                case 4:
                                    DeleteContact(context);
                                    break;

                                case 5:
                                    KeepRunning = false;
                                    Console.WriteLine("Exiting the application...");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                                    break;

                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n❌ An error occurred: {ex.Message}");
                    }
                    
                 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.ReadKey();

            }

            #endregion

 #region//Add ContactMethod
            static void AddContact(ContactContext context)
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                var contact = new Contact(name, phoneNumber, email);
                context.Contacts.Add(contact);
                context.SaveChanges();
                Console.WriteLine("Contact added successfully.");
            }

            #endregion

#region//View All Contacts Method
            static void ViewAllContacts(ContactContext context)
            {
                var contacts = context.Contacts.ToList();
                if (contacts.Count == 0)
                {
                    Console.WriteLine("No contacts found.");
                }
                else
                {
                    Console.WriteLine("\nContacts:");
                    foreach (var contact in contacts)
                    {
                        Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Created On: {contact.CreationDate}");
                    }
                }
            }
            #endregion

            #region//Update Contact Method
            static void UpdateContact(ContactContext context)
            {
                
            }
            #endregion

        }
    }
}
