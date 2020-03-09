namespace PavlovLabs.Storage
{
    [System.Serializable]
    public class IncorrectCarRepairExeption : System.Exception
    {
        public IncorrectCarRepairExeption()
        {
            
        }
        public IncorrectCarRepairExeption(string message) : base(message){}
        public IncorrectCarRepairExeption(string message, System.Exception inner) : base(message,inner){}
        protected IncorrectCarRepairExeption(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info,context) {}
    }
}