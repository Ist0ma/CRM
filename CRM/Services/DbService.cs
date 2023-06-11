using CRM.DAL.Interfaces;
using CRM.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Services
{
    public class DbService
    {
        IUnitOfWork _unitOfWork;

        public DbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region User
        public User Login(string login, string password)
        {
            var users = _unitOfWork.Users.Find(user => user.Login == login && user.Password == password);
            return users.FirstOrDefault();
        }

        public async Task Register(string login, string password)
        {
            User user = new User();
            user.Login = login;
            user.Password = HashPassword(password);
            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();
        }
        public async Task<User> GetUser(Expression<Func<User, Boolean>> predicate = null)
        {
            User user = await _unitOfWork.Users.Find(predicate).FirstOrDefaultAsync();
            await _unitOfWork.Save();
            return user;
        }
        public async Task Register(User user)
        {
            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.Find();
        }
        public async Task DeleteUser(User user)
        {
            await _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();
        }
        public async Task UpdateUser(User user)
        {
            user.Password = HashPassword(user.Password);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        #endregion

        #region Base
        public IEnumerable<Base> GetAllBases()
        {
            return _unitOfWork.Bases.Find()
                .Include(b => b.Users)
                .Include(b => b.BaseItems)
                .ToList();
        }

        public async Task<Base> GetBase(int id)
        {
            return await _unitOfWork.Bases.Get(id);
        }

        public async Task<IEnumerable<Base>> GetAllUserBases(int id)
        {
            User user = await _unitOfWork.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            return _unitOfWork.Bases.Find(s => s.Users.Contains(user));
        }

        public async Task UpdateBase(Base @base)
        {
            _unitOfWork.Bases.Update(@base);
            await _unitOfWork.Save();
        }

        public async Task CreateBase(Base @base)
        {
            await _unitOfWork.Bases.Create(@base);
            await _unitOfWork.Save();
        }

        public async Task DeleteBase(int id)
        {
            Base baseToDell = await _unitOfWork.Bases.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (baseToDell != null)
            {
                _unitOfWork.Bases.Delete(baseToDell);
                await _unitOfWork.Save();
            }
        }
        #endregion

        #region BaseItem

        public async Task UpdateBaseItem(BaseItem item)
        {
            _unitOfWork.BaseItems.Update(item);
            await _unitOfWork.Save();
        }

        public IEnumerable<BaseItem> GetAllBaseItems(int baseId)
        {
            return _unitOfWork.BaseItems.Find(b => b.BaseId == baseId);
        }
        #endregion
    }
}
