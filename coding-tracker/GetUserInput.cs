// See https://aka.ms/new-console-template for more information
using System;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.IO;



namespace coding_tracker
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        internal void MainMenu()
        {
            bool closeApp = false;

            while (closeApp ==false)
            {
                Console.WriteLine("\n\n                  MAIN MENU");
                Console.WriteLine("\n           What would you like to do");

                Console.WriteLine("\n         Type 0 to Close Application.");
                Console.WriteLine("\n         Type 1 to View Record.");
                Console.WriteLine("\n         Type 2 to Add Record.");
                Console.WriteLine("\n         Type 3 to Delete Record.");
                Console.WriteLine("\n         Type 4 to Update Record.");
                Console.WriteLine("----------------------------------------------------------\n");
                string commandInput = Console.ReadLine();
                while (string.IsNullOrEmpty(commandInput))
                    {
                        Console.WriteLine("\n         Invalid Command. Please type a number from 0 to 4.");
                        commandInput = Console.ReadLine();
                    }    

    
        switch (commandInput)
        {
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        codingController.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        ProcessDelete();
                        break;
                    case "4":
                        ProcessUpdate();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
             }
        }
      
        private void ProcessAdd()
        {
            var date = GetDateInput();
            var duration = GetDurationInput();

            Coding coding = new();

            coding.Date = date;
            coding.Duration = duration;

            codingController.Post(coding);
        }

        private void ProcessDelete()
        {
            codingController.Get();
            Console.WriteLine("Please add id of the category you want to delete (or 0 to return to Main Menu).");
            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                 Console.WriteLine("\n You have to type a valid Id (or 0 to return to Main Menu). \n");
                 commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                Console.WriteLine("\n Record with id {id} doesn't exist \n");
                //Console.WriteLine("\n Please add id of the category you want to delete (or 0 to return to Main Menu). \n");   
               // commandInput = Console.ReadLine();

               // id = Int32.Parse(commandInput);
               // if (id == 0) MainMenu();

                //coding = codingController.GetById(id);
                ProcessDelete();
            }

            codingController.Delete(id);
            MainMenu();
        }

        private void ProcessUpdate()
        {
            codingController.Get();

             Console.WriteLine("\n");
             string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                 Console.WriteLine("\n You have to type a valid Id (or 0 to return to Main Menu). \n");
                 commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                 Console.WriteLine("\n Record with id {id} doesn't exist \n");
                 ProcessUpdate();

            }

            bool updating = true;
            while (updating == true)
            {
                Console.WriteLine("\n Type 'd' for Date \n");
                Console.WriteLine("\n Type 't' for Date \n");
                Console.WriteLine("\n Type 's' for Date \n");
                Console.WriteLine("\n Type '0' to Go Back to Main Menu \n");
                
               var updateInput = Console.ReadLine();

                switch (updateInput)
                {
                    case "d":
                        coding.Date = GetDateInput();
                        break;
                    case "t":
                        coding.Duration = GetDurationInput();
                        break;
                    case "0":
                        MainMenu();
                        updating = false;
                        break;
                    case "s":
                        updating = false;
                        break;
                    default:
                        Console.WriteLine("\n Type '0' to go Back to Main menu.\n");
                        break;
                }
            }
            codingController.Update(coding);
            MainMenu();
        }


        internal string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy).Type 0 to return to main menu.\n\n");
            string dateInput = Console.ReadLine();

            if (dateInput == "0") MainMenu();

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
              Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: dd-mm-yy.\n\n");
              dateInput = Console.ReadLine();
            }
            return dateInput;
        }

        internal string GetDurationInput()
        {
            Console.WriteLine("\n\nPlease insert the duration: (Format: hh:mm).Type 0 to return to main menu.\n\n");
            string durationInput = Console.ReadLine();

           if (durationInput == "0") MainMenu();
           while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
               Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: dd-mm-yy.\n\n");
               durationInput = Console.ReadLine();
               if (durationInput == "0") MainMenu();
            }
            var parsedDuration = TimeSpan.Parse(durationInput);
            long date = parsedDuration.Ticks;
            if(date < 0)
            {
               Console.WriteLine("\n\nNegative Time is not allowed .\n\n");
               GetDurationInput();
            }
            return durationInput;
        }
    }
}