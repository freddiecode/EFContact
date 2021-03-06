using EFContact.Models;

class Program
{
    static void Main(string[] args)
    {
        //Resources:
        // https://stackoverflow.com/questions/17723276/delete-a-single-record-from-entity-framework
        // http://www.java2s.com/Code/CSharp/Language-Basics/Switchbasedconsolemenu.html
        // https://www.webtrainingroom.com/entityframework/ef-core-console-application
        // https://stackoverflow.com/questions/3717028/access-list-from-another-class

        //using ContactsContext contactsContext = new ContactsContext();

        //////Get all contacts
        //var contacts = contactsContext.Contacts
        //    .OrderBy(c => c.ContactId);

        //foreach (Contact contact in contacts)
        //{
        //    Console.WriteLine($"Id: {contact.ContactId}");
        //    Console.WriteLine($"Name: {contact.ContactName}");
        //    Console.WriteLine($"Email: {contact.ContactEmail}");
        //    Console.WriteLine($"Created: {contact.ContactCreatedDate}");
        //    Console.WriteLine($"Age: {contact.ContactAge}");
        //    Console.WriteLine($"Contact: { contact.ContactPhoneType} - {contact.ContactPhoneNumber}");
        //    Console.WriteLine($"Notes: {contact.ContactNotes}");
        //    Console.WriteLine(new String('-', 20));

        //}

        ////Add a contact
        //Contact contact = new Contact()
        //{
        //    ContactName = "Bodil Hansen",
        //    ContactEmail = "bodil.hansen@domain.com",
        //    ContactAge = 38,
        //    ContactCreatedDate = DateTime.Now,
        //    ContactPhoneType = "Private",
        //    ContactNotes = "A thief!"
        //};
        //contactsContext.Add(contact);
        //contactsContext.SaveChanges();

        // Remove a contact
        //var bodil = contactsContext.Contacts
        //    .Where(c => c.ContactName == "Bodil Hansen")
        //    .FirstOrDefault();

        //if (bodil is Contact)
        //{
        //    contactsContext.Remove(bodil);
        //}
        //contactsContext.SaveChanges();


        //Contact _contact = new Contact()
        //{
        //    ContactName = "Freddie Christiansen",
        //    ContactAge = 33,
        //    ContactCreatedDate = DateTime.Now,
        //    ContactEmail = "freddie.christiansen@gmail.com",
        //    ContactNotes = "Me!",
        //    ContactPhoneNumber = "90975443",
        //    ContactPhoneType = "Mobile"
        //};
        //AddContact(_contact);

        //RemoveContact(6);

        bool showMenu = true;
        while (showMenu)
        {
            showMenu = MainMenu();
        }


    }


    // List all Contacts
    public static void ListAll()
    {
        Contact contact = new Contact();
        List<Contact> Contacts = ListAllContacts();
        foreach (var c in Contacts)
        {
            Console.WriteLine($"Id: {c.ContactId}");
            Console.WriteLine($"Name: {c.ContactName}");
            Console.WriteLine($"Email: {c.ContactEmail}");
            Console.WriteLine($"Created: {c.ContactCreatedDate}");
            Console.WriteLine($"Age: {c.ContactAge}");
            Console.WriteLine($"Contact: { c.ContactPhoneType} - {c.ContactPhoneNumber}");
            Console.WriteLine($"Notes: {c.ContactNotes}");
            Console.WriteLine(new String('-', 20));
        }

    }

    // Add new Contact
    public static Contact AddContact(Contact contact)
    {

        using (ContactsContext context = new ContactsContext())
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
        return contact;
    }


    // List all Contacts
    public static List<Contact> ListAllContacts()
    {
        List<Contact> contacts = new List<Contact>();
        using ContactsContext contactsContext = new ContactsContext();
        {
            contacts = contactsContext.Contacts.ToList();

        }
        return contacts;
    }


    // Remove a Contact
    public static void RemoveContact(int id)
    {
        using (ContactsContext context = new ContactsContext())
        {
            var itemToRemove = context.Contacts.SingleOrDefault(i => i.ContactId == id);

            if (itemToRemove != null)
            {
                context.Contacts.Remove(itemToRemove);
                context.SaveChanges();
            }
        }
    }


    // Another RemoveContact method..
    //public static Contact RemoveContact(Contact contact)
    //{
    //    using (ContactsContext context = new ContactsContext())
    //    {
    //        context.Contacts.Remove(contact);
    //        context.SaveChanges();
    //    }
    //    return contact;
    //}


    private static bool MainMenu()
    {

        string text = @"
  _____ _____    ____                      _      
 | ____|  ___|  / ___|___  _ __  ___  ___ | | ___ 
 |  _| | |_    | |   / _ \| '_ \/ __|/ _ \| |/ _ \
 | |___|  _|   | |__| (_) | | | \__ | (_) | |  __/
 |_____|_|      \____\___/|_| |_|___/\___/|_|\___|
                                                  
";
        Console.WriteLine(text);
        Console.WriteLine("");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1) List all contacts");
        Console.WriteLine("2) Add a new contact");
        Console.WriteLine("3) Delete a contact");
        Console.WriteLine("4) Exit");
        Console.Write("\r\nSelect an option:");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                ListAll();
                Console.Write("\r\nPress Enter to return to Main Menu");
                return true;

            case "2":
                Console.Clear();
                Console.WriteLine("Enter Name of contact:");
                var name = Console.ReadLine();

                Console.Clear();
                int age;
                while (true)
                {
                    Console.WriteLine("Enter Age of contact:");
                    var _age = Console.ReadLine();
                    bool convert = Int32.TryParse(_age, out age);

                    if (convert)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Input format is invalid. Please try again.");
                    }

                }

                Console.Clear();
                Console.WriteLine("Enter Email of contact:");
                var email = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Enter Notes of contact:");
                var notes = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Enter PhoneNumber of contact:");
                var phonenumber = Console.ReadLine();

                Contact _contact = new Contact()
                {
                    ContactName = name,
                    ContactAge = age,
                    ContactCreatedDate = DateTime.Now,
                    ContactEmail = email,
                    ContactNotes = notes,
                    ContactPhoneNumber = phonenumber,
                    ContactPhoneType = "Private"
                };
                AddContact(_contact);
                return true;

            case "3":
                ListAll();
                int Id;
                while (true)
                {
                    Console.WriteLine("\r\n\nEnter Id of contact to delete:");
                    var _id = Console.ReadLine();
                    bool convert = Int32.TryParse(_id, out Id);

                    if (convert)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        ListAll();
                        Console.WriteLine("Input format is invalid. Please try again.");
                    }

                }
                RemoveContact(Id);
                return true;

            case "4":
                return false;
            default:
                return true;
        }
    }




}