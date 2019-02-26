using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBankService.Infra;
using MyBankService.Models;

namespace MyBankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        public AccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserLoginInfo loginInfo)
        {
            try
            {
                var ownerInfo = await _bankAccountService.Login(loginInfo);
                if (ownerInfo != null)
                    return new ObjectResult(ownerInfo);
                else
                    return NotFound();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var amount = double.Parse(value);
                var updatedAmount = await _bankAccountService.UpdateAmount(id, amount);
                return new ObjectResult(updatedAmount);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
