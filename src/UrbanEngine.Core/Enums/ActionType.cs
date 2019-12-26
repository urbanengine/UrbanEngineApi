using Ardalis.SmartEnum;

namespace UrbanEngine.Core.Enums
{
    public class ActionType : SmartEnum<ActionType, int>
    {
        public static ActionType None = new ActionType("None", 0);
        public static ActionType Create = new ActionType("Create", 1);
        public static ActionType Update = new ActionType("Update", 2);
        public static ActionType Delete = new ActionType("Delete", 3);
        public static ActionType Get = new ActionType("Get", 4);
        public static ActionType GetById = new ActionType("GetById", 5);

        public ActionType(string name, int value) 
            : base(name, value) { }
    }
}
