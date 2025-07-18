using Assignment_Ducat.Context;
using Assignment_Ducat.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Ducat.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            // Use stored procedure to insert data
            await _context.Database.ExecuteSqlRawAsync("EXEC InsertUser @FullName, @MobileNo, @Email, @DateOfBirth, @Age, @State, @District, @PhotoPath, @CreatedBy",
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@MobileNo", user.MobileNo),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@DateOfBirth", user.DateOfBirth),
                new SqlParameter("@Age", user.Age),
                new SqlParameter("@State", user.State),
                new SqlParameter("@District", user.District),
                new SqlParameter("@PhotoPath", user.PhotoPath),
                new SqlParameter("@CreatedBy", user.CreatedBy));
        }

        public async Task UpdateUserAsync(User user)
        {
            // Use stored procedure to update data
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateUser @UserId, @FullName, @MobileNo, @Email, @DateOfBirth, @Age, @State, @District, @PhotoPath, @CreatedBy",
                new SqlParameter("@UserId", user.UserID),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@MobileNo", user.MobileNo),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@DateOfBirth", user.DateOfBirth),
                new SqlParameter("@Age", user.Age),
                new SqlParameter("@State", user.State),
                new SqlParameter("@District", user.District),
                new SqlParameter("@PhotoPath", user.PhotoPath),
                new SqlParameter("@CreatedBy", user.CreatedBy));
        }

        public async Task DeleteUserAsync(int userId)
        {
            // Use stored procedure to delete user and store in TriggerTable
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteUser @UserId",
                new SqlParameter("@UserId", userId));
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

    }
}
