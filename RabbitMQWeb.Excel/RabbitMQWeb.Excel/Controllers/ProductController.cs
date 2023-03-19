using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RabbitMQWeb.Excel.Models;
using RabbitMQWeb.Excel.Services;
using System.Runtime.ConstrainedExecution;

namespace RabbitMQWeb.Excel.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public ProductController(UserManager<IdentityUser> userManager, AppDbContext appDbContext, RabbitMQPublisher rabbitMQPublisher)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CreateProductExcel()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var fileName = $"product-excel-{Guid.NewGuid().ToString().Substring(1, 5)}";

            UserFile userFile = new UserFile()
            { UserId = user.Id, FileName = fileName, FileStatus = FileStatus.Creating };

            await _appDbContext.UserFiles.AddAsync(userFile);

            await _appDbContext.SaveChangesAsync();

            _rabbitMQPublisher.Publish(new Shared.CreateExcelMessage()
            {
                    FileId = userFile.Id,
                   // UserId = userFile.UserId
            });


            TempData["StartCreatinExcel"] = true;

            return RedirectToAction(nameof(Files));
        }



        public async Task<IActionResult> Files()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userFiles = await _appDbContext.UserFiles.Where(x => x.UserId == user.Id).ToListAsync(); 

            return View(userFiles);
        }






    }
}
