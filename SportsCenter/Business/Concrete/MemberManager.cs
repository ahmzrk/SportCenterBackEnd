using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class MemberManager : IMemberService
    {
        IMemberDal _memberDal;
        public MemberManager(IMemberDal memberDal)
        {
            _memberDal = memberDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(MemberValidator))]
        [CacheAspect]
        public IResult Add(Member member)
        {
            BusinessRules.Run(
                CheckMemberTelephoneNumber(member.Phone),
                CheckMemberLimitExceed()
            );
            _memberDal.Add(member);
            return new SuccessResult();
        }

        public IDataResult<List<Member>> GetAllMembers()
        {
            return new SuccessDataResult<List<Member>>(_memberDal.GetAll());
        }

        public IDataResult<List<Member>> GetMembersSigningBeforeDate(DateTime date)
        {
            return new SuccessDataResult<List<Member>>(_memberDal.GetAll(m => m.StartDate < date));
        }

        private IResult CheckMemberTelephoneNumber(string phone)
        {
            var result = _memberDal.Get(m => m.Phone == phone);
            if (result != null)
            {
                return new ErrorResult("This phone number is already registered.");
            }
            return new SuccessResult("Phone number is available.");
        }
        private IResult CheckMemberLimitExceed()
        {
            if (_memberDal.GetAll().Count >= 100) 
            {
                return new ErrorResult("Member limit exceeded.");
            }
            return new SuccessResult("Member limit is within acceptable range.");
        }

        [TransactionScopeAspect]
        [PerformanceAspect(5)]
        public IResult AddTransactionalTest(Member member)
        {
           _memberDal.Add(member);
            return new SuccessResult();
        }
    }
}
