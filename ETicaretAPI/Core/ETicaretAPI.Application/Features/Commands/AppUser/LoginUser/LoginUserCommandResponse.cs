using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {

    }
    public class LoginUserSuccesCommandResponse:LoginUserCommandResponse 
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse: LoginUserCommandResponse 
    {
        public string Message { get; set; }
    }

}
