using AutoMapper;
using IT_Talent_Program.Dtos;
using IT_Talent_Program.Entities;
using IT_Talent_Program.Extensions;
using IT_Talent_Program.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Talent_Program.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGen _tokenGen;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, ITokenGen tokenGen,IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenGen = tokenGen;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            var user = await _userRepository.GetUserByLogin(loginRequest.Login);
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }
            if (user.Password != loginRequest.Password)
            {
                return Unauthorized("Invalid password");
            }
            if (user.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            return new LoginResponseDto
            {
                Login = loginRequest.Login,
                Token = _tokenGen.GenerateToken(user)
            };
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm]RegisterDto registerDto)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if(currentUser.Admin)
            {   
                var existingUser = await _userRepository.GetUserByLogin(registerDto.Login);
                if (existingUser != null)
                {
                    return BadRequest("A user with this login already exists.");
                }
                var newUser = _mapper.Map<User>(registerDto);
                newUser.CreatedBy = currentUser.Login;
                await _userRepository.Create(newUser);
                return Ok("A new user has been successfully created.");
            }
            else
            {
                return BadRequest("Access is denied. Only an administrator can create a user.");
            }
         
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromForm] UserUpdateDto userUpdateDto)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (!currentUser.Admin&&currentUser.Login!=userUpdateDto.Login)
            {
                return BadRequest("Access is denied.");
            }
            else
            {
                var existingUser = await _userRepository.GetUserByLogin(userUpdateDto.Login);
                if(existingUser != null)
                {
                    var userForUpdate = _mapper.Map<User>(userUpdateDto);
                    userForUpdate.Id = existingUser.Id;
                    userForUpdate.Password = existingUser.Password;
                    userForUpdate.ModifiedOn = DateTime.UtcNow;
                    userForUpdate.ModifiedBy = currentUser.Login;
                    userForUpdate.CreatedBy = existingUser.CreatedBy;
                    userForUpdate.CreatedOn = existingUser.CreatedOn;
                    userForUpdate.Admin = existingUser.Admin;
                    await _userRepository.Update(userForUpdate);
                    return Ok("successfully");
                }
                else
                {
                    return NotFound("user with this login not found");
                }
            }
        }


        [Authorize]
        [HttpPut("update-password")]
        public async Task<ActionResult> UpdatePasssword([FromForm] LoginRequestDto dto)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (!currentUser.Admin&&currentUser.Login != dto.Login)
            {
                return BadRequest("Access is denied.");
            }
            else
            {
                var existingUser = await _userRepository.GetUserByLogin(dto.Login);
                if (existingUser != null)
                {
                    var userForUpdate = new User
                    {
                    Id = existingUser.Id,
                    Password = dto.Password,
                    Name = existingUser.Name,
                    Login = existingUser.Login,
                    Admin = existingUser.Admin,
                    Gender = existingUser.Gender,
                    ModifiedOn = DateTime.UtcNow,
                    ModifiedBy = currentUser.Login,
                    CreatedBy = existingUser.CreatedBy,
                    CreatedOn = existingUser.CreatedOn,
                    };

                    await _userRepository.Update(userForUpdate);
                    return Ok("Password successfully updated");
                }
                else
                {
                    return NotFound("user with this login not found");
                }
            }
        }


        [Authorize]
        [HttpPut("update-login")]
        public async Task<ActionResult> UpdateLogin([FromForm] LoginUpdateDto dto)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (!currentUser.Admin && currentUser.Login != dto.Login)
            {
                return BadRequest("Access is denied.");
            }
            else
            {
                var existingUser = await _userRepository.GetUserByLogin(dto.Login);

                if (existingUser != null)
                {
                    var checkLogin = await _userRepository.GetUserByLogin(dto.NewLogin);
                    if (checkLogin != null)
                    {
                        BadRequest("A user with this login already exists.");
                    }
                    else
                    {
                        var userForUpdate = new User
                        {
                            Id = existingUser.Id,
                            Password = existingUser.Password,
                            Name = existingUser.Name,
                            Login = dto.NewLogin,
                            Admin = existingUser.Admin,
                            Gender = existingUser.Gender,
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = currentUser.Login,
                            CreatedBy = existingUser.CreatedBy,
                            CreatedOn = existingUser.CreatedOn,

                        };
                        await _userRepository.Update(userForUpdate);
                        return Ok("Login successfully updated");
                    }
                    

                }
                return NotFound("User with this login not found");
            }
        }




        [Authorize]
        [HttpGet("get-active-users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (currentUser.Admin)
            {
                var activeUsers = await _userRepository.GetActiveUsers();
                return activeUsers;
            }
            else
            {
                return BadRequest("Access is denied.");
            }
        }

        [Authorize]
        [HttpGet("get-by-login")]
        public async Task<ActionResult<UserDto>> GetUserByLogin(string login)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (currentUser.Admin)
            {
                var user = await _userRepository.GetUserByLogin(login);
                if (user == null)
                {
                    return NotFound("User is not found.");
                }
                var userToReturn = _mapper.Map<UserDto>(user);
                return userToReturn;
            }
            else
            {
                return BadRequest("Access is denied.");
            }
        }

        [Authorize]
        [HttpGet("getall-by-age")]
        public async Task<ActionResult<List<User>>> GetAllByAge(int age=18)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (currentUser.Admin)
            {
                var users = await _userRepository.GetUsersByAge(age);
                return users;
            }
            else
            {
                return BadRequest("Access is denied.");
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteByLogin(string login)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            var existingUser = await _userRepository.GetUserByLogin(login);
            if (existingUser == null) return NotFound("User with this login not found");
            if (currentUser.Admin)
            {
                await _userRepository.Delete(existingUser);
                return Ok("User deleted");
            }
            if(currentUser.Login == login)
            {
                existingUser.Id = existingUser.Id;
                existingUser.Login = existingUser.Login;
                existingUser.Name = existingUser.Name;
                existingUser.Password = existingUser.Password;
                existingUser.Admin = existingUser.Admin;
                existingUser.Gender = existingUser.Gender;
                existingUser.Birthday = existingUser.Birthday;
                existingUser.CreatedBy = existingUser.CreatedBy;
                existingUser.CreatedOn = existingUser.CreatedOn;
                existingUser.ModifiedBy = existingUser.ModifiedBy;
                existingUser.ModifiedOn = existingUser.ModifiedOn;
                existingUser.RevokedOn = DateTime.UtcNow;
                existingUser.RevokedBy = existingUser.Login;
                await _userRepository.Update(existingUser);
                return Ok("Your account deleted, to restore it contact the admin");
            }
            return BadRequest("Problem with deletion");
        }



        [Authorize]
        [HttpPut("restore")]
        public async Task<ActionResult> RestoreAccount(string login)
        {
            var currentUser = await _userRepository.GetUserByLogin(User.GetLogin());
            if (currentUser.RevokedBy != null) return BadRequest("Your account deleted, to restore it contact the admin");
            if (!currentUser.Admin)
            {
                return BadRequest("Access is denied.");
            }
            else
            {
                var existingUser = await _userRepository.GetUserByLogin(login);

                if (existingUser != null)
                {
                        existingUser.Id = existingUser.Id;
                        existingUser.Login = existingUser.Login;
                        existingUser.Name = existingUser.Name;
                        existingUser.Password = existingUser.Password;
                        existingUser.Admin = existingUser.Admin;
                        existingUser.Gender = existingUser.Gender;
                        existingUser.Birthday = existingUser.Birthday;
                        existingUser.CreatedBy = existingUser.Login;
                        existingUser.CreatedOn = existingUser.CreatedOn;
                        existingUser.ModifiedBy = currentUser.Login;
                        existingUser.ModifiedOn = DateTime.UtcNow;
                        existingUser.RevokedBy = null;
                        existingUser.RevokedOn = null;
                        await _userRepository.Update(existingUser);
                        return Ok("Login successfully restored");
                }
                return NotFound("User with this login not found");
            }
        }

    }
}
