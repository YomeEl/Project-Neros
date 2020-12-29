namespace Project_Neros.Engine
{
    enum CommandType
    {
        Quit, StartNew
    }

    struct Command
    {
        public CommandType type;
        public object[] args;
    }
}