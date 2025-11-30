using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(m => m.FullName).NotEmpty().WithMessage("First name cannot be empty.");
            RuleFor(m => m.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(m => m.Phone).NotEmpty().WithMessage("Phone number cannot be empty.");
        }
    }
    
    }

