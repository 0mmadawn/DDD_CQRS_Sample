namespace LibraryApplication.Users.Commands.Requests
{
    public  class RegisterUserCommand
    {
        public string FirstName { get; }

        public string FamilyName { get; }

        public RegisterUserCommand(string firstName, string familyName)
        {
            FirstName = firstName;
            FamilyName = familyName;
        }
    }
}
