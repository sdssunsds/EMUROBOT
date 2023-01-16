namespace EMU.RolePermissions
{
    public class Global
    {
        internal static bool IsInit { get { return DataBase != null; } }
        internal static IRoleData DataBase;
        public static User LoginUser = null;
        public static void SetupDataBase(IRoleData dataBase)
        {
            DataBase = dataBase;
        }
    }
}
