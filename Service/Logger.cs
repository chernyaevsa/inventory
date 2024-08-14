namespace Service
{
   public class Logger
    {
        public const int ACTION_MESSAGE = 0;
        public const int ERROR_MESSAGE = 1;

        private static Dictionary<int, string> types = new Dictionary<int, string>{
            {ACTION_MESSAGE, "Action"},
            {ERROR_MESSAGE, "Error"}
        };
        public static void Print(int typeId, string methodName, string msg){
            var now = DateTime.UtcNow.ToString();
            string type = types[typeId];
            Console.WriteLine($"{now} | {type} | {methodName} | {msg}");
        }
    } 
}

