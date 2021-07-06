
namespace App.Common.Services.Hashing
{
    public interface IHashingService
    {
        /// <summary>
        /// To Hash any string
        /// </summary>
        /// <param name="plainText">the password/string need to be hashed</param>
        /// <returns>Hashed string</returns>
        string CreateHash(string plainText);

        /// <summary>
        /// Validate plain text against to the hashed one
        /// </summary>
        /// <param name="plainText">The plain string</param>
        /// <param name="correctHash">The hashed string</param>
        /// <returns>boolean value</returns>
        bool ValidatePassword(string plainText, string correctHash);
    }
}
