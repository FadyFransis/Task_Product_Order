using App.Common.Repositories;

namespace App.Common.Services.Hashing
{
    public class HashingService : IHashingService
    {
        public string CreateHash(string password)
        {
            return PasswordHashRepository.CreateHash(password);
        }

        public bool ValidatePassword(string plainText, string correctHash)
        {
            return PasswordHashRepository.ValidatePassword(plainText, correctHash);
        }
    }

}
