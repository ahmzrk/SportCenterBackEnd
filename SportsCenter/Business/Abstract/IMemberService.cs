using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMemberService
    {
        IDataResult<List<Member>> GetAllMembers();
        IDataResult<List<Member>> GetMembersSigningBeforeDate(DateTime date);
        IResult Add(Member member);
        IResult AddTransactionalTest(Member member);
    }
}
