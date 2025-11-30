using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            
            return new SuccessDataResult<User>(user, "kullanıcı kaydoldu");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            Console.WriteLine($"\n=== LOGIN START ===");
            Console.WriteLine($"Email: '{userForLoginDto.Email}'");
            Console.WriteLine($"Password: '{userForLoginDto.Password}'");

            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                Console.WriteLine("User not found!");
                return new ErrorDataResult<User>("kullanıcı bulunamadı");
            }

            Console.WriteLine("User found, checking password...");

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                Console.WriteLine("Password verification FAILED!");
                return new ErrorDataResult<User>("parola hatalı");
            }

            Console.WriteLine("Password verification SUCCESS!");
            return new SuccessDataResult<User>(userToCheck, "başarılı giriş");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("kullanıcı zaten var");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "token üretildi.");
        }
    }
}
