using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountId)
        {
            AccountMember accountMember = new AccountMember();
            if (!string.IsNullOrEmpty(accountId) && accountId.ToLower().Equals("ps001"))
            {
                accountMember.MemberId = accountId;
                accountMember.MemberPassword = "@1";
                accountMember.MemberRole = 1;
            }
            else
            {
                accountMember = null;
            }
            return accountMember;
        }
    }
}
