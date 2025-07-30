namespace Capstone.Menus
{
    internal class ExitMenuItem : MenuItem
    {
        private ConsoleMenu _menu; 

        public ExitMenuItem(ConsoleMenu parentMenu)
        {
            _menu = parentMenu;
        }

        public override string MenuText()
        {
            return "Exit";
        }

        public override void Select()
        {
            _menu.IsActive = false;
        }
    }
}
