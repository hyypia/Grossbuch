using System;
using System.Collections.Generic;
using System.Text;

namespace GrossbuchLibrary
{
    public interface IAccount
    {
        void Put(decimal sum);
        decimal Withdraw(decimal sum);
    }
}
