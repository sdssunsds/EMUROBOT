using System;
using System.Collections.Generic;
using System.Data;

namespace EMU.Interface
{
    public delegate object Custom(params object[] pars);
    public interface IDataBase
    {
        event Custom CustomEvent;
        T GetT<T>(Func<T, bool> func, params object[] pars);
        List<T> GetTs<T>(Func<T, bool> func = null, params object[] pars);
        DataTable GetTable(string rowFilter = "", params object[] pars);
        bool AddT<T>(T t, params object[] pars);
        bool DelT<T>(T t, params object[] pars);
        bool UpdT<T>(T t, params object[] pars);
        bool SaveTs<T>(List<T> ts, params object[] pars);
        bool SaveTable(DataTable table, params object[] pars);
    }
}
