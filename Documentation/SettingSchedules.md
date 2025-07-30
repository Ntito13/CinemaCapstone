# Setting Schedules Workflow (10%)

In the workflow sections you must give clear instructions as to how to perform this workflow in your applcation. Use images and diagrams where necessary. Failure to give adequate instructions here may result in loss of marks.

This workflow should do the following things:

- Load information about cinema staff who can either be managers or workers without allowing invalid data
- Select a staff member at the start of the application
- A manager should be able to edit the schedule of screenings
- The schedule must conform to the scheduling rules
    - You cannot schedule two screenings in the same screen at the same time
    - You must leave enough time between films for "turnaround" (15 minutes for 50 seats or less, 30 minutes for 51 - 100 seats, 45 minutes for more than 100 seats)
- A manager should be able to save the schedule of screenings in a format of your choosing with the extension .fs
    - one file should contain the schedule for one day, with the date indicated in the filename 
- Any staff member should be able to load the schedule from a list of dates



In this part of my application, I created a feature for cinema staff to manage the screening schedule. It includes adding new screenings, checking for clashes, and saving them for specific dates.

1. Load Staff Information
When the app starts, it loads a list of staff members (both managers and regular workers). I validated the data to avoid errors from missing or invalid staff info.


        // Load staff data from file 
        public static List<Staff> LoadStaffFromFile(string path)
        {
            List<Staff> staffList = new();
            HashSet<string> seenIds = new(); // Track used staff IDs

   2. Select Staff Member
The user is told to log in by choosing their name from the staff list. This is used to check if they’re a manager or not.

HashSet<string> seenIds = new(); // Track used staff IDs

if (!File.Exists(path))
    throw new FileNotFoundException($"Missing staff file: {path}");


3. If Manager → Edit Screening Schedule
If the selected staff member is a manager, they can view, add, or update screenings. I made sure to check that no two films overlap in the same screen

 // This menu item allows a manager to add a new screening to the schedule
 internal class ScheduleEditor(Cinema cinema) : MenuItem
 {
     private readonly Cinema _cinema = cinema;

     public override string MenuText()
     {
         return "Edit Screening Schedule (Manager)";
     }

     public override void Select()
     {
         Console.Clear();
         Console.WriteLine("Schedule Editor");


4. Load Saved Schedules 
Any staff member, manager or not, can open saved schedules from a list of existing files. 

  public override void Select()
  {
      Console.Clear();
      Console.WriteLine("Schedule Editor");

      // Asks which screen to use
      var screens = _cinema.GetScreens();
      var screenOptions = screens.Select(s => $"Screen {s.Id}").ToList();
      int screenChoice = ConsoleHelpers.GetSelectionFromMenu(screenOptions, "Select a screen:");
      var chosenScreen = screens[screenChoice];

      // Asks which movie to show
      var movies = _cinema.GetMovies();
      var movieOptions = movies.Select(m => $"{m.Title} ({m.Rating})").ToList();
      int movieChoice = ConsoleHelpers.GetSelectionFromMenu(movieOptions, "Select a movie:");
      var chosenMovie = movies[movieChoice];

      // Asks for the date and time to show the movie
      Console.Write("Enter start date and time (dd-MM-yyyy HH:mm): ");
      if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
      {
          Console.WriteLine("Invalid date/time format.");
          Console.ReadLine();
          return;


