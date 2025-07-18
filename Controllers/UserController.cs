using Assignment_Ducat.Models;
using Assignment_Ducat.Repository;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Assignment_Ducat.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment env;

        public UserController(IUserRepository userRepository , IWebHostEnvironment env)
        {
            _userRepository = userRepository;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetUsersAsync();
            return View(users);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
 //           if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    MobileNo = model.MobileNo,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Age = model.Age,
                    State = model.State,
                    District = model.District,
                    PhotoPath = await SavePhoto(model.PhotoUpload),
                    CreatedBy = model.CreatedBy,
                    CreatedOn = DateTime.UtcNow
                };

                await _userRepository.AddUserAsync(user);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                MobileNo = user.MobileNo,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Age = user.Age,
                State = user.State,
                District = user.District,
                PhotoPath = user.PhotoPath,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = user.CreatedBy
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            //if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByIdAsync(model.UserID);
                if (user == null)
                {
                    return NotFound();
                }

                user.FullName = model.FullName;
                user.MobileNo = model.MobileNo;
                user.Email = model.Email;
                user.DateOfBirth = model.DateOfBirth;
                user.Age = model.Age;
                user.State = model.State;
                user.District = model.District;

                // If a new photo is uploaded, save it and update the path
                if (model.PhotoUpload != null)
                {
                    user.PhotoPath = await SavePhoto(model.PhotoUpload);
                }

                user.CreatedBy = model.CreatedBy;
                user.CreatedOn = DateTime.UtcNow;

                await _userRepository.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                MobileNo = user.MobileNo,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Age = user.Age,
                State = user.State,
                District = user.District,
                PhotoPath = user.PhotoPath,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = user.CreatedBy
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                MobileNo = user.MobileNo,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Age = user.Age,
                State = user.State,
                District = user.District,
                PhotoPath = user.PhotoPath, 
                CreatedOn = DateTime.UtcNow,
                CreatedBy = user.CreatedBy
            };
            return View(model);
        }

        private async Task<string> SavePhoto(IFormFile photo)
        {
           string fileName = "";
            string folder = Path.Combine(env.WebRootPath,"Images");
            fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(folder, fileName);
            await photo.CopyToAsync(new FileStream(filePath, FileMode.Create));



            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await photo.CopyToAsync(stream);
            //}
            return fileName;
        }

        public IActionResult ExportToExcel()
        {
            var users = _userRepository.GetUsersAsync().Result;
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Users");

            worksheet.Cells[1, 1].Value = "FullName";
            worksheet.Cells[1, 2].Value = "MobileNo";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "DateOfBirth";
            worksheet.Cells[1, 5].Value = "Age";
            worksheet.Cells[1, 6].Value = "State";
            worksheet.Cells[1, 7].Value = "District";
            worksheet.Cells[1, 8].Value = "CreatedOn";

            for (int i = 0; i < users.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = users[i].FullName;
                worksheet.Cells[i + 2, 2].Value = users[i].MobileNo;
                worksheet.Cells[i + 2, 3].Value = users[i].Email;
                worksheet.Cells[i + 2, 4].Value = users[i].DateOfBirth.ToString("dd/MM/yyyy");
                worksheet.Cells[i + 2, 5].Value = users[i].Age;
                worksheet.Cells[i + 2, 6].Value = users[i].State;
                worksheet.Cells[i + 2, 7].Value = users[i].District;
                worksheet.Cells[i + 2, 8].Value = users[i].CreatedOn.ToString("dd/MM/yyyy");
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
        }

    }

}
